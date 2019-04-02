using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    class Bullet : BaseObject
    {
        Random rand = new Random();
        /// <summary>Инициализирует объект Bullet на основании базового класса BaseObject</summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 5;
            //Pos.Y = Convert.ToInt32(rand.NextDouble() * (double)Game.Height - Size.Height);
        }

        /// <summary>
        /// Прересоздаёт объект при столкновении
        /// </summary>
        public void ReСreate()
        {
            Pos.X = 0;
            Pos.Y = Convert.ToInt32(rand.NextDouble() * (double)Game.Height - Size.Height);
        }
    }

}