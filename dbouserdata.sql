-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema dbouserdata
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema dbouserdata
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `dbouserdata` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `dbouserdata` ;

-- -----------------------------------------------------
-- Table `dbouserdata`.`Accounts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbouserdata`.`Accounts` ;

CREATE TABLE IF NOT EXISTS `dbouserdata`.`Accounts` (
  `AccountID` INT UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '',
  `UserName` VARCHAR(16) NOT NULL COMMENT '',
  `UserPassword` VARCHAR(32) NULL COMMENT '',
  `IsBanned` TINYINT(1) NULL DEFAULT 0 COMMENT '',
  `IsGM` TINYINT(1) NULL DEFAULT 0 COMMENT '',
  `LastServerID` INT NULL DEFAULT 255 COMMENT '',
  `LastChannelID` INT NULL DEFAULT 255 COMMENT '',
  PRIMARY KEY (`AccountID`, `UserName`)  COMMENT '',
  UNIQUE INDEX `AccountID_UNIQUE` (`AccountID` ASC)  COMMENT '')
ENGINE = InnoDB;

USE `dbouserdata` ;

-- -----------------------------------------------------
-- procedure getAccountAuthResult
-- -----------------------------------------------------

USE `dbouserdata`;
DROP procedure IF EXISTS `dbouserdata`.`getAccountAuthResult`;

DELIMITER $$
USE `dbouserdata`$$
CREATE PROCEDURE `getAccountAuthResult` (IN inUser VARCHAR(17), IN inPwd VARCHAR(32))
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
END
$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAccountID
-- -----------------------------------------------------

USE `dbouserdata`;
DROP procedure IF EXISTS `dbouserdata`.`getAccountID`;

DELIMITER $$
USE `dbouserdata`$$
CREATE PROCEDURE `getAccountID` (IN inUser VARCHAR(16))
BEGIN
	DECLARE dbAccountID INT;
    SET dbAccountID = 0;
	SELECT `AccountID` INTO dbAccountID FROM `Accounts` WHERE `UserName` = inUser;
	SELECT dbAccountID;
END
$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
