using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompany
{
    class Employee
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }

        /// <summary>
        /// Инициализация работника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="surname">Фамилия сотрудника</param>
        /// <param name="age">Возраст</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="department">Отдел</param>

        public Employee (string name, string surname, uint age, decimal salary, string department)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            Department = department;
        }
        public override string ToString()
        {
            return $"{Name},\n{Surname},\n{Age},\n{Salary},\n{Department}";
        }

    }
}
