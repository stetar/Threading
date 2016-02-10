using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Farm : GameObject
    {
        public static Vector2D position;

        public Farm(string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {
            base.position = startpos;
        }
    }
}
