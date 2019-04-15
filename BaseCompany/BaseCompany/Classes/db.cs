using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BaseCompany.Classes
{
    class db
    {
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        private ObservableCollection<Department> departments = new ObservableCollection<Department>();

        public event Action updateData;

        /// <summary>
        /// Общее количество работников
        /// </summary>
        public int emlCount { get; set; } = 18;

        /// <summary>
        /// Общее количество департаментов
        /// </summary>
        public int depCount { get; set; } = 14;

        Random rand = new Random();

        /// <summary>
        /// Конструктор базы данных
        /// </summary>
        public db()
        {
            CreateDep();
            CreateEmpl();
        }

        /// <summary>
        /// Создание списка департаметов
        /// </summary>
        public void CreateDep()
        {
            for (uint i = 0; i < depCount; i++)
                departments.Add(new Department("Новый_отдел_" + i));
        }

        /// <summary>
        /// Создание списка сотрудников
        /// </summary>
        public void CreateEmpl()
        {
            for (int i = 0; i < emlCount; i++)
            {
                employees.Add(new Employee(
                    "Имя_" + i, "Фамилия_" + i, (uint)rand.Next(16, 70),
                    rand.Next(20000, 100000),
                    departments[rand.Next(0, depCount)].DepartmentID)
                    );
            }
        }

        /// <summary>
        /// Возврат списка сотрудников
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetEmployees()
        {
            return employees;
        }

        /// <summary>
        /// Возврат списка департаментов
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetDepartments()
        {
            return departments;
        }

        /// <summary>
        /// Возврат информации об объекте
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <returns></returns>
        public string GetInfo(object sender)
        {
            if ((sender is ListBox) && (sender as ListBox).SelectedItem is Employee)
            {
                Employee lbi = ((sender as ListBox).SelectedItem as Employee);
                return lbi.GetInfo();
            }
            else if ((sender is ComboBox) && (sender as ComboBox).SelectedItem is Department)
            {
                Department cbi = ((sender as ComboBox).SelectedItem as Department);

                string result = cbi.GetEmployees(employees);

                return result;
            }
            else
                return "";

        }

        /// <summary>
        /// Добавляет отдел. Возвращает истину, если добавлен
        /// </summary>
        /// <param name="text">Название отдела</param>
        /// <returns></returns>
        internal bool addDep(string text)
        {
            Department newDep = new Department(text);
            if (!departments.Contains(newDep))
            {
                departments.Add(newDep);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Добавляет сотрудника. Возвращает True, если добавлен
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="dep">Название отдела</param>
        /// <returns></returns>
        internal bool addEmp(string name, string surname, string age, string salary, uint depid)
        {
            if (name.Any(char.IsDigit) || surname.Any(char.IsDigit))
                return false;
            uint intAge = 0;
            if (!uint.TryParse(age, out intAge))
                return false;
            decimal decSal = 0M;
            if (!decimal.TryParse(salary, out decSal))
                return false;

            Employee newEmp = new Employee(name, surname, intAge, decSal, depid);

            if (!employees.Contains(newEmp))
            {
                employees.Add(newEmp);
                updateData?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Изменяет данные об отделе. True если имена не совпадают
        /// </summary>
        /// <param name="newname">Новое название</param>
        /// <param name="depid">ID отдела</param>
        /// <returns></returns>
        internal bool editDep(string newname, uint depid)
        {
            Department editDep = new Department(newname);
            if (!departments.Contains(editDep))
            {
                for (int i = 0; i < departments.Count; i++)
                {
                    if (departments[i].DepartmentID == depid)
                    {
                        departments[i].Name = newname;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Изменяет данные сотрудника. Возвращает True если данные изменены 
        /// </summary>
        /// <param name="old">Старые данные о сотруднике</param>
        /// <param name="name">Новое имя</param>
        /// <param name="surname">Новая фамилия</param>
        /// <param name="age">Новый возраст</param>
        /// <param name="salary">Новая зарплата</param>
        /// <param name="dep">Новый отдел</param>
        /// <returns></returns>
        internal bool editEmp(Employee old, string name, string surname, string age, string salary, uint depid)
        {
            uint intAge = 0;
            if (!uint.TryParse(age, out intAge))
                return false;
            decimal decSal = 0;
            if (!decimal.TryParse(salary, out decSal))
                return false;
            if (!checkDep(depid))
                return false;

            Employee editmp = new Employee(name, surname, intAge, decSal, depid);

            if (!employees.Contains(editmp))
            {
                for (int i = 0; i < departments.Count; i++)
                {
                    if (employees[i].Equals(old))
                    {
                        employees.RemoveAt(i);
                        employees.Insert(i, editmp);
                    }
                }
                updateData?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Проверка на существующие отделы
        /// </summary>
        /// <param name="depid">ID отдела</param>
        /// <returns></returns>
        private bool checkDep(uint depid)
        {
            foreach (var depart in departments)
            {
                if (depart.DepartmentID == depid)
                    return true;
            }
            return false;
        }
    }
}
