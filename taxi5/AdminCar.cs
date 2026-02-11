using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminCar
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        // Получение списка автомобилей с информацией о водителе, марке, модели, цвете
        public DataTable GetCars()
        {
            DataTable dt = new DataTable();

            // Создаём структуру заранее
            dt.Columns.Add("car_id", typeof(int));
            dt.Columns.Add("brand_name", typeof(string));
            dt.Columns.Add("model_name", typeof(string));
            dt.Columns.Add("color_name", typeof(string));
            dt.Columns.Add("license_number", typeof(string));
            dt.Columns.Add("region_code", typeof(string));
            dt.Columns.Add("year_of_manufacture", typeof(int));
            dt.Columns.Add("driver_name", typeof(string));
            dt.Columns.Add("driver_id", typeof(int));

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            c.car_id,
                            COALESCE(b.name, '') as brand_name,
                            COALESCE(m.name, '') as model_name,
                            COALESCE(col.name, '') as color_name,
                            COALESCE(c.license_number, '') as license_number,
                            COALESCE(c.region_code, '') as region_code,
                            c.year_of_manufacture,
                            COALESCE(d.last_name || ' ' || d.first_name, 'Не назначен') as driver_name,
                            c.driver_id
                        FROM car c
                        LEFT JOIN brand b ON c.brand_id = b.brand_id
                        LEFT JOIN model m ON c.model_id = m.model_id
                        LEFT JOIN color col ON c.color_id = col.color_id
                        LEFT JOIN driver d ON c.driver_id = d.driver_id
                        ORDER BY c.car_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    // Замена DBNull
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (row[col] == DBNull.Value)
                            {
                                if (col.DataType == typeof(string))
                                    row[col] = "";
                                else if (col.DataType == typeof(int))
                                    row[col] = 0;
                            }
                        }
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

        // Получение списка брендов
        public DataTable GetBrands()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT brand_id, name FROM brand ORDER BY name";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки брендов: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // Получение моделей по ID бренда
        public DataTable GetModelsByBrand(int brandId)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT model_id, name FROM model WHERE brand_id = @brandId ORDER BY name";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                            adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки моделей: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // Получение списка цветов
        public DataTable GetColors()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT color_id, name FROM color ORDER BY name";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки цветов: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // Получение списка водителей (активных)
        public DataTable GetDrivers()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT driver_id, last_name || ' ' || first_name AS full_name 
                        FROM driver 
                        WHERE driver_status_id = 1 
                        ORDER BY last_name";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки водителей: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // Добавление автомобиля
        public bool AddCar(int brandId, int modelId, int colorId, string licenseNumber, string regionCode, int year, int? driverId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO car (brand_id, model_id, color_id, license_number, region_code, year_of_manufacture, driver_id)
                        VALUES (@brandId, @modelId, @colorId, @licenseNumber, @regionCode, @year, @driverId)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        cmd.Parameters.AddWithValue("@modelId", modelId);
                        cmd.Parameters.AddWithValue("@colorId", colorId);
                        cmd.Parameters.AddWithValue("@licenseNumber", licenseNumber);
                        cmd.Parameters.AddWithValue("@regionCode", regionCode);
                        cmd.Parameters.AddWithValue("@year", year);
                        if (driverId.HasValue)
                            cmd.Parameters.AddWithValue("@driverId", driverId.Value);
                        else
                            cmd.Parameters.AddWithValue("@driverId", DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления автомобиля: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Обновление автомобиля
        public bool UpdateCar(int carId, int brandId, int modelId, int colorId, string licenseNumber, string regionCode, int year, int? driverId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
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

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
                        cmd.Parameters.AddWithValue("@brandId", brandId);
                        cmd.Parameters.AddWithValue("@modelId", modelId);
                        cmd.Parameters.AddWithValue("@colorId", colorId);
                        cmd.Parameters.AddWithValue("@licenseNumber", licenseNumber);
                        cmd.Parameters.AddWithValue("@regionCode", regionCode);
                        cmd.Parameters.AddWithValue("@year", year);
                        if (driverId.HasValue)
                            cmd.Parameters.AddWithValue("@driverId", driverId.Value);
                        else
                            cmd.Parameters.AddWithValue("@driverId", DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления автомобиля: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Удаление автомобиля
        public bool DeleteCar(int carId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM car WHERE car_id = @carId";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
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
                        MessageBox.Show("Невозможно удалить автомобиль: имеются связанные записи",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка базы данных: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}