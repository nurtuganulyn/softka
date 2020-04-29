using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTest
{
    public static class Settings
    {
        // To Do:
        //Свойство Timeout(каждые N-секунд списывает средства за парковочное место) - по умолчанию 3 секунды
        //Dictionary - словарь для хранения цен за парковку(например: для грузовых - 5, для легковых - 3, для автобусов - 2, для мотоциклов - 1)

        private static Dictionary<CarType, double> PriceList = new Dictionary<CarType, double>()
        {
            {CarType.Motorcycle, 1},
            {CarType.Bus, 2},
            {CarType.Passenger, 3},
            {CarType.Truck, 4}
        };

        public static Dictionary<CarType, double> getPriceList()
        {
            return PriceList;
        }

        public static void changePriceList(Dictionary<CarType, double> priceList)
        {
            PriceList = priceList;
        }

        public static int Timeout { get; set; } = 3000;

        public static int ParkingSpace { get; set; } = 20;

        public static double Fine { get; set; } = 2;


    }
}
