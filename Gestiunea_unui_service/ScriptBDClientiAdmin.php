<?php
$servername = "localhost";
$username = "root";
$password = "root";
$dbname = "proiect_bd";

try {
    // Create connection
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    // Set the PDO error mode to exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        $operation = $_POST['operation'];
        $codcli = $_POST['codcli'];
        $nume = $_POST['nume'];
        $prenume = $_POST['prenume'];
        $adresa = $_POST['adresa'];
        $telefon = $_POST['telefon'];

        switch ($operation) {
            case 'search':
                performSearch($conn, $codcli, $nume, $prenume, $adresa, $telefon);
                break;
            case 'insert':
                performInsert($conn, $codcli, $nume, $prenume, $adresa, $telefon);
                break;
            case 'delete':
                performDelete($conn, $codcli, $nume, $prenume, $adresa, $telefon);
                break;
            case 'update':
                performUpdate($conn, $codcli, $nume, $prenume, $adresa, $telefon);
                break;
        }
    }
} catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}

function performSearch($conn, $codcli, $nume, $prenume, $adresa, $telefon) {
    $query = "SELECT * FROM Clienti";
    $params = [];
    if (!empty($codcli) || !empty($nume) || !empty($prenume) || !empty($adresa) || !empty($telefon)) {
        $query .= " WHERE ";
        if (!empty($codcli)) {
            $query .= "Codcli = :codcli AND ";
            $params[':codcli'] = $codcli;
        }
        if (!empty($nume)) {
            $query .= "Nume = :nume AND ";
            $params[':nume'] = $nume;
        }
        if (!empty($prenume)) {
            $query .= "Prenume = :prenume AND ";
            $params[':prenume'] = $prenume;
        }
        if (!empty($adresa)) {
            $query .= "Adresa = :adresa AND ";
            $params[':adresa'] = $adresa;
        }
        if (!empty($telefon)) {
            $query .= "Telefon = :telefon AND ";
            $params[':telefon'] = $telefon;
        }
        $query = substr($query, 0, -5); // Remove the trailing ' AND '
    }
    $stmt = $conn->prepare($query);
    $stmt->execute($params);
    $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
    foreach ($results as $row) {
        echo "Codcli: " . $row['codcli'] . ", Nume: " . $row['nume'] . ", Prenume: " . $row['prenume'] . ", Adresa: " . $row['adresa'] . ", Telefon: " . $row['telefon'] . "\n--------------\n";
    }
}

function performInsert($conn, $codcli, $nume, $prenume, $adresa, $telefon) {
    if (empty($codcli)) {
        echo "Cod Client este obligatoriu!";
        return;
    }
    $query = "INSERT INTO Clienti (Codcli, Nume, Prenume, Adresa, Telefon) VALUES (:codcli, :nume, :prenume, :adresa, :telefon)";
    $stmt = $conn->prepare($query);
    $stmt->bindParam(':codcli', $codcli);
    $stmt->bindParam(':nume', $nume);
    $stmt->bindParam(':prenume', $prenume);
    $stmt->bindParam(':adresa', $adresa);
    $stmt->bindParam(':telefon', $telefon);
    if ($stmt->execute()) {
        echo "Client adăugat cu succes!";
    } else {
        echo "Eroare la adăugare client.";
    }
}

function performDelete($conn, $codcli, $nume, $prenume, $adresa, $telefon) {
    if (empty($codcli) && empty($nume) && empty($prenume) && empty($adresa) && empty($telefon)) {
        echo "Introduceți minim o valoare pentru ștergere.";
        return;
    }
    $query = "DELETE FROM Clienti WHERE ";
    $params = [];
    if (!empty($codcli)) {
        $query .= "Codcli = :codcli AND ";
        $params[':codcli'] = $codcli;
    }
    if (!empty($nume)) {
        $query .= "Nume = :nume AND ";
        $params[':nume'] = $nume;
    }
    if (!empty($prenume)) {
        $query .= "Prenume = :prenume AND ";
        $params[':prenume'] = $prenume;
    }
    if (!empty($adresa)) {
        $query .= "Adresa = :adresa AND ";
        $params[':adresa'] = $adresa;
    }
    if (!empty($telefon)) {
        $query .= "Telefon = :telefon AND ";
        $params[':telefon'] = $telefon;
    }
    $query = substr($query, 0, -5); // Remove the trailing ' AND '
    $stmt = $conn->prepare($query);
    if ($stmt->execute($params)) {
        if ($stmt->rowCount() > 0) {
            echo "Ștergere reușită.";
        } else {
            echo "Nu s-a găsit nicio înregistrare cu aceste valori.";
        }
    } else {
        echo "Eroare la ștergere.";
    }
}

function performUpdate($conn, $codcli, $nume, $prenume, $adresa, $telefon) {
    if (empty($codcli)) {
        echo "Cod Client este obligatoriu pentru actualizare.";
        return;
    }
    if (empty($nume) && empty($prenume) && empty($adresa) && empty($telefon)) {
        echo "Introduceți minim o valoare pentru actualizare.";
        return;
    }
    $query = "UPDATE Clienti SET ";
    $params = [];
    if (!empty($nume)) {
        $query .= "Nume = :nume, ";
        $params[':nume'] = $nume;
    }
    if (!empty($prenume)) {
        $query .= "Prenume = :prenume, ";
        $params[':prenume'] = $prenume;
    }
    if (!empty($adresa)) {
        $query .= "Adresa = :adresa, ";
        $params[':adresa'] = $adresa;
    }
    if (!empty($telefon)) {
        $query .= "Telefon = :telefon, ";
        $params[':telefon'] = $telefon;
    }
    $query = substr($query, 0, -2); // Remove the trailing ', '
    $query .= " WHERE Codcli = :codcli";
    $params[':codcli'] = $codcli;
    $stmt = $conn->prepare($query);
    if ($stmt->execute($params)) {
        if ($stmt->rowCount() > 0) {
            echo "Actualizare reușită.";
        } else {
            echo "Nu s-a găsit nicio înregistrare cu acest Codcli.";
        }
    } else {
        echo "Eroare la actualizare.";
    }
}
?>
