using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminCar
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        // ---------- ПОЛУЧЕНИЕ СПИСКА АВТОМОБИЛЕЙ ----------
        public DataTable GetCars()
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
                            COALESCE(b.name, '') AS brand_name,
                            COALESCE(m.name, '') AS model_name,
                            COALESCE(col.name, '') AS color_name,
                            COALESCE(c.license_number, '') AS license_number,
                            COALESCE(c.region_code, '') AS region_code,
                            c.year_of_manufacture,
                            COALESCE(d.last_name || ' ' || d.first_name, 'Не назначен') AS driver_name,
                            c.driver_id
                        FROM car c
                        LEFT JOIN brand b ON c.brand_id = b.brand_id
                        LEFT JOIN model m ON c.model_id = m.model_id
                        LEFT JOIN color col ON c.color_id = col.color_id
                        LEFT JOIN driver d ON c.driver_id = d.driver_id
                        ORDER BY c.car_id";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки автомобилей:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // ---------- ПОЛУЧЕНИЕ СПРАВОЧНИКОВ ----------
        public DataTable GetBrands()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT brand_id, name FROM brand ORDER BY name";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки марок:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public DataTable GetModelsByBrand(int brandId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT model_id, name FROM model WHERE brand_id = @brandId ORDER BY name";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                            adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки моделей:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public DataTable GetColors()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT color_id, name FROM color ORDER BY name";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки цветов:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public DataTable GetDrivers()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT driver_id, last_name || ' ' || first_name AS full_name 
                        FROM driver 
                        WHERE driver_status_id = 1 
                        ORDER BY last_name";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки водителей:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // ---------- ДОБАВЛЕНИЕ / ОБНОВЛЕНИЕ / УДАЛЕНИЕ АВТОМОБИЛЯ ----------
        public bool AddCar(int brandId, int modelId, int colorId, string licenseNumber, string regionCode, int year, int? driverId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO car 
                            (brand_id, model_id, color_id, license_number, region_code, year_of_manufacture, driver_id)
                        VALUES 
                            (@brandId, @modelId, @colorId, @licenseNumber, @regionCode, @year, @driverId)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        cmd.Parameters.AddWithValue("@modelId", modelId);
                        cmd.Parameters.AddWithValue("@colorId", colorId);
                        cmd.Parameters.AddWithValue("@licenseNumber", licenseNumber ?? "");
                        cmd.Parameters.AddWithValue("@regionCode", regionCode ?? "");
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@driverId", driverId.HasValue ? (object)driverId.Value : DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления автомобиля:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool UpdateCar(int carId, int brandId, int modelId, int colorId, string licenseNumber, string regionCode, int year, int? driverId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        UPDATE car SET
                            brand_id = @brandId,
                            model_id = @modelId,
                            color_id = @colorId,
                            license_number = @licenseNumber,
                            region_code = @regionCode,
                            year_of_manufacture = @year,
                            driver_id = @driverId
                        WHERE car_id = @carId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        cmd.Parameters.AddWithValue("@modelId", modelId);
                        cmd.Parameters.AddWithValue("@colorId", colorId);
                        cmd.Parameters.AddWithValue("@licenseNumber", licenseNumber ?? "");
                        cmd.Parameters.AddWithValue("@regionCode", regionCode ?? "");
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@driverId", driverId.HasValue ? (object)driverId.Value : DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления автомобиля:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool DeleteCar(int carId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM car WHERE car_id = @carId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (NpgsqlException ex) when (ex.Message.Contains("23503") || ex.Message.Contains("foreign key"))
                {
                    MessageBox.Show("Невозможно удалить автомобиль: имеются связанные записи.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления автомобиля:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // ---------- ДОБАВЛЕНИЕ НОВЫХ ЗАПИСЕЙ В СПРАВОЧНИКИ (ДЛЯ ФОРМЫ) ----------
        public int AddBrand(string brandName)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO brand (name) VALUES (@name) RETURNING brand_id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", brandName);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления марки:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public int AddModel(int brandId, string modelName)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO model (name, brand_id) VALUES (@name, @brandId) RETURNING model_id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", modelName);
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления модели:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public int AddColor(string colorName)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO color (name) VALUES (@name) RETURNING color_id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", colorName);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления цвета:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }
    }
}