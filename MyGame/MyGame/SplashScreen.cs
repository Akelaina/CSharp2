using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class SplashScreen
    {

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }

        public static int Height { get; set; }

        static SplashScreen()
        {
        }

        /// <summary>
        /// Метод инициализации объектов в форме
        /// </summary>
        public static void Load()
        { 
        }

        /// <summary>
        /// Метод для вывода графики в форме
        /// </summary>
        /// <param name="form"> Форма игрового меню </param>
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

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }

        /// <summary>
        /// Метод ообработки события ожидания таймера
        /// </summary>
        /// <param name="sender">Объект, который вызывают </param>
        /// <param name="e">Параметр события</param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        /// <summary>
        /// Метод отрисовки фона игрового меню
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Render();
        }

        /// <summary>
        /// Метод инициализации обновления экрана
        /// </summary>
        public static void Update()
        {
        }
    }
}

