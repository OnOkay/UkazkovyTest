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

        public MainWindow(User LogedUser)
        {
            InitializeComponent();
            MainWindowModel mainWindowModel = new MainWindowModel(LogedUser);
            this.DataContext = mainWindowModel;

            foreach (User user in mainWindowModel.Users)
            {
                //Pridat kommand který zmeni label
                if (user.id != LogedUser.id) //mainWindowModel.ActiveUser.id
                {
                    Button button = new Button() { Content = user.username, Command = mainWindowModel.ChangeReceiverCommand, CommandParameter = user.id };

                    UserList.Children.Add(button);
                }

            }
        }
    }

}