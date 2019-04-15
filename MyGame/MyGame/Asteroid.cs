﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Asteroid : BaseObject, ICloneable
    {
        //методы для создания логов в программе
        public static event Action<string> AsteroidCreation;
        public static event Action<string> AsteroidRecreation;

        Random rand = new Random();
        /// <summary>
        /// Список картинок для добавления астеоридов
        /// </summary>
        List<Bitmap> AsteroidList = new List<Bitmap>() {
            new Bitmap("..\\..\\img/asteroid.png"), new Bitmap("..\\..\\img/asteroid1.png"),
            new Bitmap("..\\..\\img/asteroid2.png"), new Bitmap("..\\..\\img/asteroid3.png"),
            new Bitmap("..\\..\\img/asteroid4.png"), new Bitmap("..\\..\\img/asteroid5.png"),
            new Bitmap("..\\..\\img/asteroid6.png"), new Bitmap("..\\..\\img/asteroid7.png"),
            new Bitmap("..\\..\\img/asteroid8.png"), new Bitmap("..\\..\\img/asteroid9.png"),
            new Bitmap("..\\..\\img/asteroid10.png"), new Bitmap("..\\..\\img/asteroid11.png") };

        private Bitmap image;

        public int Power { get; set; } = 3;

        /// <summary>Инициализирует объект Asteroid на основании базового класса BaseObject</summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 10;
            image = AsteroidList[random.Next(0, AsteroidList.Count)];

            switch (rand.Next(0,2))
            {
                case 0:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 1:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 2:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            AsteroidCreation?.Invoke($"{DateTime.Now}: Cоздан астероид в позиции ({Pos.X}, {Pos.Y}), с размерами {Size.Width}");
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        public override void Update()
        {

            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Screen.PrimaryScreen.Bounds.Width - Size.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Screen.PrimaryScreen.Bounds.Height - Size.Height) Dir.Y = -Dir.Y;

        }

        /// <summary>
        /// Прересоздаёт объект при столкновении
        /// </summary>
        public void ReCreate()
        {
            Pos.X = rand.Next(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Width - Size.Width);
            Pos.Y = Convert.ToInt32(rand.NextDouble() * ((double)Screen.PrimaryScreen.Bounds.Height) - Size.Height);
            AsteroidRecreation?.Invoke($"{DateTime.Now}: Астероид был уничтожен и создан в коорданитах ({Pos.X}, {Pos.Y})");
        }

        public object Clone()
        {
            // Создаем копию нашего робота
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y),
                new Size(Size.Width, Size.Height))

            // Копируем новому астероиду Power старого астероида
            { Power = Power };
            return asteroid;
        }
       
    }
}