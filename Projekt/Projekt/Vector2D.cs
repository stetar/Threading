using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Vector2D
    {
        private float x;
        private float y;

        public float X
        {
            get
            { return x; }

            set
            { x = value; }
        }

        public float Y
        {
            get
            { return y; }

            set
            { y = value; }
        }
        public Vector2D Subtract(Vector2D vec)
        {
            Vector2D newVec = new Vector2D(x, y);
            newVec.X = vec.X - this.x;
            newVec.Y = vec.Y - this.y;
            return newVec;
        }
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        private float Lenght()
        {
            return (float)Math.Sqrt((this.x * this.x) + (this.y * this.y));
        }
        public void Normalize()
        {
            float lenght = Lenght();

            this.x = this.x / lenght;
            this.y = this.y / lenght;
        }
    }
}
