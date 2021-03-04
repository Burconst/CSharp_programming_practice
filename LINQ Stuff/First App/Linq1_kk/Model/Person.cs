using System;
using System.IO;
using System.ComponentModel;

namespace LINQ.Model
{
    [Serializable]
    public struct Person : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string patronymic;
        private DateTime birthDate;
        private DateTime deathDate;
        private string profession;
        private bool isDead;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Patronymic
        {
            get
            {
                return patronymic;
            }
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        public DateTime DeathDate
        {
            get
            {
                return deathDate;
            }
            set
            {
                deathDate = value;
                OnPropertyChanged("DeathDate");
            }
        }

        public string Profession
        {
            get
            {
                return profession;
            }
            set
            {
                profession = value;
                OnPropertyChanged("Profession");
            }
        }

        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
                OnPropertyChanged("IsDead");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", FirstName, LastName, Patronymic, BirthDate.ToString("dd/MM/yyyy"), IsDead ? "1" : "0", DeathDate.ToString("dd/MM/yyyy"), Profession);
        }

        //private Person(string firstName, string lastName, string patronymic, DateTime birthDate, DateTime deathDate, string profession, bool hasDeathDate)
        //{
        //    CheckFields(firstName,lastName,patronymic,birthDate,deathDate,profession,hasDeathDate);
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Patronymic = patronymic;
        //    BirthDate = birthDate;
        //    Profession = profession;
        //    DeathDate = deathDate;
        //    IsDead = hasDeathDate;
        //}

        //public Person(string firstName, string lastName, string middleName, DateTime birthDate, DateTime deathDate, string profession) :
        //             this(firstName, lastName, middleName, birthDate, deathDate, profession, true)
        //{ }

        //public Person(string firstName, string lastName, string middleName, DateTime birthDate, string profession) :
        //              this(firstName, lastName, middleName, birthDate, default(DateTime), profession, false)
        //{ }

        private static void CheckFields(string firstName, string lastName, string patronymic, DateTime birthDate, DateTime deathDate, string profession, bool hasDeathDate)
        {
            if (lastName == "")
            {
                throw new ArgumentException("Фамилия не может быть пустой!");
            }
            if (firstName == "")
            {
                throw new ArgumentException("Имя не может быть пустым!");
            }
            if (patronymic == "")
            {
                throw new ArgumentException("Отчество не может быть пустым!");
            }
            if (profession == "")
            {
                throw new ArgumentException("Профессия не может быть пустой!");
            }
            if (hasDeathDate && birthDate.CompareTo(deathDate) > 0)
            {
                throw new ArgumentException("Дата рождения не должна быть раньше даты смерти!");
            }
        }
        
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

    }
}
