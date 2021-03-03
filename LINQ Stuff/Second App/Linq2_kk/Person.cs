//Linq2_kk Калюжный К.А, июнь 2019 г.

//Демонстрация создания списка персон, каждая из которых задана структурой.
//Использованы примеры выборки с помощью LINQ-запросов.
//Использованы регулярные выражения для проверки корректности введённых данных

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Linq2_kk
{
    /// <summary>
    /// Класс людей
    /// </summary>
    [Serializable]
    public struct Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; private set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; private set; }
        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateTime DeathDate { get; private set; }
        /// <summary>
        /// Профессия
        /// </summary>
        public string Profession { get; private set; }
        /// <summary>
        /// Мёртв
        /// </summary>
        public bool IsDead { get; private set; }

        //Конструктор
        private Person(string firstName, string lastName, string patronymic, DateTime birthDate, DateTime deathDate, string profession, bool hasDeathDate)
        {
            CheckFields(firstName, lastName, patronymic, birthDate, deathDate, profession, hasDeathDate);
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            BirthDate = birthDate;
            Profession = profession;
            DeathDate = deathDate;
            IsDead = hasDeathDate;
        }

        public Person(string firstName, string lastName, string middleName, DateTime birthDate, DateTime deathDate, string profession) :
                     this(firstName, lastName, middleName, birthDate, deathDate, profession, true)
        { }

        public Person(string firstName, string lastName, string middleName, DateTime birthDate, string profession) :
                      this(firstName, lastName, middleName, birthDate, default(DateTime), profession, false)
        { }

        /// <summary>
        /// Проверка правильности введённых данных
        /// </summary>
        private static void CheckFields(string firstName, string lastName, string patronymic, DateTime birthDate, DateTime deathDate, string profession, bool hasDeathDate)
        {
            if (!Regex.Match(lastName, "^([А-ЯЁ][а-яё]+)(-[А-ЯЁ][а-яё]+)?$").Success)
            {
                throw new ArgumentException("Неверная запись фамилии!");
            }
            if (!Regex.Match(firstName, "^[А-ЯЁ][а-яё]+$").Success)
            {
                throw new ArgumentException("Неверная запись имени!");
            }
            if (!Regex.Match(patronymic, "^[А-ЯЁ][а-яё]+$").Success)
            {
                throw new ArgumentException("Неверная запись отчества!");
            }
            if (!Regex.Match(profession, "^([а-яё]+(-[а-яё_]+)*)(,[а-яё]+(-[а-яё_]+)*)*$").Success)
            {
                throw new ArgumentException("Неверная запись профессии!");
            }
            if (hasDeathDate && birthDate.CompareTo(deathDate) > 0)
            {
                throw new ArgumentException("Дата рождения не должна быть раньше даты смерти!");
            }
        }

        /// <summary>
        /// Получение объекта класса Person из потока данных
        /// </summary>
        public static Person GetFromStream(StreamReader stream)
        {
            var firstName = stream.ReadLine();
            var lastName = stream.ReadLine();
            var patronymic = stream.ReadLine();
            string sdate = stream.ReadLine();
			DateTime tmp = default(DateTime);
            DateTime birthDate;
            if (DateTime.TryParseExact(sdate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                birthDate = DateTime.ParseExact(sdate, "dd/MM/yyyy", null);
            }
            else if (DateTime.TryParseExact(sdate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                birthDate = DateTime.ParseExact(sdate, "dd.MM.yyyy", null);
            }
            else
            {
                throw new ArgumentException("Неверная запись даты рождения");
            }
            var isDead = stream.ReadLine() == "1";
            sdate = stream.ReadLine();
            DateTime deathDate;
            if (DateTime.TryParseExact(sdate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                deathDate = DateTime.ParseExact(sdate, "dd/MM/yyyy", null);
            }
            else if (DateTime.TryParseExact(sdate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                deathDate = DateTime.ParseExact(sdate, "dd.MM.yyyy", null);
            }
            else
            {
                throw new ArgumentException("Неверная запись даты смерти");
            }
            var profession = stream.ReadLine();
            Person person = new Person()
            {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                BirthDate = birthDate,
                IsDead = isDead,
                DeathDate = deathDate,
                Profession = profession
            };
            return person;
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", FirstName, LastName, Patronymic, BirthDate.ToString("dd/MM/yyyy"), IsDead ? "1" : "0", DeathDate.ToString("dd/MM/yyyy"), Profession);
        }

    }
}
