<?php
class DatabaseHandler {
    private $connection;
    private $codprogField;
    private $codcliField;
    private $codmField;
    private $dataField;
    private $oraField;

    public function __construct($codprogField, $codcliField, $codmField, $dataField, $oraField) {
        $this->codprogField = $codprogField;
        $this->codcliField = $codcliField;
        $this->codmField = $codmField;
        $this->dataField = $dataField;
        $this->oraField = $oraField;
        $this->connectToDatabase();
    }

    private function connectToDatabase() {
        $dsn = 'mysql:host=localhost;dbname=proiect_bd';
        $username = 'root';
        $password = 'root';

        try {
            $this->connection = new PDO($dsn, $username, $password);
            $this->connection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        } catch (PDOException $e) {
            echo "Eroare la conectarea la baza de date: " . $e->getMessage();
        }
    }

    private function areAllFieldsEmpty() {
        return empty($this->codprogField) && empty($this->codcliField)
            && empty($this->codmField) && empty($this->dataField)
            && empty($this->oraField);
    }

    public function performSearch() {
        try {
            if ($this->areAllFieldsEmpty()) {
                $query = "SELECT * FROM Programari";
            } else {
                $whereClause = " WHERE ";

                if (!empty($this->codprogField)) {
                    $whereClause .= "Codprog = :codprog AND ";
                }
                if (!empty($this->codcliField)) {
                    $whereClause .= "Codcli = :codcli AND ";
                }
                if (!empty($this->codmField)) {
                    $whereClause .= "Codm = :codm AND ";
                }
                if (!empty($this->dataField)) {
                    $whereClause .= "Data = :data AND ";
                }
                if (!empty($this->oraField)) {
                    $whereClause .= "Ora = :ora AND ";
                }

                $whereClause = rtrim($whereClause, " AND ");
                $query = "SELECT * FROM Programari" . $whereClause;
            }

            $stmt = $this->connection->prepare($query);

            if (!empty($this->codprogField)) {
                $stmt->bindParam(':codprog', $this->codprogField);
            }
            if (!empty($this->codcliField)) {
                $stmt->bindParam(':codcli', $this->codcliField);
            }
            if (!empty($this->codmField)) {
                $stmt->bindParam(':codm', $this->codmField);
            }
            if (!empty($this->dataField)) {
                $stmt->bindParam(':data', $this->dataField);
            }
            if (!empty($this->oraField)) {
                $stmt->bindParam(':ora', $this->oraField);
            }

            $stmt->execute();
            $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

            if ($results) {
                foreach ($results as $row) {
                    echo "Codprog: " . $row['codprog'] . " | Codcli: " . $row['codcli'] . " | Codm: " . $row['codm'] . " | Data: " . $row['data'] . " | Ora: " . $row['ora'] . "\n";
                   echo "----------\n";
                }
            } else {
                echo "No records found.";
            }
            $this->logOperation('search');
        } catch (PDOException $e) {
            echo "Eroare la căutare: " . $e->getMessage();
        }
    }

    public function performInsert() {
        try {
            if (empty($this->codprogField) || empty($this->codcliField) || empty($this->codmField) || empty($this->dataField) || empty($this->oraField)) {
                echo "Toate câmpurile sunt obligatorii pentru adăugare.";
                return;
            }

            $query = "INSERT INTO Programari (Codprog, Codcli, Codm, Data, Ora) VALUES (:codprog, :codcli, :codm, :data, :ora)";
            $stmt = $this->connection->prepare($query);

            $stmt->bindParam(':codprog', $this->codprogField);
            $stmt->bindParam(':codcli', $this->codcliField);
            $stmt->bindParam(':codm', $this->codmField);
            $stmt->bindParam(':data', $this->dataField);
            $stmt->bindParam(':ora', $this->oraField);

            $stmt->execute();
            echo "Înregistrare adăugată cu succes.";
            $this->logOperation('insert');
        } catch (PDOException $e) {
            echo "Eroare la adăugare: " . $e->getMessage();
        }
    }

