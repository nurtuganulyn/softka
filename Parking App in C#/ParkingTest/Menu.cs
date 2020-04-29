using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTest
{
    public class Menu
    {
        private static int carIdentificator = 0;
        private Parking parking;

        public Menu(Parking parking)
        {
            this.parking = parking;
        }

        public int MainMenu()
        {
            Console.WriteLine("Please, select the action:");

            try
            {
                Console.WriteLine("1 - Add the car");
                Console.WriteLine("2 - Remove the car");
                Console.WriteLine("3 - Get transaction history");
                Console.WriteLine("4 - Get total parking revenue");
                Console.WriteLine("5 - Get available parking places");
                Console.WriteLine("6 - Show Transaction.log");
                Console.WriteLine("7 - Exit");

                int actionId = Convert.ToInt32(Console.ReadLine());
                if(actionId < 1 && actionId > 7)
                {
                    throw new Exception();
                }
                return actionId;
            }
            catch(Exception)
            {
                Console.WriteLine("The data is incorrect!");
                return 7;
            }
        }

        public void addCarToParking()
        {
            Console.WriteLine("Select type of the car:");
            Console.WriteLine("1 - Passenger");
            Console.WriteLine("2 - Truck");
            Console.WriteLine("3 - Bus");
            Console.WriteLine("4 - Motorcycle");

            try
            {
                int carType = carType = Convert.ToInt32(Console.ReadLine());
                if (carType < 1 || carType > 4)
                {
                    throw new Exception();
                }

                Console.WriteLine("Please, enter the balance of the car");

                double carBalance = Convert.ToDouble(Console.ReadLine());
                if (carBalance <= 0)
                {
                    throw new Exception();
                }

                Car car = new Car((CarType)carType, carBalance, carIdentificator);

                carIdentificator++;

                parking.addCar(car);

                Console.WriteLine("The car with Id " + car.Id + " was successfully added to the parking");
            }

            catch (Exception)
            {
                Console.WriteLine("Please, enter the valid data abount car");
            }

        }

        public void removeCarFromParking()
        {
            Console.WriteLine("Enter the Id of the car you want to remove from parking:");
            foreach(var car in parking.getAllCars())
            {
                Console.WriteLine(car.Id);
            }
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Car carToRemove = parking.getAllCars().Find(x => x.Id == id);
                if (carToRemove != null)
                {
                    if (carToRemove.Balance <= 0)
                    {
                        addFundsMessage(carToRemove);
                    }
                }
                else
                {
                    throw new Exception();
                }
            }catch (Exception)
            {
                Console.WriteLine("The car was not found");
            }
        }

        private void addFundsMessage(Car carToRemove)
        {
            Console.WriteLine("Insufficient funds. Please, pay the fee" + carToRemove.Balance);
            try
            {
                double newBalance = Convert.ToDouble(Console.ReadLine());
                if(newBalance >= Math.Abs(carToRemove.Balance))
                {
                    int id = carToRemove.Id;
                    parking.calculateFee(newBalance, carToRemove);
                    parking.removeCar(carToRemove);
                    Console.WriteLine("The car with Id " + id + " was successfully removed from parking");

                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                addFundsMessage(carToRemove);
            }
        }

        public void addBalanceForCar()
        {
            Console.WriteLine("Please, enter the Car Id");
            try
            {
                int carId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please, enter the new Balance for the car: ");
                double newBalance = Convert.ToDouble(Console.ReadLine());

                Car car = parking.getAllCars().Find(x => x.Id == carId);
                if (car != null && newBalance > 0)
                {
                    car.Balance = newBalance;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect data");
            }
        }

        public void showTotalParkingRevenue()
        {
            Console.WriteLine("The total parking revenue is " + parking.getParkingRevenue());
        }

        public void showAvailablePlaces()
        {
            Console.WriteLine("The number of available places is " + parking.availablePlaces());
        }

        public void showAllTransactionsForCurrentMinute()
        {
            Console.WriteLine("Transactions:");
            Console.WriteLine("{0} - {1} - {2}", "Date", "Car Id", "Funds");
            foreach (var transaction in parking.getAllTransactions())
            {
                Console.WriteLine("{0} - {1} - {2}", transaction.transactionDate, transaction.carId, transaction.funds);
            }

        }

        public void showTransactionLog()
        {
            List<SerializeTransactions> transactionlog = parking.DeserializeFromFile();
            Console.WriteLine("Transaction history: ");
            foreach (var record in transactionlog)
            {
                Console.WriteLine("Date: {0} --- Total funds: {1}", record.timeOfSerialization, record.fundsForPreviousMinute);
            }
        }
    }
}
