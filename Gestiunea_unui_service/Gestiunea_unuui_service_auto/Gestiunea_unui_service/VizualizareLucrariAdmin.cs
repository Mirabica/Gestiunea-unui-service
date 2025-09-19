using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class VizualizareLucrariAdmin : Form
{
    private RichTextBox textArea;
    private MySqlConnection connection;

    private TextBox codlucField;
    private TextBox codaField;
    private TextBox datalucField;
    private TextBox costField;
    private TextBox tiplucField;
    private TextBox codmField;

    public VizualizareLucrariAdmin()
    {
        ConnectToDatabase();
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Text = "Vizualizare lucrari";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        Panel backgroundPanel = new Panel
        {
            Dock = DockStyle.Fill
        };
        backgroundPanel.Paint += (s, e) =>
        {
            Image backgroundImage = Image.FromFile(@"C:\\Users\\Mira\\RiderProjects\\WindowsFormsApp1\\Gestiunea_unui_service\\Imagini\\cli1.jpg");
            e.Graphics.DrawImage(backgroundImage, 0, 0, this.Width, this.Height);
        };
        this.Controls.Add(backgroundPanel);
        backgroundPanel.Controls.Clear();

        Button backButton = new Button
        {
            Text = "BACK",
            Bounds = new Rectangle(694, 525, 80, 34),
            BackColor = Color.FromArgb(105, 105, 105),
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold)
        };
        backButton.Click += (s, e) =>
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        };
        backgroundPanel.Controls.Add(backButton);

        Button crescatorButton = new Button
        {
            Text = "Crescator",
            Bounds = new Rectangle(24, 400, 140, 36),
            BackColor = Color.FromArgb(255, 127, 80),
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold)
        };
        crescatorButton.Click += (s, e) => PerformSearchCostAsc();
        backgroundPanel.Controls.Add(crescatorButton);

        Button descrescatorButton = new Button
        {
            Text = "Descrescator",
            Bounds = new Rectangle(24, 440, 140, 36),
            BackColor = Color.FromArgb(255, 127, 80),
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold)
        };
        descrescatorButton.Click += (s, e) => PerformSearchCostDesc();
        backgroundPanel.Controls.Add(descrescatorButton);

        Button vizualizareButton = new Button
        {
            Text = "Vizualizare",
            Bounds = new Rectangle(0, 30, 190, 38),
            BackColor = Color.Transparent,
            ForeColor = Color.White,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent ,
            }
        };
        vizualizareButton.FlatAppearance.BorderSize = 0;
        vizualizareButton.Click += (s, e) => PerformSearch();
        backgroundPanel.Controls.Add(vizualizareButton);

        Button adaugaButton = new Button
        {
            Text = "Adaugare",
            Bounds = new Rectangle(200, 30, 190, 38),
            BackColor = Color.Transparent,
            ForeColor = Color.White,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        adaugaButton.FlatAppearance.BorderSize = 0;
        adaugaButton.Click += (s, e) => PerformInsert();
        backgroundPanel.Controls.Add(adaugaButton);

        Button stergeButton = new Button
        {
            Text = "Stergere",
            Bounds = new Rectangle(400, 30, 190, 38),
            BackColor = Color.Transparent,
            ForeColor = Color.White,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent,
                MouseDownBackColor = Color.Transparent 
            }
        };
        stergeButton.FlatAppearance.BorderSize = 0;
        stergeButton.Click += (s, e) => PerformDelete();
        backgroundPanel.Controls.Add(stergeButton);

        Button editareButton = new Button
        {
            Text = "Modificare",
            Bounds = new Rectangle(600, 30, 190, 38),
            BackColor = Color.Transparent,
            ForeColor = Color.White,
            Font = new Font("Arial Black", 17, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        editareButton.FlatAppearance.BorderSize = 0;
        editareButton.Click += (s, e) => PerformUpdate();
        backgroundPanel.Controls.Add(editareButton);

        int frameWidth = 800;
        int horizontalGap = (frameWidth - 6 * 110) / 7;

        codlucField = new TextBox
        {
            Bounds = new Rectangle(horizontalGap, 120, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(codlucField);

        codaField = new TextBox
        {
            Bounds = new Rectangle(2 * horizontalGap + 110, 120, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(codaField);

        datalucField = new TextBox
        {
            Bounds = new Rectangle(3 * horizontalGap + 2 * 110, 120, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(datalucField);

        costField = new TextBox
        {
            Bounds = new Rectangle(4 * horizontalGap + 3 * 110, 120, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(costField);

        tiplucField = new TextBox
        {
            Bounds = new Rectangle(5 * horizontalGap + 4 * 110, 120, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(tiplucField);

        codmField = new TextBox
        {
            Bounds = new Rectangle(6 * horizontalGap + 5 * 110, 120, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(codmField);

        Font labelFont = new Font("Arial Black", 10, FontStyle.Bold);

        Label labelCodL = new Label
        {
            Text = "Cod Luc",
            Bounds = new Rectangle(horizontalGap + 20, 90, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent,
        };
        backgroundPanel.Controls.Add(labelCodL);

        Label labelCodAng = new Label
        {
            Text = "Cod Angajat",
            Bounds = new Rectangle(2 * horizontalGap + 114, 90, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent,
        };
        backgroundPanel.Controls.Add(labelCodAng);

        Label labelData = new Label
        {
            Text = "Data Luc",
            Bounds = new Rectangle(3 * horizontalGap + 2 * 119, 90, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent,
        };
        backgroundPanel.Controls.Add(labelData);

        Label labelCost = new Label
        {
            Text = "Cost",
            Bounds = new Rectangle(4 * horizontalGap + 3 * 120, 90, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent,
        };
        backgroundPanel.Controls.Add(labelCost);

        Label labelTip = new Label
        {
            Text = "Tip Luc",
            Bounds = new Rectangle(5 * horizontalGap + 4 * 116, 90, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent,
        };
        backgroundPanel.Controls.Add(labelTip);

        Label labelCodM = new Label
        {
            Text = "Cod Masina",
            Bounds = new Rectangle(6 * horizontalGap + 5 * 111, 90, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent,
        };
        backgroundPanel.Controls.Add(labelCodM);

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
            string connectionString = "server=localhost;user=root;database=proiect_bd;port=3306;password=root";
            connection = new MySqlConnection(connectionString);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
            }
            else
            {
                MessageBox.Show("Eroare la conectarea la baza de date.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la conectarea la baza de date: " + ex.Message);
        }
    }

    private void PerformSearch()
    {
        try
        {
            textArea.Clear();
            string query;
            if (AreAllFieldsEmpty())
            {
                query = "SELECT * FROM Lucrari";
            }
            else
            {
                string whereClause = " WHERE ";
                if (!string.IsNullOrEmpty(codlucField.Text)) whereClause += "Codluc = @Codluc AND ";
                if (!string.IsNullOrEmpty(codaField.Text)) whereClause += "Coda = @Coda AND ";
                if (!string.IsNullOrEmpty(datalucField.Text)) whereClause += "Dataluc = @Dataluc AND ";
                if (!string.IsNullOrEmpty(costField.Text)) whereClause += "Cost = @Cost AND ";
                if (!string.IsNullOrEmpty(tiplucField.Text)) whereClause += "Tipluc = @Tipluc AND ";
                if (!string.IsNullOrEmpty(codmField.Text)) whereClause += "Codm = @Codm AND ";

                whereClause = whereClause.Substring(0, whereClause.Length - 5);
                query = "SELECT * FROM Lucrari" + whereClause;
            }

            MySqlCommand command = new MySqlCommand(query, connection);

            if (!string.IsNullOrEmpty(codlucField.Text)) command.Parameters.AddWithValue("@Codluc", codlucField.Text);
            if (!string.IsNullOrEmpty(codaField.Text)) command.Parameters.AddWithValue("@Coda", codaField.Text);
            if (!string.IsNullOrEmpty(datalucField.Text)) command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
            if (!string.IsNullOrEmpty(costField.Text)) command.Parameters.AddWithValue("@Cost", costField.Text);
            if (!string.IsNullOrEmpty(tiplucField.Text)) command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
            if (!string.IsNullOrEmpty(codmField.Text)) command.Parameters.AddWithValue("@Codm", codmField.Text);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                textArea.AppendText("Codluc: " + reader["Codluc"] + ", ");
                textArea.AppendText("Coda: " + reader["Coda"] + ", ");
                textArea.AppendText("Dataluc: " + reader["Dataluc"] + ", ");
                textArea.AppendText("Cost: " + reader["Cost"] + ", ");
                textArea.AppendText("Tipluc: " + reader["Tipluc"] + ", ");
                textArea.AppendText("Codm: " + reader["Codm"] + "\n");
                textArea.AppendText("--------------\n");
            }
            reader.Close();
            LogOperation("SELECT");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la efectuarea căutării: " + ex.Message);
        }
    }

    private bool AreAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(codlucField.Text) &&
               string.IsNullOrEmpty(codaField.Text) &&
               string.IsNullOrEmpty(datalucField.Text) &&
               string.IsNullOrEmpty(costField.Text) &&
               string.IsNullOrEmpty(tiplucField.Text) &&
               string.IsNullOrEmpty(codmField.Text);
    }

    private void PerformInsert()
    {
        try
        {
            if (string.IsNullOrEmpty(codlucField.Text))
            {
                MessageBox.Show("Cod Angajat este obligatoriu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Lucrari (Codluc, Coda, Dataluc, Cost, Tipluc, Codm) VALUES (@Codluc, @Coda, @Dataluc, @Cost, @Tipluc, @Codm)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Codluc", codlucField.Text);
            command.Parameters.AddWithValue("@Coda", codaField.Text);
            command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
            command.Parameters.AddWithValue("@Cost", costField.Text);
            command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
            command.Parameters.AddWithValue("@Codm", codmField.Text);

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

            string query = "DELETE FROM Lucrari WHERE ";
            if (!string.IsNullOrEmpty(codlucField.Text)) query += "Codluc = @Codluc AND ";
            if (!string.IsNullOrEmpty(codaField.Text)) query += "Coda = @Coda AND ";
            if (!string.IsNullOrEmpty(datalucField.Text)) query += "Dataluc = @Dataluc AND ";
            if (!string.IsNullOrEmpty(costField.Text)) query += "Cost = @Cost AND ";
            if (!string.IsNullOrEmpty(tiplucField.Text)) query += "Tipluc = @Tipluc AND ";
            if (!string.IsNullOrEmpty(codmField.Text)) query += "Codm = @Codm AND ";

            query = query.Substring(0, query.Length - 5);

            MySqlCommand command = new MySqlCommand(query, connection);
            if (!string.IsNullOrEmpty(codlucField.Text)) command.Parameters.AddWithValue("@Codluc", codlucField.Text);
            if (!string.IsNullOrEmpty(codaField.Text)) command.Parameters.AddWithValue("@Coda", codaField.Text);
            if (!string.IsNullOrEmpty(datalucField.Text)) command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
            if (!string.IsNullOrEmpty(costField.Text)) command.Parameters.AddWithValue("@Cost", costField.Text);
            if (!string.IsNullOrEmpty(tiplucField.Text)) command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
            if (!string.IsNullOrEmpty(codmField.Text)) command.Parameters.AddWithValue("@Codm", codmField.Text);

            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                MessageBox.Show("Ștergere reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogOperation("DELETE");
            }
            else
            {
                MessageBox.Show("Nu s-a găsit nicio înregistrare cu valorile specificate.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            string query = "UPDATE Lucrari SET ";
            if (!string.IsNullOrEmpty(codaField.Text)) query += "Coda = @Coda, ";
            if (!string.IsNullOrEmpty(datalucField.Text)) query += "Dataluc = @Dataluc, ";
            if (!string.IsNullOrEmpty(costField.Text)) query += "Cost = @Cost, ";
            if (!string.IsNullOrEmpty(tiplucField.Text)) query += "Tipluc = @Tipluc, ";
            if (!string.IsNullOrEmpty(codmField.Text)) query += "Codm = @Codm, ";

            query = query.Substring(0, query.Length - 2);
            query += " WHERE Codluc = @Codluc";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Codluc", codlucField.Text);
            if (!string.IsNullOrEmpty(codaField.Text)) command.Parameters.AddWithValue("@Coda", codaField.Text);
            if (!string.IsNullOrEmpty(datalucField.Text)) command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
            if (!string.IsNullOrEmpty(costField.Text)) command.Parameters.AddWithValue("@Cost", costField.Text);
            if (!string.IsNullOrEmpty(tiplucField.Text)) command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
            if (!string.IsNullOrEmpty(codmField.Text)) command.Parameters.AddWithValue("@Codm", codmField.Text);

            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                MessageBox.Show("Actualizare reușită.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogOperation("UPDATE");
            }
            else
            {
                MessageBox.Show("Nu s-a găsit nicio înregistrare cu acest Cod Angajat.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la actualizare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PerformSearchCostAsc()
{
    try
    {
        textArea.Clear();

        if (AreAllFieldsEmpty())
        {
            MessageBox.Show("Introduceți cel puțin o valoare pentru căutare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string query = "SELECT l.Codluc, l.Coda, l.Dataluc, l.Cost, l.Tipluc, l.Codm " +
                       "FROM Lucrari l " +
                       "WHERE ";

        bool conditionAdded = false;

        if (!string.IsNullOrEmpty(codlucField.Text))
        {
            query += "l.Codluc = @Codluc";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(codaField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Coda = @Coda";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(datalucField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Dataluc = @Dataluc";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(costField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Cost >= @Cost";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(tiplucField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Tipluc = @Tipluc";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(codmField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Codm = @Codm";
            conditionAdded = true;
        }

        query += " ORDER BY l.Cost ASC";

        // Execute the query
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            int parameterIndex = 1;

            if (!string.IsNullOrEmpty(codlucField.Text))
            {
                command.Parameters.AddWithValue("@Codluc", codlucField.Text);
            }
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                command.Parameters.AddWithValue("@Coda", codaField.Text);
            }
            if (!string.IsNullOrEmpty(datalucField.Text))
            {
                command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
            }
            if (!string.IsNullOrEmpty(costField.Text))
            {
                command.Parameters.AddWithValue("@Cost", costField.Text);
            }
            if (!string.IsNullOrEmpty(tiplucField.Text))
            {
                command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
            }
            if (!string.IsNullOrEmpty(codmField.Text))
            {
                command.Parameters.AddWithValue("@Codm", codmField.Text);
            }

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                textArea.AppendText("Codluc: " + reader["Codluc"] + ", ");
                textArea.AppendText("Coda: " + reader["Coda"] + ", ");
                textArea.AppendText("Dataluc: " + reader["Dataluc"] + ", ");
                textArea.AppendText("Cost: " + reader["Cost"] + ", ");
                textArea.AppendText("Tipluc: " + reader["Tipluc"] + ", ");
                textArea.AppendText("Codm: " + reader["Codm"] + "\n");
                textArea.AppendText("--------------\n");
            }
            reader.Close();
            LogOperation("SELECT_ASC");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Eroare la căutare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

private void PerformSearchCostDesc()
{
    try
    {
        textArea.Clear();

        if (AreAllFieldsEmpty())
        {
            MessageBox.Show("Introduceți cel puțin o valoare pentru căutare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string query = "SELECT l.Codluc, l.Coda, l.Dataluc, l.Cost, l.Tipluc, l.Codm " +
                       "FROM Lucrari l " +
                       "WHERE ";

        bool conditionAdded = false;

        if (!string.IsNullOrEmpty(codlucField.Text))
        {
            query += "l.Codluc = @Codluc";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(codaField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Coda = @Coda";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(datalucField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Dataluc = @Dataluc";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(costField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Cost <= @Cost";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(tiplucField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Tipluc = @Tipluc";
            conditionAdded = true;
        }
        if (!string.IsNullOrEmpty(codmField.Text))
        {
            if (conditionAdded)
            {
                query += " AND ";
            }
            query += "l.Codm = @Codm";
            conditionAdded = true;
        }

        query += " ORDER BY l.Cost DESC";

        // Execute the query
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            int parameterIndex = 1;

            if (!string.IsNullOrEmpty(codlucField.Text))
            {
                command.Parameters.AddWithValue("@Codluc", codlucField.Text);
            }
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                command.Parameters.AddWithValue("@Coda", codaField.Text);
            }
            if (!string.IsNullOrEmpty(datalucField.Text))
            {
                command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
            }
            if (!string.IsNullOrEmpty(costField.Text))
            {
                command.Parameters.AddWithValue("@Cost", costField.Text);
            }
            if (!string.IsNullOrEmpty(tiplucField.Text))
            {
                command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
            }
            if (!string.IsNullOrEmpty(codmField.Text))
            {
                command.Parameters.AddWithValue("@Codm", codmField.Text);
            }

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                textArea.AppendText("Codluc: " + reader["Codluc"] + ", ");
                textArea.AppendText("Coda: " + reader["Coda"] + ", ");
                textArea.AppendText("Dataluc: " + reader["Dataluc"] + ", ");
                textArea.AppendText("Cost: " + reader["Cost"] + ", ");
                textArea.AppendText("Tipluc: " + reader["Tipluc"] + ", ");
                textArea.AppendText("Codm: " + reader["Codm"] + "\n");
                textArea.AppendText("--------------\n");
            }
            reader.Close();
            LogOperation("SELECT_DESC");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Eroare la căutare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        command.Parameters.AddWithValue("@Tabela", "Lucrari");
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