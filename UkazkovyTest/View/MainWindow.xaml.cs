using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UkazkovyTest.Model;
using UkazkovyTest.ViewModel;

namespace UkazkovyTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowModel mainViewModel = new MainWindowModel();
            this.DataContext = mainViewModel;

            foreach(User user in mainViewModel.Users)
            {
                //Pridat kommand který zmeni label
                if (user.id != 2)
                {
                    Button button = new Button() { Content = user.username, Command = mainViewModel.SetActiveUserCommand };
                    UserList.Children.Add(button);
                }
                
            }

            ActiveUser.Content = mainViewModel.ActiveUser;
        }
        

    }
}