-- Created by Vertabelo (http://vertabelo.com)

-- tables
-- Table: Doctor
CREATE TABLE Doctor (
    Id int  NOT NULL IDENTITY,
    Name varchar(255)  NOT NULL,
    LastName varchar(255)  NOT NULL,
    Specialization_Id int  NOT NULL,
    CONSTRAINT Doctor_pk PRIMARY KEY  (Id)
);

-- Table: Medicine
CREATE TABLE Medicine (
    Id int  NOT NULL IDENTITY,
    Name varchar(255)  NOT NULL,
    CONSTRAINT Medicine_pk PRIMARY KEY  (Id)
);

-- Table: Patient
CREATE TABLE Patient (
    Id int  NOT NULL IDENTITY,
    Name varchar(255)  NOT NULL,
    LastName varchar(255)  NOT NULL,
    DateOfBirth datetime  NOT NULL,
    CONSTRAINT Patient_pk PRIMARY KEY  (Id)
);

-- Table: Prescription
CREATE TABLE Prescription (
    Id int  NOT NULL IDENTITY,
    Doctor_Id int  NOT NULL,
    Patient_Id int  NOT NULL,
    Medicine_Id int  NOT NULL,
    Amount int  NOT NULL,
    CreatedAt datetime  NOT NULL,
    CONSTRAINT Prescription_pk PRIMARY KEY  (Id)
);

-- Table: Specialization
CREATE TABLE Specialization (
    Id int  NOT NULL IDENTITY,
    Name varchar(255)  NOT NULL,
    CONSTRAINT Specialization_pk PRIMARY KEY  (Id)
);

-- foreign keys
-- Reference: Doctor_Specialization (table: Doctor)
ALTER TABLE Doctor ADD CONSTRAINT Doctor_Specialization
    FOREIGN KEY (Specialization_Id)
    REFERENCES Specialization (Id);

-- Reference: Prescription_Doctor (table: Prescription)
ALTER TABLE Prescription ADD CONSTRAINT Prescription_Doctor
    FOREIGN KEY (Doctor_Id)
    REFERENCES Doctor (Id);

-- Reference: Prescription_Medicine (table: Prescription)
ALTER TABLE Prescription ADD CONSTRAINT Prescription_Medicine
    FOREIGN KEY (Medicine_Id)
    REFERENCES Medicine (Id);

-- Reference: Prescription_Patient (table: Prescription)
ALTER TABLE Prescription ADD CONSTRAINT Prescription_Patient
    FOREIGN KEY (Patient_Id)
    REFERENCES Patient (Id);

-- End of file.
GO
-- Specialization table data
INSERT INTO Specialization (Name)
VALUES ('Cardiology'), ('Dermatology'), ('Endocrinology'), ('Gastroenterology');
GO
-- Doctor table data
INSERT INTO Doctor (Name, LastName, Specialization_Id)
VALUES ('John', 'Doe', 1), ('Jane', 'Smith', 2), ('Bob', 'Johnson', 3), ('Sara', 'Lee', 4);
GO
-- Medicine table data
INSERT INTO Medicine (Name)
VALUES ('Lipitor'), ('Zoloft'), ('Metformin'), ('Prilosec');
GO
-- Patient table data
INSERT INTO Patient (Name, LastName, DateOfBirth)
VALUES ('Michael', 'Smith', '1980-01-01'), ('Emily', 'Smith', '1975-05-05'), ('David', 'Lee', '1990-10-10'), ('Julia', 'Davis', '1965-03-15');
GO
-- Prescription table data
INSERT INTO Prescription (Doctor_Id, Patient_Id, Medicine_Id, Amount, CreatedAt)
VALUES (1, 2, 3, 30, '2022-01-01'), (2, 3, 1, 90, '2022-02-01'), (3, 1, 2, 60, '2022-03-01'), (4, 4, 4, 120, '2022-04-01');