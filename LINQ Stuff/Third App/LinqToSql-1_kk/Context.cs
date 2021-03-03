// LinqToSql-1_kk - Калюжный К.А. 241 гр. июнь 2019 г.
// Создание и работа с базой данных, созданной на основе списка
// созданного в приложении Linq2_kk

using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace LinqToSql_1_kk
{
    [Database]
    public class Context : DataContext
    {
        /// <summary>
        /// Таблица персон
        /// </summary>
        public Table<Person> Persons;
        public Context(string loginString) : base(loginString) { }
    }
}