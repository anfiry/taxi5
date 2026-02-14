using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminClient
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        public DataTable GetClients()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                        c.client_id,
                        c.first_name,
                        c.last_name,
                        c.patronymic,
                        c.phone_number,
                        cs.status_name,
                        c.clent_status_id,
                        c.account_id,
                        c.address_id,
                        CONCAT(a.city, ', ', a.street, ', д.', a.house, 
                               COALESCE(CONCAT(', под.', a.entrance), '')) as address_info,
                        a.city,
                        a.street,
                        a.house,
                        a.entrance
                    FROM client c
                    JOIN clent_status cs ON c.clent_status_id = cs.clent_status_id
                    LEFT JOIN address a ON c.address_id = a.address_id
                    ORDER BY c.client_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        public int CreateAccountForClient()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string generatedLogin = $"client_{timestamp}";
                    string generatedPassword = Guid.NewGuid().ToString().Substring(0, 8);

                    string query = @"INSERT INTO account (role_id, login, password, confirmation) 
                                    VALUES (2, @login, @password, true) 
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

        public bool AddClient(string firstName, string lastName, string patronymic, string phone,
                              int statusId, int addressId, int accountId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO client 
                        (first_name, last_name, patronymic, phone_number, clent_status_id, address_id, account_id) 
                        VALUES (@firstName, @lastName, @patronymic, @phone, @statusId, @addressId, @accountId)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@patronymic", patronymic ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@addressId", addressId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool UpdateClient(int clientId, string firstName, string lastName, string patronymic,
                                 string phone, int statusId, int addressId, int accountId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE client SET 
                        first_name = @firstName, 
                        last_name = @lastName, 
                        patronymic = @patronymic,
                        phone_number = @phone,
                        clent_status_id = @statusId,
                        address_id = @addressId,
                        account_id = @accountId
                        WHERE client_id = @clientId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@patronymic", patronymic ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@addressId", addressId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool DeleteClient(int clientId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM client WHERE client_id = @clientId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);

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
                        MessageBox.Show("Невозможно удалить клиента: имеются связанные записи в других таблицах",
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

        public DataTable GetAddresses()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                        address_id as id,
                        CONCAT(city, ', ', street, ', д.', house, 
                               COALESCE(CONCAT(', под.', entrance), '')) as full_address,
                        city,
                        street,
                        house,
                        entrance
                    FROM address ORDER BY address_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки адресов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        public int AddAddress(string city, string street, string house, string entrance)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO address (city, street, house, entrance) 
                                    VALUES (@city, @street, @house, @entrance) 
                                    RETURNING address_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@street", street);
                        cmd.Parameters.AddWithValue("@house", house);
                        cmd.Parameters.AddWithValue("@entrance", string.IsNullOrEmpty(entrance) ? (object)DBNull.Value : entrance);

                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления адреса: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public DataTable GetClientStatuses()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT clent_status_id as id, status_name FROM clent_status ORDER BY clent_status_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }
    }
}