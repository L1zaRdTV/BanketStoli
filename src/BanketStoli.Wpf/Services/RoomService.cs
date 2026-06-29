using System.Collections.Generic;
using System.Data.SqlClient;
using BanketStoli.Wpf.Data;
using BanketStoli.Wpf.Models;

namespace BanketStoli.Wpf.Services
{
    public class RoomService
    {
        public List<DecorationStyle> GetStyles()
        {
            var styles = new List<DecorationStyle>();
            using (var connection = Database.CreateConnection())
            using (var command = new SqlCommand("SELECT Id, Name FROM DecorationStyles ORDER BY Name", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) styles.Add(new DecorationStyle { Id = (int)reader["Id"], Name = reader["Name"].ToString() });
                }
            }
            return styles;
        }

        public List<BanquetRoom> GetRooms(string searchText, int? styleId, string sortMode)
        {
            var rooms = new List<BanquetRoom>();
            var sql = @"SELECT b.Id, b.Name, b.StyleId, s.Name AS StyleName, b.TableCount, b.RentPricePerHour, b.ImagePath, b.Description
FROM BanquetRooms b INNER JOIN DecorationStyles s ON s.Id = b.StyleId
WHERE (@search = '' OR b.Name LIKE @likeSearch OR b.Description LIKE @likeSearch) AND (@styleId IS NULL OR b.StyleId = @styleId)";
            sql += sortMode == "desc" ? " ORDER BY b.RentPricePerHour DESC" : sortMode == "asc" ? " ORDER BY b.RentPricePerHour ASC" : " ORDER BY b.Name";
            using (var connection = Database.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@search", searchText ?? string.Empty);
                command.Parameters.AddWithValue("@likeSearch", "%" + (searchText ?? string.Empty) + "%");
                command.Parameters.AddWithValue("@styleId", (object)styleId ?? System.DBNull.Value);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) rooms.Add(ReadRoom(reader));
                }
            }
            return rooms;
        }

        public void SaveRoom(BanquetRoom room)
        {
            using (var connection = Database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                if (room.Id == 0)
                {
                    command.CommandText = @"INSERT INTO BanquetRooms (Name, StyleId, TableCount, RentPricePerHour, ImagePath, Description)
VALUES (@name, @styleId, @tableCount, @price, @imagePath, @description)";
                }
                else
                {
                    command.CommandText = @"UPDATE BanquetRooms SET Name = @name, StyleId = @styleId, TableCount = @tableCount,
RentPricePerHour = @price, ImagePath = @imagePath, Description = @description WHERE Id = @id";
                    command.Parameters.AddWithValue("@id", room.Id);
                }
                command.Parameters.AddWithValue("@name", room.Name);
                command.Parameters.AddWithValue("@styleId", room.StyleId);
                command.Parameters.AddWithValue("@tableCount", room.TableCount);
                command.Parameters.AddWithValue("@price", room.RentPricePerHour);
                command.Parameters.AddWithValue("@imagePath", (object)room.ImagePath ?? System.DBNull.Value);
                command.Parameters.AddWithValue("@description", room.Description);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteRoom(int roomId)
        {
            using (var connection = Database.CreateConnection())
            using (var command = new SqlCommand("DELETE FROM BanquetRooms WHERE Id = @id", connection))
            {
                command.Parameters.AddWithValue("@id", roomId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static BanquetRoom ReadRoom(SqlDataReader reader)
        {
            return new BanquetRoom { Id = (int)reader["Id"], Name = reader["Name"].ToString(), StyleId = (int)reader["StyleId"], StyleName = reader["StyleName"].ToString(), TableCount = (int)reader["TableCount"], RentPricePerHour = (decimal)reader["RentPricePerHour"], ImagePath = reader["ImagePath"] as string, Description = reader["Description"].ToString() };
        }
    }
}
