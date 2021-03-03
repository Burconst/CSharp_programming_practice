 //LinqToSql-1_kk - Калюжный К.А. 241 гр.июнь 2019 г.
 //Создание и работа с базой данных, созданной на основе списка
 //созданного в приложении Linq2_kk

using System;
using System.Windows;
using System.Data.SqlClient;

namespace LinqToSql_1_kk
{
    /// <summary>
    /// Interaction logic for WOpenDB.xaml
    /// </summary>
    public partial class WOpenDB : Window
    {
        public bool wasOpaned
        {
            get; private set;
        }

        public Context DB
        {
            get; private set;
        }
        
        public string DBFullName
        {
            get { return tbDBName.Text; }
        }
        public WOpenDB()
        {
            InitializeComponent();
        }

        private string GetConnectionString(string dbName, string dbPath)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = tbServerName.Text;
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = dbName;
            builder.AttachDBFilename = dbPath;
            return builder.ConnectionString;
        }

        private void BtOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB = new Context(GetConnectionString(tbDBName.Text.Substring(tbDBName.Text.LastIndexOf("\\")+1),tbDBName.Text));
                wasOpaned = true;
            }
            catch (Exception ex)
            {
                DB = null;
                wasOpaned = false;
                MessageBox.Show("Ошибка: "+ex.Message);
            }

            Close();
        }

        private void BtFind_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            ofd.Filter = "База данных|*.mdf|Все файлы|*.*";
            if (ofd.ShowDialog() == true && !string.IsNullOrEmpty(ofd.FileName))
            {
                tbDBName.Text = ofd.FileName;
            }
        }
    }
}
