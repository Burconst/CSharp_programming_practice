// LinqToSql-1_kk - Калюжный К.А. 241 гр. июнь 2019 г.
// Создание и работа с базой данных, созданной на основе списка
// созданного в приложении Linq2_kk

using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.IO;


namespace LinqToSql_1_kk
{
    /// <summary>
    /// Interaction logic for WCreateDB.xaml
    /// </summary>
    public partial class WCreateDB : Window
    {
        /// <summary>
        /// Название списка персон
        /// </summary>
        private string listName;
        /// <summary>
        /// Названия каталога базы данных
        /// </summary>
        private string dbFolder;
        /// <summary>
        /// Название файла с базой данных
        /// </summary>
        private string dbFileName;
        /// <summary>
        /// название сервера
        /// </summary>
        private string serverName;

        //Конструктор
        public WCreateDB()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получить строку подключения к SQL-серверу
        /// </summary>
        private string GetConnectionString(string dbName, string dbPath)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = serverName;
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = dbName;
            builder.AttachDBFilename = dbPath;
            return builder.ConnectionString;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Создать"
        /// </summary>
        private void BtCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(listName) || string.IsNullOrEmpty(dbFolder) || string.IsNullOrEmpty(dbFileName) || string.IsNullOrEmpty(serverName))
            {
                MessageBox.Show("Ошибка: не все поля заполнены");
                return;
            }
            string dbFilePath = Path.Combine(dbFolder, dbFileName) + ".mdf";
            var db = new Context(GetConnectionString(dbFileName, dbFilePath));
            try
            {
                db.CreateDatabase();
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при создании базы: " + ex.Message);
                return;
            }
            try
            {
                using (StreamReader stream = new StreamReader(listName))
                {
                    while (!stream.EndOfStream)
                    {
                        Person person = Person.GetFromStream(stream);
                        db.Persons.InsertOnSubmit(person);
                        stream.ReadLine();
                    }
                }
                db.SubmitChanges();
                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Ошибка при чтении списка: " + ex.Message);
                return;
            }
            db.Dispose();
            Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Найти"
        /// </summary>
        private void BtFindList_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            ofd.Filter = "Списки людей|*.txt|Все файлы|*.*";
            if (ofd.ShowDialog() == true)
            {
                tbListName.Text = ofd.FileName;
                listName = ofd.FileName;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выбрать"
        /// </summary>
        private void BtFindFolder_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbDBFolder.Text = fbd.SelectedPath;
                dbFolder = fbd.SelectedPath;
            }
        }

        /// <summary>
        /// Обработчик изменения содержания поля "Имя базы данных"
        /// </summary>
        private void TbDBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            dbFileName = tbDBName.Text;
        }

        /// <summary>
        /// Обработчик изменения содержания поля "Название сервера"
        /// </summary>
        private void TbServerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            serverName = tbServerName.Text;
        }
    }
}
