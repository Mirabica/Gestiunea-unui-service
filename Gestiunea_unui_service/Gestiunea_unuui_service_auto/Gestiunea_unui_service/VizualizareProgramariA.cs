using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using MySql.Data.MySqlClient;


public class VizualizareProgramariA : Form
{
    private RichTextBox richTextBox;
    private MySqlConnection connection;
    private TextBox codprogField;
    private TextBox codcliField;
    private TextBox codmField;
    private TextBox dataField;
    private TextBox oraField;

    public VizualizareProgramariA()
    {
        ConnectToDatabase();
        Text = "Vizualizare programari";
        Size = new Size(800, 600);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;

        

        var backgroundPanel = new Panel
        {
            Size = ClientSize,
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\ang1.jpg"),
            BackgroundImageLayout = ImageLayout.Stretch
        };
        Controls.Add(backgroundPanel);

        Button backButton = new Button();
        backButton.Text = "BACK";
        backButton.Bounds = new Rectangle(694, 500, 80, 34);
        backButton.TabStop = false;
        backButton.BackColor = Color.FromArgb(105, 105, 105);
        backButton.ForeColor = Color.Black;
        Font buttonFont = new Font("Arial Black", 12, FontStyle.Bold);
        backButton.Font = buttonFont;
    
    backButton.Click += (sender, e) => {
    Angajat angajat = new Angajat();
    angajat.Show();
    this.Hide();
};
    
    backgroundPanel.Controls.Add(backButton);
 
    Button vizualizareButton = new Button();
    vizualizareButton.Text = "Vizualizare";
    vizualizareButton.Bounds = new Rectangle(0, 30, 190, 44);
    vizualizareButton.TabStop = false;
    vizualizareButton.FlatStyle = FlatStyle.Flat;
    vizualizareButton.FlatAppearance.BorderSize = 0;
    vizualizareButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
    vizualizareButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
    vizualizareButton.BackColor = Color.Transparent;
    vizualizareButton.ForeColor = Color.White;
    Font buttonFont4 = new Font("Arial Black", 20, FontStyle.Bold);
    vizualizareButton.Font = buttonFont4;

     
    vizualizareButton.Click += (sender, e) => {
        PerformSearch();
    };
    
    backgroundPanel.Controls.Add(vizualizareButton);
     
    Button adaugaButton = new Button();
    adaugaButton.Text = "Adaugare";
    adaugaButton.Bounds = new Rectangle(200, 30, 190, 44);
    adaugaButton.TabStop = false;
    adaugaButton.FlatStyle = FlatStyle.Flat;
    adaugaButton.FlatAppearance.BorderSize = 0;
    adaugaButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
    adaugaButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
    adaugaButton.BackColor = Color.Transparent;
    adaugaButton.ForeColor = Color.White;
    adaugaButton.Font = buttonFont4;
     
    adaugaButton.Click += (sender, e) => {
        PerformInsert();
    };
    backgroundPanel.Controls.Add(adaugaButton);
    
    Button stergeButton = new Button();
    stergeButton.Text = "Stergere";
    stergeButton.Bounds = new Rectangle(400, 30, 190, 44);
    stergeButton.TabStop = false;
    stergeButton.FlatStyle = FlatStyle.Flat;
    stergeButton.FlatAppearance.BorderSize = 0;
    stergeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
    stergeButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
    stergeButton.BackColor = Color.Transparent;
    stergeButton.ForeColor = Color.White;
    stergeButton.Font = buttonFont4;
     
    stergeButton.Click += (sender, e) => {
        PerformDelete();
    };
    
    backgroundPanel.Controls.Add(stergeButton);
     
    Button editareButton = new Button();
    editareButton.Text = "Modificare";
    editareButton.Bounds = new Rectangle(600, 30, 190, 44);
    editareButton.TabStop = false;
    editareButton.FlatStyle = FlatStyle.Flat;
    editareButton.FlatAppearance.BorderSize = 0;
    editareButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
    editareButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
    editareButton.BackColor = Color.Transparent;
    editareButton.ForeColor = Color.White;
    editareButton.Font = buttonFont4;
     
    editareButton.Click += (sender, e) => {
        PerformUpdate();
    };
    backgroundPanel.Controls.Add(editareButton);
     
    int frameWidth = 800;
    int horizontalGap = (frameWidth - 5 * 110) / 6;
     
    codprogField = new TextBox();
    codprogField.Size = new Size(110, 25);
    codprogField.Location = new Point(horizontalGap, 140);
    codprogField.BackColor = Color.Gray;
    codprogField.ForeColor = Color.Black;
    codprogField.BorderStyle = BorderStyle.FixedSingle;
    backgroundPanel.Controls.Add(codprogField);
     
    codcliField = new TextBox();
    codcliField.Size = new Size(110, 25);
    codcliField.Location = new Point(2 * horizontalGap + 110, 140);
    codcliField.BackColor = Color.Gray;
    codcliField.ForeColor = Color.Black;
    codcliField.BorderStyle = BorderStyle.FixedSingle;
    backgroundPanel.Controls.Add(codcliField);
     
    codmField = new TextBox();
    codmField.Size = new Size(110, 25);
    codmField.Location = new Point(3 * horizontalGap + 2 * 110, 140);
    codmField.BackColor = Color.Gray;
    codmField.ForeColor = Color.Black;
    codmField.BorderStyle = BorderStyle.FixedSingle;
    backgroundPanel.Controls.Add(codmField);
     
    dataField = new TextBox();
    dataField.Size = new Size(110, 25);
    dataField.Location = new Point(4 * horizontalGap + 3 * 110, 140);
    dataField.BackColor = Color.Gray;
    dataField.ForeColor = Color.Black;
    dataField.BorderStyle = BorderStyle.FixedSingle;
    backgroundPanel.Controls.Add(dataField);
     
    oraField = new TextBox();
    oraField.Size = new Size(110, 25);
    oraField.Location = new Point(5 * horizontalGap + 4 * 110, 140);
    oraField.BackColor = Color.Gray;
    oraField.ForeColor = Color.Black;
    oraField.BorderStyle = BorderStyle.FixedSingle;
    backgroundPanel.Controls.Add(oraField);
     
    Font labelFont = new Font("Arial Black", 11, FontStyle.Bold);
     
    Label labelCodProg = new Label();
    labelCodProg.Text = "Cod Prog";
    labelCodProg.Size = new Size(110, 25);
    labelCodProg.Location = new Point(horizontalGap + 14, 110);
    labelCodProg.ForeColor = Color.Yellow;
    labelCodProg.BackColor = Color.Transparent;
    labelCodProg.Font = labelFont;
    backgroundPanel.Controls.Add(labelCodProg);
     
    Label labelCodCli = new Label();
    labelCodCli.Text = "Cod Client";
    labelCodCli.Size = new Size(110, 25);
    labelCodCli.Location = new Point(2 * horizontalGap + 118, 110);
    labelCodCli.ForeColor = Color.Yellow;
    labelCodCli.BackColor = Color.Transparent;
    labelCodCli.Font = labelFont;
    backgroundPanel.Controls.Add(labelCodCli);
     
    Label labelCodM = new Label();
    labelCodM.Text = "Cod Masina";
    labelCodM.Size = new Size(110, 25);
    labelCodM.Location = new Point(3 * horizontalGap + 2 * 110, 110);
    labelCodM.ForeColor = Color.Yellow;
    labelCodM.BackColor = Color.Transparent;
    labelCodM.Font = labelFont;
    backgroundPanel.Controls.Add(labelCodM);
     
    Label labelData = new Label();
    labelData.Text = "Data";
    labelData.Size = new Size(110, 25);
    labelData.Location = new Point(4 * horizontalGap + 3 * 120, 110);
    labelData.ForeColor = Color.Yellow;
    labelData.BackColor = Color.Transparent;
    labelData.Font = labelFont;
    backgroundPanel.Controls.Add(labelData);
     
    Label labelOra = new Label();
    labelOra.Text = "Ora";
    labelOra.Size = new Size(110, 25);
    labelOra.Location = new Point(5 * horizontalGap + 4 * 118, 110);
    labelOra.ForeColor = Color.Yellow;
    labelOra.BackColor = Color.Transparent;
    labelOra.Font = labelFont;
    backgroundPanel.Controls.Add(labelOra);
     
    richTextBox = new RichTextBox();
    richTextBox.Size = new Size(765, 240);
    richTextBox.Location = new Point(10, 230);
    richTextBox.BackColor = Color.FromArgb(255, 255, 204);
    richTextBox.ReadOnly = true;
    richTextBox.BorderStyle = BorderStyle.None;
    backgroundPanel.Controls.Add(richTextBox);
    }
     private void ConnectToDatabase()
    {
        try
        {
            // Configuring database connection (change URL, USER, and PASSWORD according to your settings)
            string connectionString = "Server=localhost;Database=proiect_bd;Uid=root;Pwd=root;";
            connection = new MySqlConnection(connectionString);

            connection.Open();
            Console.WriteLine("Connected to Database");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

   private void PerformSearch()
    {
    try
    {
        richTextBox.Clear(); // Curăță richTextBox înainte de a afișa noile rezultate

        string query;
        if (AreAllFieldsEmpty())
        {
            // Dacă toate câmpurile sunt goale, selectează toate înregistrările
            query = "SELECT * FROM Programari";
            Console.WriteLine("Selectare toate înregistrările: " + query);
        }
        else
        {
            // Construiește interogarea pe baza conținutului câmpurilor de text
            StringBuilder whereClause = new StringBuilder(" WHERE ");
            if (!string.IsNullOrEmpty(codprogField.Text))
            {
                whereClause.Append("Codprog = @Codprog AND ");

            }

            if (!string.IsNullOrEmpty(codcliField.Text))
            {
                whereClause.Append("Codcli = @Codcli AND ");
            }

            if (!string.IsNullOrEmpty(codmField.Text))
            {
                whereClause.Append("Codm = @Codm AND ");
            }

            if (!string.IsNullOrEmpty(dataField.Text))
            {
                whereClause.Append("Data = @Data AND ");
            }

            if (!string.IsNullOrEmpty(oraField.Text))
            {
                whereClause.Append("Ora = @Ora AND ");
            }

            // Elimină ultimul "AND" de la sfârșitul șirului
            if (whereClause.Length > 5)
            {
                whereClause.Length -= 5; // Elimină " AND " de la sfârșit
                query = "SELECT * FROM Programari" + whereClause;
                Console.WriteLine("Selectare cu condiții: " + query);
            }
            else
            {
                // Fără condiții, așa că setează interogarea pentru a selecta tot
                query = "SELECT * FROM Programari";
                Console.WriteLine("Selectare toate înregistrările: " + query);
            }
        }

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
                if (!string.IsNullOrEmpty(codprogField.Text))
                {

                    cmd.Parameters.AddWithValue("@Codprog", codprogField.Text);
                }

                if (!string.IsNullOrEmpty(codcliField.Text))
                {

                    cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);
                }

                if (!string.IsNullOrEmpty(codmField.Text))
                {

                    cmd.Parameters.AddWithValue("@Codm", codmField.Text);
                }

                if (!string.IsNullOrEmpty(dataField.Text))
                {

                    cmd.Parameters.AddWithValue("@Data", dataField.Text);
                }

                if (!string.IsNullOrEmpty(oraField.Text))
                {

                    cmd.Parameters.AddWithValue("@Ora", oraField.Text);
                }


                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        // Afișează rezultatele în richTextBox
                        richTextBox.AppendText("Codprog: " + reader["Codprog"] + ", ");
                        richTextBox.AppendText("Codcli: " + reader["Codcli"] + ", ");
                        richTextBox.AppendText("Codm: " + reader["Codm"] + ", ");
                        richTextBox.AppendText("Data: " + reader["Data"] + ", ");
                        richTextBox.AppendText("Ora: " + reader["Ora"] + "\n");
                        richTextBox.AppendText("--------------\n");

                    }
                }
        }

