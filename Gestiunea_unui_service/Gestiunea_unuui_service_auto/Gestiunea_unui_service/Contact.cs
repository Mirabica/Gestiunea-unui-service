using System;
using System.Drawing;
using System.Windows.Forms;

public class Contact : Form
{
    public Contact()
    {
        Text = "Contact";
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
            BackColor = Color.White,
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\contact2.jpg"), 
            BackgroundImageLayout = ImageLayout.Stretch
        };
        Controls.Add(backgroundPanel);

        AddComponents(backgroundPanel);
    }

    private void AddComponents(Panel backgroundPanel)
    {
   
        Button backButton = new Button()
        {
            Text = "BACK",
            Bounds = new Rectangle(694, 500, 80, 34),
            BackColor = Color.FromArgb(105, 105, 105),
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold)
        };
        backgroundPanel.Controls.Add(backButton);

        backButton.Click += (sender, e) =>
        {
            Clienti clienti = new Clienti();
            clienti.Show();
            Dispose();
        };

        Font textFont = new Font("Times New Roman", 14, FontStyle.Bold);
        Color textColor = Color.Yellow;

        Label labelNrTel = new Label()
        {
            Text = "Telefon",
            Bounds = new Rectangle(160, 180, 120, 25),
            ForeColor = textColor,
            Font = textFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelNrTel);

        Label labelNumere = new Label()
        {
            Text = "0239 671 100 | 0236 419 898",
            Bounds = new Rectangle(80, 210, 300, 30),
            ForeColor = textColor,
            Font = textFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelNumere);

        Label labelMail = new Label()
        {
            Text = "Mail",
            Bounds = new Rectangle(160, 270, 120, 25),
            ForeColor = textColor,
            Font = textFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelMail);

        Label labelMail1 = new Label()
        {
            Text = "service@autowab.ro",
            Bounds = new Rectangle(100, 300, 300, 30),
            ForeColor = textColor,
            Font = textFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelMail1);

        Label labelLoc = new Label()
        {
            Text = "Locatie",
            Bounds = new Rectangle(160, 400, 300, 25),
            ForeColor = textColor,
            Font = textFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelLoc);

        Label labelLoc1 = new Label()
        {
            Text = "Bucuresti, Strada Fizicienilor nr. 21B, Sector 3",
            Bounds = new Rectangle(10, 430, 500, 30),
            ForeColor = textColor,
            Font = textFont,
                BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelLoc1);
    }
}
