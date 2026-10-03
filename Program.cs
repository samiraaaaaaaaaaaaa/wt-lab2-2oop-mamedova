using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wt_lab2_2oop_mamedova.Interfaces;
using wt_lab2_2oop_mamedova.Models;
using wt_lab2_2oop_mamedova.Services;
using wt_lab2_2oop_mamedova.Exceptions;

namespace wt_lab2_2oop_mamedova
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            RoomService service = new RoomService();

            // Наполнение 5 тестовыми объектами (включая повторяющиеся типы для красивой группировки GroupBy)
            service.Add(new Room(1, 101, "Одноместный", 120.0m, true));
            service.Add(new Room(2, 102, "Люкс", 350.0m, false));
            service.Add(new Room(3, 201, "Двухместный", 180.0m, true));
            service.Add(new Room(4, 203, "Одноместный", 110.0m, true));
            service.Add(new Room(5, 301, "Люкс", 450.0m, true));

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("   ЛАБОРАТОРНАЯ РАБОТА № 2: Гостиничный комплекс");
                Console.WriteLine("==================================================");
                Console.WriteLine(" 1. Показать все комнаты (foreach + List)");
                Console.WriteLine(" 2. Демонстрация полиморфизма (Наследование BaseEntity)");
                Console.WriteLine(" 3. Агрегатные операции и группировка (LINQ: Count, Sum, GroupBy)");
                Console.WriteLine(" 4. Поиск комнаты по ID в Dictionary (TryGetValue)");
                Console.WriteLine(" 5. Асинхронная обработка (async/await)");
                Console.WriteLine(" 6. Параллельные фоновые задачи (Task.WhenAll)");
                Console.WriteLine(" 7. Демонстрация через интерфейс и перегрузку методов");
                Console.WriteLine(" 8. Демонстрация кастомного исключения (HotelValidationException)");
                Console.WriteLine(" 9. Асинхронная загрузка из СУБД/API (async Task<List<T>>)");
                Console.WriteLine(" 10. Асинхронная обработка с try-catch-finally");
                Console.WriteLine(" 11. Демонстрация отмены асинхронной операции (CancellationToken)");
                Console.WriteLine(" 0. Выход из программы");
                Console.WriteLine("==================================================");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("--- Все комнаты в отеле ---");
                        service.DisplayAll();
                        break;
                    case "2":
                        RunPolymorphismDemo();
                        break;
                    case "3":
                        service.ExecuteAggregationAndGrouping(); // Вызов агрегаций и GroupBy
                        break;
                    case "4":
                        Console.Write("Введите ID комнаты для поиска (1-5): ");
                        if (int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            Room found = service.GetById(searchId);
                            if (found != null)
                            {
                                Console.WriteLine("[НАЙДЕНО]:");
                                found.PrintInfo();
                            }
                            else
                            {
                                Console.WriteLine($"[!] Комната с ID {searchId} не найдена в словаре.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("[X] Неверный формат ID.");
                        }
                        break;
                   case "5":
                        await service.ProcessRoomsAsync();
                        break;
                    case "6":
                        await service.PerformBulkOperationsAsync();
                        break;
                    case "7":
                        RunInterfacePolymorphismDemo(service);
                        break;
                    case "8":
                        RunCustomExceptionDemo();
                        break;
                    case "9":
    List<Room> remoteRooms = await service.GetAllAsync();
    Console.WriteLine($"\n[ОТЧЕТ] Успешно получено объектов через async Task<List<T>>: {remoteRooms.Count}");
    foreach (var r in remoteRooms)
    {
        r.PrintInfo();
    }
    break;
                     case "10":
    Console.Write("Вызвать ошибку в асинхронном методе? (y/n): ");
    bool hasError = Console.ReadLine()?.Trim().ToLower() == "y";
    await service.ProcessWithExceptionHandlingAsync(hasError);
    break;
    case "11":
    using (var cts = new CancellationTokenSource())
    {
        // Запускаем асинхронную задачу и передаем ей токен отмены
        var task = service.LongRunningOperationWithCancellationAsync(cts.Token);

        // Фоновый поток, который ждет нажатия любой клавиши от пользователя для отмены
        var cancelTask = Task.Run(() =>
        {
            Console.ReadKey(true);
            cts.Cancel(); // Сигнализируем об отмене
        });

        // Ждем завершения операции или её отмены
        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            // Исключение перехвачено и обработано
        }
    }
    break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Завершение работы программы. До свидания!");
                        break;
                    default:
                        Console.WriteLine("[X] Неверный выбор. Пожалуйста, введите цифру от 0 до 8.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                }
            }
        }

        static void RunPolymorphismDemo()
        {
            Console.WriteLine("--- ДЕМОНСТРАЦИЯ ПОЛИМОРФИЗМА И НАСЛЕДОВАНИЯ ---\n");

            List<BaseEntity> hotelAssets = new List<BaseEntity>
            {
                new Room(1, 101, "Одноместный", 120.0m, true),
                new ConferenceHall(2, "Grand Hall", 150, 450.0m),
                new VipApartment(3, 777, 1200.0m, true),
                new Room(4, 102, "Двухместный", 200.0m, false)
            };

            foreach (var asset in hotelAssets)
            {
                asset.PrintInfo();
            }
        }

        static void RunInterfacePolymorphismDemo(IItemService<Room> itemService)
        {
            Console.WriteLine("--- ДЕМОНСТРАЦИЯ ЧЕРЕЗ ИНТЕРФЕЙСНУЮ ССЫЛКУ (IItemService<T>) ---\n");

            List<Room> allRooms = itemService.GetAll();
            Console.WriteLine($"Всего комнат получено через интерфейс: {allRooms.Count}");

            if (allRooms.Count > 0)
            {
                Console.WriteLine("\nВызов перегруженного метода PrintInfo(true):");
                allRooms[0].PrintInfo(true);
            }
        }

        static void RunCustomExceptionDemo()
        {
            Console.WriteLine("--- ДЕМОНСТРАЦИЯ СОБСТВЕННОГО ИСКЛЮЧЕНИЯ ---\n");

            try
            {
                Console.WriteLine("Попытка создать комнату с отрицательной ценой -100...");
                Room invalidRoom = new Room(99, 999, "Тест", -100.0m, true);
            }
            catch (HotelValidationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПЕРЕХВАЧЕНО ИСКЛЮЧЕНИЕ]: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}