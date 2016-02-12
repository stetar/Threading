using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt
{
    class Worker : GameObject
    {
        float speed;
        private int gold = 0;
        private int deathCount = 0;
        public static bool farmUpgraded = false;
        public static bool innUpgraded = false;
        private static Object FarmLock = new Object();
        private static Mutex innMutex = new Mutex();
        private static Semaphore UpgradeFarm = new Semaphore(2, 2);
        private static Semaphore innSpace = new Semaphore(5, 5);


        //The constructor. This also creates the string, which make the worker travel between the inn and the farm.
        public Worker(float speed, string imagepath, Vector2D startPos, float scalefactor) : base(imagepath, startPos, scalefactor)
        {
            this.speed = speed;
            Thread t = new Thread(() => Update(GameWorld.currentFps));
            t.IsBackground = true;
            t.Start();
        }
        public override void Update(float fps)
        {
            if (gold > 0)
            {
                position.X -= speed;
            }
            else
            {
                position.X += speed;
            }

            //The worker will die after 15 trips to the inn. Drinking is bad for you!
            if (deathCount >= 15)
            {
                GameWorld.removeList.Add(this);
            }

            base.Update(fps);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Farm)
            {
                if (!farmUpgraded)
                {
                    lock (FarmLock)
                    {
                        Thread.Sleep(1000);
                        gold = 5;
                    }
                }
                //The farm can be upgraded to hold two workers at once.
                if (farmUpgraded)
                {
                    UpgradeFarm.WaitOne();
                    Thread.Sleep(0);
                    gold = 5;
                    UpgradeFarm.Release();
                }
            }
            if (other is Inn)
            {
                if (!innUpgraded)
                {
                    if (gold > 0)
                    {
                        innMutex.WaitOne();
                        GameWorld.totalGold += gold;
                        gold = 0;
                        deathCount++;
                        Thread.Sleep(1000);
                        innMutex.ReleaseMutex();
                    }
                }
                //The Inn is upgradeable aswell, though this is a bigger upgrade then the Farm, hence why it is more expensive.
                else
                {
                    if (gold > 0)
                    {
                        innSpace.WaitOne();
                        GameWorld.totalGold += gold;
                        gold = 0;
                        deathCount++;
                        Thread.Sleep(0);
                        innSpace.Release();
                    }
                }

            }
        }
    }
}
