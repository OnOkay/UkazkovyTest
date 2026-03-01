using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UkazkovyTest.Model;
using UkazkovyTest.ViewModel;

namespace UkazkovyTest.View
{

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            UserInterface userInterface = new UserManager(@"..\..\..\Model\UserDatabase.xml");

            LoginModel loginModel = new LoginModel(userInterface);
            this.DataContext = loginModel;
            if (DataContext is LoginModel lm)
            {
                lm.RequestClose += () => this.Close();
            }
        }


    }
}
