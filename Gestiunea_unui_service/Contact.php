<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Contact</title>
    <style>
        body {
            background-image: url('Imagini/contact2.jpg');
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            color: yellow;
            font-family: 'Times New Roman', serif;
        }
        .container {
            background-color: rgba(0, 0, 0, 0.6);
            padding: 20px;
            border: 2px solid yellow;
            border-radius: 10px;
            text-align: center;
        }
        .container h1 {
            font-size: 30px;
            margin-bottom: 20px;
        }
        .contact-info {
            font-size: 20px;
            margin-bottom: 20px;
        }
        .contact-info div {
            margin: 10px 0;
        }
        .button-container {
            display: flex;
            justify-content: center;
        }
        .button-container button {
            padding: 10px 20px;
            border: 1px solid black;
            background-color: rgb(105, 105, 105);
            color: black;
            font-family: 'Arial Black', sans-serif;
            font-size: 12px;
            cursor: pointer;
            margin-top: 20px;
        }
        .button-container button:hover {
            background-color: rgb(169, 169, 169);
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Contact</h1>
        <div class="contact-info">
            <div><strong>Telefon:</strong> 0239 671 100 | 0236 419 898</div>
            <div><strong>Mail:</strong> service@autowab.ro</div>
            <div><strong>Locație:</strong> București, Strada Fizicienilor nr. 21B, Sector 3</div>
        </div>
        <div class="button-container">
            <form action="Clienti.html" method="post">
                <button type="submit" name="action" value="back">BACK</button>
            </form>
        </div>
    </div>
</body>
</html>
