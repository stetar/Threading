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
        public static bool upgraded = false;
        private static Object FarmLock = new Object();
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
                if (!upgraded)
                {
                    lock (FarmLock)
                    {
                        Thread.Sleep(1000);
                        gold = 5;
                    }
                }
                //The farm can be upgraded to hold two workers at once.
                if (upgraded)
                {
                    UpgradeFarm.WaitOne();
                    Thread.Sleep(5000);
                    gold = 5;
                    UpgradeFarm.Release();
                }
            }
            if (other is Inn)
            {
                if (gold >= 5)
                {
                    innSpace.WaitOne();
                    GameWorld.totalGold += 5;
                    gold = 0;
                    deathCount++;
                    Thread.Sleep(1000);
                    innSpace.Release();
                }
            }
        }
    }
}
