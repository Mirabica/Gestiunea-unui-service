<?php
// Procesarea acțiunilor de pe pagina de clienti
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $action = $_POST['action'];

    switch ($action) {
        case 'vizualizare_programari':
            header("Location: vizualizare_programari_clienti.html");
            break;
        case 'vizualizare_masini':
            header("Location: vizualizare_masini_clienti.html");
            break;
        case 'contact':
            header("Location: contact.php");
            break;
        case 'exit':
            exit();
    }
    exit();
}
?>
