using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using LINQ.Model;

namespace LINQ.ViewModel
{
    public class MainWindowViewModel
    {
        public List<Person> Persons { get; set; }
        public Person CurrentPerson { get; set; }

        public MainWindowViewModel() 
        {
            Persons = new List<Person>();
        }

        public void Add(Person person) 
        {
            if (!Persons.Contains(person))
            {
                Persons.Add(person);
            }
            else 
            {
                MessageBox.Show("The person is already exist");
            }
        }

        public void Remove(Person person)
        {
            if (!Persons.Remove(person))
            {
                MessageBox.Show($"Can\'t remove {person.FirstName} {person.LastName}");
            }
        }

        public void OpenPersonsList(string filename)
        {
            try
            {
                using (StreamReader stream = new StreamReader(filename))
                {
                    Persons = new List<Person>();
                    while (!stream.EndOfStream)
                    {
                        Persons.Add(Person.GetFromStream(stream));
                        stream.ReadLine();
                    }
                }
/*                ShowPerson(personList[0]);
                CurrPers = 0;
                OnPropertyChanged("PersonCount");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void SavePersonsList(string filename)
        {
            try
            {
                using (StreamWriter stream = new StreamWriter(filename))
                {
                    foreach (Person person in Persons)
                    {
                        stream.Write(person.ToString() + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }


        /// <summary>
        /// Вывод результатов в окно
        /// </summary>
        private void ShowResults()
        {
/*            tbResult.Text = "";
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    {
                        var selectedItems = (
                            from t in personList
                            where t.IsDead
                            orderby t.LastName
                            select t);
                        foreach (Person person in selectedItems)
                        {
                            tbResult.Text += String.Format("{0} {1} {2} ({3}) ({4})\n\n", person.LastName, person.FirstName, person.Patronymic, person.BirthDate.ToString("dd/MM/yyyy"), person.IsDead ? person.DeathDate.ToString("dd/MM/yyyy") : "");
                        }
                        break;
                    }
                case 1:
                    {
                        var selectedItems = (
                            from t in personList
                            where !t.IsDead
                            orderby t.LastName
                            select t
                        ).ToArray<Person>();
                        foreach (Person person in selectedItems)
                        {
                            tbResult.Text += String.Format("{0} {1} {2} ({3}) ({4})\n\n", person.LastName, person.FirstName, person.Patronymic, person.BirthDate.ToString("dd/MM/yyyy"), person.IsDead ? person.DeathDate.ToString("dd/MM/yyyy") : "");
                        }
                        break;
                    }
                case 2:
                    {
                        var selectedItems = (
                            from t in personList
                            orderby t.BirthDate.DayOfYear
                            select t
                        ).ToArray<Person>();
                        foreach (Person person in selectedItems)
                        {
                            tbResult.Text += String.Format("({0}) {1} {2} {3} ({4})\n\n", person.BirthDate.ToString("dd/MM/yyyy"), person.FirstName, person.LastName, person.Patronymic, person.IsDead ? person.DeathDate.ToString("dd/MM/yyyy") : "");
                        }
                        break;
                    }
                case 3:
                    {
                        var groupped = from t in personList
                                       group t by t.Profession.Split(',')[0].ToLower();
                        tbResult.Text = "";
                        foreach (IGrouping<string, Person> group in groupped)
                        {
                            tbResult.Text += "\n" + group.Key + "\n";
                            foreach (Person person in group)
                            {
                                tbResult.Text += String.Format("{0} {1} {2} ({3}) ({4})\n",
                                person.FirstName, person.LastName, person.Patronymic,
                                person.BirthDate.ToString("dd/MM/yyyy"),
                                person.IsDead ? person.DeathDate.ToString("dd/MM/yyyy") : "");
                            }
                        }
                        break;
                    }
            }*/
        }

        /// <summary>
        /// Вывод текущей персоны
        /// </summary>
        private void ShowPerson(Person person)
        {
/*            tbFName.Text = person.FirstName;
            tbLName.Text = person.LastName;
            tbPatr.Text = person.Patronymic;
            tbProff.Text = person.Profession;
            dpBirthDay.SelectedDate = person.BirthDate;
            cbIsDead.IsChecked = person.IsDead;
            dpDeathDay.IsEnabled = person.IsDead;
            dpDeathDay.SelectedDate = default(DateTime);
            if (person.DeathDate != default(DateTime))
            {
                dpDeathDay.SelectedDate = person.DeathDate;
            }
            else
            {
                dpDeathDay.SelectedDate = default(DateTime);
            }*/
        }




    }
}
