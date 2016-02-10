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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagepath"></param>
        /// <param name="startPos"></param>
        /// <param name="scalefactor"></param>
        public Worker(float speed, string imagepath, Vector2D startPos, float scalefactor) : base(imagepath, startPos, scalefactor)
        {
            this.speed = speed;
            Thread t = new Thread(() => Update(GameWorld.currentFps));
            t.IsBackground = true;
            t.Start();
        }
        public override void Update(float fps)
        {
            if (gold < 5)
            {
                Vector2D velocity = this.position.Subtract(Inn.position);
            }
            if (gold > 5)
            {
                Vector2D velocity = this.position.Subtract(Farm.position);
            }

            if (deathCount == 15)
            {
                GameWorld.removeList.Add(this);
            }
        }
    }
}
