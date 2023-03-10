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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Base.baseDate = new BaseDate();
            cbFIOEmployee.ItemsSource = Base.baseDate.Employees.ToList();
            cbFIOEmployee.SelectedValuePath = "EmployeesID";
            cbFIOEmployee.DisplayMemberPath = "FIO";
            cbFIOEmployee.SelectedIndex = 0;
        }

        private void cbFIOEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees employee = Base.baseDate.Employees.FirstOrDefault(x => x.EmployeeID == cbFIOEmployee.SelectedIndex + 1);
            if(employee != null)
            {
                lvEvents.ItemsSource = Base.baseDate.Events.Where(x => x.RoleID == employee.RoleID).ToList();
            }
        }

        public static DependencyObject GetScrollViewer(DependencyObject o)
        {
            if (o is ScrollViewer)
            { return o; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                var child = VisualTreeHelper.GetChild(o, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        private void btnUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var scrollViwer = GetScrollViewer(lvEvents) as ScrollViewer;

            if (scrollViwer != null)
            {
                scrollViwer.ScrollToVerticalOffset(scrollViwer.VerticalOffset - 1);
            }
        }

        private void btnDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var scrollViwer = GetScrollViewer(lvEvents) as ScrollViewer;

            if (scrollViwer != null)
            {
                scrollViwer.ScrollToVerticalOffset(scrollViwer.VerticalOffset + 1);
            }
        }
    }
}
