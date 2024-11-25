using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TicketTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        public void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = tbUsername.Text;
                string passwordBox = passBoxAdminSign.Password;

                //MessageBox.Show(passwordBox);

                using (var cmd = DbConnection.CreateCommand())
                {
                    string query = $"SELECT * FROM adminaccount WHERE username = @username AND password = @password";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", passwordBox);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (IsAdminEmail(tbUsername.Text))
                            {
                                // Authentication successful, open the new window or perform any desired action
                                // For example, you can create a new instance of the window and show it
                                AdminDashboard adminDashboard = new AdminDashboard();
                                adminDashboard.Show();
                            }
                            else
                            {
                                UserDashboard userDashboard = new UserDashboard();
                                userDashboard.Show();
                                Close();
                            }
                        }
                        else
                        {
                            // Authentication failed, display an error message
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AdminRegister adminRegister = new AdminRegister();
            adminRegister.Show();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            placeholderText.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passBoxAdminSign.Password))
                placeholderText.Visibility = Visibility.Visible;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private static string GetDomainFromEmail(string email)
        {
            // Split the email by '@' symbol and get the second part
            string[] parts = email.Split('@');
            if (parts.Length > 1)
            {
                return parts[1];
            }
            return string.Empty;
        }

        public static bool IsAdminEmail(string email)
        {
            // Define the expected domain for admin emails
            string adminDomain = "trainadmin.com";

            // Get the domain part of the email
            string domain = GetDomainFromEmail(email);

            // Compare the domain with the admin domain
            return string.Equals(domain, adminDomain, StringComparison.OrdinalIgnoreCase);
        }
    }
}
