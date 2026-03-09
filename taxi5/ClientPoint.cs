using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class ClientPoint
    {
        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";

        public DataTable GetPointsByClient(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.point_id,
                            p.name AS point_name,
                            pt.name AS type_name,
                            a.city,
                            a.street,
                            a.house,
                            a.entrance,
                            a.address_id
                        FROM point p
                        JOIN clent_point cp ON p.point_id = cp.point_id
                        JOIN point_tupe pt ON p.type_id = pt.point_tupe
                        JOIN address a ON p.address_id = a.address_id
                        WHERE cp.clent_id = @clientId
                        ORDER BY p.point_id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки точек:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public DataTable GetPointTypes()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT point_tupe, name FROM point_tupe ORDER BY name";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки типов точек:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public bool AddPoint(int clientId, string pointName, int typeId,
                             string city, string street, string house, string entrance)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string addressQuery = @"
                            INSERT INTO address (city, street, house, entrance)
                            VALUES (@city, @street, @house, @entrance)
                            RETURNING address_id";

                        int addressId;
                        using (var addrCmd = new NpgsqlCommand(addressQuery, conn, transaction))
                        {
                            addrCmd.Parameters.AddWithValue("@city", city ?? "");
                            addrCmd.Parameters.AddWithValue("@street", street ?? "");
                            addrCmd.Parameters.AddWithValue("@house", house ?? "");
                            addrCmd.Parameters.AddWithValue("@entrance", entrance ?? "");
                            addressId = Convert.ToInt32(addrCmd.ExecuteScalar());
                        }

                        string pointQuery = @"
                            INSERT INTO point (name, type_id, address_id)
                            VALUES (@name, @typeId, @addressId)
                            RETURNING point_id";

                        int pointId;
                        using (var pointCmd = new NpgsqlCommand(pointQuery, conn, transaction))
                        {
                            pointCmd.Parameters.AddWithValue("@name", pointName ?? "");
                            pointCmd.Parameters.AddWithValue("@typeId", typeId);
                            pointCmd.Parameters.AddWithValue("@addressId", addressId);
                            pointId = Convert.ToInt32(pointCmd.ExecuteScalar());
                        }

                        string linkQuery = @"
                            INSERT INTO clent_point (clent_id, point_id, added_date)
                            VALUES (@clientId, @pointId, @addedDate)";

                        using (var linkCmd = new NpgsqlCommand(linkQuery, conn, transaction))
                        {
                            linkCmd.Parameters.AddWithValue("@clientId", clientId);
                            linkCmd.Parameters.AddWithValue("@pointId", pointId);
                            linkCmd.Parameters.AddWithValue("@addedDate", DateTime.Now);
                            linkCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка добавления точки:\n{ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        public bool UpdatePoint(int pointId, string pointName, int typeId,
                                string city, string street, string house, string entrance,
                                int addressId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string addressQuery = @"
                            UPDATE address
                            SET city = @city, street = @street, house = @house, entrance = @entrance
                            WHERE address_id = @addressId";

                        using (var addrCmd = new NpgsqlCommand(addressQuery, conn, transaction))
                        {
                            addrCmd.Parameters.AddWithValue("@city", city ?? "");
                            addrCmd.Parameters.AddWithValue("@street", street ?? "");
                            addrCmd.Parameters.AddWithValue("@house", house ?? "");
                            addrCmd.Parameters.AddWithValue("@entrance", entrance ?? "");
                            addrCmd.Parameters.AddWithValue("@addressId", addressId);
                            addrCmd.ExecuteNonQuery();
                        }

                        string pointQuery = @"
                            UPDATE point
                            SET name = @name, type_id = @typeId
                            WHERE point_id = @pointId";

                        using (var pointCmd = new NpgsqlCommand(pointQuery, conn, transaction))
                        {
                            pointCmd.Parameters.AddWithValue("@name", pointName ?? "");
                            pointCmd.Parameters.AddWithValue("@typeId", typeId);
                            pointCmd.Parameters.AddWithValue("@pointId", pointId);
                            pointCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка обновления точки:\n{ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        public bool DeletePoint(int pointId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string getAddrQuery = "SELECT address_id FROM point WHERE point_id = @pointId";
                    int addressId;
                    using (var getCmd = new NpgsqlCommand(getAddrQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@pointId", pointId);
                        addressId = Convert.ToInt32(getCmd.ExecuteScalar());
                    }

                    string deleteLink = "DELETE FROM clent_point WHERE point_id = @pointId";
                    using (var linkCmd = new NpgsqlCommand(deleteLink, conn))
                    {
                        linkCmd.Parameters.AddWithValue("@pointId", pointId);
                        linkCmd.ExecuteNonQuery();
                    }

                    string deletePoint = "DELETE FROM point WHERE point_id = @pointId";
                    using (var pointCmd = new NpgsqlCommand(deletePoint, conn))
                    {
                        pointCmd.Parameters.AddWithValue("@pointId", pointId);
                        pointCmd.ExecuteNonQuery();
                    }

                    string checkOther = "SELECT COUNT(*) FROM point WHERE address_id = @addressId";
                    using (var checkCmd = new NpgsqlCommand(checkOther, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@addressId", addressId);
                        int cnt = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (cnt == 0)
                        {
                            string deleteAddr = "DELETE FROM address WHERE address_id = @addressId";
                            using (var addrCmd = new NpgsqlCommand(deleteAddr, conn))
                            {
                                addrCmd.Parameters.AddWithValue("@addressId", addressId);
                                addrCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления точки:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public int AddPointType(string typeName)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO point_tupe (name) VALUES (@name) RETURNING point_tupe";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", typeName);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления типа точки:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }
    }
}