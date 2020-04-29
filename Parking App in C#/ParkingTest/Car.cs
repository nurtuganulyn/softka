using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTest
{
    public enum CarType
    {
        Passenger = 1,
        Truck,
        Bus,
        Motorcycle
    }

    public class Car
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public CarType Type { get; set; }

        public Car(CarType carType, double Balance, int Id)
        {
            this.Id = Id;
            this.Type = carType;
            this.Balance = Balance;
        }
    }
}
