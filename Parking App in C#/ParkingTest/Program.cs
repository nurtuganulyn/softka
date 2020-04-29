using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking parking = Parking.getInstance();
            Menu menu = new Menu(parking);

           while(true)
            {
                switch (menu.MainMenu())
                {
                    case 1:
                        {
                            menu.addCarToParking();
                            break;
                        }
                    case 2:
                        {
                            menu.removeCarFromParking();
                            break;
                        }
                    case 3:
                        {
                            menu.showAllTransactionsForCurrentMinute();
                            break;
                        }
                    case 4:
                        {
                            menu.showTotalParkingRevenue();
                            break;
                        }
                    case 5:
                        {
                            menu.showAvailablePlaces();
                            break;
                        }
                    case 6:
                        {
                            menu.showTransactionLog();
                            break;
                        }
                    case 7:
                        {
                            return;
                        }
                }
            }


        }

   
    }
}
