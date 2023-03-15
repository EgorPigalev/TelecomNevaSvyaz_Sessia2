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
using System.Windows.Shapes;

namespace Sessia2
{
    /// <summary>
    /// Логика взаимодействия для AddCRM.xaml
    /// </summary>
    public partial class AddCRM : Window
    {
        public AddCRM(int subscriberID)
        {
            InitializeComponent();
            Subscribers subscriber = Base.baseDate.Subscribers.FirstOrDefault(x => x.SubscriberID == subscriberID);

        }
    }
}
