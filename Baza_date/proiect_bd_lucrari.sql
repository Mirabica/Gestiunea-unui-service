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
-- Table structure for table `lucrari`
--

DROP TABLE IF EXISTS `lucrari`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lucrari` (
  `codluc` int NOT NULL AUTO_INCREMENT,
  `coda` int DEFAULT NULL,
  `dataluc` date DEFAULT NULL,
  `cost` decimal(10,2) DEFAULT NULL,
  `tipluc` varchar(100) DEFAULT NULL,
  `codm` int DEFAULT NULL,
  PRIMARY KEY (`codluc`),
  KEY `codm` (`codm`),
  KEY `coda` (`coda`),
  CONSTRAINT `lucrari_ibfk_1` FOREIGN KEY (`codm`) REFERENCES `masini` (`codm`),
  CONSTRAINT `lucrari_ibfk_2` FOREIGN KEY (`coda`) REFERENCES `angajati` (`coda`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lucrari`
--

LOCK TABLES `lucrari` WRITE;
/*!40000 ALTER TABLE `lucrari` DISABLE KEYS */;
INSERT INTO `lucrari` VALUES (1,1,'2021-02-24',950.00,'distributie',3),(2,4,'2022-12-29',1250.00,'ambreiaj',4),(3,3,'2023-09-29',1300.00,'placute',7),(4,2,'2019-08-30',800.00,'cutie viteze',1),(5,6,'2018-05-23',900.00,'bujii',9),(6,5,'2020-10-15',550.00,'mentenanta',10),(7,4,'2019-05-04',400.00,'revizie',12),(8,1,'2023-06-10',200.00,'distributie',11),(9,2,'2020-04-09',300.00,'turbina',14),(10,4,'2021-09-29',1000.00,'cutie viteze',2),(11,2,'2023-11-03',1200.00,'frane',13),(12,6,'2021-11-10',850.00,'servodirectia',5),(13,3,'2022-01-22',650.00,'bujii',8),(15,5,'2023-03-31',550.00,'bujii',1);
/*!40000 ALTER TABLE `lucrari` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-06 21:20:16
