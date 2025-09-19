using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class VizualizareClientiA : Form
{
    private TextBox codcliField;
    private TextBox numeField;
    private TextBox prenumeField;
    private TextBox adresaField;
    private TextBox telefonField;
    private RichTextBox textArea;
    private MySqlConnection connection;

    public VizualizareClientiA()
    {
        ConnectToDatabase();
        Text = "Vizualizare clienti";
        Size = new Size(800, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;

        Panel backgroundPanel = new Panel()
        {
            BackgroundImage =
                Image.FromFile(
                    @"C:\\Users\\Mira\\RiderProjects\\WindowsFormsApp1\\Gestiunea_unui_service\\Imagini\\ang1.jpg"),
            BackgroundImageLayout = ImageLayout.Stretch,
            Size = ClientSize
        };
        Controls.Add(backgroundPanel);

        Button backButton = new Button();
        backButton.Text = "BACK";
        backButton.Size = new Size(80, 34);
        backButton.Location = new Point(694, 500);
        backButton.BackColor = Color.DarkGray;
        backButton.ForeColor = Color.Black;
        backButton.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backButton.Click += (sender, e) =>
        {
            Angajat angajat = new Angajat();
            angajat.Show();
            Hide();
        };
        backgroundPanel.Controls.Add(backButton);

        Button vizualizareButton = new Button();
        vizualizareButton.Text = "Vizualizare";
        vizualizareButton.Size = new Size(190, 36);
        vizualizareButton.Location = new Point(0, 30);
        vizualizareButton.BackColor = Color.Transparent;
        vizualizareButton.ForeColor = Color.White;
        vizualizareButton.Font = new Font("Arial Black", 17, FontStyle.Bold);
        vizualizareButton.FlatStyle = FlatStyle.Flat;
        vizualizareButton.FlatAppearance.BorderSize = 0;
        vizualizareButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        vizualizareButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
        vizualizareButton.Click += (sender, e) => PerformSearch();
        backgroundPanel.Controls.Add(vizualizareButton);

        Button adaugaButton = new Button();
        adaugaButton.Text = "Adaugare";
        adaugaButton.Size = new Size(190, 36);
        adaugaButton.Location = new Point(200, 30);
        adaugaButton.BackColor = Color.Transparent;
        adaugaButton.ForeColor = Color.White;
        adaugaButton.Font = new Font("Arial Black", 17, FontStyle.Bold);
        adaugaButton.FlatStyle = FlatStyle.Flat;
        adaugaButton.FlatAppearance.BorderSize = 0;
        adaugaButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        adaugaButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
        adaugaButton.Click += (sender, e) => PerformInsert();
        backgroundPanel.Controls.Add(adaugaButton);

        Button stergeButton = new Button();
        stergeButton.Text = "Stergere";
        stergeButton.Size = new Size(190, 36);
        stergeButton.Location = new Point(400, 30);
        stergeButton.BackColor = Color.Transparent;
        stergeButton.ForeColor = Color.White;
        stergeButton.Font = new Font("Arial Black", 17, FontStyle.Bold);
        stergeButton.FlatStyle = FlatStyle.Flat;
        stergeButton.FlatAppearance.BorderSize = 0;
        stergeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        stergeButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
        stergeButton.Click += (sender, e) => PerformDelete();
        backgroundPanel.Controls.Add(stergeButton);

        Button editareButton = new Button();
        editareButton.Text = "Modificare";
        editareButton.Size = new Size(190, 36);
        editareButton.Location = new Point(600, 30);
        editareButton.BackColor = Color.Transparent;
        editareButton.ForeColor = Color.White;
        editareButton.Font = new Font("Arial Black", 17, FontStyle.Bold);
        editareButton.FlatStyle = FlatStyle.Flat;
        editareButton.FlatAppearance.BorderSize = 0;
        editareButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        editareButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
        editareButton.Click += (sender, e) => PerformUpdate();
        backgroundPanel.Controls.Add(editareButton);

        int frameWidth = 800;
        int horizontalGap = (frameWidth - 5 * 110) / 6;

        codcliField = new TextBox();
        codcliField.Size = new Size(110, 25);
        codcliField.Location = new Point(horizontalGap, 140);
        codcliField.BackColor = Color.Gray;
        codcliField.ForeColor = Color.Black;
        codcliField.BorderStyle = BorderStyle.FixedSingle;
        backgroundPanel.Controls.Add(codcliField);

        numeField = new TextBox();
        numeField.Size = new Size(110, 25);
        numeField.Location = new Point(2 * horizontalGap + 110, 140);
        numeField.BackColor = Color.Gray;
        numeField.ForeColor = Color.Black;
        numeField.BorderStyle = BorderStyle.FixedSingle;
        backgroundPanel.Controls.Add(numeField);

        prenumeField = new TextBox();
        prenumeField.Size = new Size(110, 25);
        prenumeField.Location = new Point(3 * horizontalGap + 2 * 110, 140);
        prenumeField.BackColor = Color.Gray;
        prenumeField.ForeColor = Color.Black;
        prenumeField.BorderStyle = BorderStyle.FixedSingle;
        backgroundPanel.Controls.Add(prenumeField);

        adresaField = new TextBox();
        adresaField.Size = new Size(110, 25);
        adresaField.Location = new Point(4 * horizontalGap + 3 * 110, 140);
        adresaField.BackColor = Color.Gray;
        adresaField.ForeColor = Color.Black;
        adresaField.BorderStyle = BorderStyle.FixedSingle;
        backgroundPanel.Controls.Add(adresaField);

        telefonField = new TextBox();
        telefonField.Size = new Size(110, 25);
        telefonField.Location = new Point(5 * horizontalGap + 4 * 110, 140);
        telefonField.BackColor = Color.Gray;
        telefonField.ForeColor = Color.Black;
        telefonField.BorderStyle = BorderStyle.FixedSingle;
        backgroundPanel.Controls.Add(telefonField);

        Label labelCodProg = new Label();
        labelCodProg.Text = "Cod Cli";
        labelCodProg.Size = new Size(110, 25);
        labelCodProg.Location = new Point(horizontalGap + 21, 110);
        labelCodProg.BackColor = Color.Transparent;
        labelCodProg.ForeColor = Color.Yellow;
        labelCodProg.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backgroundPanel.Controls.Add(labelCodProg);

        Label labelCodCli = new Label();
        labelCodCli.Text = "Nume";
        labelCodCli.Size = new Size(110, 25);
        labelCodCli.Location = new Point(2 * horizontalGap + 137, 110);
        labelCodCli.BackColor = Color.Transparent;
        labelCodCli.ForeColor = Color.Yellow;
        labelCodCli.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backgroundPanel.Controls.Add(labelCodCli);

        Label labelCodM = new Label();
        labelCodM.Text = "Prenume";
        labelCodM.Size = new Size(110, 25);
        labelCodM.Location = new Point(3 * horizontalGap + 2 * 116, 110);
        labelCodM.BackColor = Color.Transparent;
        labelCodM.ForeColor = Color.Yellow;
        labelCodM.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backgroundPanel.Controls.Add(labelCodM);

        Label labelData = new Label();
        labelData.Text = "Adresa";
        labelData.Size = new Size(110, 25);
        labelData.Location = new Point(4 * horizontalGap + 3 * 116, 110);
        labelData.BackColor = Color.Transparent;
        labelData.ForeColor = Color.Yellow;
        labelData.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backgroundPanel.Controls.Add(labelData);

        Label labelOra = new Label();
        labelOra.Text = "Telefon";
        labelOra.Size = new Size(110, 25);
        labelOra.Location = new Point(5 * horizontalGap + 4 * 115, 110);
        labelOra.BackColor = Color.Transparent;
        labelOra.ForeColor = Color.Yellow;
        labelOra.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backgroundPanel.Controls.Add(labelOra);

        textArea = new RichTextBox();
        textArea.Size = new Size(765, 240);
        textArea.Location = new Point(10, 230);
        textArea.BackColor = Color.FromArgb(255, 255, 204);
        textArea.ReadOnly = true;
        backgroundPanel.Controls.Add(textArea);
    }

    private void ConnectToDatabase()
    {
        try
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=proiect_bd;User ID=root;Password=root";
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
        catch (SqlException ex)
        {
            Console.Error.WriteLine("Eroare la conectarea la baza de date: " + ex.Message);
        }
    }

    private void PerformSearch()
    {
        try
        {
            textArea.Text = "";
            string query = "SELECT * FROM Clienti";
            MySqlCommand command = new MySqlCommand(query, connection);

            if (!string.IsNullOrEmpty(codcliField.Text))
            {
                query += " WHERE Codcli = @Codcli";
                command.Parameters.AddWithValue("@Codcli", codcliField.Text);
            }

            if (!string.IsNullOrEmpty(numeField.Text))
            {
                query += " AND Nume = @Nume";
                command.Parameters.AddWithValue("@Nume", numeField.Text);
            }

            if (!string.IsNullOrEmpty(prenumeField.Text))
            {
                query += " AND Prenume = @Prenume";
                command.Parameters.AddWithValue("@Prenume", prenumeField.Text);
            }

            if (!string.IsNullOrEmpty(adresaField.Text))
            {
                query += " AND Adresa = @Adresa";
                command.Parameters.AddWithValue("@Adresa", adresaField.Text);
            }

            if (!string.IsNullOrEmpty(telefonField.Text))
            {
                query += " AND Telefon = @Telefon";
                command.Parameters.AddWithValue("@Telefon", telefonField.Text);
            }

            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                textArea.AppendText($"Codcli: {reader["Codcli"]}, ");
                textArea.AppendText($"Nume: {reader["Nume"]}, ");
                textArea.AppendText($"Prenume: {reader["Prenume"]}, ");
                textArea.AppendText($"Adresa: {reader["Adresa"]}, ");
                textArea.AppendText($"Telefon: {reader["Telefon"]}\n");
                textArea.AppendText("--------------\n");
            }
            reader.Close();
            LogOperation("SELECT");
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine("Eroare la efectuarea căutării: " + ex.Message);
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

    private void PerformInsert()
    {
        try
        {
            // Verificăm dacă codc este introdus
            if (string.IsNullOrEmpty(codcliField.Text))
            {
                MessageBox.Show(this, "Cod Cli este obligatoriu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            // Creează instrucțiunea SQL de inserare
            string query = "INSERT INTO Clienti (Codcli, Nume, Prenume, Adresa, Telefon) VALUES (@Codcli, @Nume, @Prenume, @Adresa, @Telefon)";

            // Execută instrucțiunea SQL
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Codcli", codcliField.Text);
                command.Parameters.AddWithValue("@Nume", numeField.Text);
                command.Parameters.AddWithValue("@Prenume", prenumeField.Text);
                command.Parameters.AddWithValue("@Adresa", adresaField.Text);
                command.Parameters.AddWithValue("@Telefon", telefonField.Text);

                
                int rowsAffected = command.ExecuteNonQuery();
               

                if (rowsAffected > 0)
                {
                    MessageBox.Show(this, "Client adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("INSERT");
                }
                else
                {
                    MessageBox.Show(this, "Eroare la adăugare client.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Eroare la adăugare client: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PerformDelete()
    {
        try
        {
            // Verifică dacă oricare dintre câmpuri este completat
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show(this, "Introduceți minim o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construiește interogarea de ștergere
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM Clienti WHERE ");
            if (!string.IsNullOrEmpty(codcliField.Text))
            {
                queryBuilder.Append("Codcli = @Codcli AND ");
            }
            if (!string.IsNullOrEmpty(numeField.Text))
            {
                queryBuilder.Append("Nume = @Nume AND ");
            }
            if (!string.IsNullOrEmpty(prenumeField.Text))
            {
                queryBuilder.Append("Prenume = @Prenume AND ");
            }
            if (!string.IsNullOrEmpty(adresaField.Text))
            {
                queryBuilder.Append("Adresa = @Adresa AND ");
            }
            if (!string.IsNullOrEmpty(telefonField.Text))
            {
                queryBuilder.Append("Telefon = @Telefon AND ");
            }

            // Elimină "AND" din ultima parte a șirului
            if (queryBuilder.Length > 30)
            {
                queryBuilder.Length -= 5;
            }
            else
            {
                // Nicio condiție, nu se poate efectua ștergerea
                MessageBox.Show(this, "Introduceți cel puțin o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = queryBuilder.ToString();

            // Execută interogarea
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(codcliField.Text))
                {
                    command.Parameters.AddWithValue("@Codcli", codcliField.Text);
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
                if (!string.IsNullOrEmpty(telefonField.Text))
                {
                    command.Parameters.AddWithValue("@Telefon", telefonField.Text);
                }

                
                int affectedRows = command.ExecuteNonQuery();
                

                if (affectedRows > 0)
                {
                    MessageBox.Show(this, "Ștergere reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("DELETE");
                }
                else
                {
                    MessageBox.Show(this, "Nu s-a găsit nicio înregistrare cu aceste valori.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Eroare la ștergere: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PerformUpdate()
    {
        try
        {
            // Verifică dacă cel puțin un câmp este completat
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show(this, "Introduceți minim o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construiește interogarea de actualizare
            StringBuilder queryBuilder = new StringBuilder("UPDATE Clienti SET ");
            if (!string.IsNullOrEmpty(numeField.Text))
            {
                queryBuilder.Append("Nume = @Nume, ");
            }
            if (!string.IsNullOrEmpty(prenumeField.Text))
            {
                queryBuilder.Append("Prenume = @Prenume, ");
            }
            if (!string.IsNullOrEmpty(adresaField.Text))
            {
                queryBuilder.Append("Adresa = @Adresa, ");
            }
            if (!string.IsNullOrEmpty(telefonField.Text))
            {
                queryBuilder.Append("Telefon = @Telefon, ");
            }

            // Elimină ", " din ultima parte a șirului
            if (queryBuilder.Length > 17)
            {
                queryBuilder.Length -= 2;
            }
            else
            {
                // Niciun câmp pentru actualizare, nu se poate efectua actualizarea
                MessageBox.Show(this, "Introduceți cel puțin o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Adaugă condiția WHERE pentru a specifica rândul care trebuie actualizat
            queryBuilder.Append(" WHERE Codcli = @Codcli");
            string query = queryBuilder.ToString();

            // Execută interogarea
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
                if (!string.IsNullOrEmpty(telefonField.Text))
                {
                    command.Parameters.AddWithValue("@Telefon", telefonField.Text);
                }

                // Adaugă valoarea pentru condiția WHERE (CodClient)
                command.Parameters.AddWithValue("@Codcli", codcliField.Text);

                
                int affectedRows = command.ExecuteNonQuery();
               

                if (affectedRows > 0)
                {
                    MessageBox.Show(this, "Actualizare reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("UPDATE");
                }
                else
                {
                    MessageBox.Show(this, "Nu s-a găsit nicio înregistrare cu acest Codcli.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Eroare la actualizare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LogOperation(string operationType)
    {
        try
        {
            // Obțineți cel mai mare număr de cheie (CodOp) din baza de date
            string getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
            MySqlCommand maxKeyCommand = new MySqlCommand(getMaxKeyQuery, connection);

            
            int nextKey = 1; // Dacă nu există chei în baza de date, începeți de la 1
            object result = maxKeyCommand.ExecuteScalar();
            if (result != DBNull.Value)
            {
                nextKey = Convert.ToInt32(result) + 1;
            }
            

            // Utilizați următoarea cheie disponibilă pentru a efectua operația de inserare
            string query = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (@CodOp, @User, @Tabela_folosita, @Tip_Operatie, @Data_Ora)";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CodOp", nextKey);
                command.Parameters.AddWithValue("@User", "Angajati");
                command.Parameters.AddWithValue("@Tabela_folosita", "Clienti");
                command.Parameters.AddWithValue("@Tip_Operatie", operationType);
                command.Parameters.AddWithValue("@Data_Ora", DateTime.Now);

                
                command.ExecuteNonQuery();

                Console.WriteLine("Cheia generată: " + nextKey);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare la înregistrarea operației în jurnal: " + ex.Message);
        }
    }
}