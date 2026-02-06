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
using UkazkovyTest.ViewModel;

namespace UkazkovyTest.View
{

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            LoginModel loginModel = new LoginModel();
            this.DataContext = loginModel;
            if (DataContext is LoginModel lm)
            {
                lm.RequestClose += () => this.Close();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
        
        }
    }
}
