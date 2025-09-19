<?php
try {
    $host = '127.0.0.1';
    $db = 'proiect_bd';
    $user = 'root';
    $pass = 'root';
    $charset = 'utf8mb4';

    $dsn = "mysql:host=$host;dbname=$db;charset=$charset";
    $options = [
        PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
        PDO::ATTR_EMULATE_PREPARES   => false,
    ];
    $pdo = new PDO($dsn, $user, $pass, $options);

    $data = isset($_POST['data']) ? $_POST['data'] : '';
    $ora = isset($_POST['ora']) ? $_POST['ora'] : '';
    $serie = isset($_POST['serie']) ? $_POST['serie'] : '';

    if (empty($data) && empty($ora) && empty($serie)) {
        echo "Introduceți minim o valoare pentru căutare.";
        exit;
    }

    $query = "SELECT p.data, p.ora, m.model, m.marca, m.seriaVIN FROM programari p JOIN Masini m ON p.codm = m.codm WHERE ";
    $conditions = [];
    $params = [];

    if (!empty($serie)) {
        $conditions[] = "m.seriaVIN = ?";
        $params[] = $serie;
    }
    if (!empty($ora)) {
        $conditions[] = "p.ora = ?";
        $params[] = $ora;
    }
    if (!empty($data)) {
        $conditions[] = "p.data = ?";
        $params[] = $data;
    }

    $query .= implode(' AND ', $conditions);

    $stmt = $pdo->prepare($query);
    $stmt->execute($params);
    $results = $stmt->fetchAll();

    foreach ($results as $row) {
        echo "Marca: " . $row['marca'] . ", Model: " . $row['model'] . ", VIN: " . $row['seriaVIN'] . ", Data: " . $row['data'] . ", Ora: " . $row['ora'] . "\n--------------\n";
    }

    // Logging the operation
    $maxKeyQuery = "SELECT MAX(CodOp) AS maxCodOp FROM istoricoperatii";
    $stmt = $pdo->query($maxKeyQuery);
    $result = $stmt->fetch();
    $nextKey = $result ? $result['maxCodOp'] + 1 : 1;

    $logQuery = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (?, ?, ?, ?, ?)";
    $stmt = $pdo->prepare($logQuery);
    $stmt->execute([$nextKey, 'Client', 'Programari', 'SELECT', date('Y-m-d H:i:s')]);

} catch (PDOException $e) {
    echo "Eroare la conectarea la baza de date: " . $e->getMessage();
}
?>