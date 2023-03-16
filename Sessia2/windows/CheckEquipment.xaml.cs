using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sessia2
{
    /// <summary>
    /// Логика взаимодействия для CheckEquipment.xaml
    /// </summary>
    public partial class CheckEquipment : Window
    {
        bool b; // Отмена тестирования (true - да; false - нет)
        public CheckEquipment()
        {
            InitializeComponent();
            b = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            b = false;
            this.Close();
        }

        private async void btnPB_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 101; i++)
            {
                await Task.Delay(50);
                pbLoading.Value++;
            }
            if(!b)
            {
                return;
            }
            Random rnd = new Random();
            int a = rnd.Next(2);
            if(a == 1)
            {
                MessageBox.Show("Оборудование исправно");
                AddCRM.b = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Оборудование не исправно");
                AddCRM.b = 2;
                this.Close();
            }
        }
    }
}
