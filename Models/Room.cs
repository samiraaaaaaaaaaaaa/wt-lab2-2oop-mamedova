using System;

namespace wt_lab2_2oop_mamedova.Models
{
    public class Room : BaseEntity
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public decimal PricePerNight { get; private set; } // Инкапсуляция
        public bool IsAvailable { get; set; }

        // protected-член в иерархии классов
        protected string internalNotes;

        public Room(int id, int number, string type, decimal pricePerNight, bool isAvailable) 
            : base(id)
        {
            Number = number;
            Type = type;
            UpdatePrice(pricePerNight);
            IsAvailable = isAvailable;
            internalNotes = "Стандартный номер гостиницы";
        }

        // Валидация при изменении цены (Инкапсуляция)
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0) 
                throw new ArgumentException("Цена не может быть отрицательной!");
            PricePerNight = newPrice;
        }

        // Переопределение метода из BaseEntity (Полиморфизм)
        public override void PrintInfo()
        {
            string status = IsAvailable ? "Свободен" : "Занят";
            Console.WriteLine($"[Обычный номер] ID: {Id} | Номер: {Number} | Тип: {Type} | Цена: {PricePerNight} руб. | Статус: {status} | Заметка: {internalNotes}");
        }

        // Перегрузка метода (Method Overloading)
        public virtual void PrintInfo(bool includeHeader)
        {
            if (includeHeader)
            {
                Console.WriteLine("=== ДЕТАЛЬНАЯ ИНФОРМАЦИЯ О НОМЕРЕ ===");
            }
            PrintInfo();
        }
    }
}