        LogOperation("SELECT");
    }
    catch (MySqlException ex)
    {
        Console.WriteLine("Eroare la efectuarea căutării: " + ex.Message);
    }
    }


    private bool AreAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(codprogField.Text) && string.IsNullOrEmpty(codcliField.Text)
                && string.IsNullOrEmpty(codmField.Text) && string.IsNullOrEmpty(dataField.Text)
                && string.IsNullOrEmpty(oraField.Text);
    }
    private void PerformInsert()
{
    try
    {
        if (string.IsNullOrEmpty(codprogField.Text))
        {
            MessageBox.Show("Cod Angajat este obligatoriu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string query = "INSERT INTO Programari (Codprog, Codcli, Codm, Data, Ora) VALUES (@Codprog, @Codcli, @Codm, @Data, @Ora)";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Codprog", codprogField.Text);
        cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);
        cmd.Parameters.AddWithValue("@Codm", codmField.Text);
        cmd.Parameters.AddWithValue("@Data", dataField.Text);
        cmd.Parameters.AddWithValue("@Ora", oraField.Text);

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
    catch (MySqlException ex)
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

        StringBuilder queryBuilder = new StringBuilder("DELETE FROM Programari WHERE ");
        MySqlCommand cmd = new MySqlCommand("", connection);

        if (!string.IsNullOrEmpty(codprogField.Text))
        {
            queryBuilder.Append("Codprog = @Codprog AND ");
            cmd.Parameters.AddWithValue("@Codprog", codprogField.Text);
        }
        if (!string.IsNullOrEmpty(codcliField.Text))
        {
            queryBuilder.Append("Codcli = @Codcli AND ");
            cmd.Parameters.AddWithValue("@Codcli", codcliField.Text);
        }
        if (!string.IsNullOrEmpty(codmField.Text))
        {
            queryBuilder.Append("Codm = @Codm AND ");
            cmd.Parameters.AddWithValue("@Codm", codmField.Text);
        }
        if (!string.IsNullOrEmpty(dataField.Text))
        {
            queryBuilder.Append("Data = @Data AND ");
            cmd.Parameters.AddWithValue("@Data", dataField.Text);
        }
        if (!string.IsNullOrEmpty(oraField.Text))
        {
            queryBuilder.Append("Ora = @Ora AND ");
            cmd.Parameters.AddWithValue("@Ora", oraField.Text);
        }

        if (queryBuilder.Length > 30)
        {
            queryBuilder.Length -= 5;
            cmd.CommandText = queryBuilder.ToString();
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Ștergere reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogOperation("DELETE");
            }
            else
            {
                MessageBox.Show("Nu s-a găsit nicio înregistrare cu aceste valori.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Introduceți cel puțin o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (MySqlException ex)
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

        StringBuilder queryBuilder = new StringBuilder("UPDATE Programari SET ");
        MySqlCommand cmd = new MySqlCommand("", connection);

        if (!string.IsNullOrEmpty(codcliField.Text))
        {
            queryBuilder.Append("Dataluc = @Dataluc, ");
            cmd.Parameters.AddWithValue("@Dataluc", codcliField.Text);
        }
        if (!string.IsNullOrEmpty(codmField.Text))
        {
            queryBuilder.Append("Codm = @Codm, ");
            cmd.Parameters.AddWithValue("@Codm", codmField.Text);
        }
        if (!string.IsNullOrEmpty(dataField.Text))
        {
            queryBuilder.Append("Data = @Data, ");
            cmd.Parameters.AddWithValue("@Data", dataField.Text);
        }
        if (!string.IsNullOrEmpty(oraField.Text))
        {
            queryBuilder.Append("Ora = @Ora, ");
            cmd.Parameters.AddWithValue("@Ora", oraField.Text);
        }

        if (queryBuilder.Length > 17)
        {
            queryBuilder.Length -= 2;
            queryBuilder.Append(" WHERE Codprog = @Codprog");
            cmd.CommandText = queryBuilder.ToString();
            cmd.Parameters.AddWithValue("@Codprog", codprogField.Text);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Actualizare reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogOperation("UPDATE");
            }
            else
            {
                MessageBox.Show("Nu s-a găsit nicio înregistrare cu acest Cod.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Introduceți cel puțin o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (MySqlException ex)
    {
        MessageBox.Show("Eroare la actualizare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

    private void LogOperation(string operationType)
    {
        try
        {
            // Get the maximum key (CodOp) from the database
            string getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
            MySqlCommand maxKeyCommand = new MySqlCommand(getMaxKeyQuery, connection);
            int nextKey = 1;

            MySqlDataReader maxKeyReader = maxKeyCommand.ExecuteReader();
            if (maxKeyReader.Read())
            {
                nextKey = maxKeyReader.GetInt32(0) + 1;
            }
            maxKeyReader.Close();

            // Use the next available key to perform the insert operation
            string insertQuery = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (@CodOp, @User, @Tabela_folosita, @Tip_Operatie, @Data_Ora)";
            MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@CodOp", nextKey);
            insertCommand.Parameters.AddWithValue("@User", "Angajat");
            insertCommand.Parameters.AddWithValue("@Tabela_folosita", "Programari");
            insertCommand.Parameters.AddWithValue("@Tip_Operatie", operationType);
            insertCommand.Parameters.AddWithValue("@Data_Ora", DateTime.Now);

            insertCommand.ExecuteNonQuery();

            Console.WriteLine("Generated Key: " + nextKey);
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error logging operation: " + ex.Message);
        }
    }

}