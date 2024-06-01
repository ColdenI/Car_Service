-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: car_service
-- ------------------------------------------------------
-- Server version	8.3.0

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
-- Table structure for table `malfunction`
--

DROP TABLE IF EXISTS `malfunction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `malfunction` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `description` varchar(300) NOT NULL,
  `cost` int NOT NULL,
  `autopart_id` int NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_malfunction_autopart1_idx` (`autopart_id`),
  KEY `fk_malfunction_user1_idx` (`user_id`),
  CONSTRAINT `fk_malfunction_autopart1` FOREIGN KEY (`autopart_id`) REFERENCES `autopart` (`id`),
  CONSTRAINT `fk_malfunction_user1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `malfunction`
--

LOCK TABLES `malfunction` WRITE;
/*!40000 ALTER TABLE `malfunction` DISABLE KEYS */;
INSERT INTO `malfunction` VALUES (1,'Замена аккумулятора','Замена аккумулятора',1000,1,5),(2,'Прокол колеса','Замена колеса',400,2,3),(3,'Замена свечи','Замена свечи',400,9,3),(4,'Сломана магнитола','Замена магнитолы',1000,10,5),(5,'Помятое крыло переднее правое','Замена крыла',2000,3,4),(6,'Помятое крыло заднее правое','Замена крыла',10000,4,4),(7,'Помятое крыло переднее левое','Замена крыла',2000,6,4),(8,'Помятое крыло заднее левое','Замена крыла',10000,5,4),(9,'Протёртый ГРМ','Замена ремня ГРМ',1000,11,3),(10,'Помята крыша (Audi A8)','Замена крыши на машине Audi A8',10000,12,4),(11,'Помятый капот Audi A8','Замена капота Audi A8',5000,13,4),(12,'Помято переднее левое крыло Audi A8','Замена переднего левого крыла на Audi A8\r\n(Audi A8 переднее левое крыло) \r\n',13000,14,7);
/*!40000 ALTER TABLE `malfunction` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-01 21:37:40
