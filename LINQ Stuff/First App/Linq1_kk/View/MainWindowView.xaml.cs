using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Effects;

namespace System.Runtime.CompilerServices
{
    sealed class CallerMemberNameAttribute : Attribute { }
}

namespace LINQ.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        /// <summary>
        /// Событие для реализации интерфейса INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Эффект размытия при открытии диалоговых окон
        /// </summary>
        BlurEffect blur = new BlurEffect() { Radius = 10 };

        /// <summary>
        /// Обновление отображения привязанного свойства
        /// </summary>
        /// <param name="prop">Имя свойства</param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Количество всех персон
        /// </summary>
        public string PersonCount
        {
            get { return personList.Count.ToString(); }
            set { OnPropertyChanged("PersonCount"); }
        }

        /// <summary>
        /// Номер текущей персоны
        /// </summary>
        public int CurrPers
        {
            get { return currPers; }
            set
            {
                currPers = value;
                if (personList.Count>0)
                {
                    ShowPerson(personList[currPers]);
                }
                OnPropertyChanged("CurrPers");
            }
        }

        //Конструктор
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Вывод результатов в окно
        /// </summary>
        private void ShowResults()
        {
            tbResult.Text = "";
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
                            tbResult.Text += String.Format("({0}) {1} {2} {3} ({4})\n\n", person.BirthDate.ToString("dd/MM/yyyy"),  person.FirstName, person.LastName, person.Patronymic, person.IsDead ? person.DeathDate.ToString("dd/MM/yyyy") : "");
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
            }
        }

        /// <summary>
        /// Вывод текущей персоны
        /// </summary>
        private void ShowPerson(Person person)
        {
            tbFName.Text = person.FirstName;
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
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки главного меню "Файл/Открыть"
        /// </summary>
        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            ofd.Filter = "Списки людей|*.txt|Все файлы|*.*";
            mainGrid.Effect = blur;
            if (ofd.ShowDialog() == true)
            {
                mainGrid.Effect = default(Effect);
                string fileName = ofd.FileName;
                try
                {
                    using (StreamReader stream = new StreamReader(fileName))
                    {
                        personList = new List<Person>();
                        while (!stream.EndOfStream)
                        {
                            personList.Add(Person.GetFromStream(stream));
                            stream.ReadLine();
                        }
                    }
                    ShowPerson(personList[0]);
                    CurrPers = 0;
                    OnPropertyChanged("PersonCount");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            mainGrid.Effect = default(Effect);
            ShowResults();
        }

        /// <summary>
        /// Обработчик нажатия кнопки главного меню "Файл/Сохранить"
        /// </summary>
        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            sfd.Filter = "Списки людей|*.txt|Все файлы|*.*";
            mainGrid.Effect = blur;
            if (sfd.ShowDialog() == true)
            {
                mainGrid.Effect = default(Effect);
                string fname = sfd.FileName;
                try
                {
                    mainGrid.Effect = default(Effect);
                    using (StreamWriter stream = new StreamWriter(fname))
                    {
                        foreach (Person person in personList)
                        {
                            stream.Write(person.ToString()+"\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            mainGrid.Effect = default(Effect);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            WAdd wa = new WAdd();
            wa.ShowDialog();
            if (wa.wasAdd && !personList.Contains(wa.newpers))
            {
                personList.Add(wa.newpers);
                CurrPers = personList.Count - 1;
                OnPropertyChanged("PersonCount");
            }
            ShowResults();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        private void BtRemove_Click(object sender, RoutedEventArgs e)
        {
            if (personList.Count>0)
            {
                personList.Remove(personList[CurrPers]);
                CurrPers = CurrPers>0? CurrPers-1:0;
                OnPropertyChanged("PersonCount");
            }
            ShowResults();
        }

        /// <summary>
        /// Обработчик нажатия на чекбокс с датой смерти
        /// </summary>
        private void CbIsDead_Click(object sender, RoutedEventArgs e)
        {
            dpDeathDay.IsEnabled = !dpDeathDay.IsEnabled;
        }

        /// <summary>
        /// Обработчик именения выбора сортировки данных
        /// </summary>
        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowResults();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Следующая персона"
        /// </summary>
        private void BtNext_Click(object sender, RoutedEventArgs e)
        {
            if (personList.Count > 0)
            {
                CurrPers = ((CurrPers+1) % personList.Count);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки главного меню "Справка/О программе"
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Effect = blur;
            mainGrid.Effect = default(Effect);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Править"
        /// </summary>
        private void BtCorr_Click(object sender, RoutedEventArgs e)
        {
            if (personList.Count==0)
            {
                return;
            }
            try
            {
                if (cbIsDead.IsChecked == true)
                {
                    personList[CurrPers] = new Person(tbFName.Text, tbLName.Text, tbPatr.Text, dpBirthDay.SelectedDate.Value, dpDeathDay.SelectedDate.Value, tbProff.Text);
                }
                else
                {
                    personList[CurrPers] = new Person(tbFName.Text, tbLName.Text, tbPatr.Text, dpBirthDay.SelectedDate.Value, tbProff.Text);
                }
                ShowResults();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    MessageBox.Show("Ошибка! " + ex.Message);
                }
                else
                {
                    MessageBox.Show("Ошибка! Не введена дата смерти или рождения.");
                }
                return;
            }
        }

    }
}
