using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BaseCompany.Classes
{
    class Department : IEquatable<Department>, INotifyPropertyChanged
    {

        static uint ID = 0;

        string departmentName;

        public string Name
        {
            get { return this.departmentName; }
            set
            {
                this.departmentName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }
        public uint DepartmentID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Инициализирует отдел
        /// </summary>
        /// <param name="name">Название</param>
        public Department(string name)
        {
            Name = name;
            DepartmentID = ID++;
        }

        //Сергей Камянецкий приказал убрать :)
        /// <summary>
        /// Возвращает название отдела 
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    return $"{Name}";
        //}

        /// <summary>
        /// Возвращает информацию о всех сотрудниках в отделе
        /// </summary>
        /// <param name="name">Название отдела</param>
        /// <param name="list">Список всех сотрудников</param>
        /// <returns></returns>
        internal string GetEmployees(ObservableCollection<Employee> list)
        {
            var request = from e
                         in list
                          where e.DepartmentID == DepartmentID
                          select e;

            string result = String.Empty;

            foreach (Employee item in request)
            {
                result += $"\n{item.Name} {item.Surname}, возраст: {item.Age}, зарплата: {item.Salary}, отдел: {this.Name}\n";
            }

            return result;
        }

        /// <summary>
        /// Сравнение отделов
        /// </summary>
        /// <param name="another"></param>
        /// <returns></returns>
        public bool Equals(Department another)
        {
            if (this.Name == another.Name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
