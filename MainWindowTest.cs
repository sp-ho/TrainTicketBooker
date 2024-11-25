using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TicketTest
{
    [TestClass]
    public class MainWindowTest
    {
        [TestMethod]
        public void TestSuccessfulSignIn()
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Act
            mainWindow.tbUsername.Text = "test1@trainadmin.com";
            mainWindow.passBoxAdminSign.Password = "1234";
            mainWindow.btnSignIn_Click(null, null);

            // Assert
            // Verify that the correct dashboard window is shown
            Assert.IsTrue(MainWindow.IsAdminEmail("test1@trainadmin.com"));
            Assert.IsInstanceOfType(mainWindow, typeof(AdminDashboard));
        }
    }
}
