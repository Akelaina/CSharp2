using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompany
{
    class Department
    {
        /// <summary>
        /// Принимаем значения названия департамента
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Инициализируем департамент
        /// </summary>
        /// <param name="name">Название</param>
        public Department (string name)
        {
            Name = name;
        }
        
        /// <summary>
        /// Метод вывода департамента на экран
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}";

        }
    }
}
