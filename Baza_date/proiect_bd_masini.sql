-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: proiect_bd
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `masini`
--

DROP TABLE IF EXISTS `masini`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `masini` (
  `codm` int NOT NULL AUTO_INCREMENT,
  `codcli` int DEFAULT NULL,
  `marca` varchar(50) DEFAULT NULL,
  `model` varchar(50) DEFAULT NULL,
  `an_fabricatie` int DEFAULT NULL,
  `seriaVIN` varchar(9) DEFAULT NULL,
  PRIMARY KEY (`codm`),
  KEY `codcli` (`codcli`),
  CONSTRAINT `masini_ibfk_1` FOREIGN KEY (`codcli`) REFERENCES `clienti` (`codcli`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `masini`
--

LOCK TABLES `masini` WRITE;
/*!40000 ALTER TABLE `masini` DISABLE KEYS */;
INSERT INTO `masini` VALUES (1,10,'Audi','A8',2010,'134688897'),(2,14,'Mercedes','300 SL',2000,'874563219'),(3,16,'Ford','Focus 2',2008,'623598741'),(4,15,'Nissan','Juke',2016,'198347562'),(5,17,'Volkswagen','Eos',2008,'752614839'),(6,21,'Ford','Mustang',2018,'485372916'),(7,20,'Dacia','Duster',2010,'917465823'),(8,23,'Volkswagen','Passat',2016,'536271894'),(9,24,'Bmw','I8',2020,'879134526'),(10,26,'Audi','Q7',2014,'111111111'),(11,25,'Porsche','Taycan',2022,'647258319'),(12,30,'Kia','Sportage',2021,'521497836'),(13,29,'Honda','Civic',2019,'817345926'),(14,19,'Volvo','XC90',2019,'752819346'),(15,23,'Volkswagen','Paasat',2020,'538492618'),(16,17,'Volkswagen','Eos',2013,'492549442');
/*!40000 ALTER TABLE `masini` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-06 21:20:15