    public function performDelete() {
        try {
            if (empty($this->codprogField)) {
                echo "Codprog este obligatoriu pentru ștergere.";
                return;
            }

            $query = "DELETE FROM Programari WHERE Codprog = :codprog";
            $stmt = $this->connection->prepare($query);

            $stmt->bindParam(':codprog', $this->codprogField);

            $stmt->execute();
            echo "Înregistrare ștearsă cu succes.";
            $this->logOperation('delete');
        } catch (PDOException $e) {
            echo "Eroare la ștergere: " . $e->getMessage();
        }
    }

    public function performUpdate() {
        try {
            if (empty($this->codprogField)) {
                echo "Codprog este obligatoriu pentru modificare.";
                return;
            }

            $setClause = "";
            if (!empty($this->codcliField)) {
                $setClause .= "Codcli = :codcli, ";
            }
            if (!empty($this->codmField)) {
                $setClause .= "Codm = :codm, ";
            }
            if (!empty($this->dataField)) {
                $setClause .= "Data = :data, ";
            }
            if (!empty($this->oraField)) {
                $setClause .= "Ora = :ora, ";
            }

            $setClause = rtrim($setClause, ", ");

            $query = "UPDATE Programari SET " . $setClause . " WHERE Codprog = :codprog";
            $stmt = $this->connection->prepare($query);

            $stmt->bindParam(':codprog', $this->codprogField);
            if (!empty($this->codcliField)) {
                $stmt->bindParam(':codcli', $this->codcliField);
            }
            if (!empty($this->codmField)) {
                $stmt->bindParam(':codm', $this->codmField);
            }
            if (!empty($this->dataField)) {
                $stmt->bindParam(':data', $this->dataField);
            }
            if (!empty($this->oraField)) {
                $stmt->bindParam(':ora', $this->oraField);
            }

            $stmt->execute();
            echo "Înregistrare modificată cu succes.";
            $this->logOperation('update');
        } catch (PDOException $e) {
            echo "Eroare la modificare: " . $e->getMessage();
        }
    }

    private function logOperation($operationType) {
        try {
            $getMaxKeyQuery = "SELECT MAX(CodOp) FROM istoricoperatii";
            $stmt = $this->connection->prepare($getMaxKeyQuery);
            $stmt->execute();
            $maxKey = $stmt->fetchColumn();
            $nextKey = $maxKey ? $maxKey + 1 : 1;

            $query = "INSERT INTO istoricoperatii (CodOp, User, Tabela_folosita, Tip_Operatie, Data_Ora) VALUES (:codop, :user, :tabela, :tip_operatie, :data_ora)";
            $stmt = $this->connection->prepare($query);
            $stmt->bindParam(':codop', $nextKey);
            $stmt->bindParam(':user', $user);
            $stmt->bindParam(':tabela', $tabela);
            $stmt->bindParam(':tip_operatie', $tip_operatie);

            $user = "Angajat";
            $tabela = "Programari";
            $tip_operatie = $operationType;
            $data_ora = date('Y-m-d H:i:s');
            $stmt->bindParam(':data_ora', $data_ora);

            $stmt->execute();
        } catch (PDOException $e) {
            echo "Eroare la înregistrarea operației în jurnal: " . $e->getMessage();
        }
    }
}

// Handle the AJAX request
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $operation = $_POST['operation'];
    $codprog = $_POST['codprog'];
    $codcli = $_POST['codcli'];
    $codm = $_POST['codm'];
    $data = $_POST['data'];
    $ora = $_POST['ora'];

    $handler = new DatabaseHandler($codprog, $codcli, $codm, $data, $ora);

    switch ($operation) {
        case 'search':
            $handler->performSearch();
            break;
        case 'insert':
            $handler->performInsert();
            break;
        case 'delete':
            $handler->performDelete();
            break;
        case 'update':
            $handler->performUpdate();
            break;
        default:
            echo "Operație necunoscută.";
            break;
    }
}
?>
