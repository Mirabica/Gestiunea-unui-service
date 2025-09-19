using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class VizualizareClientiAdmin : Form
{
    private TextBox codcliField;
    private TextBox numeField;
    private TextBox prenumeField;
    private TextBox adresaField;
    private TextBox telefonField;
    private TextBox textArea;
    private MySqlConnection connection;

    public VizualizareClientiAdmin()
    {
        ConnectToDatabase();
        Text = "Vizualizare clienti";
        Size = new Size(800, 600);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var backgroundPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackgroundImage = Image.FromFile(@"C:\\Users\\Mira\\RiderProjects\\WindowsFormsApp1\\Gestiunea_unui_service\\Imagini\\cli1.jpg"),
            BackgroundImageLayout = ImageLayout.Stretch
        };
        Controls.Add(backgroundPanel);

        var backButton = new Button
        {
            Text = "BACK",
            Bounds = new Rectangle(694, 525, 80, 34),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat
        };
        backButton.Click += (sender, e) =>
        {
            var admin = new Admin();
            admin.Show();
            Close();
        };
        backgroundPanel.Controls.Add(backButton);

        var vizualizareButton = new Button
        {
            Text = "Vizualizare",
            Bounds = new Rectangle(0, 30, 190, 38),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        vizualizareButton.Click += (sender, e) => PerformSearch();
        backgroundPanel.Controls.Add(vizualizareButton);

        var adaugaButton = new Button
        {
            Text = "Adaugare",
            Bounds = new Rectangle(200, 30, 190, 38),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        adaugaButton.Click += (sender, e) => PerformInsert();
        backgroundPanel.Controls.Add(adaugaButton);

        var stergeButton = new Button
        {
            Text = "Stergere",
            Bounds = new Rectangle(400, 30, 190, 38),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        stergeButton.Click += (sender, e) => PerformDelete();
        backgroundPanel.Controls.Add(stergeButton);

        var editareButton = new Button
        {
            Text = "Modificare",
            Bounds = new Rectangle(600, 30, 190, 38),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        editareButton.Click += (sender, e) => PerformUpdate();
        backgroundPanel.Controls.Add(editareButton);

        int frameWidth = 800;
        int horizontalGap = (frameWidth - 5 * 110) / 6;

        codcliField = CreateTextBox(horizontalGap, 120);
        backgroundPanel.Controls.Add(codcliField);

        numeField = CreateTextBox(2 * horizontalGap + 110, 120);
        backgroundPanel.Controls.Add(numeField);

        prenumeField = CreateTextBox(3 * horizontalGap + 2 * 110, 120);
        backgroundPanel.Controls.Add(prenumeField);

        adresaField = CreateTextBox(4 * horizontalGap + 3 * 110, 120);
        backgroundPanel.Controls.Add(adresaField);

        telefonField = CreateTextBox(5 * horizontalGap + 4 * 110, 120);
        backgroundPanel.Controls.Add(telefonField);

        var labelFont = new Font("Arial Black", 14, FontStyle.Bold);

        AddLabel(backgroundPanel, "Cod Cli", horizontalGap + 13, 90, labelFont);
        AddLabel(backgroundPanel, "Nume", 2 * horizontalGap + 131, 90, labelFont);
        AddLabel(backgroundPanel, "Prenume", 3 * horizontalGap + 2 * 112, 90, labelFont);
        AddLabel(backgroundPanel, "Adresa", 4 * horizontalGap + 3 * 114, 90, labelFont);
        AddLabel(backgroundPanel, "Telefon", 5 * horizontalGap + 4 * 113, 90, labelFont);

        textArea = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Bounds = new Rectangle(450, 332, 300, 190),
            BackColor = Color.FromArgb(255, 255, 204),
            ReadOnly = true,
            BorderStyle = BorderStyle.None
        };
        backgroundPanel.Controls.Add(textArea);
    }

    private TextBox CreateTextBox(int x, int y)
    {
        return new TextBox
        {
            Bounds = new Rectangle(x, y, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
    }

    private void AddLabel(Panel panel, string text, int x, int y, Font font)
    {
        panel.Controls.Add(new Label
        {
            Text = text,
            Bounds = new Rectangle(x, y, 110, 25),
            ForeColor = Color.Yellow,
            Font = font,
            BackColor = Color.Transparent,
        });
    }
 private void ConnectToDatabase()
    {
        try
        {
            string connectionString = "server=localhost;database=proiect_bd;uid=root;pwd=root;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la conectarea la baza de date: " + ex.Message);
        }
    }

    private bool AreAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(codcliField.Text) &&
               string.IsNullOrEmpty(numeField.Text) &&
               string.IsNullOrEmpty(prenumeField.Text) &&
               string.IsNullOrEmpty(adresaField.Text) &&
               string.IsNullOrEmpty(telefonField.Text);
    }

    private void PerformSearch()
    {
        try
        {
            textArea.Text = string.Empty;
            string query;
            if (AreAllFieldsEmpty())
            {
                query = "SELECT * FROM Clienti";
            }
            else
            {
                var whereClause = new System.Text.StringBuilder(" WHERE ");
                if (!string.IsNullOrEmpty(codcliField.Text)) whereClause.Append("Codcli = @Codcli AND ");
                if (!string.IsNullOrEmpty(numeField.Text)) whereClause.Append("Nume = @Nume AND ");
                if (!string.IsNullOrEmpty(prenumeField.Text)) whereClause.Append("Prenume = @Prenume AND ");
                if (!string.IsNullOrEmpty(adresaField.Text)) whereClause.Append("Adresa = @Adresa AND ");
                if (!string.IsNullOrEmpty(telefonField.Text)) whereClause.Append("Telefon = @Telefon AND ");
                whereClause.Length -= 5; // Remove last " AND "
                query = "SELECT * FROM Clienti" + whereClause;
            }

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(codcliField.Text)) cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);
                if (!string.IsNullOrEmpty(numeField.Text)) cmd.Parameters.AddWithValue("@Nume", numeField.Text);
                if (!string.IsNullOrEmpty(prenumeField.Text)) cmd.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                if (!string.IsNullOrEmpty(adresaField.Text)) cmd.Parameters.AddWithValue("@Adresa", adresaField.Text);
                if (!string.IsNullOrEmpty(telefonField.Text)) cmd.Parameters.AddWithValue("@Telefon", telefonField.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textArea.AppendText("Codcli: " + reader["Codcli"] + ", ");
                        textArea.AppendText("Nume: " + reader["Nume"] + ", ");
                        textArea.AppendText("Prenume: " + reader["Prenume"] + ", ");
                        textArea.AppendText("Adresa: " + reader["Adresa"] + ", ");
                        textArea.AppendText("Telefon: " + reader["Telefon"] + "\r\n");
                        textArea.AppendText("--------------\r\n");
                    }
                }
            }
            LogOperation("SELECT");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la efectuarea căutării: " + ex.Message);
        }
    }

    private void PerformInsert()
    {
        try
        {
            if (string.IsNullOrEmpty(codcliField.Text))
            {
                MessageBox.Show("Cod Angajat este obligatoriu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Clienti (Codcli, Nume, Prenume, Adresa, Telefon) VALUES (@Codcli, @Nume, @Prenume, @Adresa, @Telefon)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);
                cmd.Parameters.AddWithValue("@Nume", numeField.Text);
                cmd.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                cmd.Parameters.AddWithValue("@Adresa", adresaField.Text);
                cmd.Parameters.AddWithValue("@Telefon", telefonField.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

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
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la adăugare angajat: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            var queryBuilder = new System.Text.StringBuilder("DELETE FROM Clienti WHERE ");
            if (!string.IsNullOrEmpty(codcliField.Text)) queryBuilder.Append("Codcli = @Codcli AND ");
            if (!string.IsNullOrEmpty(numeField.Text)) queryBuilder.Append("Nume = @Nume AND ");
            if (!string.IsNullOrEmpty(prenumeField.Text)) queryBuilder.Append("Prenume = @Prenume AND ");
            if (!string.IsNullOrEmpty(adresaField.Text)) queryBuilder.Append("Adresa = @Adresa AND ");
            if (!string.IsNullOrEmpty(telefonField.Text)) queryBuilder.Append("Telefon = @Telefon AND ");
            queryBuilder.Length -= 5; // Remove last " AND "

            using (MySqlCommand cmd = new MySqlCommand(queryBuilder.ToString(), connection))
            {
                if (!string.IsNullOrEmpty(codcliField.Text)) cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);
                if (!string.IsNullOrEmpty(numeField.Text)) cmd.Parameters.AddWithValue("@Nume", numeField.Text);
                if (!string.IsNullOrEmpty(prenumeField.Text)) cmd.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                if (!string.IsNullOrEmpty(adresaField.Text)) cmd.Parameters.AddWithValue("@Adresa", adresaField.Text);
                if (!string.IsNullOrEmpty(telefonField.Text)) cmd.Parameters.AddWithValue("@Telefon", telefonField.Text);

                int affectedRows = cmd.ExecuteNonQuery();

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
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la ștergere: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            var queryBuilder = new System.Text.StringBuilder("UPDATE Clienti SET ");
            if (!string.IsNullOrEmpty(numeField.Text)) queryBuilder.Append("Nume = @Nume, ");
            if (!string.IsNullOrEmpty(prenumeField.Text)) queryBuilder.Append("Prenume = @Prenume, ");
            if (!string.IsNullOrEmpty(adresaField.Text)) queryBuilder.Append("Adresa = @Adresa, ");
            if (!string.IsNullOrEmpty(telefonField.Text)) queryBuilder.Append("Telefon = @Telefon, ");
            queryBuilder.Length -= 2; // Remove last ", "
            queryBuilder.Append(" WHERE Codcli = @Codcli");

            using (MySqlCommand cmd = new MySqlCommand(queryBuilder.ToString(), connection))
            {
                if (!string.IsNullOrEmpty(numeField.Text)) cmd.Parameters.AddWithValue("@Nume", numeField.Text);
                if (!string.IsNullOrEmpty(prenumeField.Text)) cmd.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                if (!string.IsNullOrEmpty(adresaField.Text)) cmd.Parameters.AddWithValue("@Adresa", adresaField.Text);
                if (!string.IsNullOrEmpty(telefonField.Text)) cmd.Parameters.AddWithValue("@Telefon", telefonField.Text);
                cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);

                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Actualizare reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("UPDATE");
                }
                else
                {
                    MessageBox.Show("Nu s-a găsit nicio înregistrare cu acest Coda.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la actualizare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LogOperation(string operationType)
    {
        try
        {
            string getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
            using (MySqlCommand maxKeyCommand = new MySqlCommand(getMaxKeyQuery, connection))
            {
                int nextKey = 1;
                using (MySqlDataReader maxKeyReader = maxKeyCommand.ExecuteReader())
                {
                    if (maxKeyReader.Read() && !maxKeyReader.IsDBNull(0))
                    {
                        nextKey = maxKeyReader.GetInt32(0) + 1;
                    }
                }

                string query = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (@CodOp, @User, @Tabela_folosita, @Tip_Operatie, @Data_Ora)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CodOp", nextKey);
                    cmd.Parameters.AddWithValue("@User", "Admin");
                    cmd.Parameters.AddWithValue("@Tabela_folosita", "Clienti");
                    cmd.Parameters.AddWithValue("@Tip_Operatie", operationType);
                    cmd.Parameters.AddWithValue("@Data_Ora", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la înregistrarea operației în jurnal: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
