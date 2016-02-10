using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt
{
    class Mine
    {
        private static Object MineLock = new Object();
        static Semaphore UpgradeMine = new Semaphore(0,2);

        static void WhichMine()

        {
            if (placeholder == true)
            {
                lock (MineLock)
                {
                    Thread.Sleep(4000);
                    Worker.gold = 5;
                }
            }
            if (placeholder == false)
            {
                UpgradeMine.Release(0);

                Thread.Sleep(4000);
                Worker.gold = 5;
                UpgradeMine.Release();
            }
        }

        static void Enter(object Worker)
        {
            
        }
    }
}
