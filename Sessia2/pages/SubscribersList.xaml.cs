﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sessia2
{
    /// <summary>
    /// Логика взаимодействия для Subscribers.xaml
    /// </summary>
    public partial class SubscribersList : Page
    {
        public SubscribersList()
        {
            InitializeComponent();
            dgSubscribers.ItemsSource = Base.baseDate.Subscribers.ToList();
        }

        private void dgSubscribers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Subscribers subscriber = new Subscribers();
            foreach(Subscribers subscribers in dgSubscribers.SelectedItems)
            {
                subscriber = subscribers;
            }
            if (subscriber == null)
            {
                return;
            }
            else
            {
                FrameClass.frame.Navigate(new Subscribe(subscriber));
            }
        }
    }
}
