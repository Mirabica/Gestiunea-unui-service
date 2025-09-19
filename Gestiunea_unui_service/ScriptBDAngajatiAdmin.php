<?php

class DatabaseOperations {
    private $pdo;

    public function __construct() {
        $this->connectToDatabase();
    }

    private function connectToDatabase() {
        try {
            $dsn = 'mysql:host=localhost;dbname=proiect_bd;charset=utf8';
            $user = 'root';
            $password = 'root';
            $this->pdo = new PDO($dsn, $user, $password, [
                PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
                PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
            ]);
        } catch (PDOException $e) {
            echo "Eroare la conectarea la baza de date: " . $e->getMessage();
            exit;
        }
    }
	private function areAllFieldsEmpty($fields) {
       	 foreach ($fields as $field) {
           	 if (!empty($field)) {
                return false;
            }
        }
        return true;
    }

        public function performOperation($operation, $fields) {
        switch ($operation) {
            case 'search':
                $this->performSearch($fields);
                break;
            case 'insert':
                $this->performInsert($fields);
                break;
            case 'delete':
                $this->performDelete($fields);
                break;
            case 'update':
                $this->performUpdate($fields, $fields['coda']);
                break;
            default:
                echo "Operatie necunoscută.";
        }
    }

    public function performSearch($fields) {
        try {
            if ($this->areAllFieldsEmpty($fields)) {
                $query = "SELECT * FROM Angajati";
            } else {
                $whereClause = [];
                foreach ($fields as $key => $value) {
                    if (!empty($value)) {
                        $whereClause[] = "$key = :$key";
                    }
                }
                $query = "SELECT * FROM Angajati WHERE " . implode(' AND ', $whereClause);
            }

	$stmt = $this->pdo->prepare($query);
           	 foreach ($fields as $key => $value) {
               	 if (!empty($value)) {
                    $stmt->bindValue(":$key", $value);
                }
            }
            $stmt->execute();
            $results = $stmt->fetchAll();

            foreach ($results as $row) {
                echo "Coda: " . $row['coda'] . ", ";
                echo "Nume: " . $row['nume'] . ", ";
                echo "Prenume: " . $row['prenume'] . ", ";
                echo "Adresa: " . $row['adresa'] . ", ";
                echo "Functie: " . $row['functie'] ."\n ";
                echo "--------------\n";
            }
            $this->logOperation("SELECT");
        } catch (PDOException $e) {
            echo "Eroare la efectuarea căutării: " . $e->getMessage();
        }
    }

   public function performInsert($fields) {
        try {
            if (empty($fields['coda'])) {
                echo "Cod Angajat este obligatoriu!";
                return;
            }

            $query = "INSERT INTO Angajati (coda, nume, prenume, adresa, functie) VALUES (:coda, :nume, :prenume, :adresa, :functie)";
            $stmt = $this->pdo->prepare($query);
            foreach ($fields as $key => $value) {
                $stmt->bindValue(":$key", $value);
            }
            $rowsAffected = $stmt->execute();

            if ($rowsAffected) {
            echo "Angajat adăugat cu succes!";
            $this->logOperation("INSERT");
        	} 
	else {
	 echo "Eroare la adăugare angajat. ";
	}
     } catch (PDOException $e) {
            echo "Eroare la adăugare angajat: " . $e->getMessage();
        }
    }

    public function performDelete($fields) {
        try {
            if ($this->areAllFieldsEmpty($fields)) {
                echo "Introduceți minim o valoare pentru ștergere.";
                return;
            }
  $whereClause = [];
            foreach ($fields as $key => $value) {
                if (!empty($value)) {
                    $whereClause[] = "$key = :$key";
                }
            }
            $query = "DELETE FROM Angajati WHERE " . implode(' AND ', $whereClause);

            $stmt = $this->pdo->prepare($query);
            foreach ($fields as $key => $value) {
                if (!empty($value)) {
                    $stmt->bindValue(":$key", $value);
                }
            }
            $affectedRows = $stmt->execute();

            if ($affectedRows) {
                echo "Ștergere reușită.";
                $this->logOperation("DELETE");
            } else {
                echo "Nu s-a găsit nicio înregistrare cu aceste valori.";
            }
        } catch (PDOException $e) {
            echo "Eroare la ștergere: " . $e->getMessage();
        }
    }

    public function performUpdate($fields, $coda) {
         try {
            if ($this->areAllFieldsEmpty($fields)) {
                echo "Introduceți cel puțin o valoare pentru actualizare.";
                return;
            }

	$setClause = [];
            foreach ($fields as $key => $value) {
                if (!empty($value)) {
                    $setClause[] = "$key = :$key";
                }
            }
            $query = "UPDATE Angajati SET " . implode(', ', $setClause) . " WHERE coda = :coda";

            $stmt = $this->pdo->prepare($query);
            foreach ($fields as $key => $value) {
                if (!empty($value)) {
                    $stmt->bindValue(":$key", $value);
                }
            }
            $stmt->bindValue(":coda", $coda);
            $affectedRows = $stmt->execute();

            if ($affectedRows) {
                echo "Actualizare reușită.";
                $this->logOperation("UPDATE");
            } else {
                echo "Nu s-a găsit nicio înregistrare cu acest coda.";
            }
        } catch (PDOException $e) {
            echo "Eroare la actualizare: " . $e->getMessage();
        }
    }

          private function logOperation($operationType) {
        try {
            $getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
            $stmt = $this->pdo->prepare($getMaxKeyQuery);
            $stmt->execute();
            $maxKey = $stmt->fetchColumn();
            $nextKey = $maxKey ? $maxKey + 1 : 1;

        $query = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (:codop, :user, :tabela, :tip_operatie, :data_ora)";
       $stmt = $this->pdo->prepare($query);
            $stmt->bindParam(':codop', $nextKey);
            $stmt->bindParam(':user', $user);
            $stmt->bindParam(':tabela', $tabela);
            $stmt->bindParam(':tip_operatie', $tip_operatie);

            $user = "Admin";
            $tabela = "Angajati";
            $tip_operatie = $operationType;
            $data_ora = date('Y-m-d H:i:s');
            $stmt->bindParam(':data_ora', $data_ora);

            $stmt->execute();
        } catch (PDOException $e) {
            echo "Eroare la înregistrarea operației în jurnal: " . $e->getMessage();
        }
    }
}

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $operation = $_POST['operation'];
    $fields = [
        'coda' => $_POST['coda'] ?? null,
        'nume' => $_POST['nume'] ?? null,
        'prenume' => $_POST['prenume'] ?? null,
        'adresa' => $_POST['adresa'] ?? null,
        'functie' => $_POST['functie'] ?? null,
    ];

    $dbOperations = new DatabaseOperations();
    $dbOperations->performOperation($operation, $fields);
} else {
    echo "Invalid request method.";
}
?>