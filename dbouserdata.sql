/*
SQLyog Community v12.2.2 (64 bit)
MySQL - 5.7.12-log : Database - dbouserdata
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dbouserdata` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `dbouserdata`;

/*Table structure for table `accounts` */

DROP TABLE IF EXISTS `accounts`;

CREATE TABLE `accounts` (
  `AccountID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `UserName` varchar(16) NOT NULL,
  `UserPassword` varchar(32) NOT NULL,
  `IsBanned` tinyint(1) NOT NULL DEFAULT '0',
  `IsGM` tinyint(1) NOT NULL DEFAULT '0',
  `LastServerID` int(11) NOT NULL DEFAULT '255',
  `LastChannelID` int(11) NOT NULL DEFAULT '255',
  PRIMARY KEY (`AccountID`),
  UNIQUE KEY `AccountID_UNIQUE` (`AccountID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Table structure for table `characters` */

DROP TABLE IF EXISTS `characters`;

CREATE TABLE `characters` (
  `CharacterID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `AccountID` bigint(20) unsigned NOT NULL,
  `GuildID` bigint(20) unsigned NOT NULL,
  `Name` varchar(20) NOT NULL,
  `ServerID` int(10) unsigned NOT NULL,
  `RaceID` int(10) unsigned NOT NULL,
  `ClassID` int(10) unsigned NOT NULL,
  `GenderID` int(10) unsigned NOT NULL,
  `FaceID` int(10) unsigned NOT NULL,
  `HairID` int(10) unsigned NOT NULL,
  `HairColorID` int(10) unsigned NOT NULL,
  `SkinColorID` int(10) unsigned NOT NULL,
  `CurrentLevel` int(10) unsigned NOT NULL DEFAULT '1',
  `CurrentExp` bigint(20) unsigned NOT NULL DEFAULT '0',
  `MapInfoID` bigint(20) unsigned NOT NULL DEFAULT '4294967295',
  `WorldTableID` bigint(20) unsigned NOT NULL DEFAULT '1',
  `WorldID` bigint(20) unsigned NOT NULL DEFAULT '1',
  `BindType` int(10) unsigned NOT NULL DEFAULT '0',
  `BindWorldID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `BindObjectID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `Position_X` float(11,6) NOT NULL,
  `Position_Y` float(11,6) NOT NULL,
  `Position_Z` float(11,6) NOT NULL,
  `Direction_X` float(11,6) NOT NULL,
  `Direction_Y` float(11,6) NOT NULL,
  `Direction_Z` float(11,6) NOT NULL,
  `ZennyInventory` bigint(20) NOT NULL DEFAULT '0',
  `ZennyBank` bigint(20) NOT NULL DEFAULT '0',
  `Marking` int(10) unsigned NOT NULL DEFAULT '255',
  `IsAdult` tinyint(1) NOT NULL DEFAULT '0',
  `IsTutorialDone` tinyint(1) NOT NULL DEFAULT '0',
  `IsToRename` tinyint(1) NOT NULL DEFAULT '0',
  `IsToDelete` tinyint(1) NOT NULL DEFAULT '0',
  `IsToChangeClass` tinyint(1) NOT NULL DEFAULT '0',
  `IsGameMaster` tinyint(1) NOT NULL DEFAULT '0',
  `HintsDone` bigint(20) unsigned NOT NULL DEFAULT '0',
  `Reputation` bigint(20) unsigned NOT NULL DEFAULT '0',
  `Mudosa` bigint(20) unsigned NOT NULL DEFAULT '0',
  `SkillPoints` int(10) unsigned NOT NULL DEFAULT '0',
  `createdAt` timestamp NULL DEFAULT NULL,
  `deletionStartedAt` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`CharacterID`),
  UNIQUE KEY `CharacterID_UNIQUE` (`CharacterID`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Table structure for table `guilds` */

DROP TABLE IF EXISTS `guilds`;

CREATE TABLE `guilds` (
  `GuildID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(20) NOT NULL,
  PRIMARY KEY (`GuildID`),
  UNIQUE KEY `GuildID_UNIQUE` (`GuildID`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `inventories` */

DROP TABLE IF EXISTS `inventories`;

CREATE TABLE `inventories` (
  `InventoryID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `CharacterID` bigint(20) unsigned NOT NULL,
  `ItemSerial` bigint(20) unsigned NOT NULL,
  `ItemID` bigint(20) unsigned NOT NULL,
  `Place` int(10) unsigned NOT NULL,
  `Slot` int(10) unsigned NOT NULL,
  `StackCount` int(10) unsigned DEFAULT '1',
  `Rank` int(10) unsigned DEFAULT '0',
  `Grade` int(10) unsigned DEFAULT '0',
  `BattleAttribute` int(10) unsigned DEFAULT '0',
  `CurrentDurability` int(10) unsigned DEFAULT '100',
  `NeedToIdentify` tinyint(1) DEFAULT '0',
  `RestrictType` int(10) unsigned DEFAULT '0',
  `DurationType` int(10) unsigned DEFAULT '0',
  `Option1` bigint(20) unsigned DEFAULT '4294967295',
  `Option2` bigint(20) unsigned DEFAULT '4294967295',
  `MakerName` varchar(20) DEFAULT '',
  `UseStartTime` timestamp NULL DEFAULT NULL,
  `UseEndTime` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`InventoryID`),
  UNIQUE KEY `InventoryID_UNIQUE` (`InventoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/* Procedure structure for procedure `checkCharacterName` */

/*!50003 DROP PROCEDURE IF EXISTS  `checkCharacterName` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`dboserver`@`%` PROCEDURE `checkCharacterName`(IN inName VARCHAR(20))
BEGIN
	DECLARE isInUse BOOL;
	DECLARE ResultCount INT;
	
	SELECT COUNT(*) INTO ResultCount FROM `characters` WHERE `Name` = inName;
	
	IF (ResultCount > 0) THEN
		SET isInUse = true;
	ELSE
		SET isInUse = FALSE;
	END IF;
	
	SELECT isInUse;
END */$$
DELIMITER ;

/* Procedure structure for procedure `getAccountAuthResult` */

/*!50003 DROP PROCEDURE IF EXISTS  `getAccountAuthResult` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getAccountAuthResult`(IN inUser VARCHAR(17), IN inPwd VARCHAR(32))
BEGIN
	DECLARE dbUser VARCHAR(16);
	DECLARE dbPwd VARCHAR(32);
	DECLARE dbIsBanned BOOLEAN;
	DECLARE ResultCode INT;
	SET inPwd = MD5(inPwd);
    
    SELECT `UserName`, `UserPassword`, `IsBanned` INTO dbUser, dbPwd, dbIsBanned
    FROM `Accounts` WHERE `UserName` = inUser;
    
    IF inUser = dbUser THEN
		IF inPwd = dbPwd THEN
			IF dbIsBanned = FALSE THEN
				SET ResultCode = 100; # AUTH_SUCCESS
			ELSE
				SET ResultCode = 113; # AUTH_USER_BLOCK
			END IF;
		ELSE
			SET ResultCode = 107; # AUTH_WRONG_PASSWORD
		END IF;
	ELSE
		SET ResultCode = 108; # AUTH_USER_NOT_FOUND
	END IF;
    
    SELECT ResultCode;
END */$$
DELIMITER ;

/* Procedure structure for procedure `getAccountCharacters` */

/*!50003 DROP PROCEDURE IF EXISTS  `getAccountCharacters` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getAccountCharacters`(IN AccID BIGINT, IN SrvID INT)
BEGIN
	SELECT 
	`CharacterID`, 
	`Name`,  
	`RaceID`, 
	`ClassID`, 
	`GenderID`, 
	`FaceID`, 
	`HairID`, 
	`HairColorID`, 
	`SkinColorID`, 
	`CurrentLevel`,  
	`MapInfoID`, 
	`WorldTableID`, 
	`WorldID`,   
	`Position_X`, 
	`Position_Y`, 
	`Position_Z`, 
	`ZennyInventory`, 
	`ZennyBank`,
	`IsAdult`,
	`GuildID`,
	`IsTutorialDone`, 
	`IsToRename`,
	`Marking`	 
	FROM 
	`characters` 
	WHERE `AccountID` = AccID AND `ServerID` = SrvID
	ORDER BY `CharacterID` asc
	LIMIT 8;
    END */$$
DELIMITER ;

/* Procedure structure for procedure `getAccountID` */

/*!50003 DROP PROCEDURE IF EXISTS  `getAccountID` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getAccountID`(IN inUser VARCHAR(16))
BEGIN
	DECLARE dbAccountID INT;
    SET dbAccountID = 0;
	SELECT `AccountID` INTO dbAccountID FROM `Accounts` WHERE `UserName` = inUser;
	SELECT dbAccountID;
END */$$
DELIMITER ;

/* Procedure structure for procedure `getCharacterEquipment` */

/*!50003 DROP PROCEDURE IF EXISTS  `getCharacterEquipment` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getCharacterEquipment`(IN CharID BIGINT)
BEGIN
	SELECT `Slot`,`ItemID`,`Rank`,`Grade`,`BattleAttribute`
	FROM `inventories`
	WHERE `CharacterID` = CharID AND `Place` = 7
	ORDER BY `Slot` ASC;
END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
