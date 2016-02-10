using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt
{
    class Farm : GameObject
    {
        private static bool upgraded = false;
        private static Object FarmLock = new Object();
        static Semaphore UpgradeFarm = new Semaphore(0, 2);

        public Farm(string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {

        }

        static void WhichFarm()
        {
            if (!upgraded)
            {
                lock (FarmLock)
                {
                    Thread.Sleep(4000);
                    Worker.gold = 5;
                }
            }
            if (upgraded)
            {
                UpgradeFarm.Release(0);

                Thread.Sleep(4000);
                Worker.gold = 5;
                UpgradeFarm.Release();
            }
        }

        static void Enter(object Worker)
        {

        }
    }
}
