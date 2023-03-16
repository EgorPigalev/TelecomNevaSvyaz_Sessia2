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
using static System.Net.Mime.MediaTypeNames;

namespace Sessia2
{
    /// <summary>
    /// Логика взаимодействия для AddCRM.xaml
    /// </summary>
    public partial class AddCRM : Window
    {
        CRM crm;
        public static int b; // Исправность оборудования (1 - исправно; 2 - не исправно)
        public AddCRM(int subscriberID)
        {
            InitializeComponent();
            crm = new CRM();
            Subscribers subscriber = Base.baseDate.Subscribers.FirstOrDefault(x => x.SubscriberID == subscriberID);
            crm.SubscriberID = subscriberID; // Формирование клиента
            tbHeader.Text = tbHeader.Text + subscriber.FIO;
            crm.NumberCRM = subscriber.Contracts.PersonalAccount + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"); // Создание номера заявки
            tbNomer.Text = tbNomer.Text + crm.NumberCRM;
            crm.DateCreation = DateTime.Today; // Создание даты заказа
            dateOfCreation.Text = crm.DateCreation.ToString("D");
            tbPhone.Text = subscriber.Phone;
            tbPersonalAccount.Text = subscriber.Contracts.PersonalAccount.ToString();
            cmbService.ItemsSource = Base.baseDate.Services.ToList(); // Заполнение списка услуг
            cmbService.SelectedValuePath = "ServicesID";
            cmbService.DisplayMemberPath = "Services1";
            cmbTypeOfService.ItemsSource = Base.baseDate.TypeOfServices.ToList(); // Заполнение списка вида услуг
            cmbTypeOfService.SelectedValuePath = "TypeOfServiceID";
            cmbTypeOfService.DisplayMemberPath = "TypeOfService";
            cmbStatus.ItemsSource = Base.baseDate.ServiceStatus.ToList(); // Заполнение списка статусов
            cmbStatus.SelectedValuePath = "ServiceStatusID";
            cmbStatus.DisplayMemberPath = "ServiceStatus1";
            cmbStatus.SelectedIndex = 0;
            cmbProblemType.ItemsSource = Base.baseDate.ProblemTypes.ToList(); // Заполнение списка типа проблем
            cmbProblemType.SelectedValuePath = "ProblemTypeID";
            cmbProblemType.DisplayMemberPath = "ProblemType";
            EquipmentInstallations equipmentInstallations = Base.baseDate.EquipmentInstallations.FirstOrDefault(x => x.SubscriberID == subscriberID); // Формирование типа оборудования клиента
            tbTypeEquipment.Text = tbTypeEquipment.Text + equipmentInstallations.Equipments.TypeEquioment.TypeEquipment;
            cmbServiceType.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbTypeOfService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbServiceType.IsEnabled = true;
            List<KindsAndTypesService> kindsAndTypesServices = Base.baseDate.KindsAndTypesService.Where(x => x.TypeOfServiceID == (int)cmbTypeOfService.SelectedValue).ToList(); // Формирование списка типа услуг на основание вида
            List<ServiceTypes> serviceTypes = new List<ServiceTypes>();
            foreach(KindsAndTypesService kind in kindsAndTypesServices)
            {
                serviceTypes.Add(kind.ServiceTypes);
            }
            cmbServiceType.ItemsSource = serviceTypes;
            cmbServiceType.SelectedValuePath = "ServiceTypeID";
            cmbServiceType.DisplayMemberPath = "ServiceType";
        }

        private void btnCRM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbService.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"услуга\" не заполнено!");
                    return;
                }
                if (cmbTypeOfService.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"вид услуги\" не заполнено!");
                    return;
                }
                if (cmbServiceType.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"тип услуги\" не заполнено!");
                    return;
                }
                if (cmbProblemType.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"тип проблемы\" не заполнено!");
                    return;
                }
                crm.ServicesID = (int)cmbService.SelectedValue;
                crm.TypeOfServiceID = (int)cmbTypeOfService.SelectedValue;
                crm.ServiceTypeID = (int)cmbServiceType.SelectedValue;
                crm.ServiceStatusID = (int)cmbStatus.SelectedValue;
                crm.ProblemTypeID = (int)cmbProblemType.SelectedValue;
                if (tbDescription.Text.Length > 0)
                {
                    crm.Description = tbDescription.Text;
                }
                if (dpClosingDate.SelectedDate != null)
                {
                    crm.ClosingDate = dpClosingDate.SelectedDate;
                }
                Base.baseDate.CRM.Add(crm);
                Base.baseDate.SaveChanges();
                MessageBox.Show("Заявка успешно создана");
                this.Close();
            }
            catch
            {
                MessageBox.Show("При создание заявки клиента возникла ошибка!");
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            b = 3;
            CheckEquipment checkEquipment = new CheckEquipment();
            checkEquipment.ShowDialog();
            if(b == 1)
            {
                cmbStatus.SelectedIndex = 2;
                dpClosingDate.Text = DateTime.Today.ToString("D");
            }
            else if(b == 2)
            {
                cmbStatus.SelectedIndex = 1;
                dpClosingDate.Text = "";
            }
        }
    }
}
