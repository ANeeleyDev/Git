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
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email_address varchar(50) NOT NULL,
	phone_number varchar(10) NOT NULL,
	user_street_address varchar(50) NOT NULL,
	user_city int NOT NULL,
	user_state int NOT NULL,
	user_zip int NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
	CONSTRAINT FK_user_city FOREIGN KEY (user_city) REFERENCES cities (city_id),
	CONSTRAINT FK_user_state FOREIGN KEY (user_state) REFERENCES states (state_id),
	CONSTRAINT FK_user_zip FOREIGN KEY (user_zip) REFERENCES zips (zip_id)
)

CREATE TABLE pets (
	pet_id int IDENTITY(1,1) NOT NULL,
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
	post_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	post_title varchar(50) NOT NULL,
	post_content varchar(200) NOT NULL
	CONSTRAINT PK_post PRIMARY KEY (post_id)
	CONSTRAINT FK_user_id FOREIGN KEY (user_id) REFERENCES users (user_id)
)

CREATE TABLE playdates (
	playdate_id int IDENTITY(1,1) NOT NULL,
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

GO