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

        public static void Load()
        {
            Random rnd = new Random();

            _objs = new BaseObject[60];

            for (int i = 0; i < 50; i++)
                _objs[i] = new Star(new Point(rnd.Next(1, 800), i * rnd.Next(1, 20)), new Point(rnd.Next(1, 10) - i, rnd.Next(1, 10) - i), new Size(3,3));

            for (int i = 50; i < _objs.Length; i++)
                _objs[i] = new Comet(new Point(rnd.Next(1, 800), i * rnd.Next(1, 20)), new Point(rnd.Next(1, 20) - i, rnd.Next(1, 20) - i), new Size(7, 1));


            /*
            for (int i = _objs.Length / 2; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(rnd_x, i * rnd_y), new Point(-i, 10), new Size(5, 5));

            for (int i = 0; i < _objs.Length / 2; i++)
                _objs[i] = new BaseObject(new Point(rnd_x, i * 20), new Point(-i, -i), new Size(10, 10));
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
            Timer timer = new Timer { Interval = 1000 };
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
