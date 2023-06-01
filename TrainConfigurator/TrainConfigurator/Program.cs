using System;
using System.Collections.Generic;

namespace TrainConfigurator
{
    class Program
    {
        static void Main(string[] args)
        {
            RailwayStation railwayStation = new RailwayStation();

            railwayStation.CreateTrain();
        }
    }

    class RailwayStation
    {
        private Queue<Direction> _directions = new Queue<Direction>();
        private Train _train = new Train();

        private bool _isWork = true;

        public void CreateTrain()
        {
            const string CommandCreate = "1";
            const string CommandExit = "2";

            while (_isWork)
            {
                int numberTickets = SellTickets();

                Console.WriteLine($"[{CommandCreate}] Создать Направление [{CommandExit}] Выход из программы");
                string userInput = Console.ReadLine();

                if (userInput == CommandCreate)
                {
                    Create(numberTickets);
                }
                else if (userInput == CommandExit)
                {
                    _isWork = false;
                }
            }
        }

        private void CreateDirection()
        {
            _directions.Clear();

            Console.WriteLine("Откуда");
            string whereFrom = Console.ReadLine();

            Console.WriteLine("Куда");
            string where = Console.ReadLine();

            _directions.Enqueue(new Direction(whereFrom, where));
        }

        private int SellTickets()
        {
            Random random = new Random();

            int minNumberPassengers = 1;
            int maxNumberPassengers = 100;
            int numberPassengers;

            numberPassengers = random.Next(minNumberPassengers, maxNumberPassengers);

            return numberPassengers;
        }

        private void ShowDirection()
        {
            foreach (var direction in _directions)
            {
                direction.ShowInfo();
            }
        }

        private void Create(int numberTickets)
        {
            CreateDirection();

            Console.Clear();
            Console.WriteLine($"Продано билетов {numberTickets}\n");

            ShowDirection();

            while (_isWork)
            {
                if (_train.CreateWagon() == true)
                {
                    if (_train.CreateCapacity() == true)
                    {
                        if (numberTickets <= _train.GetCapacity())
                        {
                            Console.WriteLine("Поезд создан! счастливого пути!");

                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно мест для пассажиров");
                        }
                    }
                }
            }
        }
    }

    class Direction
    {
        public Direction(string whereFrom, string where)
        {
            WhereFrom = whereFrom;
            Where = where;
        }

        public string WhereFrom { get; private set; }
        public string Where { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Откуда {WhereFrom} Куда {Where}");
        }
    }

    class Train
    {
        private int numberWagons;
        private int capacity;

        public bool CreateWagon()
        {
            Console.WriteLine("Введите количество вагонов");

            int.TryParse(Console.ReadLine(), out numberWagons);

            if (numberWagons <= 0)
            {
                Console.WriteLine("Ошибка! Введены не коректный данные. Повторите попытку.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CreateCapacity()
        {
            Console.WriteLine("Введите вместимость вагонов");

            int.TryParse(Console.ReadLine(), out capacity);

            if (capacity <= 0)
            {
                Console.WriteLine("Ошибка! Введены не коректный данные. Повторите попытку.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetCapacity()
        {
            return numberWagons * capacity;
        }
    }
}
