using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Класс, для описания астероидов на карте.
    /// </summary>
    class BaseObject
    {
        
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

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

        protected Random random = new Random();

        /// <summary>Инициализирует объект астероидов</summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
            image = AsteroidList[random.Next(0, AsteroidList.Count)];
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод для обновления расположения объекта
        /// </summary>
        public virtual void Update()
        {

            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
