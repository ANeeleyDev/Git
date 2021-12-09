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
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email_address varchar(50) NOT NULL,
	phone_number varchar(10) NOT NULL,
	street_address varchar(50) NOT NULL,
	city int NOT NULL,
	state int NOT NULL,
	zip int NOT NULL,
	CONSTRAINT FK_user_city FOREIGN KEY (city) REFERENCES cities (city_id),
	CONSTRAINT FK_user_state FOREIGN KEY (state) REFERENCES states (state_id),
	CONSTRAINT FK_user_zip FOREIGN KEY (zip) REFERENCES zips (zip_id),
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE species (
	species_id int IDENTITY(0,1) NOT NULL,
	species varchar(50) NOT NULL
	CONSTRAINT PK_species PRIMARY KEY (species_id)
)

CREATE TABLE breeds (
	breed_id int IDENTITY(0,1) NOT NULL,
	breed varchar(50) NOT NULL,
	species int NOT NULL
	CONSTRAINT PK_breed PRIMARY KEY (breed_id),
	CONSTRAINT FK_species FOREIGN KEY (species) REFERENCES species (species_id)
)

CREATE TABLE pets (
	pet_id int IDENTITY(0,1) NOT NULL,
	user_id int NOT NULL,
	pet_name varchar(50) NOT NULL,
	age int NOT NULL,
	breed int NOT NULL,
	species int NOT NULL,
	playful BIT NOT NULL,
	nervous BIT NOT NULL,
	confident BIT NOT NULL,
	shy BIT NOT NULL,
	mischievous BIT NOT NULL,
	independent BIT NOT NULL,
	other_comments varchar(200) NOT NULL
	CONSTRAINT PK_pet PRIMARY KEY (pet_id)
	CONSTRAINT FK_user FOREIGN KEY (user_id) REFERENCES users (user_id),
	CONSTRAINT FK_pet_breed FOREIGN KEY (breed) REFERENCES breeds (breed_id),
	CONSTRAINT FK_pet_species FOREIGN KEY (species) REFERENCES species (species_id)
)

CREATE TABLE posts (
	post_id int IDENTITY(0,1) NOT NULL,
	user_id int NOT NULL,
	post_title varchar(50) NOT NULL,
	post_content varchar(200) NOT NULL
	CONSTRAINT PK_post PRIMARY KEY (post_id)
	CONSTRAINT FK_user_id FOREIGN KEY (user_id) REFERENCES users (user_id)
)

CREATE TABLE playdate_statuses (
	playdate_status_id int NOT NULL,
	playdate_posted_user int NOT NULL,
	playdate_status varchar(50) NOT NULL
	CONSTRAINT PK_playdate_status PRIMARY KEY (playdate_status_id),
	CONSTRAINT FK_playdate_posted_user FOREIGN KEY (playdate_posted_user) REFERENCES users (user_id)
)

CREATE TABLE playdates (
	playdate_id int IDENTITY(0,1) NOT NULL,
	meetingTime DATETIME NOT NULL,
	playdate_address varchar(50) NOT NULL,
	playdate_city int NOT NULL,
	playdate_state int NOT NULL,
	playdate_zip int NOT NULL,
	playdate_status_id int NOT NULL
	CONSTRAINT PK_playdate PRIMARY KEY (playdate_id)
	CONSTRAINT FK_playdate_city FOREIGN KEY (playdate_city) REFERENCES cities (city_id),
	CONSTRAINT FK_playdate_state FOREIGN KEY (playdate_state) REFERENCES states (state_id),
	CONSTRAINT FK_playdate_zip FOREIGN KEY (playdate_zip) REFERENCES zips (zip_id),
	CONSTRAINT FK_playdate_status FOREIGN KEY (playdate_status_id) REFERENCES playdate_statuses (playdate_status_id)
)


--populate default data

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
VALUES	(0, 'Cincinnati', 34),
		(1, 'Columbus', 34),
		(2, 'Toledo', 34),
		(3, 'Cleveland', 34),
		(4, 'Dayton', 34);

INSERT INTO zips (zip_id, zipcode)
VALUES	(0, 45249),
		(1, 41073),
		(2, 45201),
		(3, 45222),
		(4, 45327);

INSERT INTO species (species)
VALUES	('Dog');

