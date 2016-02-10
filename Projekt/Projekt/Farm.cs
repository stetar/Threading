﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt
{
    class Farm:GameObject
    {
        private static Object FarmLock = new Object();
        static Semaphore UpgradeFarm = new Semaphore(0,2);

        public Farm(float speed, string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {
            
        }

        static void WhichFarm()

        {
            if (placeholder == true)
            {
                lock (FarmLock)
                {
                    Thread.Sleep(4000);
                    Worker.gold = 5;
                }
            }
            if (placeholder == false)
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
