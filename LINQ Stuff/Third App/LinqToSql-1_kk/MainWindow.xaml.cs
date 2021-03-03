// LinqToSql-1_kk - Калюжный К.А. 241 гр. июнь 2019 г.
// Создание и работа с базой данных, созданной на основе списка
// созданного в приложении Linq2_kk

using System;
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

namespace LinqToSql_1_kk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //Конструктор
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        //Поля
        /// <summary>
        /// Текущая база данных
        /// </summary>
        Context db;

        /// <summary>
        /// Эффект размытия при открытии диалоговых окон
        /// </summary>
        BlurEffect blur = new BlurEffect() { Radius = 10 };

        /// <summary>
        /// Индекс текущей персоны
        /// </summary>
        private int currId = 0;

        /// <summary>
        /// Событие для реализации интерфейса INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        //Свойства
        /// <summary>
        /// Количество всех персон
        /// </summary>
        public string PersonCount
        {
            get
            {
                if (db != null)
                {
                    return (from t in db.Persons select t).Count().ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { OnPropertyChanged("PersonCount"); }
        }

        /// <summary>
        /// Индекс текущей персоны
        /// </summary>
        public int CurrId
        {
            get { return currId; }
            set
            {
                try
                {
                    currId = value;
                    if (db != null && (from t in db.Persons select t).Count() > 0)
                    {
                        ShowPerson();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: "+ex.Message);
                    db = null;
                }
                OnPropertyChanged("CurrId");
            }
        }

        /// <summary>
        /// Обновление отображения привязанного свойства
        /// </summary>
        /// <param name="prop">Имя свойства</param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //Методы
        /// <summary>
        /// Вывод результатов в окно
        /// </summary>
        private void ShowResults()
        {
            tbResult.Text = "";
            if (db == null)
            {
                return;
            }
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    {
                        var selectedItems = (
                            from t in db.Persons
                            where t.IsDead
                            orderby t.LastName
                            select t
                        ).ToArray<Person>();
                        foreach (Person person in selectedItems)
                        {
                            tbResult.Text += String.Format("{0} {1} {2} ({3}) ({4})\n\n", person.LastName, person.FirstName, person.Patronymic, person.birthDate, person.IsDead ? person.deathDate : "");
                        }
                        break;
                    }
                case 1:
                    {
                        var selectedItems = (
                            from t in db.Persons
                            where !t.IsDead
                            orderby t.LastName
                            select t
                        ).ToArray<Person>();
                        foreach (Person person in selectedItems)
                        {
                            tbResult.Text += String.Format("{0} {1} {2} ({3}) ({4})\n\n", person.LastName, person.FirstName, person.Patronymic, person.birthDate, person.IsDead ? person.deathDate : "");
                        }
                        break;
                    }
                case 2:
                    {
                        var selectedItems = (
                            from t in db.Persons
                            orderby t.birthDate.Substring(0, t.birthDate.Length - t.birthDate.LastIndexOf("\\") - 1)
                            select t
                        ).ToArray<Person>();
                        foreach (Person person in selectedItems)
                        {
                            tbResult.Text += String.Format("({0}) {1} {2} {3} ({4})\n\n", person.birthDate, person.FirstName, person.LastName, person.Patronymic, person.IsDead ? person.deathDate : "");
                        }
                        break;
                    }
                case 3:
                    {
                        var groupped = from t in db.Persons.AsEnumerable()
                                       group t by t.Profession.Split('-')[0].ToLower();
                        tbResult.Text = "";
                        foreach (IGrouping<string, Person> group in groupped)
                        {
                            tbResult.Text += "\n" + group.Key + "\n";
                            foreach (Person person in group)
                            {
                                tbResult.Text += String.Format("{0} {1} {2} ({3}) ({4})\n",
                                person.FirstName, person.LastName, person.Patronymic,
                                person.birthDate,
                                person.IsDead ? person.deathDate : "");
                            }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Вывод текущей персоны
        /// </summary>
        private void ShowPerson()
        {
            try
            {
                var person = (
                from t in db.Persons
                where t.Person_ID == CurrId
                select t
                ).ToArray();
                tbFName.Text = person[0].FirstName;
                tbLName.Text = person[0].LastName;
                tbPatr.Text = person[0].Patronymic;
                tbProff.Text = person[0].Profession;
                string sdate = person[0].birthDate;
				DateTime tmp = default(DateTime);
                DateTime birthDate;
                if (DateTime.TryParseExact(sdate, "M/d/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
                {
                    birthDate = DateTime.ParseExact(sdate, "M/d/yyyy", null);
                }
                else if (DateTime.TryParseExact(sdate, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
                {
                    birthDate = DateTime.ParseExact(sdate, "d/M/yyyy", null);
                }
                else
                {
                    throw new ArgumentException("Неверная запись даты рождения");
                }
                dpBirthDay.SelectedDate = birthDate;
                sdate = person[0].deathDate;
                DateTime deathDate = default(DateTime);
                if (DateTime.TryParseExact(sdate, "M/d/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
                {
                    deathDate = DateTime.ParseExact(sdate, "M/d/yyyy", null);
                }
                else if (DateTime.TryParseExact(sdate, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out tmp))
                {
                    deathDate = DateTime.ParseExact(sdate, "d/M/yyyy", null);
                }
                cbIsDead.IsChecked = person[0].IsDead;
                dpDeathDay.IsEnabled = person[0].IsDead;
                dpDeathDay.SelectedDate = deathDate;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        #region Обработчики
        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            WAddPers addpers = new WAddPers();
            addpers.ShowDialog();
            if (db != null && addpers.wasAdd && !db.Persons.Contains(addpers.newpers))
            {
                db.Persons.InsertOnSubmit(addpers.newpers);
                db.SubmitChanges();
                CurrId = Convert.ToInt32(PersonCount);
                OnPropertyChanged("PersonCount");
                ShowResults();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        private void BtRemove_Click(object sender, RoutedEventArgs e)
        {
            if (db != null && (from t in db.Persons select t).Count() > 0)
            {
                db.Persons.DeleteOnSubmit((from t in db.Persons
                                           where t.Person_ID == CurrId
                                           select t
                                           ).ToArray()[0]);
                db.SubmitChanges();
                CurrId = CurrId > 0 ? CurrId-- : 0;
                OnPropertyChanged("PersonCount");
                ShowResults();
            }
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
            if (db != null && (from t in db.Persons select t).Count() > 0)
            {
                CurrId = 1+((CurrId) % (from t in db.Persons select t).Count());
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки главного меню "Справка/О программе"
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Effect = blur;
            new WAbout().ShowDialog();
            mainGrid.Effect = default(Effect);
        }

        /// <summary>
        /// Обработчик нажатия кнопки главного меню "Базу данных.../Создать"
        /// </summary>
        private void MiCreateDB_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Effect = blur;
            new WCreateDB().ShowDialog();
            mainGrid.Effect = default(Effect);
        }

        /// <summary>
        /// Обработчик нажатия кнопки главного меню "Базу данных.../Открыть"
        /// </summary>
        private void MiOpenDB_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Effect = blur;
            WOpenDB wo = new WOpenDB();
            wo.ShowDialog();
            if (wo.wasOpaned)
            {
                db = wo.DB;
                tbDBName.Text = wo.DBFullName;
                CurrId = 1;
                OnPropertyChanged("PersonCount");
            }
            mainGrid.Effect = default(Effect);
            ShowResults();
        }

        /// <summary>
        /// Обработчик изменения поля "Текущая база"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbDBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDBName.Text == "")
            {
                tbDBName.Text = "<нет>";
                tbFName.IsEnabled = false;
                tbLName.IsEnabled = false;
                tbPatr.IsEnabled = false;
                tbProff.IsEnabled = false;
                dpBirthDay.IsEnabled = false;
                cbIsDead.IsEnabled = false;
                btAdd.IsEnabled = false;
                btRemove.IsEnabled = false;
            }
            else if(tbDBName.Text != "<нет>")
            {
                tbFName.IsEnabled = true;
                tbLName.IsEnabled = true;
                tbPatr.IsEnabled = true;
                tbProff.IsEnabled = true;
                dpBirthDay.IsEnabled = true;
                cbIsDead.IsEnabled = true;
                btAdd.IsEnabled = true;
                btRemove.IsEnabled = true;
            }
        }
        #endregion

    }
}
