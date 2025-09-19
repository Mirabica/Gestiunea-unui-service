<?php
 
// Function to connect to the database using PDO
function connectToDatabase() {
    try {
        // Configuring database connection (change the values according to your setup)
        $host = 'localhost';
        $dbname = 'proiect_bd';
        $username = 'root';
        $password = 'root';
 
        // Creating a PDO instance
        $dsn = "mysql:host=$host;dbname=$dbname";
        $pdo = new PDO($dsn, $username, $password);
 
        // Set PDO to throw exceptions on error
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
 
        
        return $pdo;
    } catch (PDOException $e) {
        echo "Connection failed: " . $e->getMessage();
    }
}
 
// Function to perform search
function performSearch($pdo) {
    try {
        $resultTextArea = "";
 
        // Check if all fields are empty
        if (areAllFieldsEmpty()) {
            // If all fields are empty, select all records
            $query = "SELECT * FROM Masini";
            
        } else {
            // Construct the query based on the content of the input fields
            $whereClause = "";
 
            if (!empty($_POST['seriaVINField'])) {
                $whereClause .= "SeriaVIN = ? AND ";
            }
 
            // Remove the last "AND" from the string
            if (!empty($whereClause)) {
                $whereClause = " WHERE " . rtrim($whereClause, "AND ");
                $query = "SELECT * FROM Masini" . $whereClause;

            } else {
                // If there are no conditions, select all records
                $query = "SELECT * FROM Masini";
            }
        }
 
        // Execute the query
        $stmt = $pdo->prepare($query);
 
        if (!empty($_POST['seriaVINField'])) {
            $stmt->execute([$_POST['seriaVINField']]);
        } else {
            $stmt->execute();
        }
 
        // Fetch results and display in textarea
        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $resultTextArea .= "Marca: " . $row['marca'] . ", ";
            $resultTextArea .= "Model: " . $row['model'] . ", ";
            $resultTextArea .= "An fabricatie: " . $row['an_fabricatie'] . ", ";
            $resultTextArea .= "SeriaVIN: " . $row['seriaVIN'] . "\n";
            $resultTextArea .= "--------------\n";
        }
 
        // Log the operation
        logOperation("SELECT", $pdo);
 
        return $resultTextArea;
    } catch (PDOException $e) {
        echo "Error performing search: " . $e->getMessage();
    }
}
 
// Function to check if all input fields are empty
function areAllFieldsEmpty() {
    return empty($_POST['seriaVINField']);
}
 
// Function to log database operations
function logOperation($operationType, $pdo) {
    try {
        // Get the maximum key from the database
        $getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
        $maxKeyStmt = $pdo->query($getMaxKeyQuery);
        $nextKey = ($maxKeyStmt->fetchColumn() ?? 0) + 1;
 
        // Prepare insert query
        $query = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (?, ?, ?, ?, ?)";
        $stmt = $pdo->prepare($query);
        $stmt->execute([$nextKey, "Client", "Masini", $operationType, date("Y-m-d H:i:s")]);
 
    } catch (PDOException $e) {
        echo "Error logging operation: " . $e->getMessage();
    }
}
 
// Check if form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Connect to the database
    $pdo = connectToDatabase();
 
    // Perform search
    $searchResult = performSearch($pdo);
 
    // Output search result
    echo $searchResult;
}
?>