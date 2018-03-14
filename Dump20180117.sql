CREATE DATABASE  IF NOT EXISTS `bug_tracking_system` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `bug_tracking_system`;
-- MySQL dump 10.13  Distrib 5.7.17, for macos10.12 (x86_64)
--
-- Host: localhost    Database: bug_tracking_system
-- ------------------------------------------------------
-- Server version	5.7.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Files`
--

DROP TABLE IF EXISTS `Files`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Files` (
  `ID` datetime NOT NULL,
  `Extension` varchar(45) DEFAULT NULL,
  `Data` longblob,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Files`
--

LOCK TABLES `Files` WRITE;
/*!40000 ALTER TABLE `Files` DISABLE KEYS */;
/*!40000 ALTER TABLE `Files` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category_table`
--

DROP TABLE IF EXISTS `category_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `category_table` (
  `category_id` int(11) NOT NULL AUTO_INCREMENT,
  `category_name` varchar(45) NOT NULL,
  PRIMARY KEY (`category_id`),
  UNIQUE KEY `category_id_UNIQUE` (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category_table`
--

LOCK TABLES `category_table` WRITE;
/*!40000 ALTER TABLE `category_table` DISABLE KEYS */;
INSERT INTO `category_table` VALUES (1,'General'),(2,'Live'),(3,'Contents'),(4,'Data Admin'),(5,'Setting');
/*!40000 ALTER TABLE `category_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer_table`
--

DROP TABLE IF EXISTS `customer_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer_table` (
  `customer_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_name` varchar(45) NOT NULL,
  PRIMARY KEY (`customer_id`),
  UNIQUE KEY `customer_id_UNIQUE` (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_table`
--

LOCK TABLES `customer_table` WRITE;
/*!40000 ALTER TABLE `customer_table` DISABLE KEYS */;
INSERT INTO `customer_table` VALUES (0,'N3N'),(1,'Cisco'),(2,'Smok'),(3,'ATT');
/*!40000 ALTER TABLE `customer_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `knowledge_base_table`
--

DROP TABLE IF EXISTS `knowledge_base_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `knowledge_base_table` (
  `knowledge_base_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `user_name` varchar(255) NOT NULL,
  `description` varchar(2000) NOT NULL,
  `ticket_no` varchar(45) NOT NULL,
  `comment_date` datetime NOT NULL,
  PRIMARY KEY (`knowledge_base_id`),
  UNIQUE KEY `knowledge_base_id_UNIQUE` (`knowledge_base_id`),
  KEY `knowledge_base_table_user_id_idx` (`user_id`),
  CONSTRAINT `knowledge_base_table_user_id` FOREIGN KEY (`user_id`) REFERENCES `user_table` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `knowledge_base_table`
--

LOCK TABLES `knowledge_base_table` WRITE;
/*!40000 ALTER TABLE `knowledge_base_table` DISABLE KEYS */;
INSERT INTO `knowledge_base_table` VALUES (1,3,'Ciscouser','Nothing is working\r\n','1','2017-12-26 16:56:13'),(2,3,'DeveloperSid','i am working on it','2','2018-01-05 17:47:14'),(3,3,'DeveloperSid','Done.please confirm','2','2018-01-05 17:47:31'),(4,3,'Ciscouser','gOOD','1','2018-01-12 16:16:19'),(5,3,'Ciscouser','Good','1','2018-01-16 12:20:57'),(6,3,'Ciscouser','s','1','2018-01-16 12:26:56'),(7,3,'Ciscouser','Do Something!! ASAP!!','8','2018-01-16 16:44:12'),(8,3,'DeveloperSid','Working On It!! Show some patience!!','8','2018-01-16 16:54:39'),(9,3,'DeveloperSid','I have Resolved it now!! kINDLY CHECK!!','8','2018-01-16 16:55:18'),(10,3,'DeveloperSid','fINISHED','8','2018-01-16 16:55:42'),(11,3,'Developer1','Will try to finish it as soon as possible','7','2018-01-16 16:59:41'),(12,3,'Ciscouser','No way man!! i want it asap!!!','7','2018-01-16 17:00:23'),(13,3,'Developer1','Lol!! ASAP means AS SOON AS POSSIBLE ONLY BRO!! Grow up!!','7','2018-01-16 17:01:16'),(14,3,'Developer1','Resolved it!! Now Go To Hell!!','7','2018-01-16 17:01:36'),(15,3,'Ciscouser','No man I did not like the solution!! You Go to hell','7','2018-01-16 17:02:18'),(16,3,'Developer1','Bro if you want to live happily..Just accept the solution and move on.','7','2018-01-16 17:05:59'),(17,3,'Ciscouser','Do something!!','7','2018-01-16 17:16:13'),(18,3,'Developer1','Ok I am working!!','7','2018-01-17 09:24:54'),(19,3,'Developer1','RESOLVED!! kINDLY CHECK !!','7','2018-01-17 09:25:23'),(20,3,'Developer1','Admin please assign to someone else','4','2018-01-17 09:29:24'),(21,20,'viraj','PLEASE SOLVE IT ASAP','9','2018-01-17 09:34:09'),(22,20,'DeveloperSid','Resolved !!','9','2018-01-17 09:35:28'),(23,3,'DeveloperSid','gfjglk','3','2018-01-17 10:57:07'),(24,3,'DeveloperSid','Pllease reaasign!!!','1','2018-01-17 11:08:59'),(25,3,'DeveloperSid','fufh\r\n','5','2018-01-17 11:13:05');
/*!40000 ALTER TABLE `knowledge_base_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `module_table`
--

DROP TABLE IF EXISTS `module_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `module_table` (
  `module_id` int(11) NOT NULL,
  `module_name` varchar(45) NOT NULL,
  `category_id` int(11) NOT NULL,
  PRIMARY KEY (`module_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `module_table`
--

LOCK TABLES `module_table` WRITE;
/*!40000 ALTER TABLE `module_table` DISABLE KEYS */;
INSERT INTO `module_table` VALUES (1,'Connection Failed',1),(2,'Login Failed',1),(3,'License Failed',1),(4,'Others',1),(5,'General',2),(6,'View',2),(7,'Map',2),(8,'Camera',2),(9,'Chart',2),(10,'Grid Template',2),(11,'Others',2),(12,'Custom Map',3),(13,'Map Template',3),(14,'Library',3),(15,'DTG Area Type',3),(16,'Map Tree',3),(17,'Object',4),(18,'Rule Engine',4),(19,'Account',5),(20,'Modules',5),(21,'Backup & Restore',5),(22,'General Setting',5);
/*!40000 ALTER TABLE `module_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `problem_table`
--

DROP TABLE IF EXISTS `problem_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `problem_table` (
  `ticket_number` int(11) NOT NULL AUTO_INCREMENT,
  `assignee_user_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `problem_desc` text NOT NULL,
  `summary` text,
  `severity` int(11) DEFAULT NULL,
  `attachment` mediumblob,
  `create_date` datetime DEFAULT NULL,
  `status_id` int(11) DEFAULT NULL,
  `last_update_date` datetime DEFAULT NULL,
  `created_by` int(11) DEFAULT NULL,
  `updated_by` int(11) DEFAULT NULL,
  `estimated_complete_date` date DEFAULT NULL,
  `module_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`ticket_number`),
  KEY `assignee_user_id` (`assignee_user_id`),
  KEY `user_id` (`user_id`),
  KEY `created_by` (`created_by`),
  KEY `updated_by` (`updated_by`),
  KEY `status_id` (`status_id`),
  CONSTRAINT `problem_table_ibfk_1` FOREIGN KEY (`assignee_user_id`) REFERENCES `user_table` (`user_id`),
  CONSTRAINT `problem_table_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `user_table` (`user_id`),
  CONSTRAINT `problem_table_ibfk_3` FOREIGN KEY (`created_by`) REFERENCES `user_table` (`user_id`),
  CONSTRAINT `problem_table_ibfk_4` FOREIGN KEY (`updated_by`) REFERENCES `user_table` (`user_id`),
  CONSTRAINT `problem_table_ibfk_5` FOREIGN KEY (`status_id`) REFERENCES `status_table` (`status_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `problem_table`
--

LOCK TABLES `problem_table` WRITE;
/*!40000 ALTER TABLE `problem_table` DISABLE KEYS */;
INSERT INTO `problem_table` VALUES (1,5,3,'Login Not working','Fix the problem bro',1,NULL,'2017-12-26 16:55:54',2,'2017-12-26 16:55:54',1,3,NULL,1),(2,4,3,'ss','ss',1,NULL,'2017-12-27 13:14:29',3,'2017-12-27 13:14:29',NULL,3,'2018-01-12',2),(3,4,3,'sd','sd',1,NULL,'2017-12-27 15:36:32',3,'2017-12-27 15:36:32',1,3,NULL,0),(4,5,3,'asdjksh','jashkjash',1,NULL,'2018-01-08 13:14:59',1,'2018-01-08 13:14:59',1,3,NULL,2),(5,4,3,'ssds','ddd',1,NULL,'2018-01-08 15:17:03',2,'2018-01-08 15:17:03',1,3,NULL,3),(6,NULL,7,'Nothing is workin','jgfhjshfkdsgfk',3,NULL,'2018-01-10 17:01:30',1,'2018-01-10 17:01:30',2,7,NULL,2),(7,5,3,'ram','shyam',1,NULL,'2018-01-12 14:08:42',3,'2018-01-12 14:08:42',1,3,'2018-01-31',3),(8,4,3,'Mouse is Not working','Battery is dead',3,NULL,'2018-01-16 16:43:54',3,'2018-01-16 16:43:54',1,3,'2018-01-18',2),(9,4,20,'Keypad not working','fgfhjsfjhdsh',3,NULL,'2018-01-17 09:33:48',3,'2018-01-17 09:33:48',1,20,'2018-01-17',1);
/*!40000 ALTER TABLE `problem_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_table`
--

DROP TABLE IF EXISTS `product_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_table` (
  `product_id` int(11) NOT NULL,
  `product_name` varchar(45) NOT NULL,
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_table`
--

LOCK TABLES `product_table` WRITE;
/*!40000 ALTER TABLE `product_table` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_table`
--

DROP TABLE IF EXISTS `role_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_table` (
  `role_id` int(11) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(45) NOT NULL,
  PRIMARY KEY (`role_id`),
  UNIQUE KEY `role_id_UNIQUE` (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_table`
--

LOCK TABLES `role_table` WRITE;
/*!40000 ALTER TABLE `role_table` DISABLE KEYS */;
INSERT INTO `role_table` VALUES (1,'user'),(2,'developer'),(3,'admin'),(4,'customeradmin');
/*!40000 ALTER TABLE `role_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status_table`
--

DROP TABLE IF EXISTS `status_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `status_table` (
  `status_id` int(11) NOT NULL,
  `status_name` varchar(45) NOT NULL,
  PRIMARY KEY (`status_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status_table`
--

LOCK TABLES `status_table` WRITE;
/*!40000 ALTER TABLE `status_table` DISABLE KEYS */;
INSERT INTO `status_table` VALUES (1,'New'),(2,'In Progress'),(3,'Complete'),(4,'On Hold'),(5,'Resolved'),(6,'Cancel');
/*!40000 ALTER TABLE `status_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_table`
--

DROP TABLE IF EXISTS `user_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_table` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `phone_number` bigint(20) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `role_id` int(11) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_id_UNIQUE` (`user_id`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `user_name_UNIQUE` (`user_name`),
  KEY `user_table_customer_id_idx` (`customer_id`),
  KEY `user_table_role_id_idx` (`role_id`),
  CONSTRAINT `user_table_customer_id` FOREIGN KEY (`customer_id`) REFERENCES `customer_table` (`customer_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `user_table_role_id` FOREIGN KEY (`role_id`) REFERENCES `role_table` (`role_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_table`
--

LOCK TABLES `user_table` WRITE;
/*!40000 ALTER TABLE `user_table` DISABLE KEYS */;
INSERT INTO `user_table` VALUES (1,'admin','siddharth.gore@n3n.io',6692546905,0,3,'202cb962ac59075b964b07152d234b70'),(2,'Ciscoadmin','cisco@i12.com',123,1,4,'202cb962ac59075b964b07152d234b70'),(3,'Ciscouser','ww@ss.com',6692546905,1,1,'202cb962ac59075b964b07152d234b70'),(4,'DeveloperSid','siddharth.gore@live.com',123,0,2,'202cb962ac59075b964b07152d234b70'),(5,'Developer1','siddharth.gore@sjsu.edu',111111111,0,2,'202cb962ac59075b964b07152d234b70'),(6,'Smokadmin','Smokdefault',123,2,4,'202cb962ac59075b964b07152d234b70'),(7,'smokuser','smoka@123.com',1234,2,1,'202cb962ac59075b964b07152d234b70'),(14,'smokuser1','sq@sc.m',1234,1,1,'202cb962ac59075b964b07152d234b70'),(19,'DeveloperSid2','sss@sss.com',22,1,1,'6512bd43d9caa6e02c990b0a82652dca'),(20,'viraj','viraj.nilakh@n3n.io',111111,1,1,'81dc9bdb52d04dc20036dbd8313ed055'),(21,'ATTadmin','ATTdefault',123,3,4,'202cb962ac59075b964b07152d234b70');
/*!40000 ALTER TABLE `user_table` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-17 11:31:17
