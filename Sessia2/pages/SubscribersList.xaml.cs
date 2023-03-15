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
        bool b;
        public SubscribersList()
        {
            InitializeComponent();
            b = true;
            dgSubscribers.ItemsSource = Base.baseDate.Subscribers.ToList();
            cbActive.IsChecked = true; // По умолчанию выводятся абоненты с активными договорами
            List<Raions> raions = Base.baseDate.Raions.ToList(); // Заполнение списка районов
            cbFilterRaion.Items.Add("Все районы");
            foreach(Raions raion in raions)
            {
                cbFilterRaion.Items.Add(raion.RaionName);
            }
            cbFilterRaion.SelectedIndex = 0;
            cbFilterStreet.IsEnabled = false;
            cbFiltNomerHouse.IsEnabled = false;
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
            if(cbFilterStreet.SelectedIndex > 0) // Фильтрация по улице
            {
                Streets street = Base.baseDate.Streets.FirstOrDefault(x => x.Street == cbFilterStreet.SelectedValue);
                subscribers = subscribers.Where(x => x.ResidentialAddress.StreetID == street.StreetID).ToList();
            }
            if (cbFiltNomerHouse.SelectedIndex > 0) // Фильтрация по дому
            {
                subscribers = subscribers.Where(x => Convert.ToString(x.ResidentialAddress.House) == (string)cbFiltNomerHouse.SelectedValue).ToList();
            }
            if (tbSearchPersonalAccount.Text.Replace(" ", "").Length > 0) // Поиск по лицевому счету
            {
                subscribers = subscribers.Where(x => x.Contracts.PersonalAccount.ToString().ToLower().Contains(tbSearchPersonalAccount.Text.Replace(" ", "").ToLower())).ToList();
            }
            dgSubscribers.ItemsSource = subscribers;          
            if(subscribers.Count == 0 && b)
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }

        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            b = true;
            Filter();
        }

        private void tbSearchSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            b = true;
            Filter();
        }

        private void cbFilterRaion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = true;
            Filter();
            if(cbFilterRaion.SelectedIndex > 0)
            {
                b = false;
                cbFilterStreet.Items.Clear();
                cbFilterStreet.IsEnabled = true;
                List<ResidentialAddress> residentialAddresses = Base.baseDate.ResidentialAddress.Where(x => x.RaionID == cbFilterRaion.SelectedIndex).ToList();
                List<string> streets = new List<string>();
                cbFilterStreet.Items.Add("Все улицы");
                foreach (ResidentialAddress res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.StreetID != null)
                    {
                        streets.Add(res.Streets.Street);
                    }
                }
                streets = streets.Distinct().ToList();
                foreach(string street in streets)
                {
                    cbFilterStreet.Items.Add(street);
                }
                cbFilterStreet.SelectedIndex = 0;
            }
            else
            {
                b = true;
                cbFilterStreet.IsEnabled = false;
                cbFilterStreet.Items.Clear();
            }
        }

        private void cbFilterStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Filter();
            if (cbFilterStreet.SelectedIndex > 0)
            {
                cbFiltNomerHouse.Items.Clear();
                cbFiltNomerHouse.IsEnabled = true;
                List<ResidentialAddress> residentialAddresses = Base.baseDate.ResidentialAddress.Where(x => x.RaionID == cbFilterRaion.SelectedIndex && x.StreetID == cbFilterStreet.SelectedIndex).ToList();
                List<string> houses = new List<string>();
                cbFiltNomerHouse.Items.Add("Все дома");
                foreach (ResidentialAddress res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.House != null)
                    {
                        houses.Add(Convert.ToString(res.House));
                    }
                }
                houses = houses.Distinct().ToList();
                foreach (string house in houses)
                {
                    cbFiltNomerHouse.Items.Add(house);
                }
                cbFiltNomerHouse.SelectedIndex = 0;
            }
            else
            {
                cbFiltNomerHouse.IsEnabled = false;
                cbFiltNomerHouse.Items.Clear();
            }
        }

        private void cbFiltNomerHouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Filter();
        }

        private void tbSearchPersonalAccount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
    }
}
