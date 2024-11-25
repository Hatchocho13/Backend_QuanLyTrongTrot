using System;
using System.Data.SqlClient;

public partial class Program
{
    static void Main()
    {
        string connectionString = @"Server=DESKTOP-NBUC3NG\KTEAM; Database=Quanlytrongtrot; Integrated Security=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Kết nối thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}