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
        public Vector2D position;
        private static Semaphore innSpace = new Semaphore(5, 5);

        public Inn(string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {
            base.position = startpos;
        }

        //This collision will make the workers wait their turn, enter the inn, spend their gold, giving it to the player, enjoying their drink and then get back to work.
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
                    Worker.deathCount++;
                    innSpace.Release();
                }
            }
        }
    }
}
