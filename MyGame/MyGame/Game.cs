using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    static class Game
    {
        public static BaseObject[] _objs;

        const int numOfStars = 100;
        const int numOfComets = 10;
        const int numOfAsteroids = 5;
        const int maxSize = 30;
        const int minSize = 10;
        const int speed = 6;

        public static void Load()
        {
            Random rand = new Random();
            _objs = new BaseObject[numOfStars + numOfAsteroids + numOfComets];

            for (int i = 0; i < _objs.Length - numOfStars - numOfAsteroids; i++)
            {
                int size = rand.Next(minSize / 2, maxSize / 2);
                _objs[i] = new Comet(new Point(Convert.ToInt32(rand.NextDouble() * (double)Game.Width),
                    Convert.ToInt32(rand.NextDouble() * (double)Game.Height)),
                    new Point(rand.Next(-speed * 2, -1), 0),
                    new Size(size, size));
            }

            for (int i = _objs.Length - numOfStars - numOfAsteroids; i < _objs.Length - numOfAsteroids; i++)
            {
                int size = rand.Next(minSize / 4, maxSize / 4);
                _objs[i] = new Star(new Point(Convert.ToInt32(rand.NextDouble() * (double)Game.Width),
                    Convert.ToInt32(rand.NextDouble() * (double)Game.Height)), 
                    new Point(rand.Next(-speed * 2, -1), 0), 
                    new Size(size, size));
            }

            for (int i = _objs.Length - numOfAsteroids; i < _objs.Length; i++)
            {
                int size = rand.Next(minSize*2, maxSize*2);
                _objs[i] = new BaseObject(new Point(Convert.ToInt32(rand.NextDouble() * (double)(Game.Width - size)),
                    Convert.ToInt32(rand.NextDouble() * (double)(Game.Height - size))), 
                    new Point(rand.Next(-speed, speed), rand.Next(-speed, speed)), 
                    new Size(size, size));
            }

            /*
            for (int i = _objs.Length / 2; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(rnd_x, i * rnd_y), 
                new Point(-i, 10), 
                new Size(5, 5));

            for (int i = 0; i < _objs.Length / 2; i++)
                _objs[i] = new BaseObject(new Point(rnd_x, i * 20), 
                new Point(-i, -i), 
                new Size(10, 10));
            */
        }

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public Timer timer = new Timer { Interval = 70 };

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;

            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой

            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            // Добавили таймер
            
            timer.Start();
            timer.Tick += Timer_Tick;

        }

        public static void Draw()
        {

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();

            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
    

    }
}
