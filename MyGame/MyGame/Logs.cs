using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MyGame
{
    /// <summary>Класс для ведения журнала действий программы </summary>
    class Logs
    {
        /// <summary>Метод записи в файл</summary>
        /// <param name="Msg">Текст сообщения</param>
        internal static void Log(string Msg)
        {
            using (var sw = new StreamWriter("data.log", true))
            {
                Debug.WriteLine(Msg);
                sw.WriteLine(Msg);
            }
        }
    }
}
