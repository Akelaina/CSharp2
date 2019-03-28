using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

// Создаем шаблон приложения, где подключаем модули
namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form menu = new Form();
            menu.Width = 800;
            menu.Height = 600;
            SplashScreen.Init(menu);
            menu.Show();
            SplashScreen.Draw();

            Button startBtn = new Button
            {
                Text = "Играть",
                Font = new Font("Times New Roman", 12),
                Location = new Point(menu.Width / 2 - 30, menu.Height / 3)
            };

            startBtn.Click += startBtnClick;
            menu.Controls.Add(startBtn);

            Button recordsBtn = new Button
            {
                Text = "Рекорды",
                Font = new Font("Times New Roman", 12),
                Location = new Point(menu.Width / 2 - 30, menu.Height - menu.Height / 2)

            };
            recordsBtn.Click += recordsBtnClick;
            menu.Controls.Add(recordsBtn);

            Button exitBtn = new Button
            {
                Text = "Выход",
                Font = new Font("Times New Roman", 12),
                Location = new Point(menu.Width / 2 - 30, menu.Height - menu.Height / 3)

            };
            exitBtn.Click += exitBtnClick;
            menu.Controls.Add(exitBtn);

            Label autor = new Label
            {
                Text = " Евгения и Аркадий Брюховецкие 2018 © ",
                ForeColor = Color.White,
                BackColor = Color.Black,
                AutoSize = true,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                Location = new Point(menu.Width/2, menu.Height - menu.Height / 8)

            };

            Label version = new Label
            {
                Text = " v.1.1.0 ",
                ForeColor = Color.White,
                BackColor = Color.Black,
                AutoSize = true,
                Font = new Font("Times New Roman", 8, FontStyle.Italic),
                Location = new Point(menu.Width / 25, menu.Height - menu.Height / 8)

            };

            menu.Controls.Add(autor);
            menu.Controls.Add(version);

            Application.Run(menu);

            void exitBtnClick(object sender, EventArgs e)
            {
                menu.Close();
            }

            void recordsBtnClick(object sender, EventArgs e)
            {
            }

            void startBtnClick(object sender, EventArgs e)
            {
                Form game = new Form();
                game.Width = 800;
                game.Height = 600;
                Game.Init(game);
                game.FormClosed += Game_FormClosed;
                Game.Load();
                game.Show();
                Game.Draw();
            }
        }

        private static void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Game.timer.Stop();
        }
    }

}