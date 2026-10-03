using System;

namespace wt_lab2_2oop_mamedova.Models
{
    public class VipApartment : BaseEntity
    {
        public int Number { get; set; }
        public decimal PricePerNight { get; set; }
        public bool HasPersonalButler { get; set; }

        public VipApartment(int id, int number, decimal pricePerNight, bool hasButler) : base(id)
        {
            Number = number;
            PricePerNight = pricePerNight;
            HasPersonalButler = hasButler;
        }

        public override void PrintInfo()
        {
            string butlerStr = HasPersonalButler ? "Есть дворецкий" : "Без дворецкого";
            Console.WriteLine($"[VIP Апартаменты] ID: {Id} | Номер: {Number} | Цена: {PricePerNight} руб./ночь | Сервис: {butlerStr}");
        }
    }
}