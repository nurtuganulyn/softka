using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTest
{
    public class Transaction
    {
        public DateTime transactionDate { get; set; }
        public int carId { get; set; }
        public double funds { get; set; }
    }

    [Serializable]
    public class SerializeTransactions
    {
        public DateTime timeOfSerialization { get; set; }
        public double fundsForPreviousMinute { get; set; }
    }
}
