using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt
{
    class GameWorld
    {
        private Graphics dc;
        private BufferedGraphics backBuffer;
        public static List<GameObject> objectList;
        public static List<GameObject> removeList;
        private static Rectangle displayRectangle;
        private DateTime endTime;
        public static float currentFps;
        public static int totalGold;


        public static Rectangle WindowRectangle
        {
            get
            {
                return displayRectangle;
            }
            set
            {
                displayRectangle = value;
            }
        }

        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            WindowRectangle = displayRectangle;
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            objectList = new List<GameObject>();
            removeList = new List<GameObject>();
            Thread t = new Thread(GameLoop);
            t.Start();
            SetupWorld();
        }

        public void SetupWorld()
        {
            Inn myInn = new Inn("Inn.jpg", new Vector2D(100, 100), 1.5f);
            objectList.Add(myInn);
        }

        public void GameLoop()
        {
            while (true)
            {
                DateTime startTime = DateTime.Now;
                TimeSpan deltaTime = startTime - endTime;
                int milliSeconds = deltaTime.Milliseconds > 0 ? deltaTime.Milliseconds : 1;
                currentFps = 1000f / milliSeconds;
                endTime = DateTime.Now;
                Update(currentFps);
                Draw();
            }
        }

        private void Update(float fps)
        {
            foreach (GameObject go in objectList.ToList()) //To list as you can't modify it in runtime elsewise.
            {
                go.Update(fps);
            }

            removeList.Clear();

        }

        private void Draw()
        {
            foreach (GameObject go in objectList)
            {
                go.Draw(dc);
            }

#if DEBUG
            Font f = new Font("IMPACT", 14);
            dc.DrawString(string.Format("FPS: {0}", currentFps), f, Brushes.Black, 0, 0);
#endif
            backBuffer.Render();
        }
    }
}
