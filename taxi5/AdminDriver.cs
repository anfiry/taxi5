using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminDriver
    {
        // Строка подключения (исправлено – теперь это поле класса)
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        // ---------- ВОДИТЕЛИ ----------
        public DataTable GetDrivers()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            d.driver_id,
                            d.last_name,
                            d.first_name,
                            d.patronymic,
                            d.phone_number,
                            COALESCE(ds.status_name, '') AS status_name,
                            d.driver_status_id,
                            d.account_id,
                            d.work_experience,
                            d.license_series,
                            d.license_number,
                            COALESCE(a.login, '') AS login
                        FROM driver d
                        LEFT JOIN driver_status ds ON d.driver_status_id = ds.driver_status_id
                        LEFT JOIN account a ON d.account_id = a.account_id
                        ORDER BY d.driver_id";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки водителей:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public int CreateAccountForDriver()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string login = $"driver_{timestamp}";
                    string password = Guid.NewGuid().ToString().Substring(0, 8);
                    string query = @"
                        INSERT INTO account (role_id, login, password, confirmation)
                        VALUES (3, @login, @password, true)
                        RETURNING account_id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);
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
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM account WHERE account_id = @accountId";
                    using (var cmd = new NpgsqlCommand(query, conn))
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
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO driver
                            (first_name, last_name, patronymic, phone_number, driver_status_id,
                             account_id, work_experience, license_series, license_number)
                        VALUES
                            (@firstName, @lastName, @patronymic, @phone, @statusId,
                             @accountId, @workExperience, @licenseSeries, @licenseNumber)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName ?? "");
                        cmd.Parameters.AddWithValue("@lastName", lastName ?? "");
                        cmd.Parameters.AddWithValue("@patronymic", string.IsNullOrEmpty(patronymic) ? DBNull.Value : (object)patronymic);
                        cmd.Parameters.AddWithValue("@phone", phone ?? "");
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@workExperience", workExperience);
                        cmd.Parameters.AddWithValue("@licenseSeries", string.IsNullOrEmpty(licenseSeries) ? DBNull.Value : (object)licenseSeries);
                        cmd.Parameters.AddWithValue("@licenseNumber", string.IsNullOrEmpty(licenseNumber) ? DBNull.Value : (object)licenseNumber);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления водителя: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool UpdateDriver(int driverId, string firstName, string lastName, string patronymic,
                                 string phone, int statusId, int accountId, decimal workExperience,
                                 string licenseSeries, string licenseNumber)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        UPDATE driver SET
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

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        cmd.Parameters.AddWithValue("@firstName", firstName ?? "");
                        cmd.Parameters.AddWithValue("@lastName", lastName ?? "");
                        cmd.Parameters.AddWithValue("@patronymic", string.IsNullOrEmpty(patronymic) ? DBNull.Value : (object)patronymic);
                        cmd.Parameters.AddWithValue("@phone", phone ?? "");
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@workExperience", workExperience);
                        cmd.Parameters.AddWithValue("@licenseSeries", string.IsNullOrEmpty(licenseSeries) ? DBNull.Value : (object)licenseSeries);
                        cmd.Parameters.AddWithValue("@licenseNumber", string.IsNullOrEmpty(licenseNumber) ? DBNull.Value : (object)licenseNumber);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления водителя: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool DeleteDriver(int driverId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM driver WHERE driver_id = @driverId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (NpgsqlException ex) when (ex.Message.Contains("23503"))
                {
                    MessageBox.Show("Невозможно удалить водителя: имеются связанные записи.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public DataTable GetDriverStatuses()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT driver_status_id AS id, status_name FROM driver_status ORDER BY driver_status_id";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // ---------- АВТОМОБИЛИ (ДОБАВЛЕНИЕ НОВОГО ПРЯМО В ФОРМЕ) ----------
        public bool AddCarForDriver(int driverId,
                                    string brandName, string modelName, string colorName,
                                    string licenseNumber, string regionCode, int year)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        int brandId = GetOrCreateBrand(conn, brandName);
                        int modelId = GetOrCreateModel(conn, brandId, modelName);
                        int colorId = GetOrCreateColor(conn, colorName);

                        string query = @"
                            INSERT INTO car
                                (driver_id, brand_id, model_id, color_id, license_number, region_code, year_of_manufacture)
                            VALUES
                                (@driverId, @brandId, @modelId, @colorId, @licenseNumber, @regionCode, @year)";

                        using (var cmd = new NpgsqlCommand(query, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@driverId", driverId);
                            cmd.Parameters.AddWithValue("@brandId", brandId);
                            cmd.Parameters.AddWithValue("@modelId", modelId);
                            cmd.Parameters.AddWithValue("@colorId", colorId);
                            cmd.Parameters.AddWithValue("@licenseNumber", licenseNumber ?? "");
                            cmd.Parameters.AddWithValue("@regionCode", regionCode ?? "");
                            cmd.Parameters.AddWithValue("@year", year);

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show($"Ошибка добавления автомобиля: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private int GetOrCreateBrand(NpgsqlConnection conn, string brandName)
        {
            string select = "SELECT brand_id FROM brand WHERE name = @name";
            using (var cmd = new NpgsqlCommand(select, conn))
            {
                cmd.Parameters.AddWithValue("@name", brandName);
                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
            }

            string insert = "INSERT INTO brand (name) VALUES (@name) RETURNING brand_id";
            using (var cmd = new NpgsqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@name", brandName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int GetOrCreateModel(NpgsqlConnection conn, int brandId, string modelName)
        {
            string select = "SELECT model_id FROM model WHERE brand_id = @brandId AND name = @name";
            using (var cmd = new NpgsqlCommand(select, conn))
            {
                cmd.Parameters.AddWithValue("@brandId", brandId);
                cmd.Parameters.AddWithValue("@name", modelName);
                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
            }

            string insert = "INSERT INTO model (name, brand_id) VALUES (@name, @brandId) RETURNING model_id";
            using (var cmd = new NpgsqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@name", modelName);
                cmd.Parameters.AddWithValue("@brandId", brandId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int GetOrCreateColor(NpgsqlConnection conn, string colorName)
        {
            string select = "SELECT color_id FROM color WHERE name = @name";
            using (var cmd = new NpgsqlCommand(select, conn))
            {
                cmd.Parameters.AddWithValue("@name", colorName);
                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
            }

            string insert = "INSERT INTO color (name) VALUES (@name) RETURNING color_id";
            using (var cmd = new NpgsqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@name", colorName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public DataTable GetCarsForDriver(int driverId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT
                            c.car_id,
                            b.name AS brand_name,
                            m.name AS model_name,
                            col.name AS color_name,
                            c.license_number,
                            c.region_code,
                            c.year_of_manufacture
                        FROM car c
                        LEFT JOIN brand b ON c.brand_id = b.brand_id
                        LEFT JOIN model m ON c.model_id = m.model_id
                        LEFT JOIN color col ON c.color_id = col.color_id
                        WHERE c.driver_id = @driverId
                        ORDER BY c.car_id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                            adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки автомобилей: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }
    }
}