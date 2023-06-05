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
        private List<Direction> _directions = new List<Direction>();
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
            string pointDeparture = Console.ReadLine();

            Console.WriteLine("Куда");
            string pointArrival = Console.ReadLine();

            _directions.Add(new Direction(pointDeparture, pointArrival));
        }

        private int SellTickets()
        {
            Random random = new Random();

            int minCounPassengers = 1;
            int maxCountPassengers = 100;
            int counPassengers;

            counPassengers = random.Next(minCounPassengers, maxCountPassengers);

            return counPassengers;
        }

        private void ShowDirection()
        {
            foreach (Direction direction in _directions)
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
                if (_train.CreateWagon())
                {
                    if (_train.CreateCapacity())
                    {
                        if (numberTickets <= _train.GetCapacity())
                        {
                            Console.WriteLine($"Поезд [{_directions[0]._pointDeparture}] [{_directions[0]._pointArrival}] создан! счастливого пути!");

                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();

                            _isWork = false;
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно мест для пассажиров!!!\n");
                        }
                    }
                }
            }

            _isWork = true;
        }
    }

    class Direction
    {
        public Direction(string whereFrom, string where)
        {
            _pointDeparture = whereFrom;
            _pointArrival = where;
        }

        public string _pointDeparture { get; private set; }
        public string _pointArrival { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Откуда {_pointDeparture} Куда {_pointArrival}");
        }
    }

    class Train
    {
        private int _countWagons;
        private int _capacity;

        public bool CreateWagon()
        {
            Console.WriteLine("Введите количество вагонов");

            int.TryParse(Console.ReadLine(), out _countWagons);

            if (_countWagons <= 0)
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

            int.TryParse(Console.ReadLine(), out _capacity);

            if (_capacity <= 0)
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
            return _countWagons * _capacity;
        }
    }
}
