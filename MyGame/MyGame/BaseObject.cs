using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Абстрактный класс, для описания базовых объектов.
    /// </summary>
    abstract class BaseObject : ICollision
    {
        public delegate void Message();

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

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
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Метод для обновления расположения объекта
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }


        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы 
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        // нашим объектом (а можно наоборот)

        /// <summary>Свойство, возвращающее истину, если объекты столкнулись</summary>
        /// <param name="o"> Взаимодействие с интерфейсом ICollision</param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Свойство передачи позиции и размера объекта
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

    }
}
