using System;

namespace wt_lab2_2oop_mamedova.Models
{
    public class ConferenceHall : BaseEntity
    {
        public string HallName { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; private set; }

        public ConferenceHall(int id, string hallName, int capacity, decimal pricePerHour) : base(id)
        {
            HallName = hallName;
            Capacity = capacity;
            PricePerHour = pricePerHour;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"[Конференц-зал] ID: {Id} | Название: {HallName} | Вместимость: {Capacity} чел. | Цена: {PricePerHour} руб./час");
        }
    }
}