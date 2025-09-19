using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class VizualizareMasini : Form
{
    private TextBox seriaVINField;
    private RichTextBox textArea;
    private MySqlConnection connection;

    public VizualizareMasini()
    {
        ConnectToDatabase();
        Text = "Vizualizare masini";
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
            BackgroundImage = Image.FromFile(@"C:\Users\Mira\RiderProjects\WindowsFormsApp1\Gestiunea_unui_service\Imagini\Viz_mas.jpg"), 
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

        Label labelseriaVIN = new Label()
        {
            Text = "Seria",
            Bounds = new Rectangle(110, 260, 110, 30),
            ForeColor = Color.Yellow,
            Font = new Font("Arial Black", 18, FontStyle.Bold),
            BackColor = Color.Transparent
        };
        backgroundPanel.Controls.Add(labelseriaVIN);

        seriaVINField = new TextBox()
        {
            Bounds = new Rectangle(96, 290, 110, 25),
            BackColor = Color.Gray,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        backgroundPanel.Controls.Add(seriaVINField);

        Button vizualizareButton = new Button()
        {
            Text = "Vizualizare",
            Bounds = new Rectangle(300, 10, 190, 40),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Transparent,
            ForeColor = Color.Black,
            Font = new Font("Arial Black", 20, FontStyle.Bold),
            FlatAppearance =
            {
                BorderSize = 0,
                MouseOverBackColor = Color.Transparent, 
                MouseDownBackColor = Color.Transparent 
            }
        };
        backgroundPanel.Controls.Add(vizualizareButton);

        vizualizareButton.Click += (sender, e) =>
        {
            PerformSearch();
        };

        textArea = new RichTextBox()
        {
            Bounds = new Rectangle(30, 50, 500, 150),
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
            string query;
            if (AreAllFieldsEmpty())
            {
                query = "SELECT * FROM Masini";
                Console.WriteLine("Select all records: " + query);
            }
            else
            {
                string whereClause = " WHERE ";
                if (!string.IsNullOrEmpty(seriaVINField.Text))
                {
                    whereClause += "SeriaVIN = @SeriaVIN AND ";
                }

                if (whereClause.Length > 7)
                {
                    whereClause = whereClause.Substring(0, whereClause.Length - 5);
                    query = "SELECT * FROM masini" + whereClause;
                    Console.WriteLine("Select with conditions: " + query);
                }
                else
                {
                    query = "SELECT * FROM masini";
                    Console.WriteLine("Select all records: " + query);
                }
            }

            MySqlCommand command = new MySqlCommand(query, connection);

            if (!string.IsNullOrEmpty(seriaVINField.Text))
            {
                command.Parameters.AddWithValue("@SeriaVIN", seriaVINField.Text);
            }

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                textArea.AppendText("Marca: " + reader["marca"] + ",");
                textArea.AppendText("Model: " + reader["model"] + ",");
                textArea.AppendText("An fabricatie: " + reader["an_fabricatie"] + ",");
                textArea.AppendText("SeriaVIN: " + reader["SeriaVIN"] + "\n");
                textArea.AppendText("--------------\n");
            }

            reader.Close();
            LogOperation("SELECT");
        }
        catch (MySqlException e)
        {
            Console.WriteLine("Error performing search: " + e.Message);
        }
    }

    private bool AreAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(seriaVINField.Text);
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
            insertCommand.Parameters.AddWithValue("@User", "Client");
            insertCommand.Parameters.AddWithValue("@Tabela_folosita", "Masini");
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
