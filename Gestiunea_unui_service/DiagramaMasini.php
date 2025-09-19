<?php
// Establish a PDO connection
$servername = "localhost";
$username = "root";
$password = "root";
$dbname = "proiect_bd";
 
try {
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
 
    // Query to retrieve data for the pie chart
    $query = "SELECT marca, COUNT(*) AS Count FROM Masini GROUP BY marca";
    $stmt = $conn->prepare($query);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
 
    // Convert the result to JSON format
    $data = json_encode($result);
 
    // Output the JSON data
    header('Content-Type: application/json');
    echo $data;
} catch(PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}
?>