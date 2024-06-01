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
-- Table structure for table `malfunction_order`
--

DROP TABLE IF EXISTS `malfunction_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `malfunction_order` (
  `id` int NOT NULL AUTO_INCREMENT,
  `malfunction_id` int NOT NULL,
  `order_id` int NOT NULL,
  `status` varchar(45) NOT NULL DEFAULT 'В ремонте',
  PRIMARY KEY (`id`),
  KEY `fk_malfunction_order_malfunction1_idx` (`malfunction_id`),
  KEY `fk_malfunction_order_order1_idx` (`order_id`),
  CONSTRAINT `fk_malfunction_order_malfunction1` FOREIGN KEY (`malfunction_id`) REFERENCES `malfunction` (`id`),
  CONSTRAINT `fk_malfunction_order_order1` FOREIGN KEY (`order_id`) REFERENCES `order` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `malfunction_order`
--

LOCK TABLES `malfunction_order` WRITE;
/*!40000 ALTER TABLE `malfunction_order` DISABLE KEYS */;
INSERT INTO `malfunction_order` VALUES (14,3,3,'В процессе'),(15,7,3,'В процессе'),(16,8,3,'В процессе'),(17,6,3,'В процессе'),(18,5,3,'Готово'),(19,2,3,'В процессе'),(20,1,3,'В процессе'),(21,4,3,'Готово'),(30,4,2,'В процессе'),(31,2,4,'В процессе'),(32,10,1,'Готово'),(33,11,1,'В процессе'),(34,12,1,'В процессе');
/*!40000 ALTER TABLE `malfunction_order` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-01 21:37:41
