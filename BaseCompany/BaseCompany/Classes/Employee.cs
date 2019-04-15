using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BaseCompany.Classes
{
    class Employee : IEquatable<Employee>, INotifyPropertyChanged
    {
        string employeeName;
        public string Name
        {
            get { return this.employeeName; }
            set
            {
                this.employeeName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }

        public string Surname { get; set; }
        public uint Age { get; set; }
        public decimal Salary { get; set; }
        public uint DepartmentID { get; set; }

        /// <summary>
        /// Инициализация работника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="surname">Фамилия сотрудника</param>
        /// <param name="age">Возраст</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="department">Отдел</param>

        public Employee (string name, string surname, uint age, decimal salary, uint depid)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            DepartmentID = depid;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Сергей Камянецкий приказал убрать :)
        /// <summary>
        /// Возвращет имя и фамилию сотрудника
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    return $"{Name} {Surname}";
        //}

        /// <summary>
        /// Возвращает информацию о сотруднике
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return $"\nИмя: {Name}\nФамилия: {Surname}\nВозраст: {Age}\nЗарплата: {Salary}\n" +
                $"Отдел: {GetDepartmentName(MainWindow.database.GetDepartments() as ObservableCollection<Department>)}\n";
        }

        /// <summary>
        /// Сравнение сотрудников
        /// </summary>
        /// <param name="another"></param>
        /// <returns></returns>
        public bool Equals(Employee another)
        {
            if (this.Name == another.Name && this.Surname == another.Surname && this.Age == another.Age &&
                this.DepartmentID == another.DepartmentID && this.Salary == another.Salary)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Возвращает название отдела
        /// </summary>
        /// <param name="list">Список отделов</param>
        /// <returns></returns>
        internal string GetDepartmentName(ObservableCollection<Department> list)
        {
            var request = from e
                          in list
                          where e.DepartmentID == DepartmentID
                          select e;

            string result = (request.ElementAt(0)).Name;

            return result;
        }
    }
}
