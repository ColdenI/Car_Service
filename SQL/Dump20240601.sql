CREATE DATABASE  IF NOT EXISTS `car_service` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `car_service`;
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
-- Table structure for table `autopart`
--

DROP TABLE IF EXISTS `autopart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `autopart` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `description` varchar(300) NOT NULL,
  `cost` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autopart`
--

LOCK TABLES `autopart` WRITE;
/*!40000 ALTER TABLE `autopart` DISABLE KEYS */;
INSERT INTO `autopart` VALUES (1,'Аккумулятор','Аккумулятор 12 вольт',5000),(2,'Колесо','Колесо',10000),(3,'Крыло (правое, переднее)','Крыло (правое, переднее)',8000),(4,'Крыло (правое, заднее)','Крыло (правое, переднее)',15000),(5,'Крыло (левое, заднее)','Крыло (правое, переднее)',15000),(6,'Крыло (левое, переднее)','Крыло (правое, переднее)',8000),(7,'Крыша','Крыша',30000),(8,'Капот','Капот',20000),(9,'Свеча','Свеча',800),(10,'Магнитола','Магнитола',10000),(11,'Ремень ГРМ','Ремень ГРМ',2000),(12,'Крыша (Audi A8)','Крыша (Audi A8)',30000),(13,'Капот Audi A8','Капот Audi A8',15000),(14,'Переднее левое крыло Audi A8','Переднее левое крыло Audi A8',25000);
/*!40000 ALTER TABLE `autopart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `car`
--

DROP TABLE IF EXISTS `car`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `car` (
  `id` int NOT NULL AUTO_INCREMENT,
  `stamp` varchar(45) NOT NULL,
  `model` varchar(45) NOT NULL,
  `year_release` int NOT NULL,
  `mileage` int NOT NULL,
  `license_plate_number` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `car`
--

LOCK TABLES `car` WRITE;
/*!40000 ALTER TABLE `car` DISABLE KEYS */;
INSERT INTO `car` VALUES (1,'Audi','A8',1994,60937,'Р003ВО37'),(2,'Toyota','Corolla',2008,83886,'А038ЕК66'),(3,'ЗАЗ','968М',1982,125083,'Р353МО96'),(4,'Kia','Sorento',2019,239003,'О234ТА799');
/*!40000 ALTER TABLE `car` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `id` int NOT NULL AUTO_INCREMENT,
  `lname` varchar(45) NOT NULL,
  `fname` varchar(45) NOT NULL,
  `mname` varchar(45) NOT NULL,
  `number_phone` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'Петров','Иван','Яковлевич','+7 987 654 32-10'),(2,'Смирнов','Пётр','Дмитриевич','+7 942 532 34 29'),(3,'Петров','Иван','Саватеевич','+7 984 665 44-00'),(4,'Сидоров','Сергей','Николаевич','+7 945 123 44 84');
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

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

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `id` int NOT NULL AUTO_INCREMENT,
  `status` varchar(45) NOT NULL,
  `description` varchar(300) NOT NULL,
  `client_id` int NOT NULL,
  `car_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_order_client_idx` (`client_id`),
  KEY `fk_order_car1_idx` (`car_id`),
  CONSTRAINT `fk_order_car1` FOREIGN KEY (`car_id`) REFERENCES `car` (`id`),
  CONSTRAINT `fk_order_client` FOREIGN KEY (`client_id`) REFERENCES `client` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,'В процессе','помята',1,1),(2,'Диагностика завершена','не работает магнитола',2,2),(3,'Отменён','не едет',3,3),(4,'Диагностика завершена','спустило колесо',4,4);
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `post` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `description` varchar(300) NOT NULL,
  `wages` int NOT NULL,
  `tech_name` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post`
--

LOCK TABLES `post` WRITE;
/*!40000 ALTER TABLE `post` DISABLE KEYS */;
INSERT INTO `post` VALUES (1,'Администратор','Администратор',100000,1),(2,'Менеджер','Менеджер',80000,2),(3,'Мастер','Мастер, механик может проводить диагностику авто',85000,4),(4,'Кузовщик','Кузовщик',70000,3),(5,'Электрик','Электрик',70000,3);
/*!40000 ALTER TABLE `post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `lname` varchar(45) NOT NULL,
  `fname` varchar(45) NOT NULL,
  `mname` varchar(45) NOT NULL,
  `date_birth` date NOT NULL,
  `education` varchar(300) NOT NULL,
  `start_work_date` date NOT NULL,
  `end_work_date` date NOT NULL DEFAULT '1900-01-01',
  `post_id` int NOT NULL,
  PRIMARY KEY (`id`,`post_id`),
  KEY `fk_user_post1_idx` (`post_id`),
  CONSTRAINT `fk_user_post1` FOREIGN KEY (`post_id`) REFERENCES `post` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Иванов','Давид','Вячеславович','1978-01-04','Высшее','2013-05-12','1900-01-01',1),(2,'Малышев','Юрий','Павлович','1984-03-21','Высшее','2018-08-09','1900-01-01',2),(3,'Панов','Алексей','Иванович','1992-02-05','Среднее','2020-11-20','1900-01-01',3),(4,'Малинин','Даниил','Святославович','1987-09-23','Среднее','2020-11-20','1900-01-01',4),(5,'Тарасов','Михаил','Павлович','1989-06-19','Высшее','2017-03-18','1900-01-01',5),(6,'Пеньков','Тарас','Петрович','2024-05-29','Среднее','2024-05-29','2024-05-29',1),(7,'Зубов','Роман','Кириллович','1985-07-01','Высшее','2024-05-30','1900-01-01',3);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user-auth`
--

DROP TABLE IF EXISTS `user-auth`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user-auth` (
  `login` varchar(64) NOT NULL,
  `password` varchar(64) NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`user_id`),
  CONSTRAINT `fk_user-auth_user1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user-auth`
--

LOCK TABLES `user-auth` WRITE;
/*!40000 ALTER TABLE `user-auth` DISABLE KEYS */;
INSERT INTO `user-auth` VALUES ('l1','p1',1),('l2','p2',2),('l3','p3',3),('l4','p4',4),('l5','p5',5),('12345678','12345678',6),('zubov1985','zubov1985',7);
/*!40000 ALTER TABLE `user-auth` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-01 21:38:43
