using System;

namespace wt_lab2_2oop_mamedova.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        protected BaseEntity(int id)
        {
            Id = id;
        }

        // Абстрактный метод для полиморфизма
        public abstract void PrintInfo();
    }
}