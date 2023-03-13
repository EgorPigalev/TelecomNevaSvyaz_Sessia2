using System;
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
            cbActive.IsChecked = true; // По умолчанию выводятся абоненты с активными договорами
            List<Raions> raions = Base.baseDate.Raions.ToList(); // Заполнение списка районов
            cbFilterRaion.Items.Add("Все районы");
            foreach(Raions raion in raions)
            {
                cbFilterRaion.Items.Add(raion.RaionName);
            }
            cbFilterRaion.SelectedIndex = 0;
        }

        private void dgSubscribers_MouseDoubleClick(object sender, MouseButtonEventArgs e) // При двойном нажатие открывается страница с подробным описанием абонента
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

        /// <summary>
        /// Реализация поиска, фильтрации списка абонентов
        /// </summary>
        private void Filter()
        {
            List<Subscribers> subscribers = new List<Subscribers>();
            if((bool)cbActive.IsChecked && (bool)cbNotActive.IsChecked) // Фильтрация по активности договоров
            {
                subscribers = Base.baseDate.Subscribers.ToList();
            }
            else if((bool)cbActive.IsChecked && (bool)!cbNotActive.IsChecked)
            {
                subscribers = Base.baseDate.Subscribers.Where(x => x.Contracts.TermibationDate == null).ToList();
            }
            else if ((bool)!cbActive.IsChecked && (bool)cbNotActive.IsChecked)
            {
                subscribers = Base.baseDate.Subscribers.Where(x => x.Contracts.TermibationDate != null).ToList();
            }
            else
            {
                subscribers = new List<Subscribers>();
            }
            if(tbSearchSurname.Text.Replace(" ", "").Length > 0) // Поиск по фамилии
            {
                subscribers = subscribers.Where(x => x.Surname.ToLower().Contains(tbSearchSurname.Text.ToLower())).ToList();
            }
            if (cbFilterRaion.SelectedIndex > 0) // Фильтрация по району
            {
                Raions raion = Base.baseDate.Raions.FirstOrDefault(x => x.RaionName == cbFilterRaion.SelectedValue); // Район по названию
                subscribers = subscribers.Where(x => x.ResidentialAddress.RaionID == raion.RaionID).ToList();
            }
            if (tbSearchPersonalAccount.Text.Replace(" ", "").Length > 0) // Поиск по лицевому счету
            {
                subscribers = subscribers.Where(x => x.Contracts.PersonalAccount.ToString().ToLower().Contains(tbSearchPersonalAccount.Text.ToLower())).ToList();
            }
            dgSubscribers.ItemsSource = subscribers;
            if(subscribers.Count == 0)
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }

        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void tbSearchSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cbFilterRaion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
