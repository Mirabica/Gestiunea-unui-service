using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class VizualizareAngajatiAdmin : Form
{
    private TextBox codaField;
    private TextBox numeField;
    private TextBox prenumeField;
    private TextBox adresaField;
    private TextBox functieField;
    private RichTextBox textArea;
    private MySqlConnection connection;

    public VizualizareAngajatiAdmin()
    {
        ConnectToDatabase();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Vizualizare angajati";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        Panel backgroundPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackgroundImage = Image.FromFile(@"C:\\Users\\Mira\\RiderProjects\\WindowsFormsApp1\\Gestiunea_unui_service\\Imagini\\cli1.jpg"),
            BackgroundImageLayout = ImageLayout.Stretch
        };
        this.Controls.Add(backgroundPanel);

        Button backButton = new Button
        {
            Text = "BACK",
            Size = new Size(80, 34),
            Location = new Point(694, 525),
            BackColor = Color.FromArgb(105, 105, 105),
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold)
        };
        backButton.Click += (sender, e) =>
        {
            Admin admin = new Admin();
            admin.Show();
            Close();
        };
        backgroundPanel.Controls.Add(backButton);

        Button vizualizareButton = new Button
        {
            Text = "Vizualizare",
            Size = new Size(190, 39),
            Location = new Point(0, 10),
            ForeColor = Color.Black,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 16, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, // Set the hover color to Transparent
                MouseDownBackColor = Color.Transparent // Set the click color to Transparent
            }
        };
        vizualizareButton.Click += (sender, e) =>
        {
            PerformSearch();
        };
        backgroundPanel.Controls.Add(vizualizareButton);

        Button adaugaButton = new Button
        {
            Text = "Adaugare",
            Size = new Size(190, 39),
            Location = new Point(200, 10),
            ForeColor = Color.Black,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 16, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
            BorderSize = 0,
            MouseOverBackColor = Color.Transparent, // Set the hover color to Transparent
            MouseDownBackColor = Color.Transparent // Set the click color to Transparent
            }
            
        };
        adaugaButton.Click += (sender, e) =>
        {
            PerformInsert();
        };
        backgroundPanel.Controls.Add(adaugaButton);

        Button stergeButton = new Button
        {
            Text = "Stergere",
            Size = new Size(190, 39),
            Location = new Point(400, 10),
            ForeColor = Color.Black,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 16, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
            BorderSize = 0,
            MouseOverBackColor = Color.Transparent, // Set the hover color to Transparent
            MouseDownBackColor = Color.Transparent // Set the click color to Transparent
        }
        };
        stergeButton.Click += (sender, e) =>
        {
            PerformDelete();
        };
        backgroundPanel.Controls.Add(stergeButton);

        Button editareButton = new Button
        {
            Text = "Modificare",
            Size = new Size(190, 39),
            Location = new Point(600, 10),
            ForeColor = Color.Black,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 16, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, // Set the hover color to Transparent
                MouseDownBackColor = Color.Transparent // Set the click color to Transparent
            }
        };
        editareButton.Click += (sender, e) =>
        {
            PerformUpdate();
        };
        backgroundPanel.Controls.Add(editareButton);

        int frameWidth = 800;
        int horizontalGap = (frameWidth - 5 * 110) / 6;

        codaField = new TextBox
        {
            Size = new Size(110, 25),
            Location = new Point(horizontalGap, 120),
            BackColor = Color.Gray,
            ForeColor = Color.Black
        };
        backgroundPanel.Controls.Add(codaField);

        numeField = new TextBox
        {
            Size = new Size(110, 25),
            Location = new Point(2 * horizontalGap + 110, 120),
            BackColor = Color.Gray,
            ForeColor = Color.Black
        };
        backgroundPanel.Controls.Add(numeField);

        prenumeField = new TextBox
        {
            Size = new Size(110, 25),
            Location = new Point(3 * horizontalGap + 2 * 110, 120),
            BackColor = Color.Gray,
            ForeColor = Color.Black
        };
        backgroundPanel.Controls.Add(prenumeField);

        adresaField = new TextBox
        {
            Size = new Size(110, 25),
            Location = new Point(4 * horizontalGap + 3 * 110, 120),
            BackColor = Color.Gray,
            ForeColor = Color.Black
        };
        backgroundPanel.Controls.Add(adresaField);

        functieField = new TextBox
        {
            Size = new Size(110, 25),
            Location = new Point(5 * horizontalGap + 4 * 110, 120),
            BackColor = Color.Gray,
            ForeColor = Color.Black
        };
        backgroundPanel.Controls.Add(functieField);

        Font labelFont = new Font("Arial Black", 14, FontStyle.Bold);

        Label labelCodA = new Label
        {
            Text = "Cod Ang",
            Size = new Size(110, 25),
            Location = new Point(horizontalGap + 8, 90),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelCodA);

        Label labelNume = new Label
        {
            Text = "Nume",
            Size = new Size(110, 25),
            Location = new Point(2 * horizontalGap + 130, 90),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelNume);

        Label labelPren = new Label
        {
            Text = "Prenume",
            Size = new Size(110, 25),
            Location = new Point(3 * horizontalGap + 2 * 113, 90),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelPren);

        Label labelAdr = new Label
        {
            Text = "Adresa",
            Size = new Size(110, 25),
            Location = new Point(4 * horizontalGap + 3 * 114, 90),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelAdr);

        Label labelFct = new Label
        {
            Text = "Functie",
            Size = new Size(110, 25),
            Location = new Point(5 * horizontalGap + 4 * 113, 90),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelFct);

        textArea = new RichTextBox()
        {
            Multiline = true,
            Size = new Size(300, 190),
            Location = new Point(450, 332),
            BackColor = Color.FromArgb(255, 255, 204),
            ReadOnly = true,
            BorderStyle = BorderStyle.None
        };
        backgroundPanel.Controls.Add(textArea);
        
    }
    

    private void ConnectToDatabase()
    {
        try
        {
            string connectionString = "server=localhost;database=proiect_bd;user=root;password=root";
            connection = new MySqlConnection(connectionString);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                Console.WriteLine("Conexiune la baza de date reușită!");
            }
            else
            {
                Console.Error.WriteLine("Eroare la conectarea la baza de date.");
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Eroare la conectarea la baza de date.");
            Console.Error.WriteLine(e.Message);
        }
    }

    private void PerformSearch()
    {
        try
        {
            textArea.Text = "";
            string query;
            if (AreAllFieldsEmpty())
            {
                query = "SELECT * FROM Angajati";
                Console.WriteLine("Select toate inregistrarile: " + query);
            }
            else
            {
                string whereClause = " WHERE ";

                if (!string.IsNullOrEmpty(codaField.Text))
                {
                    whereClause += "Coda = @Coda AND ";
                }
                if (!string.IsNullOrEmpty(numeField.Text))
                {
                    whereClause += "Nume = @Nume AND ";
                }
                if (!string.IsNullOrEmpty(prenumeField.Text))
                {
                    whereClause += "Prenume = @Prenume AND ";
                }
                if (!string.IsNullOrEmpty(adresaField.Text))
                {
                    whereClause += "Adresa = @Adresa AND ";
                }
                if (!string.IsNullOrEmpty(functieField.Text))
                {
                    whereClause += "Functie = @Functie AND ";
                }

                if (whereClause.Length > 7)
                {
                    whereClause = whereClause.Substring(0, whereClause.Length - 5);
                    query = "SELECT * FROM Angajati" + whereClause;
                    Console.WriteLine("Select cu conditii: " + query);
                }
                else
                {
                    query = "SELECT * FROM Angajati";
                    Console.WriteLine("Select toate inregistrarile: " + query);
                }
            }

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(codaField.Text))
                {
                    command.Parameters.AddWithValue("@Coda", codaField.Text);
                }
                if (!string.IsNullOrEmpty(numeField.Text))
                {
                    command.Parameters.AddWithValue("@Nume", numeField.Text);
                }
                if (!string.IsNullOrEmpty(prenumeField.Text))
                {
                    command.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                }
                if (!string.IsNullOrEmpty(adresaField.Text))
                {
                    command.Parameters.AddWithValue("@Adresa", adresaField.Text);
                }
                if (!string.IsNullOrEmpty(functieField.Text))
                {
                    command.Parameters.AddWithValue("@Functie", functieField.Text);
                }

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textArea.AppendText($"Coda: {reader["Coda"]}, ");
                        textArea.AppendText($"Nume: {reader["Nume"]}, ");
                        textArea.AppendText($"Prenume: {reader["Prenume"]}, ");
                        textArea.AppendText($"Adresa: {reader["Adresa"]}, ");
                        textArea.AppendText($"Functie: {reader["Functie"]}\r\n");
                        textArea.AppendText("--------------\r\n");
                    }
                }
                LogOperation("SELECT");
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Eroare la efectuarea căutării.");
            Console.Error.WriteLine(e.Message);
        }
    }

    private bool AreAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(codaField.Text) && string.IsNullOrEmpty(numeField.Text)
            && string.IsNullOrEmpty(prenumeField.Text) && string.IsNullOrEmpty(adresaField.Text)
            && string.IsNullOrEmpty(functieField.Text);
    }

    private void PerformInsert()
    {
        try
        {
            if (string.IsNullOrEmpty(codaField.Text))
            {
                MessageBox.Show("Cod Angajat este obligatoriu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Angajati (Coda, Nume, Prenume, Adresa, Functie) VALUES (@Coda, @Nume, @Prenume, @Adresa, @Functie)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Coda", codaField.Text);
                command.Parameters.AddWithValue("@Nume", numeField.Text);
                command.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                command.Parameters.AddWithValue("@Adresa", adresaField.Text);
                command.Parameters.AddWithValue("@Functie", functieField.Text);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Angajat adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("INSERT");
                }
                else
                {
                    MessageBox.Show("Eroare la adăugare angajat.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Eroare la adăugare angajat: " + e.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.Error.WriteLine(e.Message);
        }
    }

    private void PerformDelete()
    {
        try
        {
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show("Introduceți cel puțin o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "DELETE FROM Angajati WHERE ";
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                query += "Coda = @Coda AND ";
            }
            if (!string.IsNullOrEmpty(numeField.Text))
            {
                query += "Nume = @Nume AND ";
            }
            if (!string.IsNullOrEmpty(prenumeField.Text))
            {
                query += "Prenume = @Prenume AND ";
            }
            if (!string.IsNullOrEmpty(adresaField.Text))
            {
                query += "Adresa = @Adresa AND ";
            }
            if (!string.IsNullOrEmpty(functieField.Text))
            {
                query += "Functie = @Functie AND ";
            }

            if (query.Length > 30)
            {
                query = query.Substring(0, query.Length - 5);
            }
            else
            {
                MessageBox.Show("Introduceți cel puțin o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(codaField.Text))
                {
                    command.Parameters.AddWithValue("@Coda", codaField.Text);
                }
                if (!string.IsNullOrEmpty(numeField.Text))
                {
                    command.Parameters.AddWithValue("@Nume", numeField.Text);
                }
                if (!string.IsNullOrEmpty(prenumeField.Text))
                {
                    command.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                }
                if (!string.IsNullOrEmpty(adresaField.Text))
                {
                    command.Parameters.AddWithValue("@Adresa", adresaField.Text);
                }
                if (!string.IsNullOrEmpty(functieField.Text))
                {
                    command.Parameters.AddWithValue("@Functie", functieField.Text);
                }

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Ștergere reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("DELETE");
                }
                else
                {
                    MessageBox.Show("Nu s-a găsit nicio înregistrare cu aceste valori.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Eroare la ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.Error.WriteLine(e.Message);
        }
    }

    private void PerformUpdate()
    {
        try
        {
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show("Introduceți cel puțin o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "UPDATE Angajati SET ";
            if (!string.IsNullOrEmpty(numeField.Text))
            {
                query += "Nume = @Nume, ";
            }
            if (!string.IsNullOrEmpty(prenumeField.Text))
            {
                query += "Prenume = @Prenume, ";
            }
            if (!string.IsNullOrEmpty(adresaField.Text))
            {
                query += "Adresa = @Adresa, ";
            }
            if (!string.IsNullOrEmpty(functieField.Text))
            {
                query += "Functie = @Functie, ";
            }

            if (query.Length > 17)
            {
                query = query.Substring(0, query.Length - 2);
            }
            else
            {
                MessageBox.Show("Introduceți minim o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            query += " WHERE Coda = @Coda";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(numeField.Text))
                {
                    command.Parameters.AddWithValue("@Nume", numeField.Text);
                }
                if (!string.IsNullOrEmpty(prenumeField.Text))
                {
                    command.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                }
                if (!string.IsNullOrEmpty(adresaField.Text))
                {
                    command.Parameters.AddWithValue("@Adresa", adresaField.Text);
                }
                if (!string.IsNullOrEmpty(functieField.Text))
                {
                    command.Parameters.AddWithValue("@Functie", functieField.Text);
                }
                command.Parameters.AddWithValue("@Coda", codaField.Text);

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Actualizare reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("UPDATE");
                }
                else
                {
                    MessageBox.Show("Nu s-a găsit nicio înregistrare cu acest indice.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Eroare la actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.Error.WriteLine(e.Message);
        }
    }

    private void LogOperation(string operationType)
    {
        try
        {
            string getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
            MySqlCommand maxKeyCommand = new MySqlCommand(getMaxKeyQuery, connection);
            int nextKey = 1;

            object result = maxKeyCommand.ExecuteScalar();
            if (result != DBNull.Value)
            {
                nextKey = Convert.ToInt32(result) + 1;
            }

            string query = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (@CodOp, @User, @Tabela, @Tip, @DataOra)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@CodOp", nextKey);
            command.Parameters.AddWithValue("@User", "Admin");
            command.Parameters.AddWithValue("@Tabela", "Angajati");
            command.Parameters.AddWithValue("@Tip", operationType);

            command.Parameters.AddWithValue("@DataOra", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            command.ExecuteNonQuery();

            Console.WriteLine("Cheia generată: " + nextKey);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Eroare la înregistrarea operației în jurnal.");
            Console.Error.WriteLine(e.Message);
        }
    }
}
