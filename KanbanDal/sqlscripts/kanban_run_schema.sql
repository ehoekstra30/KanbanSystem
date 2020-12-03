-- kanbandb programmability
-- for creating the db schema for kanbandb

use master;
drop database if exists kanbandb;

create database kanbandb;

go

use kanbandb;


create table ConfigTable (
	SystemProperty varchar(81) primary key not null,
	SystemValue varchar(24) not null
);



create table Workstation (
	WorkstationId int primary key identity(1, 1) not null,
	ExperienceLevel int,
	IsCurrentlyWorking bit not null,
	HarnessAmount int,
	ReflectorAmount int,
	HousingAmount int,
	LensAmount int,
	BulbAmount int,
	BezelAmount int
);


create table TestTray (
	TestTrayId int primary key identity(1, 1) not null,
	IsCompleted bit not null,
	IsCurrentlyInUse bit not null
);


create table FogLamp (
	TestTrayId int foreign key references TestTray(TestTrayId) not null,
	FogLampId int not null,
	ExperienceLevelOfAssembler int not null,
	IsEffective bit,
	primary key (TestTrayId, FogLampId)
);


create table FogLampOrder (
	OrderId int primary key identity(1, 1) not null,
	AmountOrdered int not null,
	IsFulfilled bit not null
);



create table OrderLine (
	TestTrayId int foreign key references TestTray(TestTrayId) not null,
	FogLampId int not null,
	OrderId int foreign key references FogLampOrder(OrderId) not null
);



-- Next, populate tables based on ConfigTable attributes
-- This is only on first db start-up (all other alterations to ConfigTable must be made
-- through the Visual Studio ConfigTool)



-- STEP 1
-- Populate the ConfigTable with our defaults
-- These can be changed using the ConfigTool
insert into ConfigTable 
	(SystemProperty, SystemValue) 
values 
	('HarnessCapacity', '55'),
	('ReflectorCapacity', '35'),
	('HousingCapacity', '24'),
	('LensCapacity', '40'),
	('BulbCapacity', '60'),
	('BezelCapacity', '75'),

	('HarnessMinimum', '5'),
	('ReflectorMinimum', '5'),
	('HousingMinimum', '5'),
	('LensMinimum', '5'),
	('BulbMinimum', '5'),
	('BezelMinimum', '5'),
	
	('RunnerFrequency', '300'),
	('TickTime', '1'),
	('NumberOfAssemblyStations', '3'),

	('RookieDefectRate', '0.85'),
	('ExperiencedDefectRate', '0.5'),
	('SeniorDefectRate', '0.15'),
	
	('AssemblyBaseTime', '60'),
	('RookieAssemblyTimeVariance', '0.5'),
	('ExperiencedAssemblyTimeVariance', '0.1'),
	('SeniorAssemblyTimeVariance', '0.15'),

	('RookieAssemblySpeed', '60'),
	('ExperiencedAssemblySpeed', '60'),
	('SeniorAssemblySpeed', '60'),
	
	('TestTrayCapacity', '60');



-- STEP 2
-- Create/Alter procedure for inializing workstations according to ConfigTable


-- Get amount of workstations to initialize
declare @numWorkstations int;
select @numWorkstations=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='NumberOfAssemblyStations';

-- Get capacity of workstation bins for each part
declare @harnessBin int,
	@reflectorBin int,
	@housingBin int,
	@lensBin int,
	@bulbBin int,
	@bezelBin int;

-- HARNESS
select @harnessBin=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='HarnessCapacity';

-- REFLECTOR
select @reflectorBin=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='ReflectorCapacity';

-- HOUSING
select @housingBin=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='HousingCapacity';

-- LENS
select @lensBin=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='LensCapacity';

-- BULB
select @bulbBin=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='BulbCapacity';

-- BEZEL
select @bezelBin=CAST(SystemValue as int)
from ConfigTable
where SystemProperty='BezelCapacity';

-- Add each workstation to Workstation table
-- Each workstation starts with the max capacity of each bin, set above
declare @count int = 0;
while @count < @numWorkstations
begin
	insert into Workstation (
		IsCurrentlyWorking,
		HarnessAmount,
		ReflectorAmount,
		HousingAmount,
		LensAmount,
		BulbAmount,
		BezelAmount
	)
	values (
		0,
		@harnessBin,
		@reflectorBin,
		@housingBin,
		@lensBin,
		@bulbBin,
		@bezelBin
	);
	set @count = @count + 1;
end;


select * from Workstation;
select * from TestTray;
select * from FogLamp;