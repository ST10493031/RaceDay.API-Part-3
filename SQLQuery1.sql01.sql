
Create Database RaceDayDB;
use RaceDayDB;

Create table Organizer(
OrganizerID int identity(1,1) primary key,
OrganizerName VARCHAR(150) NOT NULL,
ContactEmail VARCHAR(90) NOT NULL,
ContactPhone VARCHAR(10) NOT NULL
);

Create table Race(
RaceID int identity(1,1) primary key,
OrganizerID int,
RaceName VARCHAR(100) NOT NULL,
RaceDate DATE NOT NULL,
StartTime TIME NOT NULL,
Location VARCHAR(250) NOT NULL,
DistanceKm DECIMAL(5,2) NOT NULL,
FOREIGN KEY (OrganizerID) REFERENCES Organizer(OrganizerID)
);

Create table Category(
CategoryID int identity(1,1) primary key,
RaceID int,
CategoryName VARCHAR(100) NOT NULL,
MinAge INT NOT NULL,
MaxAge INT NOT NULL,
GenderRestriction VARCHAR(20) NOT NULL,
FOREIGN KEY(RaceID) REFERENCES Race(RaceID)
);

Create table Runner(
RunnerID int identity(1,1) primary key,
FirstName VARCHAR(70) NOT NULL,
LastName VARCHAR(70) NOT NULL,
Email VARCHAR(90) UNIQUE NOT NULL,
DateOfBirth DATE NOT NULL,
Gender VARCHAR(20) NOT NULL,
Phone VARCHAR(10) NOT NULL
);

Create table Registration(
RegistrationID int identity(1,1) primary key,
RunnerID int,
CategoryID int,
RegistrationDate DATE NOT NULL,
BibNumber VARCHAR(10) NOT NULL,
PaymentStatus VARCHAR(20) NOT NULL CHECK (PaymentStatus IN ('Paid', 'Pending', 'Refunded')),
FOREIGN KEY(RunnerID) REFERENCES Runner(RunnerID),
FOREIGN KEY(CategoryID) REFERENCES Category(CategoryID)
);

Create table Result(
ResultID int identity(1,1) primary key,
RegistrationID int UNIQUE,
FinishTime VARCHAR(10),
OverallRank INT,
CategoryRank INT,
Status VARCHAR(20) NOT NULL,
FOREIGN KEY(RegistrationID) REFERENCES Registration(RegistrationID)
);

-- ==========================================
-- POPULATE TABLES
-- ==========================================

-- Organizer
INSERT INTO Organizer (OrganizerName, ContactEmail, ContactPhone) VALUES
('Cape Town Athletics Club', 'info@ctac.co.za', '0211234567'),
('Johannesburg Runners Assoc', 'contact@jra.co.za', '0117654321'),
('Durban Coastal Runners', 'hello@dcr.co.za', '0319988776');

-- Race
INSERT INTO Race (OrganizerID, RaceName, RaceDate, StartTime, Location, DistanceKm) VALUES
(1, 'Cape Town Peace Run', '2026-10-10', '06:00:00', 'Green Point, Cape Town', 10.00),
(1, 'Table Mountain Trail Run', '2026-11-15', '05:30:00', 'Table Mountain, Cape Town', 21.10),
(2, 'Joburg City Marathon', '2026-09-20', '06:00:00', 'Sandton, Johannesburg', 42.20),
(3, 'Durban Beachfront Fun Run', '2026-10-05', '07:00:00', 'North Beach, Durban', 5.00);

-- Category
INSERT INTO Category (RaceID, CategoryName, MinAge, MaxAge, GenderRestriction) VALUES
(1, 'Open 10km', 18, 65, 'Mixed'),
(1, 'Junior 10km', 12, 17, 'Mixed'),
(2, 'Half Marathon Open', 18, 60, 'Mixed'),
(3, 'Full Marathon Open', 18, 60, 'Mixed'),
(3, 'Full Marathon Veteran', 45, 70, 'Mixed'),
(4, 'Fun Run Open', 10, 70, 'Mixed');

-- Runner
INSERT INTO Runner (FirstName, LastName, Email, DateOfBirth, Gender, Phone) VALUES
('Thabo', 'Mokoena', 'thabo.mokoena@email.com', '1995-03-14', 'Male', '0821234567'),
('Sarah', 'Naidoo', 'sarah.naidoo@email.com', '1998-07-22', 'Female', '0839876543'),
('Johan', 'van der Merwe', 'johan.vdm@email.com', '1988-11-02', 'Male', '0824567890'),
('Lerato', 'Dlamini', 'lerato.dlamini@email.com', '2001-05-19', 'Female', '0837654321'),
('Michael', 'Botha', 'michael.botha@email.com', '1975-01-30', 'Male', '0825551234'),
('Nomvula', 'Khumalo', 'nomvula.khumalo@email.com', '1993-09-08', 'Female', '0836667788');

-- Registration
INSERT INTO Registration (RunnerID, CategoryID, RegistrationDate, BibNumber, PaymentStatus) VALUES
(1, 1, '2026-09-01', '1001', 'Paid'),
(2, 1, '2026-09-02', '1002', 'Paid'),
(3, 3, '2026-08-20', '2001', 'Paid'),
(4, 4, '2026-08-25', '3001', 'Pending'),
(5, 5, '2026-08-26', '3002', 'Paid'),
(6, 6, '2026-09-03', '4001', 'Refunded');

-- Result
INSERT INTO Result (RegistrationID, FinishTime, OverallRank, CategoryRank, Status) VALUES
(1, '00:45:12', 3, 1, 'Finished'),
(2, '00:48:30', 7, 2, 'Finished'),
(3, '01:35:47', 1, 1, 'Finished'),
(4, '04:15:22', 15, 5, 'Finished'),
(5, '04:02:10', 10, 1, 'Finished'),
(6, NULL, NULL, NULL, 'DNS');