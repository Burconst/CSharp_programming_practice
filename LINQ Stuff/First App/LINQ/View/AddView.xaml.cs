//Linq1_kk Калюжный К.А, июнь 2019 г.

//Демонстрация создания списка персон, каждая из которых задана структурой.
//Использованы примеры выборки с помощью LINQ-запросов.


using System;
using System.Windows;

namespace LINQ.View
{
    /// <summary>
    /// Interaction logic for WAdd.xaml
    /// </summary>
    public partial class WAdd : Window
    {
        public WAdd()
        {
            InitializeComponent();
        }

        public bool wasAdd = false;
        public Person newpers;

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbIsDead.IsChecked == true)
                {
                    newpers = new Person(tbFName.Text, tbLName.Text, tbPatr.Text, dpBirthDay.SelectedDate.Value, dpDeathDay.SelectedDate.Value, tbProff.Text);
                }
                else
                {
                    newpers = new Person(tbFName.Text, tbLName.Text, tbPatr.Text, dpBirthDay.SelectedDate.Value, tbProff.Text);
                }
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
                Close();
                return;
            }
            wasAdd = true;
            Close();
        }

        /// <summary>
        /// Обработчик нажатия на чекбокс с датой смерти
        /// </summary>
        private void CbIsDead_Click(object sender, RoutedEventArgs e)
        {
            dpDeathDay.IsEnabled = !dpDeathDay.IsEnabled;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
