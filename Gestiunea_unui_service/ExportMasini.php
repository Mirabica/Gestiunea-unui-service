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
 
    // Include TCPDF library
    include ('tcpdf/tcpdf.php');
 
    // Fetch data from the Echipare table
    $sql = "SELECT * FROM Masini";
    $result = $conn->query($sql);
 
    // Create new PDF instance
    $pdf = new TCPDF();
    $pdf->setPrintHeader(false);
    $pdf->setPrintFooter(false);
    $pdf->AddPage();
 
    // Set title for the PDF
    $pdf->SetTitle('Masini');
 
    // Add title to the PDF
    $pdf->SetFont('helvetica', 'B', 14);
    $pdf->Cell(0, 10, 'Masini', 0, 1, 'C');
    $pdf->Ln(10); // Add some space after the title
 
    // Add table header to PDF
    $html = '<table border="1">';
    $html .= '<tr><th>Codm</th><th>Codcli</th><th>Marca</th><th>Model</th><th>An_fabricatie</th><th>SeriaVIN</th></tr>';
 
    // Add data rows to the PDF
    if ($result->rowCount() > 0) {
        while($row = $result->fetch(PDO::FETCH_ASSOC)) {
            $html .= '<tr>';
            $html .= '<td>'.$row['codm'].'</td>';
            $html .= '<td>'.$row['codcli'].'</td>';
            $html .= '<td>'.$row['marca'].'</td>';
            $html .= '<td>'.$row['model'].'</td>';
            $html .= '<td>'.$row['an_fabricatie'].'</td>';
            $html .= '<td>'.$row['seriaVIN'].'</td>';
            $html .= '</tr>';
        }
    }
    $html .= '</table>';
 
    $pdf->writeHTML($html, true, false, true, false, '');
 
    // Set headers for PDF download
    header('Content-Type: application/pdf');
    header('Content-Disposition: attachment; filename="Masini.pdf"');
 
    // Output the PDF file
    $pdf->Output('Masini.pdf', 'D');
} catch(PDOException $e) {
    echo "Conexiunea la baza de date a eșuat: " . $e->getMessage();
}
?>