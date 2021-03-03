using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using Microsoft.Win32;

namespace ContextPlayer
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ContextPlayer : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        MediaPlayer plr = new MediaPlayer();

        List<string> playListFullName = new List<string>();
        List<string> playListName = new List<string>();
        string currSongName = "";
        int indcurrsng = -1;

        private double Volume
        {
            get { return plr.Volume * 100; }
            set { plr.Volume = value / 100; }
        }

        private double position;

        public double Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");

            }
        }

        //public string Position
        //{
        //    get
        //    {
        //        if (plr != null && plr.Position != default(TimeSpan))
        //        {
        //            return plr.Position.TotalMinutes.ToString();
        //        }
        //        else
        //        {
        //            return "00:00";
        //        } 
        //    }
        //}

        public ContextPlayer()
        {
            InitializeComponent();
            RefreshDataBinding();
        }


        private void RefreshDataBinding()
        {
            tbTitle.Text = currSongName;
            lbPlayList.ItemsSource = null;
            lbPlayList.ItemsSource = playListName;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var op = new OpenFileDialog();
            op.ShowDialog();
            if (op.FileName != "")
            {
                try
                {
                    plr.Open(new Uri(op.FileName));
                    plr.Play();
                    playListFullName.Add(op.FileName);
                    currSongName = op.FileName.Substring(op.FileName.LastIndexOf("\\") + 1);
                    playListName.Add(currSongName);
                    indcurrsng++;
                    RefreshDataBinding();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (plr.IsMuted == false)
            {
                plr.Pause();
            }
            else
            {
                plr.Play();
            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            if (playListFullName.Count != 0)
            {
                indcurrsng = (indcurrsng + 1) % playListFullName.Count;
                plr.Stop();
                plr.Open(new Uri(playListFullName[indcurrsng]));
                plr.Play();
            }
        }

        private void tbPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (playListFullName.Count != 0)
            {
                indcurrsng = (indcurrsng - 1) % playListFullName.Count;
                plr.Stop();
                plr.Open(new Uri(playListFullName[indcurrsng]));
                plr.Play();
            }
        }

        private void slVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Volume = slVolume.Value;
        }

        private void lbPlayList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (lbPlayList.SelectedItem != null)
                {
                    plr.Stop();
                    plr.Open(new Uri(playListFullName[lbPlayList.SelectedIndex]));
                    currSongName = playListName[lbPlayList.SelectedIndex];
                    plr.Play();
                    RefreshDataBinding();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
