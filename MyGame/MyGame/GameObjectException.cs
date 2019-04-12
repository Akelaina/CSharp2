using System;

namespace MyGame
{
    class GameObjectException : Exception
    {
        /// <summary>Код ошибки</summary>
        public int Error { get; set; }

        /// <summary>Создаёт объект GameObjectException</summary>
        /// <param name="Msg">Сообщение</param>
        /// <param name="Error">Код ошибки</param>
        public GameObjectException(string Msg, int Error) : base(Msg)
        {
            this.Error = Error;
        }

        /// <summary>Возращает сообщение об ошибке и её код</summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.Message}. Код ошибки: {this.Error}";
        }

    }
}