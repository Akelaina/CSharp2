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

        public static event Action<string> BulletDestroyed;
        public static event Action<string> BulletOutOfScreen;

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
            Pos.X = Pos.X + Dir.X;
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

        /// <summary>Метод возвращает true, если снаряд вышел за пределы экрана</summary>
        /// <returns></returns>
        public bool OutOfScreen()
        {
            if (Pos.X > Game.Width)
            {
                BulletOutOfScreen?.Invoke($"{DateTime.Now}: Снаряд вышел за пределы экрана");
                return true;
            }
            else
                return false;
        }

        /// <summary>Метод записи уничтожения снаряда</summary>
        internal void Destroyed()
        {
            BulletDestroyed?.Invoke($"{DateTime.Now}: Снаряд уничтожен");
        }
    }

}