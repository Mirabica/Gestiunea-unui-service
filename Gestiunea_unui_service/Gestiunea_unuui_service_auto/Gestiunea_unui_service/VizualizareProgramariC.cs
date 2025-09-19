using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class VizualizareProgramariC : Form
{
    private TextBox oraField;
    private TextBox dataField;
    private TextBox serieField;
    private RichTextBox textArea;
    private MySqlConnection connection;

    public VizualizareProgramariC()
    {
        ConnectToDatabase();
        Text = "Vizualizare programari";
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
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\viz.jpg"),
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
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(105, 105, 105),
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 12, FontStyle.Bold)
        };
        backButton.FlatAppearance.BorderSize = 0;
        backgroundPanel.Controls.Add(backButton);

        backButton.Click += (sender, e) =>
        {
            Clienti clienti = new Clienti();
            clienti.Show();
            Dispose();
        };

        Button vizualizareButton = new Button()
        {
            Text = "Vizualizare",
            Bounds = new Rectangle(300, 26, 190, 40),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Transparent,
            ForeColor = Color.White,
            Font = new Font("Arial Black", 20, FontStyle.Bold),
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, // Set the hover color to Transparent
                MouseDownBackColor = Color.Transparent // Set the click color to Transparent
            }
        };
        backgroundPanel.Controls.Add(vizualizareButton);

        // Add action listener for Vizualizare button
        vizualizareButton.Click += (sender, e) =>
        {
            PerformSearch();
        };

        int frameWidth = 800;
        int horizontalGap = (frameWidth - 2 * 110) / 4;

        dataField = new TextBox()
        {
            Bounds = new Rectangle(horizontalGap + 270, 140, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(dataField);

        oraField = new TextBox()
        {
            Bounds = new Rectangle(2 * horizontalGap + 252, 140, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(oraField);

        serieField = new TextBox()
        {
            Bounds = new Rectangle(3 * horizontalGap + 234, 140, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(serieField);

        Font labelFont = new Font("Arial Black", 14, FontStyle.Bold);

        Label labelData = new Label()
        {
            Text = "Data",
            Bounds = new Rectangle(horizontalGap + 296, 110, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelData);

        Label labelOra = new Label()
        {
            Text = "Ora",
            Bounds = new Rectangle(2 * horizontalGap + 284, 110, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelOra);

        Label laberSerie = new Label()
        {
            Text = "VIN",
            Bounds = new Rectangle(3 * horizontalGap + 264, 110, 110, 25),
            ForeColor = Color.Yellow,
            Font = labelFont,
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(laberSerie);

        textArea = new RichTextBox()
        {
            Bounds = new Rectangle(430, 226, 360, 240),
            BackColor = Color.FromArgb(255, 255, 204),
            BorderStyle = BorderStyle.None,
            ReadOnly = true
        };
        backgroundPanel.Controls.Add(textArea);
    }

    private void ConnectToDatabase()
    {
        try
        {
            string connectionString = "Server=localhost;Database=proiect_bd;Uid=root;Pwd=root;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected to the database successfully!");
        }
        catch (MySqlException e)
        {
            Console.WriteLine("Error connecting to the database: " + e.Message);
        }
    }

    private void PerformSearch()
    {
        try
        {
            textArea.Clear();

            // Check if any field is empty
            if (AreAllFieldsEmpty())
            {
                MessageBox.Show("Introduceți seria de sasiu!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "SELECT p.data, p.ora, m.model, m.marca, m.seriaVIN " +
                           "FROM programari p " +
                           "JOIN Masini m ON p.codm = m.codm " +
                           "WHERE ";
            bool conditionAdded = false;

            if (!string.IsNullOrEmpty(serieField.Text))
            {
                query += "m.seriaVIN = @SeriaVIN";
                conditionAdded = true;
            }

            if (!string.IsNullOrEmpty(oraField.Text))
            {
                if (conditionAdded)
                {
                    query += " AND ";
                }
                query += "p.ora = @Ora";
            }

            if (!string.IsNullOrEmpty(dataField.Text))
            {
                if (conditionAdded)
                {
                    query += " AND ";
                }
                query += "p.data = @Data";
            }

            if (!conditionAdded)
            {
                MessageBox.Show("Introduceți minim o valoare pentru căutare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SeriaVIN", serieField.Text);
                command.Parameters.AddWithValue("@Ora", oraField.Text);
                command.Parameters.AddWithValue("@Data", dataField.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Display the results in the text area
                    while (reader.Read())
                    {
                        textArea.AppendText("Marca: " + reader["marca"] + ", ");
                        textArea.AppendText("Model: " + reader["model"] + ", ");
                        textArea.AppendText("VIN: " + reader["seriaVIN"] + ", ");
                        textArea.AppendText("Data: " + reader["data"] + ", ");
                        textArea.AppendText("Ora: " + reader["ora"] + "\n");
                        textArea.AppendText("--------------\n");
                    }
                    LogOperation("SELECT");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error performing search: " + ex.Message);
            MessageBox.Show("Eroare la căutare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool AreAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(dataField.Text) && string.IsNullOrEmpty(oraField.Text) && string.IsNullOrEmpty(serieField.Text);
    }

    private void LogOperation(string operationType)
    {
        try
        {

            int nextKey = 1;
            using (MySqlCommand maxKeyCommand = new MySqlCommand("SELECT MAX(CodOp) FROM istoricoperatii", connection))
            {
                object result = maxKeyCommand.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    nextKey = Convert.ToInt32(result) + 1;
                }
            }

            string insertQuery = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) " +
                                 "VALUES (@CodOp, @User, @Tabela_folosita, @Tip_Operatie, @Data_Ora)";
            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@CodOp", nextKey);
                insertCommand.Parameters.AddWithValue("@User", "Client");
                insertCommand.Parameters.AddWithValue("@Tabela_folosita", "Programari");
                insertCommand.Parameters.AddWithValue("@Tip_Operatie", operationType);
                insertCommand.Parameters.AddWithValue("@Data_Ora", DateTime.Now);
                insertCommand.ExecuteNonQuery();
            }

            Console.WriteLine("Generated key: " + nextKey);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error logging operation: " + e.Message);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {

            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Database connection closed.");
            }
        }
        base.Dispose(disposing);
    }
}