INSERT INTO breeds (breed, species)
VALUES	('Affenpinscher', 0),
		('Afghan Hound', 0),
		('Airedale Terrier', 0),
		('Akita', 0),
		('Alaskan Klee Kai', 0),
		('Alaskan Malamute', 0),
		('American Bulldog', 0),
		('American English Coonhound', 0),
		('American Eskimo Dog', 0),
		('American Foxhound', 0),
		('American Hairless Terrier', 0),
		('American Leopard Hound', 0),
		('American Staffordshire Terrier', 0),
		('American Water Spaniel', 0),
		('Anatolian Shepherd Dog', 0),
		('Appenzeller Sennenhund', 0),
		('Australian Cattle Dog', 0),
		('Australian Kelpie', 0),
		('Australian Shepherd', 0),
		('Australian Stumpy Tail Cattle Dog', 0),
		('Australian Terrier', 0),
		('Azawakh', 0),
		('Barbado da Terceira', 0),
		('Barbet', 0),
		('Basenji', 0),
		('Basset Fauve de Bretagne', 0),
		('Basset Hound', 0),
		('Bavarian Mountain Scent Hound', 0),
		('Beagle', 0),
		('Bearded Collie', 0),
		('Beauceron', 0),
		('Bedlington Terrier', 0),
		('Belgian Laekenois', 0),
		('Belgian Malinois', 0),
		('Belgian Sheepdog', 0),
		('Belgian Tervuren', 0),
		('Bergamasco Sheepdog', 0),
		('Berger Picard', 0),
		('Bernese Mountain Dog', 0),
		('Bichon Frise', 0),
		('Biewer Terrier', 0);

INSERT INTO users (username, password_hash, salt, user_role, first_name, last_name,	email_address, phone_number, street_address, city, state, zip)
VALUES	('hsolo','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 'Han', 'Solo', 'hsolo@gmail.com', '7684729290', '111 Smugglers Way', 0, 34, 0),
		('user1','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 'Tyson', 'Thorp', 'faketyson@gmail.com', '7684729291', '112 News Crew Drive', 0, 34, 0),
		('user2','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 'Annie', 'Cochran', 'fakeannie@gmail.com', '7684729292', '777 Consultant Street', 0, 34, 0),
		('user3','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 'Tiffany', 'McGillvary', 'faketiff@gmail.com', '7684729293', '8909 Stalk Market Road', 0, 34, 0),
		('admin1','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 'Kevin', 'O''Leary', 'fakekevin@gmail.com', '7684729294', '9890 Columbus Way', 0, 34, 0),
		('admin2','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 'Robert', 'Kaiser', 'fakerob@gmail.com', '7684729295', '7564 Oscar Wilde Boulevard', 0, 34, 0),
		('admin3','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 'Amanda', 'Neeley', 'fakeamanda@gmail.com', '7684729296', '5555 Boss Lady Road', 0, 34, 0),
		('admin4','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 'Ashley', 'Vorpe', 'fakeashley@gmail.com', '7684729297', '6754 Nerd Avenue', 0, 34, 0),
		('mod1', 'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=', 'mod', 'Joe', 'Riggs', 'fakejoe@gmail.com', '7684729298', '9483 Bobblehead Drive', 0, 34, 0),
		('mod2', 'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=', 'mod', 'Monika', 'Royal-Fischer', 'fakemonika@gmail.com', '7684729299', '7493 Leadership Court', 0, 34, 0);

INSERT INTO pets (user_id, pet_name, age, breed, species, playful, nervous, confident, shy, mischievous, independent, other_comments)
VALUES	(0, 'Jabba', 13, 0, 0, 0, 1, 0, 1, 1, 0, 'loves to eat'),
		(1, 'Loose Seal AKA Bby Ppy Destructo', 3, 0, 0, 0, 0, 0, 0, 0, 0, 'destroys everything');

INSERT INTO posts (user_id, post_title, post_content)
VALUES	(0, 'Jabba needs a snack- I mean friend', 'Looking for a friend for my lovely little Jabba');

INSERT INTO playdate_statuses (playdate_status_id, playdate_status)
VALUES	(0, 'Pending'),
		(1, 'Approved'),
		(2, 'Denied'),
		(3, 'Canceled'),
		(4, 'Finished');


INSERT INTO playdates (meetingTime, playdate_address, playdate_city, playdate_state, playdate_zip, playdate_status_id)
VALUES ('2021-12-08 10:30:00', '888 Galaxy Drive', 0, 0, 0, 4);

GO