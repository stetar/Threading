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
        public static int gold = 0;
        public static int deathCount = 0;
        private Inn inn;
        private Farm farm;
        
        //The constructor. This also creates the string, which make the worker travel between the inn and the farm.
        public Worker(float speed, string imagepath, Vector2D startPos, float scalefactor, Inn Inn, Farm Farm) : base(imagepath, startPos, scalefactor)
        {
            this.speed = speed;
            Thread t = new Thread(() => Update(GameWorld.currentFps));
            t.IsBackground = true;
            t.Start();
            this.inn = Inn;
            this.farm = Farm;
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
        }
    }
}
