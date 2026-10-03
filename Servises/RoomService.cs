using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wt_lab2_2oop_mamedova.Interfaces;
using wt_lab2_2oop_mamedova.Models;

namespace wt_lab2_2oop_mamedova.Services
{
    public class RoomService : IItemService<Room>
    {
        private List<Room> _rooms = new List<Room>();
        private Dictionary<int, Room> _roomDictionary = new Dictionary<int, Room>();

        public void Add(Room item)
        {
            _rooms.Add(item);
            _roomDictionary[item.Id] = item;
        }

        public List<Room> GetAll()
        {
            return _rooms;
        }

        public Room GetById(int id)
        {
            if (_roomDictionary.TryGetValue(id, out Room room))
            {
                return room;
            }
            return null;
        }

        public void Remove(int id)
        {
            Room room = GetById(id);
            if (room != null)
            {
                _rooms.Remove(room);
                _roomDictionary.Remove(id);
            }
        }

        public void DisplayAll()
        {
            Console.WriteLine($"[INFO] Всего объектов в коллекции: {_rooms.Count}\n");
            foreach (var room in _rooms)
            {
                room.PrintInfo();
            }
        }

        public void ExecuteAggregationAndGrouping()
        {
            if (_rooms.Count == 0)
            {
                Console.WriteLine("[!] Коллекция комнат пуста.");
                return;
            }

            Console.WriteLine("--- 1. АГРЕГАТНЫЕ ОПЕРАЦИИ ---");
            int totalCount = _rooms.Count();
            decimal totalPrice = _rooms.Sum(r => r.PricePerNight);
            decimal avgPrice = _rooms.Average(r => r.PricePerNight);
            decimal minPrice = _rooms.Min(r => r.PricePerNight);
            decimal maxPrice = _rooms.Max(r => r.PricePerNight);

            Console.WriteLine($"Всего комнат (Count): {totalCount}");
            Console.WriteLine($"Суммарная стоимость всех ночей (Sum): {totalPrice} руб.");
            Console.WriteLine($"Средняя цена за ночь (Average): {avgPrice:F2} руб.");
            Console.WriteLine($"Минимальная цена (Min): {minPrice} руб.");
            Console.WriteLine($"Максимальная цена (Max): {maxPrice} руб.");

            Console.WriteLine("\n--- 2. ГРУППИРОВКА ДАННЫХ (GroupBy по категориям) ---");
            var groupedRooms = _rooms.GroupBy(r => r.Type);

            foreach (var group in groupedRooms)
            {
                Console.WriteLine($"\n📁 Категория: '{group.Key}' (Количество комнат: {group.Count()})");
                Console.WriteLine($"   Средняя цена в группе: {group.Average(r => r.PricePerNight):F2} руб.");
                Console.WriteLine("   Список номеров:");
                foreach (var room in group)
                {
                    Console.WriteLine($"     - Комната №{room.Number} (Цена: {room.PricePerNight} руб., Доступна: {room.IsAvailable})");
                }
            }
        }

        public async Task ProcessRoomsAsync()
        {
            Console.WriteLine("[ASYNC] Начало асинхронной обработки комнат...");
            await Task.Delay(1500);
            Console.WriteLine("[ASYNC] Обработка завершена! Всего комнат в системе: " + _rooms.Count);
        }

        public async Task PerformBulkOperationsAsync()
        {
            Console.WriteLine("[ASYNC] Запуск параллельных фоновых задач (Task.WhenAll)...");

            var task1 = Task.Run(async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine(" -> Задача 1: Проверка статуса бронирования завершена.");
            });

            var task2 = Task.Run(async () =>
            {
                await Task.Delay(1500);
                Console.WriteLine(" -> Задача 2: Синхронизация цен с сервером завершена.");
            });

            var task3 = Task.Run(async () =>
            {
                await Task.Delay(800);
                Console.WriteLine(" -> Задача 3: Создание резервной копии логов завершено.");
            });

            await Task.WhenAll(task1, task2, task3);
            Console.WriteLine("[ASYNC] Все параллельные фоновые операции успешно выполнены!\n");
        }

        public async Task<List<Room>> GetAllAsync()
        {
            Console.WriteLine("[ASYNC DB] Подключение к удаленной СУБД / внешнему API...");
            await Task.Delay(1200);
            Console.WriteLine("[ASYNC DB] Данные успешно загружены из источника!");
            return _rooms;
        }

        public async Task ProcessWithExceptionHandlingAsync(bool triggerError)
        {
            Console.WriteLine("[ASYNC TRY-CATCH] Запуск асинхронной операции...");
            try
            {
                await Task.Delay(800);
                
                if (triggerError)
                {
                    throw new InvalidOperationException("Критическая ошибка: сбой соединения с удаленным сервером базы данных!");
                }

                Console.WriteLine("[ASYNC TRY-CATCH] Данные успешно обработаны в штатном режиме.");
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПЕРЕХВАЧЕНО ОШИБКУ]: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("[ASYNC FINALLY] Освобождение ресурсов, закрытие сетевых соединений.");
            }
        }
        // Поддержка отмены асинхронной операции через CancellationToken
        public async Task LongRunningOperationWithCancellationAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("[ASYNC CANCELLATION] Запуск длительной операции... Нажмите любую клавишу для отмены.");
            
            try
            {
                for (int i = 1; i <= 5; i++)
                {
                    // Проверяем, не запрошена ли отмена перед каждым шагом
                    cancellationToken.ThrowIfCancellationRequested();

                    Console.WriteLine($"[ASYNC CANCELLATION] Шаг {i} из 5 выполняется...");
                    
                    // Передаем токен в Task.Delay, чтобы прервать ожидание мгновенно
                    await Task.Delay(1000, cancellationToken);
                }

                Console.WriteLine("[ASYNC CANCELLATION] Операция успешно завершена!");
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[ASYNC CANCELLATION] Операция была отменена пользователем!");
                Console.ResetColor();
            }
        }
    }
}