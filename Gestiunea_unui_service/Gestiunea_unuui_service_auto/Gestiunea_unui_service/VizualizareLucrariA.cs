using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

public class VizualizareLucrariA : Form
{
    private TextBox codlucField;
    private TextBox codaField;
    private TextBox datalucField;
    private TextBox costField;
    private TextBox tiplucField;
    private TextBox codmField;
    private RichTextBox textArea;
    private MySqlConnection connection;

    public VizualizareLucrariA()
    {
        ConnectToDatabase();
        Text = "Vizualizare lucrari";
        Size = new Size(800, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;

        Panel backgroundPanel = new Panel();
        Controls.Add(backgroundPanel);
        backgroundPanel.Dock = DockStyle.Fill;
        backgroundPanel.Paint += (sender, e) =>
        {
            Image backgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\ang1.jpg");
            e.Graphics.DrawImage(backgroundImage, 0, 0, Width, Height);
        };

        Button backButton = new Button();
        backgroundPanel.Controls.Add(backButton);
        backButton.Text = "BACK";
        backButton.Size = new Size(80, 34);
        backButton.Location = new Point(694, 500);
        backButton.FlatStyle = FlatStyle.Flat;
        backButton.BackColor = Color.DimGray;
        backButton.ForeColor = Color.Black;
        backButton.Font = new Font("Arial Black", 12, FontStyle.Bold);
        backButton.Click += (sender, e) =>
        {
            Angajat angajat = new Angajat();
            angajat.Show();
            Close();
        };

        Button CrescatorButton = new Button();
        backgroundPanel.Controls.Add(CrescatorButton);
        CrescatorButton.Text = "Crescator";
        CrescatorButton.Size = new Size(140, 34);
        CrescatorButton.Location = new Point(30, 500);
        CrescatorButton.FlatStyle = FlatStyle.Flat;
        CrescatorButton.BackColor = Color.Coral;
        CrescatorButton.ForeColor = Color.Black;
        CrescatorButton.Font = new Font("Arial Black", 11, FontStyle.Bold);
        CrescatorButton.Click += (sender, e) => { PerformSearchCostAsc(); };

        Button DescrescatorButton = new Button();
        backgroundPanel.Controls.Add(DescrescatorButton);
        DescrescatorButton.Text = "Descrescator";
        DescrescatorButton.Size = new Size(140, 34);
        DescrescatorButton.Location = new Point(185, 500);
        DescrescatorButton.FlatStyle = FlatStyle.Flat;
        DescrescatorButton.BackColor = Color.Coral;
        DescrescatorButton.ForeColor = Color.Black;
        DescrescatorButton.Font = new Font("Arial Black", 11, FontStyle.Bold);
        DescrescatorButton.Click += (sender, e) => { PerformSearchCostDesc(); };

        Button vizualizareButton = new Button();
        backgroundPanel.Controls.Add(vizualizareButton);
        vizualizareButton.Text = "Vizualizare";
        vizualizareButton.Size = new Size(190, 38);
        vizualizareButton.Location = new Point(0, 30);
        vizualizareButton.FlatStyle = FlatStyle.Flat;
        vizualizareButton.BackColor = Color.Transparent;
        vizualizareButton.ForeColor = Color.White;
        vizualizareButton.FlatAppearance.BorderSize = 0;
        vizualizareButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        vizualizareButton.FlatAppearance.MouseDownBackColor = Color.Transparent; 
            
        vizualizareButton.Font = new Font("Arial Black", 16, FontStyle.Bold);
        vizualizareButton.Click += (sender, e) => { PerformSearch(); };

        Button adaugaButton = new Button();
        backgroundPanel.Controls.Add(adaugaButton);
        adaugaButton.Text = "Adaugare";
        adaugaButton.Size = new Size(190, 38);
        adaugaButton.Location = new Point(200, 30);
        adaugaButton.FlatStyle = FlatStyle.Flat;
        adaugaButton.BackColor = Color.Transparent;
        adaugaButton.ForeColor = Color.White;
        adaugaButton.FlatAppearance.BorderSize = 0;
        adaugaButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        adaugaButton.FlatAppearance.MouseDownBackColor = Color.Transparent; 
        adaugaButton.Font = new Font("Arial Black", 16, FontStyle.Bold);
        adaugaButton.Click += (sender, e) => { PerformInsert(); };

        Button stergeButton = new Button();
        backgroundPanel.Controls.Add(stergeButton);
        stergeButton.Text = "Stergere";
        stergeButton.Size = new Size(190, 38);
        stergeButton.Location = new Point(400, 30);
        stergeButton.FlatStyle = FlatStyle.Flat;
        stergeButton.BackColor = Color.Transparent;
        stergeButton.ForeColor = Color.White;
        stergeButton.FlatAppearance.BorderSize = 0;
        stergeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        stergeButton.FlatAppearance.MouseDownBackColor = Color.Transparent; 
        stergeButton.Font = new Font("Arial Black", 16, FontStyle.Bold);
        stergeButton.Click += (sender, e) => { PerformDelete(); };

        Button editareButton = new Button();
        backgroundPanel.Controls.Add(editareButton);
        editareButton.Text = "Modificare";
        editareButton.Size = new Size(190, 38);
        editareButton.Location = new Point(600, 30);
        editareButton.FlatStyle = FlatStyle.Flat;
        editareButton.BackColor = Color.Transparent;
        editareButton.ForeColor = Color.White;
        editareButton.FlatAppearance.BorderSize = 0;
        editareButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        editareButton.FlatAppearance.MouseDownBackColor = Color.Transparent; 
        editareButton.Font = new Font("Arial Black", 16, FontStyle.Bold);
        editareButton.Click += (sender, e) => { PerformUpdate(); };

        int frameWidth = 800;
        int horizontalGap = (frameWidth - 6 * 110) / 7;

        codlucField = new TextBox();
        backgroundPanel.Controls.Add(codlucField);
        codlucField.Size = new Size(110, 25);
        codlucField.Location = new Point(horizontalGap, 140);
        codlucField.BackColor = Color.Gray;
        codlucField.ForeColor = Color.Black;
        codlucField.BorderStyle = BorderStyle.FixedSingle;

        codaField = new TextBox();
        backgroundPanel.Controls.Add(codaField);
        codaField.Size = new Size(110, 25);
        codaField.Location = new Point(2 * horizontalGap + 110, 140);
        codaField.BackColor = Color.Gray;
        codaField.ForeColor = Color.Black;
        codaField.BorderStyle = BorderStyle.FixedSingle;

        datalucField = new TextBox();
        backgroundPanel.Controls.Add(datalucField);
        datalucField.Size = new Size(110, 25);
        datalucField.Location = new Point(3 * horizontalGap + 2 * 110, 140);
        datalucField.BackColor = Color.Gray;
        datalucField.ForeColor = Color.Black;
        datalucField.BorderStyle = BorderStyle.FixedSingle;

        costField = new TextBox();
        backgroundPanel.Controls.Add(costField);
        costField.Size = new Size(110, 25);
        costField.Location = new Point(4 * horizontalGap + 3 * 110, 140);
        costField.BackColor = Color.Gray;
        costField.ForeColor = Color.Black;
        costField.BorderStyle = BorderStyle.FixedSingle;

        tiplucField = new TextBox();
        backgroundPanel.Controls.Add(tiplucField);
        tiplucField.Size = new Size(110, 25);
        tiplucField.Location = new Point(5 * horizontalGap + 4 * 110, 140);
        tiplucField.BackColor = Color.Gray;
        tiplucField.ForeColor = Color.Black;
        tiplucField.BorderStyle = BorderStyle.FixedSingle;

        codmField = new TextBox();
        backgroundPanel.Controls.Add(codmField);
        codmField.Size = new Size(110, 25);
        codmField.Location = new Point(6 * horizontalGap + 5 * 110, 140);
        codmField.BackColor = Color.Gray;
        codmField.ForeColor = Color.Black;
        codmField.BorderStyle = BorderStyle.FixedSingle;

        Label labelCodL = new Label();
        backgroundPanel.Controls.Add(labelCodL);
        labelCodL.Text = "Cod Luc";
        labelCodL.BackColor = Color.Transparent;
        labelCodL.Size = new Size(110, 25);
        labelCodL.Location = new Point(horizontalGap + 20, 105);
        labelCodL.ForeColor = Color.Yellow;
        labelCodL.Font = new Font("Arial Black", 10, FontStyle.Bold);

        Label labelCodAng = new Label();
        backgroundPanel.Controls.Add(labelCodAng);
        labelCodAng.Text = "Cod Angajat";
        labelCodAng.BackColor = Color.Transparent;
        labelCodAng.Size = new Size(110, 25);
        labelCodAng.Location = new Point(2 * horizontalGap + 114, 105);
        labelCodAng.ForeColor = Color.Yellow;
        labelCodAng.Font = new Font("Arial Black", 10, FontStyle.Bold);

        Label labelData = new Label();
        backgroundPanel.Controls.Add(labelData);
        labelData.Text = "Data Luc";
        labelData.BackColor = Color.Transparent;
        labelData.Size = new Size(110, 25);
        labelData.Location = new Point(3 * horizontalGap + 2 * 119, 105);
        labelData.ForeColor = Color.Yellow;
        labelData.Font = new Font("Arial Black", 10, FontStyle.Bold);

        Label labelCost = new Label();
        backgroundPanel.Controls.Add(labelCost);
        labelCost.Text = "Cost";
        labelCost.BackColor = Color.Transparent;
        labelCost.Size = new Size(110, 25);
        labelCost.Location = new Point(4 * horizontalGap + 3 * 120, 105);
        labelCost.ForeColor = Color.Yellow;
        labelCost.Font = new Font("Arial Black", 10, FontStyle.Bold);

        Label labelTip = new Label();
        backgroundPanel.Controls.Add(labelTip);
        labelTip.Text = "Tip Luc";
        labelTip.BackColor = Color.Transparent;
        labelTip.Size = new Size(110, 25);
        labelTip.Location = new Point(5 * horizontalGap + 4 * 116, 105);
        labelTip.ForeColor = Color.Yellow;
        labelTip.Font = new Font("Arial Black", 10, FontStyle.Bold);

        Label labelCodM = new Label();
        backgroundPanel.Controls.Add(labelCodM);
        labelCodM.Text = "Cod Masina";
        labelCodM.BackColor = Color.Transparent;
        labelCodM.Size = new Size(110, 25);
        labelCodM.Location = new Point(6 * horizontalGap + 5 * 111, 105);
        labelCodM.ForeColor = Color.Yellow;
        labelCodM.Font = new Font("Arial Black", 10, FontStyle.Bold);

        textArea = new RichTextBox();
        backgroundPanel.Controls.Add(textArea);
        textArea.Size = new Size(765, 240);
        textArea.Location = new Point(10, 230);
        textArea.BackColor = Color.FromArgb(255, 255, 204);
        textArea.ReadOnly = true;
    }
    
    private void ConnectToDatabase()
    {
        try
        {
            string connectionString = "Server=localhost;Database=proiect_bd;User ID=root;Password=root;";

            connection = new MySqlConnection(connectionString);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
            }
            else
            {
                MessageBox.Show("Eroare la conectarea la baza de date.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la conectarea la baza de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                StringBuilder whereClause = new StringBuilder(" WHERE ");

                if (!string.IsNullOrEmpty(codlucField.Text))
                {
                    whereClause.Append("Codluc = @Codluc AND ");
                }
                if (!string.IsNullOrEmpty(codaField.Text))
                {
                    whereClause.Append("Coda = @Coda AND ");
                }
                if (!string.IsNullOrEmpty(datalucField.Text))
                {
                    whereClause.Append("Dataluc = @Dataluc AND ");
                }
                if (!string.IsNullOrEmpty(costField.Text))
                {
                    whereClause.Append("Cost = @Cost AND ");
                }
                if (!string.IsNullOrEmpty(tiplucField.Text))
                {
                    whereClause.Append("Tipluc = @Tipluc AND ");
                }
                if (!string.IsNullOrEmpty(codmField.Text))
                {
                    whereClause.Append("Codm = @Codm AND ");
                }

                if (whereClause.Length > 7)
                {
                    whereClause.Length -= 5;
                    query = "SELECT * FROM Lucrari" + whereClause.ToString();
                }
                else
                {
                    query = "SELECT * FROM Lucrari";
                }
            }

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
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

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textArea.AppendText($"Codluc: {reader["Codluc"]}, ");
                        textArea.AppendText($"Coda: {reader["Coda"]}, ");
                        textArea.AppendText($"Dataluc: {reader["Dataluc"]}, ");
                        textArea.AppendText($"Cost: {reader["Cost"]}, ");
                        textArea.AppendText($"Tipluc: {reader["Tipluc"]}, ");
                        textArea.AppendText($"Codm: {reader["Codm"]}\n");
                        textArea.AppendText("--------------\n");
                    }
                }
            }
            LogOperation("SELECT");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la efectuarea căutării: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Cod Lucrare este obligatoriu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Lucrari (Codluc, Coda, Dataluc, Cost, Tipluc, Codm) VALUES (@Codluc, @Coda, @Dataluc, @Cost, @Tipluc, @Codm)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Codluc", codlucField.Text);
                command.Parameters.AddWithValue("@Coda", codaField.Text);
                command.Parameters.AddWithValue("@Dataluc", datalucField.Text);
                command.Parameters.AddWithValue("@Cost", costField.Text);
                command.Parameters.AddWithValue("@Tipluc", tiplucField.Text);
                command.Parameters.AddWithValue("@Codm", codmField.Text);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lucrare adăugată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("INSERT");
                }
                else
                {
                    MessageBox.Show("Eroare la adăugarea lucrării.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la adăugarea lucrării: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PerformDelete()
    {
        try
        {
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show("Introduceți minim o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder queryBuilder = new StringBuilder("DELETE FROM Lucrari WHERE ");
            if (!string.IsNullOrEmpty(codlucField.Text))
            {
                queryBuilder.Append("Codluc = @Codluc AND ");
            }
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                queryBuilder.Append("Coda = @Coda AND ");
            }
            if (!string.IsNullOrEmpty(datalucField.Text))
            {
                queryBuilder.Append("Dataluc = @Dataluc AND ");
            }
            if (!string.IsNullOrEmpty(costField.Text))
            {
                queryBuilder.Append("Cost = @Cost AND ");
            }
            if (!string.IsNullOrEmpty(tiplucField.Text))
            {
                queryBuilder.Append("Tipluc = @Tipluc AND ");
            }
            if (!string.IsNullOrEmpty(codmField.Text))
            {
                queryBuilder.Append("Codm = @Codm AND ");
            }

            if (queryBuilder.Length > 30)
            {
                queryBuilder.Length -= 5;
            }
            else
            {
                MessageBox.Show("Introduceți minim o valoare pentru ștergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = queryBuilder.ToString();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
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

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Lucrare ștearsă cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("DELETE");
                }
                else
                {
                    MessageBox.Show("Eroare la ștergerea lucrării.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la ștergerea lucrării: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PerformUpdate()
    {
        try
        {
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show("Introduceți minim o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder queryBuilder = new StringBuilder("UPDATE Lucrari SET ");
            if (!string.IsNullOrEmpty(codlucField.Text))
            {
                queryBuilder.Append("Codluc = @Codluc, ");
            }
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                queryBuilder.Append("Coda = @Coda, ");
            }
            if (!string.IsNullOrEmpty(datalucField.Text))
            {
                queryBuilder.Append("Dataluc = @Dataluc, ");
            }
            if (!string.IsNullOrEmpty(costField.Text))
            {
                queryBuilder.Append("Cost = @Cost, ");
            }
            if (!string.IsNullOrEmpty(tiplucField.Text))
            {
                queryBuilder.Append("Tipluc = @Tipluc, ");
            }
            if (!string.IsNullOrEmpty(codmField.Text))
            {
                queryBuilder.Append("Codm = @Codm, ");
            }

            if (queryBuilder.Length > 13)
            {
                queryBuilder.Length -= 2;
            }
            else
            {
                MessageBox.Show("Introduceți minim o valoare pentru actualizare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            queryBuilder.Append(" WHERE Codluc = @Codluc");
            string query = queryBuilder.ToString();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
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

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Lucrare actualizată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogOperation("UPDATE");
                }
                else
                {
                    MessageBox.Show("Eroare la actualizarea lucrării.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la actualizarea lucrării: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            StringBuilder queryBuilder = new StringBuilder("SELECT l.Codluc, l.Coda, l.Dataluc, l.Cost, l.Tipluc, l.Codm FROM Lucrari l WHERE ");
            bool conditionAdded = false;

            if (!string.IsNullOrEmpty(codlucField.Text))
            {
                queryBuilder.Append("l.Codluc = @Codluc");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Coda = @Coda");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(datalucField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Dataluc = @Dataluc");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(costField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Cost >= @Cost");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(tiplucField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Tipluc = @Tipluc");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(codmField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Codm = @Codm");
                conditionAdded = true;
            }

            queryBuilder.Append(" ORDER BY l.Cost ASC");

            string query = queryBuilder.ToString();
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

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textArea.AppendText($"Codluc: {reader["Codluc"]}, ");
                        textArea.AppendText($"Coda: {reader["Coda"]}, ");
                        textArea.AppendText($"Dataluc: {reader["Dataluc"]}, ");
                        textArea.AppendText($"Cost: {reader["Cost"]}, ");
                        textArea.AppendText($"Tipluc: {reader["Tipluc"]}, ");
                        textArea.AppendText($"Codm: {reader["Codm"]}\n");
                        textArea.AppendText("--------------\n");
                    }
                }
            }
            LogOperation("SELECT_CRESC");
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

            StringBuilder queryBuilder = new StringBuilder("SELECT l.Codluc, l.Coda, l.Dataluc, l.Cost, l.Tipluc, l.Codm FROM Lucrari l WHERE ");
            bool conditionAdded = false;

            if (!string.IsNullOrEmpty(codlucField.Text))
            {
                queryBuilder.Append("l.Codluc = @Codluc");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(codaField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Coda = @Coda");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(datalucField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Dataluc = @Dataluc");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(costField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Cost <= @Cost");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(tiplucField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Tipluc = @Tipluc");
                conditionAdded = true;
            }
            if (!string.IsNullOrEmpty(codmField.Text))
            {
                if (conditionAdded) queryBuilder.Append(" AND ");
                queryBuilder.Append("l.Codm = @Codm");
                conditionAdded = true;
            }

            queryBuilder.Append(" ORDER BY l.Cost DESC");

            string query = queryBuilder.ToString();
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

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textArea.AppendText($"Codluc: {reader["Codluc"]}, ");
                        textArea.AppendText($"Coda: {reader["Coda"]}, ");
                        textArea.AppendText($"Dataluc: {reader["Dataluc"]}, ");
                        textArea.AppendText($"Cost: {reader["Cost"]}, ");
                        textArea.AppendText($"Tipluc: {reader["Tipluc"]}, ");
                        textArea.AppendText($"Codm: {reader["Codm"]}\n");
                        textArea.AppendText("--------------\n");
                    }
                }
            }
            LogOperation("SELECT_DESC");
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
            MySqlCommand maxKeyCommand = new MySqlCommand("SELECT MAX(CodOp) FROM istoricoperatii", connection);
            int nextKey = Convert.ToInt32(maxKeyCommand.ExecuteScalar()) + 1;

            string insertQuery = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) " +
                                 "VALUES (@CodOp, @User, @Tabela_folosita, @Tip_Operatie, @Data_Ora)";
            MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@CodOp", nextKey);
            insertCommand.Parameters.AddWithValue("@User", "Angajat");
            insertCommand.Parameters.AddWithValue("@Tabela_folosita", "Lucrari");
            insertCommand.Parameters.AddWithValue("@Tip_Operatie", operationType);
            insertCommand.Parameters.AddWithValue("@Data_Ora", DateTime.Now);

            insertCommand.ExecuteNonQuery();

            Console.WriteLine("Generated key: " + nextKey);
        }
        catch (MySqlException e)
        {
            Console.WriteLine("Error logging operation: " + e.Message);
        }
    }
}



