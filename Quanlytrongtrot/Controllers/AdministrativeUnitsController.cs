using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AdministrativeUnitsAPI.Models;

namespace AdministrativeUnitsAPI.Controllers

{
    [ApiController]
    [Route("api/administrative-units")]
    public class AdministrativeUnitsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdministrativeUnitsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //1. Lấy danh sách đơn vị hành chính (GET)
        [HttpGet]
        public IActionResult GetAdministrativeUnits()
        {
            var units = new List<AdministrativeUnit>();

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT Huyen.TenHuyen AS Huyen, Xa.TenXa AS Xa
                    FROM Huyen
                    INNER JOIN Xa ON Huyen.ID = Xa.ID_Huyen;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            units.Add(new AdministrativeUnit
                            {
                                Huyen = reader["Huyen"].ToString(),
                                Xa = reader["Xa"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(units);
        }

        // 2.Thêm mới đơn vị hành chính (POST)
        [HttpPost]
        public IActionResult AddAdministrativeUnit([FromBody] AdministrativeUnit newUnit)
        {
            if (newUnit == null || string.IsNullOrWhiteSpace(newUnit.Huyen) || string.IsNullOrWhiteSpace(newUnit.Xa))
            {
                return BadRequest("Thông tin không hợp lệ.");
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO Huyen (TenHuyen) VALUES (@TenHuyen);
            DECLARE @HuyenID INT = SCOPE_IDENTITY();
            INSERT INTO Xa (TenXa, ID_Huyen) VALUES (@TenXa, @HuyenID);
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenHuyen", newUnit.Huyen);
                    command.Parameters.AddWithValue("@TenXa", newUnit.Xa);

                    connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Đơn vị hành chính đã được thêm thành công.");
                        }
                        else
                        {
                            return StatusCode(500, "Không thể thêm đơn vị hành chính.");
                        }                
                }
            }
        }


        //3. Cập nhật đơn vị hành chính (PUT)
        [HttpPut("{id}")]
        public IActionResult UpdateAdministrativeUnit(int id, [FromBody] AdministrativeUnit updatedUnit)
        {
            if (updatedUnit == null || string.IsNullOrEmpty(updatedUnit.Huyen) || string.IsNullOrEmpty(updatedUnit.Xa))
            {
                return BadRequest("Thông tin đơn vị hành chính không hợp lệ.");
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Xa
                    SET TenXa = @TenXa
                    WHERE ID = @ID;

                    UPDATE Huyen
                    SET TenHuyen = @TenHuyen
                    WHERE ID = (SELECT ID_Huyen FROM Xa WHERE ID = @ID);
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@TenXa", updatedUnit.Xa);
                    command.Parameters.AddWithValue("@TenHuyen", updatedUnit.Huyen);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Cập nhật đơn vị hành chính thành công.");
                    }
                    else
                    {
                        return NotFound("Không tìm thấy đơn vị hành chính với ID được cung cấp.");
                    }
                }
            }
        }

        //4. Xóa đơn vị hành chính (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteAdministrativeUnit(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            DELETE FROM Xa WHERE ID = @ID;
            DELETE FROM Huyen WHERE ID = @ID;
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound("Không tìm thấy đơn vị hành chính với ID này.");
                    }
                }
            }

            return Ok("Đã xóa đơn vị hành chính thành công." );
        }

    }
}



