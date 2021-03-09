using System;
using System.Windows;

using LINQ.ViewModel;

namespace LINQ.View
{
    /// <summary>
    /// Interaction logic for WAdd.xaml
    /// </summary>
    public partial class AddView : Window
    {
        private AddViewModel ViewModel;

        public AddView()
        {
            InitializeComponent();
            ViewModel = new AddViewModel();
            DataContext = ViewModel;
        }

        public bool wasAdd = false;

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
