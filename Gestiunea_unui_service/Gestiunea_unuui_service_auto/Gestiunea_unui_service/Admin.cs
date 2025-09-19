using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

public class Admin : Form
{
    private SqlConnection connection;

    public Admin()
    {
        Text = "Admin";
        Size = new Size(800, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;
        
        Panel backgroundPanel = new Panel
        {
            Size = new Size(800, 600)
        };
        backgroundPanel.Paint += (sender, e) =>
        {
            Image backgroundImage = Image.FromFile(@"C:\\Users\\Mira\\RiderProjects\\WindowsFormsApp1\\Gestiunea_unui_service\\Imagini\\adminbun.jpg");
            e.Graphics.DrawImage(backgroundImage, 0, 0, backgroundPanel.Width, backgroundPanel.Height);
        };
        Controls.Add(backgroundPanel);
        
        Button angajatiButton = new Button
        {
            Text = "VIZUALIZARE ANGAJATI",
            Size = new Size(182, 36),
            Location = new Point(20, 40),
            BackColor = Color.FromArgb(65, 105, 225),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 9, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat
        };
        angajatiButton.FlatAppearance.BorderSize = 0;
        angajatiButton.Click += (sender, e) =>
        {
            VizualizareAngajatiAdmin vizualizareAngajatiAdminForm = new VizualizareAngajatiAdmin();
            vizualizareAngajatiAdminForm.Show();
            Close();
        };
        backgroundPanel.Controls.Add(angajatiButton);


        Button lucrariButton = new Button
        {
            Text = "VIZUALIZARE LUCRARI",
            Size = new Size(182, 36),
            Location = new Point(20, 85),
            BackColor = Color.FromArgb(65, 105, 225),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 9, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat
        };
        lucrariButton.FlatAppearance.BorderSize = 0;
        lucrariButton.Click += (sender, e) =>
        {
            VizualizareLucrariAdmin vizualizareLucrariAdminForm = new VizualizareLucrariAdmin();
            vizualizareLucrariAdminForm.Show();
            Close();
        };
        backgroundPanel.Controls.Add(lucrariButton);


        Button clientiButton = new Button
        {
            Text = "VIZUALIZARE CLIENTI",
            Size = new Size(182, 36),
            Location = new Point(20, 130),
            BackColor = Color.FromArgb(65, 105, 225),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 9, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat
        };
        clientiButton.FlatAppearance.BorderSize = 0;
        clientiButton.Click += (sender, e) =>
        {
            VizualizareClientiAdmin vizualizareClientiAdminForm = new VizualizareClientiAdmin();
            vizualizareClientiAdminForm.Show();
            Close();
        };
        backgroundPanel.Controls.Add(clientiButton);

  
        Button exitButton = new Button
        {
            Text = "EXIT",
            Size = new Size(80, 34),
            Location = new Point(700, 480),
            BackColor = Color.Red,
            ForeColor = Color.White,
            Font = new Font("Arial Black", 16, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat
        };
        exitButton.FlatAppearance.BorderSize = 0;
        exitButton.Click += (sender, e) => Application.Exit();
        backgroundPanel.Controls.Add(exitButton);
    }

}
