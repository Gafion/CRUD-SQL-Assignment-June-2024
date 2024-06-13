DROP DATABASE IF EXISTS `repo`;
CREATE DATABASE `repo`;
USE `repo`;

CREATE TABLE `City` (
    `PostID` int PRIMARY KEY,
    `City` varchar(255) NOT NULL
);

CREATE TABLE `Company` (
    `CompanyID` int PRIMARY KEY AUTO_INCREMENT,
    `CompanyName` varchar(255) NOT NULL
);

CREATE TABLE `Course` (
    `CourseID` int PRIMARY KEY AUTO_INCREMENT,
    `CourseName` varchar(255) NOT NULL
);

CREATE TABLE `Education` (
    `EducationID` int,
    `CourseID` int NOT NULL,
    `EducationEndDate` Date NOT NULL,
    FOREIGN KEY (CourseID) REFERENCES course(CourseID)
);

CREATE TABLE `Employment` (
    `EmploymentID` int,
    `CompanyID` int NOT NULL,
    `Employed` Date NOT NULL,
    `EmployEnd` Date NOT NULL,
    FOREIGN KEY (CompanyID) REFERENCES company(CompanyID)
);

CREATE TABLE `Person` (
    `PersonId` int PRIMARY KEY AUTO_INCREMENT,
    `FirstName` varchar(255) NOT NULL,
    `LastName` varchar(255) NOT NULL,
    `PostID` int NOT NULL,
    `Address` varchar(255) NOT NULL,
    FOREIGN KEY (PostID) REFERENCES city(PostID)
);

INSERT INTO `city` (`PostID`, `City`) VALUES
(9000, 'Aalborg'),
(8000, 'Aarhus C'),
(7000, 'Aarhus'),
(6000, 'Esbjerg'),
(5000, 'Odense C'),
(4420, 'Regstrup'),
(4000, 'Roskilde'),
(3000, 'Helsingør'),
(2000, 'Frederiksberg'),
(1000, 'Copenhagen');

INSERT INTO `company` (`CompanyID`, `CompanyName`) VALUES
(1, 'Google'),
(2, 'Facebook'),
(3, 'Microsoft'),
(4, 'Apple'),
(5, 'Amazon'),
(6, 'Netflix'),
(7, 'Tesla'),
(8, 'IBM'),
(9, 'Intel'),
(10, 'Oracle');

INSERT INTO `course` (`CourseID`, `CourseName`) VALUES
(1, 'Computer Science'),
(2, 'Mathematics'),
(3, 'Physics'),
(4, 'Chemistry'),
(5, 'Biology'),
(6, 'English Literature'),
(7, 'History'),
(8, 'Psychology'),
(9, 'Business Administration'),
(10, 'Economics');

INSERT INTO `education` (`EducationID`, `CourseID`, `EducationEndDate`) VALUES
(1, 1, '2018-04-21'),
(1, 2, '2019-08-03');

INSERT INTO `employment` (`EmploymentID`, `CompanyID`, `Employed`, `EmployEnd`) VALUES
(1, 1, '2018-02-31', '2019-08-04'),
(1, 2, '2019-01-01', '2020-03-21');

INSERT INTO `person` (`PersonId`, `FirstName`, `LastName`, `PostID`, `Address`) VALUES
(1, 'John', 'Doe', 4000, 'Gadenavn 1'),
(2, 'Jane', 'Doe', 4420, 'Gadenavn 2');

