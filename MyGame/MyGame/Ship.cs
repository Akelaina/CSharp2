using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    class Ship : BaseObject
    {
        Bitmap image = new Bitmap("..\\..\\img/ship.png");

        public static event Message MessageDie;
        public static event Action<string> ShipDie;
        public static event Action<string> ShipEnergyLow;
        public static event Action<string> ShipEnergyHigh;

        private static int maxEnergy = 100;

        private int _energy = maxEnergy;

        public int Energy => _energy;

        /// <summary> Метод получения урона кораблю </summary>
        /// <param name="n">Величина урона</param>
        public void EnergyLow(int n)
        {
            _energy -= n;
            ShipEnergyLow?.Invoke($"{DateTime.Now}: Корабль получил повреждения");
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
            ShipDie?.Invoke($"{DateTime.Now}: Корабль был уничтожен");
        }

    }

}
