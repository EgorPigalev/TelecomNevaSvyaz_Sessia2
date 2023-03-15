using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CRMPage.xaml
    /// </summary>
    public partial class CRMPage : Page
    {
        public CRMPage()
        {
            InitializeComponent();
            calculateDateSubscriber();
        }

        private void btnAddCRM_Click(object sender, RoutedEventArgs e)
        {
            if(cmbSubscriber.SelectedItem != null)
            {
                AddCRM addCRM = new AddCRM((int)cmbSubscriber.SelectedValue);
                addCRM.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользователь не выбран!");
            }
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == "(") || (e.Text == ")") || (e.Text == "+") || (e.Text == "-")))
            {
                e.Handled = true;
            }
        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateDateSubscriber();
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateDateSubscriber();
        }

        public void calculateDateSubscriber()
        {
            List<Subscribers> subscribers = Base.baseDate.Subscribers.ToList();
            if(tbPhone.Text.Length > 0)
            {
                subscribers = subscribers.Where(x => x.Phone.ToLower().Contains(tbPhone.Text.ToLower())).ToList();
            }
            if (tbSurname.Text.Length > 0)
            {
                subscribers = subscribers.Where(x => x.Surname.ToLower().Contains(tbSurname.Text.ToLower())).ToList();
            }
            cmbSubscriber.ItemsSource = subscribers;
            cmbSubscriber.SelectedValuePath = "SubscriberID";
            cmbSubscriber.DisplayMemberPath = "FIOFull";
        }
    }
}
