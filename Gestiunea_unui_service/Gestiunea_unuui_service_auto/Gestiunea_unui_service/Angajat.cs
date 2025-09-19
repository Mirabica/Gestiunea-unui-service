using System;
using System.Drawing;
using System.Windows.Forms;

public class Angajat : Form
{
    public Angajat()
    {
        Text = "Angajat";
        Size = new Size(800, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;

        var backgroundPanel = new Panel
        {
            Size = ClientSize,
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\Angajati.png"),
            BackgroundImageLayout = ImageLayout.Stretch
        };
        Controls.Add(backgroundPanel);

        Button programariButton = new Button()
        {
            Text = "VIZUALIZARE PROGRAMARI",
            Size = new Size(182, 34),
            Location = new Point(90, 16),
            Font = new Font("Arial Black", 7, FontStyle.Bold),
            BackColor = Color.FromArgb(100, 149, 237),
            ForeColor = Color.White
        };
        backgroundPanel.Controls.Add(programariButton);
        programariButton.Click += (sender, e) =>
        {
            VizualizareProgramariA vizualizareprogramari = new VizualizareProgramariA();
            vizualizareprogramari.Show();
            Close();
        };

        Button lucrariButton = new Button()
        {
            Text = "VIZUALIZARE LUCRARI",
            Size = new Size(182, 34),
            Location = new Point(300, 16),
            Font = new Font("Arial Black", 7, FontStyle.Bold),
            BackColor = Color.FromArgb(100, 149, 237),
            ForeColor = Color.White
        };
        backgroundPanel.Controls.Add(lucrariButton);
        lucrariButton.Click += (sender, e) =>
        {
            VizualizareLucrariA vizualizarelucrari = new VizualizareLucrariA();
            vizualizarelucrari.Show();
            Close();
        };

        Button clientiButton = new Button()
        {
            Text = "VIZUALIZARE CLIENTI",
            Size = new Size(182, 34),
            Location = new Point(510, 16),
            Font = new Font("Arial Black", 7, FontStyle.Bold),
            BackColor = Color.FromArgb(100, 149, 237),
            ForeColor = Color.White
        };
        backgroundPanel.Controls.Add(clientiButton);
        clientiButton.Click += (sender, e) =>
        {
            VizualizareClientiA vizualizareclienti = new VizualizareClientiA();
            vizualizareclienti.Show();
            Close();
        };

        Button exitButton = new Button()
        {
            Text = "EXIT",
            Size = new Size(80, 34),
            Location = new Point(660, 510),
            Font = new Font("Arial Black", 15, FontStyle.Bold),
            BackColor = Color.Red,
            ForeColor = Color.White
        };
        backgroundPanel.Controls.Add(exitButton);
        exitButton.Click += (sender, e) =>
        {
            Application.Exit();
        };
    }
}
