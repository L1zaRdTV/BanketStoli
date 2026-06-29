using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BanketStoli.Data;
using BanketStoli.Models;

namespace BanketStoli.Services
{
    public class RoomService
    {
        public List<DecorationStyle> GetStyles()
        {
            using (var context = new BanketStoliEntities())
            {
                return context.DecorationStyles.OrderBy(style => style.Name).ToList();
            }
        }

        public List<BanquetRoom> GetRooms(string searchText, int? styleId, string sortMode)
        {
            using (var context = new BanketStoliEntities())
            {
                var query = from room in context.BanquetRooms
                            join style in context.DecorationStyles on room.StyleId equals style.Id
                            select new { Room = room, StyleName = style.Name };

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(item => item.Room.Name.Contains(searchText) || item.Room.Description.Contains(searchText));
                }

                if (styleId.HasValue)
                {
                    query = query.Where(item => item.Room.StyleId == styleId.Value);
                }

                query = sortMode == "desc"
                    ? query.OrderByDescending(item => item.Room.RentPricePerHour)
                    : sortMode == "asc"
                        ? query.OrderBy(item => item.Room.RentPricePerHour)
                        : query.OrderBy(item => item.Room.Name);

                return query.AsNoTracking().ToList().Select(item =>
                {
                    item.Room.StyleName = item.StyleName;
                    return item.Room;
                }).ToList();
            }
        }

        public void SaveRoom(BanquetRoom room)
        {
            using (var context = new BanketStoliEntities())
            {
                if (room.Id == 0)
                {
                    context.BanquetRooms.Add(room);
                }
                else
                {
                    context.BanquetRooms.Attach(room);
                    context.Entry(room).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }

        public void DeleteRoom(int roomId)
        {
            using (var context = new BanketStoliEntities())
            {
                var room = new BanquetRoom { Id = roomId };
                context.BanquetRooms.Attach(room);
                context.BanquetRooms.Remove(room);
                context.SaveChanges();
            }
        }
    }
}
