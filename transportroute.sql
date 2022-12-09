-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.4.19-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных transportroute
CREATE DATABASE IF NOT EXISTS `transportroute` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `transportroute`;

-- Дамп структуры для таблица transportroute.buses
CREATE TABLE IF NOT EXISTS `buses` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Brand` longtext NOT NULL,
  `Number` longtext NOT NULL,
  `Driver` longtext NOT NULL,
  `Seats` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы transportroute.buses: ~3 rows (приблизительно)
/*!40000 ALTER TABLE `buses` DISABLE KEYS */;
INSERT INTO `buses` (`Id`, `Brand`, `Number`, `Driver`, `Seats`) VALUES
	(1, 'ПАЗ-3206', 'А154ВА', 'Сергей Владимирович', 28),
	(2, 'ПАЗ-3205', 'В875ПА', 'Алексей Иванович', 25),
	(3, 'ЛиАЗ-5293', 'К456РО', 'Иван Сергеевич', 30);
/*!40000 ALTER TABLE `buses` ENABLE KEYS */;

-- Дамп структуры для таблица transportroute.routes
CREATE TABLE IF NOT EXISTS `routes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RouteStart` longtext NOT NULL,
  `RouteEnd` longtext DEFAULT NULL,
  `TravelTime` time(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы transportroute.routes: ~3 rows (приблизительно)
/*!40000 ALTER TABLE `routes` DISABLE KEYS */;
INSERT INTO `routes` (`Id`, `RouteStart`, `RouteEnd`, `TravelTime`) VALUES
	(1, 'Широкая', 'Дом офицеров', '00:30:00.000000'),
	(2, 'Автовокзал', 'пос. Лукашова', '00:35:00.000000'),
	(3, 'Широкая', 'Осенняя', '00:30:00.000000'),
	(5, 'Шалаева', 'Невская', '02:54:00.000000');
/*!40000 ALTER TABLE `routes` ENABLE KEYS */;

-- Дамп структуры для таблица transportroute.runs
CREATE TABLE IF NOT EXISTS `runs` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RunStart` time(6) NOT NULL,
  `RunEnd` time(6) NOT NULL,
  `Routeid` int(11) NOT NULL,
  `Busid` int(11) NOT NULL,
  `TicketPrice` int(11) NOT NULL,
  `Date` date NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Runs_Busid` (`Busid`),
  KEY `IX_Runs_Routeid` (`Routeid`),
  CONSTRAINT `FK_Runs_Buses_Busid` FOREIGN KEY (`Busid`) REFERENCES `buses` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Runs_Routes_Routeid` FOREIGN KEY (`Routeid`) REFERENCES `routes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы transportroute.runs: ~9 rows (приблизительно)
/*!40000 ALTER TABLE `runs` DISABLE KEYS */;
INSERT INTO `runs` (`Id`, `RunStart`, `RunEnd`, `Routeid`, `Busid`, `TicketPrice`, `Date`) VALUES
	(1, '13:15:00.000000', '10:53:00.000000', 2, 2, 23, '2011-11-18'),
	(3, '07:53:00.000000', '07:53:00.000000', 2, 1, 24, '2022-11-19'),
	(10, '11:11:00.000000', '22:22:00.000000', 1, 1, 27, '2022-11-26'),
	(16, '11:11:00.000000', '22:22:00.000000', 1, 1, 27, '2022-11-26'),
	(17, '11:11:00.000000', '22:22:00.000000', 1, 1, 27, '2022-11-26'),
	(18, '11:11:00.000000', '22:22:00.000000', 1, 1, 27, '2022-11-26'),
	(19, '11:11:00.000000', '22:22:00.000000', 1, 1, 27, '2022-11-26'),
	(20, '11:11:00.000000', '22:22:00.000000', 1, 1, 27, '2022-11-26');
/*!40000 ALTER TABLE `runs` ENABLE KEYS */;

-- Дамп структуры для таблица transportroute.stoproutes
CREATE TABLE IF NOT EXISTS `stoproutes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NameStop` longtext NOT NULL,
  `Routeid` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Stoproutes_Routeid` (`Routeid`),
  CONSTRAINT `FK_Stoproutes_Routes_Routeid` FOREIGN KEY (`Routeid`) REFERENCES `routes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы transportroute.stoproutes: ~13 rows (приблизительно)
/*!40000 ALTER TABLE `stoproutes` DISABLE KEYS */;
INSERT INTO `stoproutes` (`Id`, `NameStop`, `Routeid`) VALUES
	(1, 'Широкая', 1),
	(2, 'Грузовое', 1),
	(5, 'Великан', 1),
	(6, 'Поликлиника', 1),
	(8, 'Рынок', 1),
	(9, 'ГДК', 1),
	(10, 'Старая площадь', 1),
	(11, 'Радуга', 1),
	(12, 'Стадион', 1),
	(13, 'Сервисный центр', 1),
	(14, 'Школа №6', 1),
	(15, 'Дом офицеров1', 1);
/*!40000 ALTER TABLE `stoproutes` ENABLE KEYS */;

-- Дамп структуры для таблица transportroute.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы transportroute.__efmigrationshistory: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20221119032221_data1', '6.0.10');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
