using System.Collections.Generic;
using wt_lab2_2oop_mamedova.Models;

namespace wt_lab2_2oop_mamedova.Interfaces
{
    public interface IRoomService
    {
        void AddRoom(Room room);
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        void DisplayAll();
        void ExecuteLinqQueries();
    }
}