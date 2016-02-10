using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt
{
    class Inn : GameObject
    {
        public static Vector2D position;
        private static Semaphore innSpace = new Semaphore(5, 5);

        public Inn(string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {
            base.position = startpos;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Worker)
            {
                if (Worker.gold >= 5)
                {
                    innSpace.WaitOne();
                    GameWorld.totalGold += 5;
                    Worker.gold = 0;
                    Thread.Sleep(3000);
                    Worker.deathCount += 1;
                    innSpace.Release();
                }
            }
        }
    }
}
