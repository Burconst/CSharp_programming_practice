 //LinqToSql-1_kk - Калюжный К.А. 241 гр.июнь 2019 г.
 //Создание и работа с базой данных, созданной на основе списка
 //созданного в приложении Linq2_kk


using System;
using System.Windows;

namespace LinqToSql_1_kk
{
    /// <summary>
    /// Interaction logic for WAddPers.xaml
    /// </summary>
    public partial class WAddPers : Window
    {
        public WAddPers()
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
