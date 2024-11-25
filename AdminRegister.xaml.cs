using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace TicketTest
{
    /// <summary>
    /// Interaction logic for AdminRegister.xaml
    /// </summary>
    public partial class AdminRegister : Window
    {
        public AdminRegister()
        {
            InitializeComponent();
        }

        private void imgHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void tbUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void tbConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = tbUsername.Text;
                // use .Password for added security feature
                string password = passBoxRegister.Password;
                string confirmPassword = passBoxConfirm.Password;
                // call and open a DB connection
                using (var cmd = DbConnection.CreateCommand())
                {
                    // Check if the username already exists in database
                    string checkQuery = "SELECT COUNT(*) FROM adminaccount WHERE username = @usernameCheck";
                    cmd.CommandText = checkQuery;
                    cmd.Parameters.AddWithValue("@usernameCheck", username);
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        if (password.Equals(confirmPassword))
                        {
                            //say okkeh
                            // Confirm password, register an open an admin account
                            string query = $"INSERT INTO adminaccount VALUES (@username, @password)";
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Registration successful.");
                        }
                        else
                        {
                            // Authentication failed, display an error message
                            MessageBox.Show("Confirm your password again.");
                        }
                    } 
                    else
                    {
                        // Username already exists, display an error message
                        MessageBox.Show("Username already exists.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            placeholderPassword.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passBoxRegister.Password))
                placeholderPassword.Visibility = Visibility.Visible;
        }

        private void PasswordBoxConfirm_GotFocus(object sender, RoutedEventArgs e)
        {
            placeholderConfirmPassword.Visibility = Visibility.Collapsed;
        }

        private void PasswordBoxConfirm_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passBoxConfirm.Password))
                placeholderConfirmPassword.Visibility = Visibility.Visible;
        }
    }
}
