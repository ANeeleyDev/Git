USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables

CREATE TABLE states (
	state_id int NOT NULL,
	state_name varchar(50) NOT NULL,
	state_abbreviation varchar(2) NOT NULL
	CONSTRAINT PK_state PRIMARY KEY (state_id)
)

CREATE TABLE cities (
	city_id int NOT NULL,
	city_name varchar(50) NOT NULL,
	state_id int NOT NULL
	CONSTRAINT PK_city PRIMARY KEY (city_id)
	CONSTRAINT FK_state FOREIGN KEY (state_id) REFERENCES states (state_id)
)

CREATE TABLE zips (
	zip_id int NOT NULL,
	zipcode int NOT NULL
	CONSTRAINT PK_zip PRIMARY KEY (zip_id)
)

CREATE TABLE users (
	user_id int IDENTITY(0,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE profiles (
	profile_id int IDENTITY(0,1) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email_address varchar(50) NOT NULL,
	phone_number varchar(10) NOT NULL,
	street_address varchar(50) NOT NULL,
	city int NOT NULL,
	state int NOT NULL,
	zip int NOT NULL,
	user_id int NOT NULL
	CONSTRAINT PK_profile PRIMARY KEY (profile_id)
	CONSTRAINT FK_profile_city FOREIGN KEY (city) REFERENCES cities (city_id),
	CONSTRAINT FK_profile_state FOREIGN KEY (state) REFERENCES states (state_id),
	CONSTRAINT FK_profile_zip FOREIGN KEY (zip) REFERENCES zips (zip_id),
	CONSTRAINT FK_profile_user_id FOREIGN KEY (user_id) REFERENCES users (user_id)
)

CREATE TABLE pets (
	pet_id int IDENTITY(0,1) NOT NULL,
	user_id int NOT NULL,
	pet_name varchar(50) NOT NULL,
	age int NOT NULL,
	breed varchar(50) NOT NULL,
	species varchar(50) NOT NULL,
	playful BIT NOT NULL,
	nervous BIT NOT NULL,
	confident BIT NOT NULL,
	shy BIT NOT NULL,
	mischievous BIT NOT NULL,
	independent BIT NOT NULL,
	other_comments varchar(200) NOT NULL
	CONSTRAINT PK_pet PRIMARY KEY (pet_id)
	CONSTRAINT FK_user FOREIGN KEY (user_id) REFERENCES users (user_id)
)

CREATE TABLE posts (
	post_id int IDENTITY(0,1) NOT NULL,
	user_id int NOT NULL,
	post_title varchar(50) NOT NULL,
	post_content varchar(200) NOT NULL
	CONSTRAINT PK_post PRIMARY KEY (post_id)
	CONSTRAINT FK_user_id FOREIGN KEY (user_id) REFERENCES users (user_id)
)

CREATE TABLE playdates (
	playdate_id int IDENTITY(0,1) NOT NULL,
	meetingTime DATETIME NOT NULL,
	playdate_address varchar(50) NOT NULL,
	playdate_city int NOT NULL,
	playdate_state int NOT NULL,
	playdate_zip int NOT NULL
	CONSTRAINT PK_playdate PRIMARY KEY (playdate_id)
	CONSTRAINT FK_playdate_city FOREIGN KEY (playdate_city) REFERENCES cities (city_id),
	CONSTRAINT FK_playdate_state FOREIGN KEY (playdate_state) REFERENCES states (state_id),
	CONSTRAINT FK_playdate_zip FOREIGN KEY (playdate_zip) REFERENCES zips (zip_id)
)


--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO states (state_id, state_name, state_abbreviation)
VALUES	(0, 'Alabama', 'AL'),
		(1, 'Alaska', 'AK'),
		(2, 'Arizona', 'AZ'),
		(3, 'Arkansas', 'AR'),
		(4, 'California', 'CA'),
		(5, 'Colorado', 'CO'),
		(6, 'Connecticut', 'CT'),
		(7, 'Delaware', 'DE'),
		(8, 'Florida', 'FL'),
		(9, 'Georgia', 'GA'),
		(10, 'Hawaii', 'HI'),
		(11, 'Idaho', 'ID'),
		(12, 'Illinois', 'IL'),
		(13, 'Indiana', 'IN'),
		(14, 'Iowa', 'IA'),
		(15, 'Kansas', 'KS'),
		(16, 'Kentucky', 'KY'),
		(17, 'Louisiana', 'LA'),
		(18, 'Maine', 'ME'),
		(19, 'Maryland', 'MD'),
		(20, 'Massachusetts', 'MA'),
		(21, 'Michigan', 'MI'),
		(22, 'Minnesota', 'MN'),
		(23, 'Mississippi', 'MS'),
		(24, 'Missouri', 'MO'),
		(25, 'Montana', 'MT'),
		(26, 'Nebraska', 'NE'),
		(27, 'Nevada', 'NV'),
		(28, 'New Hampshire', 'NH'),
		(29, 'New Jersey', 'NJ'),
		(30, 'New Mexico', 'NM'),
		(31, 'New York', 'NY'),
		(32, 'North Carolina', 'NC'),
		(33, 'North Dakota', 'ND'),
		(34, 'Ohio', 'OH'),
		(35, 'Oklahoma', 'OK'),
		(36, 'Oregon', 'OR'),
		(37, 'Pennsylvania', 'PA'),
		(38, 'Rhode Island', 'RI'),
		(39, 'South Carolina', 'SC'),
		(40, 'South Dakota', 'SD'),
		(41, 'Tennessee', 'TN'),
		(42, 'Texas', 'TX'),
		(43, 'Utah', 'UT'),
		(44, 'Vermont', 'VT'),
		(45, 'Virginia', 'VA'),
		(46, 'Washington', 'WA'),
		(47, 'West Virginia', 'WV'),
		(48, 'Wisconsin', 'WI'),
		(49, 'Wyoming', 'WY');

INSERT INTO cities (city_id, city_name, state_id)
VALUES	(0, 'Cincinnati', 34);

INSERT INTO zips (zip_id, zipcode)
VALUES	(0, 45249);

INSERT INTO profiles (first_name, last_name, email_address, phone_number, street_address, city, state, zip, user_id)
VALUES	('Han', 'Solo', 'hsolo@gmail.com', '7684729290', '111 Smugglers Way', 0, 34, 0, 1);

INSERT INTO pets (user_id, pet_name, age, breed, species, playful, nervous, confident, shy, mischievous, independent, other_comments)
VALUES (0, 'Jabba', 13, 'mutt', 'dog', 0, 1, 0, 1, 1, 0, 'loves to eat');

INSERT INTO posts (user_id, post_title, post_content)
VALUES	(0, 'Jabba needs a snack- I mean friend', 'Looking for a friend for my lovely little Jabba')

INSERT INTO playdates (meetingTime, playdate_address, playdate_city, playdate_state, playdate_zip)
VALUES ('2021-12-08 10:30:00', '888 Galaxy Drive', 0, 0, 0)

GO