using System;
using System.Drawing;
using System.Windows.Forms;


public class Clienti : Form
{
    public Clienti()
    {
        Text = "Clienti";
        Size = new Size(800, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        CenterToScreen();
        InitializeComponents();
    }

    private void InitializeComponents()
    {

        Panel backgroundPanel = new Panel()
        {
            Dock = DockStyle.Fill,
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\client12.jpg"), BackgroundImageLayout = ImageLayout.Stretch
        };
        Controls.Add(backgroundPanel);

        AddComponents(backgroundPanel);
    }

    private void AddComponents(Panel backgroundPanel)
    {

        Button programariButton = new Button()
        {
            Text = "VIZUALIZARE PROGRAMARI",
            Bounds = new Rectangle(84, 76, 182, 34),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(160, 82, 45),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 7, FontStyle.Bold)
        };
        programariButton.FlatAppearance.BorderSize = 0;
        backgroundPanel.Controls.Add(programariButton);

        programariButton.Click += (sender, e) =>
        {
            VizualizareProgramariC vizualizareprogramari = new VizualizareProgramariC();
            vizualizareprogramari.Show();
            Dispose();
        };

        Button masiniButton = new Button()
        {
            Text = "VIZUALIZARE MASINI",
            Bounds = new Rectangle(296, 76, 182, 34),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(160, 82, 45),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 7, FontStyle.Bold)
        };
        masiniButton.FlatAppearance.BorderSize = 0;
        backgroundPanel.Controls.Add(masiniButton);

        masiniButton.Click += (sender, e) =>
        {
            VizualizareMasini vizualizaremasini = new VizualizareMasini();
            vizualizaremasini.Show();
            Dispose();
        };

        Button contactButton = new Button()
        {
            Text = "CONTACT",
            Bounds = new Rectangle(510, 76, 182, 34),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(160, 82, 45),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 7, FontStyle.Bold)
        };
        contactButton.FlatAppearance.BorderSize = 0;
        backgroundPanel.Controls.Add(contactButton);

        contactButton.Click += (sender, e) =>
        {
            Contact contact = new Contact();
            contact.Show();
            Dispose();
        };

        Button exitButton = new Button()
        {
            Text = "EXIT",
            Bounds = new Rectangle(694, 440, 80, 34),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(255, 0, 0),
            ForeColor = Color.White,
            Font = new Font("Arial Black", 16, FontStyle.Bold)
        };
        exitButton.FlatAppearance.BorderSize = 0;
        backgroundPanel.Controls.Add(exitButton);

        exitButton.Click += (sender, e) =>
        {
            Application.Exit();
        };
    }

}
