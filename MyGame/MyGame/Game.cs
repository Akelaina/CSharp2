﻿/*
 * Коды ошибок Exception:
 * -1 - отрицательное значение
 *  0 - равно нулю
 *  1 - слишком большая скорость
 *  2 - выход за пределы формы
 * 
 */

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
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        private static Asteroid[] _asteroids;
        private static Bullet _bullet;
        private static Ship _ship;

        //Количество объектов
        const int numOfStars = 250;
        const int numOfComets = 13;
        const int numOfAsteroids = 17;

        //Скорости объектов
        const int asteroidSpeed = 10;
        const int speed = 6;

        //Размеры объектов
        const int ShipSize = 70;
        const int maxSize = 30;
        const int minSize = 10;
        const int maxAstSize = 70;
        const int minAstSize = 30;

        //Ограничения движения объектов
        const int speedLimit = 20;

        //Проверка на размеры задания формы экрана
        const int formSizeLimit = 1000;

        // Конструктор
        static Game()
        {
        }

        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        /// <summary>
        /// Метод подгрузки объектов в игре
        /// </summary>
        public static void Load()
        {
            Random rand = new Random();

            bool exceptionExists = false;
            try
            {
                _objs = new BaseObject[numOfStars + numOfComets];
                _ship = new Ship(new Point(10, 200), new Point(5, 5), new Size(ShipSize, ShipSize));
                _bullet = new Bullet(new Point(0, 100), new Point(5, 0), new Size(5, 1));
                _asteroids = new Asteroid[numOfAsteroids];

                exceptionExists = false;

                //описываем появление обьектов в форме
                for (int i = 0; i < _objs.Length - numOfStars; i++)
                {
                    int size = rand.Next(minSize / 2, maxSize / 2);
                    _objs[i] = new Comet(new Point(Convert.ToInt32(rand.NextDouble() * (double)Game.Width),
                        Convert.ToInt32(rand.NextDouble() * (double)Game.Height)),
                        new Point(rand.Next(-speed * 2, -1), 0),
                        new Size(size, size));
                }

                for (int i = _objs.Length - numOfStars; i < _objs.Length; i++)
                {
                    int size = rand.Next(minSize / 4, maxSize / 4);
                    _objs[i] = new Star(new Point(Convert.ToInt32(rand.NextDouble() * (double)Game.Width),
                        Convert.ToInt32(rand.NextDouble() * (double)Game.Height)),
                        new Point(rand.Next(-speed * 2, -1), 0),
                        new Size(size, size));
                }
               
                for (int i = 0; i < _asteroids.Length; i++)
                {
                    int size = rand.Next(minAstSize, maxAstSize);
                    int widthPosition = Convert.ToInt32(rand.NextDouble() * (double)(Screen.PrimaryScreen.Bounds.Width - size));
                    int heightPosition = Convert.ToInt32(rand.NextDouble() * (double)Screen.PrimaryScreen.Bounds.Height - size);
                    int speed1 = rand.Next(-asteroidSpeed, asteroidSpeed);
                    int speed2 = rand.Next(-asteroidSpeed, asteroidSpeed);

                    _asteroids[i] = new Asteroid(new Point(widthPosition, heightPosition),
                                    new Point(speed1, speed2), new Size(size, size));

                    ///Exception на размер астероидов
                    //if (size < 0)
                    //    throw new GameObjectException($"Размер объекта {typeof(Asteroid)} меньше нуля", -1);
                    //if (widthPosition < 0 || widthPosition > Game.Width || heightPosition < 0 || heightPosition > Game.Height)
                    //    throw new GameObjectException($"Объект {typeof(Asteroid)} появился за пределами экрана", 2);
                    //if (speed1 == 0 && speed2 == 0)
                    //    throw new GameObjectException($"Объект {typeof(Asteroid)} стоит на месте", 0);
                    //if (Math.Abs(speed1) > speedLimit || Math.Abs(speed2) > speedLimit)
                    //    throw new GameObjectException($"Объект {typeof(Asteroid)} двигается со слишком большой скоростью", 1);

                }
            }
            catch (GameObjectException except)
            {
                exceptionExists = true;
                MessageBox.Show(except.Message, "Уведомление");
            }
            finally
            {
                if (exceptionExists)
                    Load();
                
            }
        }

        /// <summary>
        /// Метод ообработки события ожидания таймера
        /// </summary>
        /// <param name="sender">Объект, который вызывают </param>
        /// <param name="e">Параметр события</param>
        static public Timer timer = new Timer { Interval = 70};

        /// <summary>
        /// Метод для вывода графики в форме
        /// </summary>
        /// <param name="form"> Форма игрового пространства </param>
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

            //Добавляем исключение при размерах формы свыше 1000 пикселей
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                
                //Exception на превышение размеров формы
                if (Width > formSizeLimit || Height > formSizeLimit)
                {
                    throw new ArgumentOutOfRangeException("Игровое окно будет развернуто на весь экран");
                }
            }
            catch (ArgumentOutOfRangeException except)
            {
                MessageBox.Show(except.Message, "Уведомление");
            }

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            // Добавили таймер
            timer.Start();
            timer.Tick += Timer_Tick;

            //Добавили обработчик событий на кнопки
            form.KeyDown += Form_KeyDown;

            //Добавили конец игры через делегат события Message
            Ship.MessageDie += Finish;
        }

        /// <summary>
        /// Метод, реализующий действия на нажатие клавиш
        /// </summary>
        /// <param name="sender"> Действие </param>
        /// <param name="e"> Обработчик действия </param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Метод отрисовки фона игрового пространства
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj?.Draw();

            foreach (Asteroid obj in _asteroids)
                obj?.Draw();
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);

            Buffer.Render();
        }

        /// <summary>
        /// Метод инициализации обновления экрана
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
                _bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }

            //foreach (Asteroid ast in _asteroids)
            //{
            //    ast?.Update();
            //    if (ast.Collision(_bullet))
            //    {
            //        System.Media.SystemSounds.Hand.Play();
            //        ast.ReCreate();
            //        _bullet.ReСreate();
            //    }
            //}

        }

        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("Конец игры!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.White, 200, 100);
            Buffer.Render();
        }

    }
}
