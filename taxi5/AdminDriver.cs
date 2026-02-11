using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminDriver
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        public DataTable GetDrivers()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                        d.driver_id,
                        d.last_name,
                        d.first_name,
                        d.patronymic,
                        d.phone_number,
                        COALESCE(ds.status_name, 'Неизвестно') as status_name,
                        d.driver_status_id,
                        d.account_id,
                        COALESCE(d.work_experience, 0) as work_experience,
                        COALESCE(d.license_series, '') as license_series,
                        COALESCE(d.license_number, '') as license_number,
                        COALESCE(a.login, '') as login
                    FROM driver d
                    LEFT JOIN driver_status ds ON d.driver_status_id = ds.driver_status_id
                    LEFT JOIN account a ON d.account_id = a.account_id
                    ORDER BY d.driver_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    // Проверяем, что таблица не null и есть колонки
                    if (dt != null && dt.Columns.Count > 0)
                    {
                        // Заменяем DBNull на пустые строки/0
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                if (row[col] == DBNull.Value)
                                {
                                    if (col.DataType == typeof(string))
                                        row[col] = "";
                                    else if (col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(int))
                                        row[col] = 0;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки водителей: {ex.Message}\n\n{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return dt;
        }

        public int CreateAccountForDriver()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string generatedLogin = $"driver_{timestamp}";
                    string generatedPassword = Guid.NewGuid().ToString().Substring(0, 8);

                    string query = @"INSERT INTO account (role_id, login, password, confirmation) 
                                    VALUES (3, @login, @password, true) 
                                    RETURNING account_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", generatedLogin);
                        cmd.Parameters.AddWithValue("@password", generatedPassword);

                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка создания аккаунта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public bool AccountExists(int accountId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM account WHERE account_id = @accountId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool AddDriver(string firstName, string lastName, string patronymic, string phone,
                              int statusId, int accountId, decimal workExperience,
                              string licenseSeries, string licenseNumber)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO driver 
                        (first_name, last_name, patronymic, phone_number, driver_status_id, 
                         account_id, work_experience, license_series, license_number) 
                        VALUES (@firstName, @lastName, @patronymic, @phone, @statusId, 
                                @accountId, @workExperience, @licenseSeries, @licenseNumber)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName ?? "");
                        cmd.Parameters.AddWithValue("@lastName", lastName ?? "");
                        cmd.Parameters.AddWithValue("@patronymic", string.IsNullOrEmpty(patronymic) ? (object)DBNull.Value : patronymic);
                        cmd.Parameters.AddWithValue("@phone", phone ?? "");
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@workExperience", workExperience);
                        cmd.Parameters.AddWithValue("@licenseSeries", string.IsNullOrEmpty(licenseSeries) ? (object)DBNull.Value : licenseSeries);
                        cmd.Parameters.AddWithValue("@licenseNumber", string.IsNullOrEmpty(licenseNumber) ? (object)DBNull.Value : licenseNumber);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления водителя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool UpdateDriver(int driverId, string firstName, string lastName, string patronymic,
                                 string phone, int statusId, int accountId, decimal workExperience,
                                 string licenseSeries, string licenseNumber)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE driver SET 
                        first_name = @firstName, 
                        last_name = @lastName, 
                        patronymic = @patronymic,
                        phone_number = @phone,
                        driver_status_id = @statusId,
                        account_id = @accountId,
                        work_experience = @workExperience,
                        license_series = @licenseSeries,
                        license_number = @licenseNumber
                        WHERE driver_id = @driverId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        cmd.Parameters.AddWithValue("@firstName", firstName ?? "");
                        cmd.Parameters.AddWithValue("@lastName", lastName ?? "");
                        cmd.Parameters.AddWithValue("@patronymic", string.IsNullOrEmpty(patronymic) ? (object)DBNull.Value : patronymic);
                        cmd.Parameters.AddWithValue("@phone", phone ?? "");
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@workExperience", workExperience);
                        cmd.Parameters.AddWithValue("@licenseSeries", string.IsNullOrEmpty(licenseSeries) ? (object)DBNull.Value : licenseSeries);
                        cmd.Parameters.AddWithValue("@licenseNumber", string.IsNullOrEmpty(licenseNumber) ? (object)DBNull.Value : licenseNumber);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления водителя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool DeleteDriver(int driverId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM driver WHERE driver_id = @driverId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (NpgsqlException ex)
                {
                    if (ex.Message.Contains("violates foreign key constraint") ||
                        ex.Message.Contains("23503") ||
                        ex.Message.Contains("foreign key"))
                    {
                        MessageBox.Show("Невозможно удалить водителя: имеются связанные записи в других таблицах",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public DataTable GetDriverStatuses()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT driver_status_id as id, status_name 
                                   FROM driver_status ORDER BY driver_status_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки статусов водителей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        public DataTable GetCarsForDriver(int driverId)
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                        c.car_id,
                        COALESCE(b.name, '') as brand,
                        COALESCE(m.name, '') as model,
                        COALESCE(col.name, '') as color,
                        COALESCE(c.license_number, '') as license_number,
                        COALESCE(c.region_code, '') as region_code,
                        COALESCE(c.year_of_manufacture, 0) as year_of_manufacture
                    FROM car c
                    LEFT JOIN brand b ON c.brand_id = b.brand_id
                    LEFT JOIN model m ON c.model_id = m.model_id
                    LEFT JOIN color col ON c.color_id = col.color_id
                    WHERE c.driver_id = @driverId
                    ORDER BY c.car_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки автомобилей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }
    }
}