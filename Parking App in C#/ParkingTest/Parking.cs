using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingTest
{
    public class Parking
    {
        private static Dictionary<CarType, double> PriceList = Settings.getPriceList();
        private static int Timeout = Settings.Timeout;
        private static double Fine = Settings.Fine;
        private static int ParkingSpace = Settings.ParkingSpace;

        private List<Car> Cars;
        private List<Transaction> Transactions;

        private double ParkingBalance;

        private static Parking instance;

        private Parking()
        {

            Cars = new List<Car>(ParkingSpace);
            Transactions = new List<Transaction>();

            ParkingBalance = 0;

            TimerCallback calculateFunds = new TimerCallback(calculateFundsForAllCars);
            Timer timer1 = new Timer(calculateFunds, null, 0, Timeout);

            TimerCallback serializeToFile = new TimerCallback(SerializeToFile);
            Timer timer2 = new Timer(serializeToFile, null, 0, 60000);


        }

        public static Parking getInstance()
        {

                if (instance == null)
                {
                    instance = new Parking();
                }
                return instance;
        }

        public double getParkingRevenue()
        {
            return ParkingBalance;
        }

        public void addCar(Car car)
        {
            if(Cars.Count() < ParkingSpace)
            {
                Cars.Add(car);
            }
            else
            {
                Console.WriteLine("The parking is overloaded!");
            }
        }

        public List<Car> getAllCars()
        {
            return Cars;
        }

        public List<Transaction> getAllTransactions()
        {
            return Transactions;
        }

        public void addCarBalance(double newBalance, Car car)
        {
            car.Balance += newBalance;
        }

        public void removeCar(Car car)
        {
            Cars.Remove(car);
        }

        public int availablePlaces()
        {
            return ParkingSpace - Cars.Count;
        }


        public void calculateFee(double newBalance, Car car)
        {
            ParkingBalance += newBalance;
            Transactions.Add(new Transaction
            {
                transactionDate = DateTime.Now,
                carId = car.Id,
                funds = newBalance
            });
        }

        private void calculateFundsForAllCars(object obj)
        {
           foreach(Car car in Cars)
            {
                double priceForCar = PriceList[car.Type];
                if (car.Balance < priceForCar)
                {
                    car.Balance -= priceForCar * Fine;
                }
                else
                {
                    car.Balance -= priceForCar;
                    ParkingBalance += priceForCar;
                    Transactions.Add(new Transaction
                    {
                        transactionDate = DateTime.Now,
                        carId = car.Id,
                        funds = priceForCar
                    });
                }
            }
        }


        private void SerializeToFile(object obj)
        {

            List<SerializeTransactions> previousTransactions = this.DeserializeFromFile();

            SerializeTransactions transactionsForPreviousMinute = new SerializeTransactions()
            {
                timeOfSerialization = DateTime.Now,
                fundsForPreviousMinute = Transactions.Select(x => x.funds).Sum()
            };


            previousTransactions.Add(transactionsForPreviousMinute);
    

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Transactions.log", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, previousTransactions);

                Console.WriteLine("All transactions were serialized to Transaction.log file");
            }


            Transactions = new List<Transaction>();
        }



        public List<SerializeTransactions> DeserializeFromFile()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<SerializeTransactions> transactionLog;

            try
            {
                using (FileStream fs = new FileStream("Transactions.log", FileMode.OpenOrCreate))
                {
                    transactionLog = (List<SerializeTransactions>)formatter.Deserialize(fs);

                }
            }
            catch (SerializationException)
            {
                transactionLog = new List<SerializeTransactions>();
            }

            return transactionLog;
        }

    }
}
