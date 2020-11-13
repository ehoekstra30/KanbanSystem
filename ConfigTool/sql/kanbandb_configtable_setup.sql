use kanbandb;

create table ConfigTable (
	SystemProperty varchar(81) primary key not null,
	SystemValue varchar(24) not null
);


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
	('RookieAssemblyTimeVariance', '+0.5'),
	('ExperiencedAssemblyTimeVariance', '+-0.1'),
	('SeniorAssemblyTimeVariance', '-0.15'),

	('RookieAssemblySpeed', '60'),
	('ExperiencedAssemblySpeed', '60'),
	('SeniorAssemblySpeed', '60'),
	
	('TestTrayCapacity', '60');