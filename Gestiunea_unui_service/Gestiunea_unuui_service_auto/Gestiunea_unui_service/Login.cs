using System;
using System.Drawing;
using System.Windows.Forms;

public class Login : Form
{
    private TextBox usernameField;
    private TextBox passwordField;

    public Login()
    {
        Text = "Login";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Font fieldFont = new Font("Times New Roman", 11, FontStyle.Bold);
        Panel backgroundPanel = new Panel()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\login.jpg"),
            BackgroundImageLayout = ImageLayout.Stretch
        };

        Label usernameLabel = new Label()
        {
            Text = "USERNAME",
            Location = new Point(80, 2),
            Size = new Size(80, 25),
            ForeColor = Color.Black,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(usernameLabel);
        
        usernameField = new TextBox()
        {
            Location = new Point(42, 40),
            Size = new Size(150, 25),
            BackColor = Color.Pink,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle,
            Font= fieldFont
        };
        backgroundPanel.Controls.Add(usernameField);

        Label passwordLabel = new Label()
        {
            Text = "PASSWORD",
            Location = new Point(357, 2),
            Size = new Size(80, 25),
            ForeColor = Color.Black,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(passwordLabel);

        passwordField = new TextBox()
        {
            Location = new Point(316, 40),
            Size = new Size(150, 25),
            BackColor = Color.Pink,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle,
            UseSystemPasswordChar = true,
            Font= fieldFont
        };
        backgroundPanel.Controls.Add(passwordField);

        Button loginButton = new Button()
        {
            Text = "LOGIN",
            Location = new Point(616, 40),
            Size = new Size(100, 28),
            BackColor = Color.Pink,
            ForeColor = Color.Black,
        };
        loginButton.FlatAppearance.BorderColor = Color.Black;
        loginButton.Click += LoginButton_Click;
        backgroundPanel.Controls.Add(loginButton);

        Controls.Add(backgroundPanel);

        Size = new Size(800, 600);
        CenterToScreen();
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        string enteredUsername = usernameField.Text;
        string enteredPassword = passwordField.Text;

        UserType userType = ValidateCredentials(enteredUsername, enteredPassword);

        OpenUserWindow(userType);
    }

    private enum UserType
    {
        ADMIN,
        EMPLOYEE,
        CLIENT
    }

    private UserType ValidateCredentials(string enteredUsername, string enteredPassword)
    {
        string adminUsername = "admin";
        string adminPassword = "admin1";

        string[] employeeUsernames = { "angajat1", "angajat2", "angajat3" };
        string[] employeePasswords = { "bd1", "bd2", "bd3" };

        if (enteredUsername == adminUsername && enteredPassword == adminPassword)
        {
            return UserType.ADMIN;
        }
        else if (Array.Exists(employeeUsernames, u => u == enteredUsername) &&
                 Array.Exists(employeePasswords, p => p == enteredPassword))
        {
            return UserType.EMPLOYEE;
        }
        else
        {
            return UserType.CLIENT;
        }
    }

    private void OpenUserWindow(UserType userType)
    {
        switch (userType)
        {
            case UserType.ADMIN:
                Admin adminForm = new Admin();
                adminForm.Show();
                break;
            case UserType.EMPLOYEE:
                Angajat angajatForm = new Angajat();
                angajatForm.Show();
                break;
            case UserType.CLIENT:
                Clienti clientForm = new Clienti();
                clientForm.Show();
                break;
        }
        Hide();
    }
}