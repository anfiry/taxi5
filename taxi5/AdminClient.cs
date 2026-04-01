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
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
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
                           CASE WHEN a.entrance IS NOT NULL AND a.entrance != '' 
                                THEN ', подъезд ' || a.entrance ELSE '' END) as address_info,
                    a.city,
                    a.street,
                    a.house,
                    a.entrance,
                    acc.login,
                    acc.password AS password
                FROM client c
                JOIN clent_status cs ON c.clent_status_id = cs.clent_status_id
                LEFT JOIN address a ON c.address_id = a.address_id
                LEFT JOIN account acc ON c.account_id = acc.account_id
                ORDER BY c.client_id";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
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

        public int CreateAccountForClient(string login, string password)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO account (role_id, login, password, confirmation) 
                            VALUES (2, @login, @password, true) 
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
                    MessageBox.Show($"Ошибка создания аккаунта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                catch { return false; }
            }
        }

        public bool AddClient(string firstName, string lastName, string patronymic, string phone,
                              int statusId, int addressId, int accountId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO client 
                        (first_name, last_name, patronymic, phone_number, clent_status_id, address_id, account_id) 
                        VALUES (@firstName, @lastName, @patronymic, @phone, @statusId, @addressId, @accountId)";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@patronymic", patronymic ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@addressId", addressId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        return cmd.ExecuteNonQuery() > 0;
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
            using (var conn = new NpgsqlConnection(connectionString))
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
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@patronymic", patronymic ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        cmd.Parameters.AddWithValue("@addressId", addressId);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // ---------- МЕТОДЫ ДЛЯ БЛОКИРОВКИ/РАЗБЛОКИРОВКИ ----------
        public int GetAccountIdByClientId(int clientId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT account_id FROM client WHERE client_id = @clientId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public DataTable GetBlockReasons()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT reason_id, reason_text FROM reason ORDER BY reason_id";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public bool IsClientBlocked(int clientId)
        {
            int accountId = GetAccountIdByClientId(clientId);
            if (accountId == -1) return false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM blacklist WHERE account_id = @accountId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool BlockClient(int clientId, int reasonId, DateTime startDate, DateTime? endDate)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int accountId = GetAccountIdByClientId(clientId);
                        if (accountId == -1) return false;

                        string insertBlacklist = @"
                            INSERT INTO blacklist (account_id, reason_id, start_date, end_date)
                            VALUES (@accountId, @reasonId, @startDate, @endDate)";
                        using (var cmd = new NpgsqlCommand(insertBlacklist, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@accountId", accountId);
                            cmd.Parameters.AddWithValue("@reasonId", reasonId);
                            cmd.Parameters.AddWithValue("@startDate", startDate);
                            cmd.Parameters.AddWithValue("@endDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }

                        string updateStatus = "UPDATE client SET clent_status_id = 2 WHERE client_id = @clientId";
                        using (var cmd = new NpgsqlCommand(updateStatus, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool UnblockClient(int clientId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int accountId = GetAccountIdByClientId(clientId);
                        if (accountId == -1) return false;

                        string deleteBlacklist = "DELETE FROM blacklist WHERE account_id = @accountId";
                        using (var cmd = new NpgsqlCommand(deleteBlacklist, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@accountId", accountId);
                            cmd.ExecuteNonQuery();
                        }

                        string updateStatus = "UPDATE client SET clent_status_id = 1 WHERE client_id = @clientId";
                        using (var cmd = new NpgsqlCommand(updateStatus, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ---------- РАБОТА С АДРЕСАМИ ----------
        public DataTable GetAddresses()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT 
            address_id as id,
            CONCAT(city, ', ', street, ', д.', house, 
                   CASE WHEN entrance IS NOT NULL AND entrance != '' 
                        THEN ', подъезд ' || entrance ELSE '' END) as full_address,
            city, street, house, entrance
            FROM address ORDER BY address_id";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public int AddAddress(string city, string street, string house, string entrance)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO address (city, street, house, entrance) 
                         VALUES (@city, @street, @house, @entrance) 
                         RETURNING address_id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@house", house);
                    cmd.Parameters.AddWithValue("@entrance", string.IsNullOrEmpty(entrance) ? (object)DBNull.Value : entrance);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public DataTable GetClientStatuses()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT clent_status_id as id, status_name FROM clent_status ORDER BY clent_status_id";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}