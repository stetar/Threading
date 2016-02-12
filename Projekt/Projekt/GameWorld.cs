using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    class GameWorld
    {
        private Graphics dc;
        private BufferedGraphics backBuffer;
        public static List<GameObject> objectList = new List<GameObject>();
        public static List<GameObject> removeList = new List<GameObject>();
        public static List<Worker> workerList = new List<Worker>();
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

        //Constructor. This also creates the thread, which is running the gameloop.
        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            WindowRectangle = displayRectangle;
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            SetupWorld();
            Thread t = new Thread(GameLoop);
            t.Start();
        }

        //spawns the initial objects of the game.
        public void SetupWorld()
        {
            Inn myInn = new Inn("Inn.jpg", new Vector2D(20, 200), 1f);
            Farm myFarm = new Farm("Farm.jpg", new Vector2D(400, 200), 1f);
            objectList.Add(myInn);
            objectList.Add(myFarm);
            Thread t = new Thread(CreateWorker);
            t.IsBackground = true;
            t.Start();
        }

        //This keeps itself going, since the thread created in the constructor runs the while(true) loop.
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
            foreach (GameObject go in objectList.ToList())
            {
                go.Update(fps);
            }

            foreach (Worker worker in workerList.ToList())
            {
                worker.Update(fps);
            }

            removeList.Clear();
        }

        private void Draw()
        {
            dc.Clear(Color.White);
            foreach (GameObject go in objectList.ToList())
            {
                go.Draw(dc);
            }

            foreach (Worker worker in workerList.ToList())
            {
                worker.Draw(dc);
            }

            Font f = new Font("IMPACT", 14);
#if DEBUG
            dc.DrawString(string.Format("FPS: {0}", currentFps), f, Brushes.Black, 0, 0);
#endif
            dc.DrawString(string.Format("Gold: {0}", totalGold), f, Brushes.Black, 0, 20);

            backBuffer.Render();
        }

        public void CreateWorker()
        {
            Random myRandom = new Random();
            workerList.Add(new Worker(.5f, "Worker.jpg", new Vector2D(200, myRandom.Next(200, 300)), .5f));
        }
    }
}
