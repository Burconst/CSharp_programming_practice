// LinqToSql-1_kk - Калюжный К.А. 241 гр. июнь 2019 г.
// Создание и работа с базой данных, созданной на основе списка
// созданного в приложении Linq2_kk

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Linq.Mapping;

namespace LinqToSql_1_kk
{
    /// <summary>
    /// Класс людей
    /// </summary>
    [Table(Name = "Person")]
    public class Person
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Person_ID { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Column(Name = "Имя")]
        public string FirstName { get; private set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Column(Name = "Фамилия")]
        public string LastName { get; private set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [Column(Name = "Отчество")]
        public string Patronymic { get; private set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column(Name = "Дата рождения")]
        public string birthDate {
            get;
            private set;
        }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; private set; }
        /// <summary>
        /// Дата смерти
        /// </summary>
        [Column(Name = "Дата смерти")]
        public string deathDate {
            get;
            private set; }
        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateTime DeathDate { get; private set; }
        /// <summary>
        /// Профессия
        /// </summary>
        [Column(Name = "Профессия")]
        public string Profession { get; private set; }
        /// <summary>
        /// Мёртв
        /// </summary>
        [Column]
        public bool IsDead { get; private set; }

        //Конструкторы
        public Person() { }

        private Person(string firstName, string lastName, string patronymic, DateTime birthdate, DateTime deathdate, string profession, bool hasDeathDate)
        {
            CheckFields(firstName, lastName, patronymic, birthdate, deathdate, profession, hasDeathDate);
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            BirthDate = birthdate;
            Profession = profession;
            DeathDate = deathdate;
            IsDead = hasDeathDate;
            birthDate = birthdate.ToShortDateString();
            deathDate = deathdate.ToShortDateString();
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
            DateTime birthdate;
            if (DateTime.TryParseExact(sdate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                birthdate = DateTime.ParseExact(sdate, "dd.MM.yyyy", null);
            }
            else if (DateTime.TryParseExact(sdate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                birthdate = DateTime.ParseExact(sdate, "dd/MM/yyyy", null);
            } else
            {
                throw new ArgumentException("Неверная запись даты рождения");
            }
            var isDead = stream.ReadLine() == "1";
            sdate = stream.ReadLine();
            DateTime deathdate;
            if (DateTime.TryParseExact(sdate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                deathdate = DateTime.ParseExact(sdate, "dd.MM.yyyy", null);
            }
            else if (DateTime.TryParseExact(sdate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
            {
                deathdate = DateTime.ParseExact(sdate, "dd/MM/yyyy", null);
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
                BirthDate = birthdate,
                IsDead = isDead,
                DeathDate = deathdate,
                Profession = profession,
                birthDate = birthdate.ToShortDateString(),
                deathDate = deathdate.ToShortDateString()
            };
            return person;
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", FirstName, LastName, Patronymic, birthDate, IsDead ? "1" : "0", deathDate, Profession);
        }

    }
}
