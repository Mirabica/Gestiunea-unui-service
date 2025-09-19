<?php
session_start();

// Funcție pentru validarea credențialelor și determinarea tipului de utilizator
function validateCredentials($username, $password) {
    // Credențiale pentru administrator
    $adminUsername = "admin";
    $adminPassword = "admin1";

    // Credențiale pentru angajați
    $employees = [
        ["username" => "angajat1", "password" => "bd1"],
        ["username" => "angajat2", "password" => "bd2"],
        ["username" => "angajat3", "password" => "bd3"]
    ];

    // Verifică credențialele
    if ($username == $adminUsername && $password == $adminPassword) {
        return "ADMIN";
    } else {
        foreach ($employees as $employee) {
            if ($username == $employee['username'] && $password == $employee['password']) {
                return "EMPLOYEE";
            }
        }
    }
    return "CLIENT";
}

// Procesarea formularului de login
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_POST['username'];
    $password = $_POST['password'];

    $userType = validateCredentials($username, $password);

    // Redirecționează utilizatorul la pagina corespunzătoare tipului de utilizator
    switch ($userType) {
        case "ADMIN":
            header("Location: Admin.html");
            break;
        case "EMPLOYEE":
            header("Location: Angajat.html");
            break;
        case "CLIENT":
            header("Location: Clienti.html");
            break;
    }
    exit();
}
?>
