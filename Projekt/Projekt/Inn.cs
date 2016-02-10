using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Inn : GameObject
    {
        public static Vector2D position;

        public Inn(string imagepath, Vector2D startpos, float scalefactor) : base(imagepath, startpos, scalefactor)
        {
            base.position = startpos;
        }
    }
}
