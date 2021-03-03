//Linq2_kk Калюжный К.А, июнь 2019 г.

//Демонстрация создания списка персон, каждая из которых задана структурой.
//Использованы примеры выборки с помощью LINQ-запросов.
//Использованы регулярные выражения для проверки корректности введённых данных

using System.Windows;

namespace Linq2_kk
{
    /// <summary>
    /// Interaction logic for WAbout.xaml
    /// </summary>
    public partial class WAbout : Window
    {
        public WAbout()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
