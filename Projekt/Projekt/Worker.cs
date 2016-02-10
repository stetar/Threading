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
        public static int gold;
        private int deathCount = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagepath"></param>
        /// <param name="startPos"></param>
        /// <param name="scalefactor"></param>
        public Worker(float speed, string imagepath, Vector2D startPos, float scalefactor) : base(imagepath, startPos, scalefactor)
        {
            this.speed = speed;
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
        }
        public override void OnCollision(GameObject other)
        {
            if (other is Inn)
            {
                if (gold >= 5)
                {
                    Thread.Sleep(1000);
                    gold == 0;
                    deathCount += 1;
                }

            }

            if (other is Farm)
            {

            }
        }
    }
}
