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

using LINQ.ViewModel;

namespace System.Runtime.CompilerServices
{
    sealed class CallerMemberNameAttribute : Attribute { }
}

namespace LINQ.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Эффект размытия при открытии диалоговых окон
        /// </summary>
        BlurEffect blur = new BlurEffect() { Radius = 10 };

        private MainWindowViewModel ViewModel { get; }

        //Конструктор
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;
        }

        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            ofd.Filter = "Списки людей|*.txt|Все файлы|*.*";
            mainGrid.Effect = blur;
            if (ofd.ShowDialog() == true)
            {
                ViewModel.OpenPersonsList(ofd.FileName);
            }
            mainGrid.Effect = default(Effect);
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            sfd.Filter = "Списки людей|*.txt|Все файлы|*.*";
            mainGrid.Effect = blur;
            if (sfd.ShowDialog() == true)
            {
                ViewModel.SavePersonsList(sfd.FileName);
            }
            mainGrid.Effect = default(Effect);
        }


        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddView wa = new AddView();
            if (wa.ShowDialog() == true) 
            {
                if (wa.wasAdd)
                {
     //               ViewModel.Add(wa.newpers);
                }
            }
        }

        private void BtRemove_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Remove(ViewModel.CurrentPerson);
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
/*            if (personList.Count == 0)
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
            }*/
        }


        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ShowResults();
        }


    }
}
