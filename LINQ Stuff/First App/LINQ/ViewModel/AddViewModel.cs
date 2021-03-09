using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LINQ.Model;

namespace LINQ.ViewModel
{
    public class AddViewModel
    {
        public Person Person { get; set; }

        public void smth() 
        {
            /*            try
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
                wasAdd = true;*/
        }

    }
}
