using System;

namespace LINQ.Model
{
    public class PersonBuilder
    {
        private Person _person = new Person();

        public PersonBuilder FirstName(string firstName)
        {
            _person.FirstName = firstName;
            return this;
        }

        public PersonBuilder LastName(string lastName)
        {
            _person.LastName = lastName;
            return this;
        }

        public PersonBuilder Patronymic(string patronymic)
        {
            _person.Patronymic = patronymic;
            return this;
        }

        public PersonBuilder BirthDate(DateTime birthDate)
        {
            _person.BirthDate = birthDate;
            return this;
        }

        public PersonBuilder DeathDate(DateTime deathDate)
        {
            _person.DeathDate = deathDate;
            return this;
        }

        public PersonBuilder Profession(string profession)
        {
            _person.Profession = profession;
            return this;
        }

        public PersonBuilder IsDead(bool isDead)
        {
            _person.IsDead = isDead;
            return this;
        }

        public Person Build()
        {
            return _person;
        }
    }
}
