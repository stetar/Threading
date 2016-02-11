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
        public static Vector2D position;
        private static bool upgraded = false;
        private static Object FarmLock = new Object();
        private static Semaphore UpgradeFarm = new Semaphore(2, 2);


        public Farm(string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {
            base.position = startpos;
        }

        //This is where the worker actually works. He goes into the farm, does some work and comes out with gold.
        public override void OnCollision(GameObject other)
        {
            if (other is Worker)
            {
                if (!upgraded)
                {
                    lock (FarmLock)
                    {
                        Thread.Sleep(4000);
                        Worker.gold = 5;
                    }
                }
                //The farm can be upgraded to hold two workers at once.
                if (upgraded)
                {
                    UpgradeFarm.WaitOne();
                    Thread.Sleep(4000);
                    Worker.gold = 5;
                    UpgradeFarm.Release();
                }

            }
        }
    }
}
