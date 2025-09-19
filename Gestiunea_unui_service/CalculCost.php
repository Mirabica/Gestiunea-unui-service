<?php
// Configurare conexiune la baza de date
$servername = "localhost";
$username = "root";
$password = "root";
$dbname = "proiect_bd";
 
try {
    // Creare conexiune folosind PDO
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    // Setarea modului de eroare al conexiunii PDO ca exceptii
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
 
    // Fetch and group the costs by date from the Lucrari table
    $sql = "SELECT dataluc, SUM(cost) as totalCost FROM Lucrari GROUP BY dataluc";
    $stmt = $conn->prepare($sql);
    $stmt->execute();
 
    // Fetch the result
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
 
    // Calculate the overall sum of costs
    $totalSum = 0;
    foreach ($result as $row) {
        $totalSum += $row['totalCost'];
    }
 
    // Append the overall sum to the result
    $result[] = ['dataluc' => 'Total', 'totalCost' => $totalSum];
 
    // Return the result as JSON
    echo json_encode($result);
 
} catch(PDOException $e) {
    echo json_encode(["error" => "Conexiunea la baza de date a eșuat: " . $e->getMessage()]);
}
?>