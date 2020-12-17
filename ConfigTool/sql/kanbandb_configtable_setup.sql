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

	go

create procedure sp_UpdateConfigTable 
	@HarnessCap as varchar(24), @ReflectorCap as varchar(24), @HousingCap as varchar(24),
	@LensCap as varchar(24), @BulbCap as varchar(24), @BezelCap as varchar(24),
	@HarnessMin as varchar(24), @ReflectorMin as varchar(24), @HousingMin as varchar(24),
	@LensMin as varchar(24), @BulbMin as varchar(24), @BezelMin as varchar(24),
	@RunnerFreq as varchar(24), @Tick as varchar(24), @NumStations as varchar(24),
	@RookieDefect as varchar(24), @ExperiencedDefect as varchar(24), @SeniorDefect as varchar(24),
	@AssemblyTime as varchar(24),
	@RookieVariance as varchar(24), @ExperiencedVariance as varchar(24), @SeniorVariance as varchar(24),
	@RookieSpeed as varchar(24), @ExperiencedSpeed as varchar(24), @SeniorSpeed as varchar(24),
	@TrayCap as varchar(24)
as
begin
	truncate table ConfigTable;

	insert into ConfigTable 
	(SystemProperty, SystemValue) 
values 
	('HarnessCapacity', @HarnessCap),
	('ReflectorCapacity', @ReflectorCap),
	('HousingCapacity', @HousingCap),
	('LensCapacity', @LensCap),
	('BulbCapacity', @BulbCap),
	('BezelCapacity', @BezelCap),

	('HarnessMinimum', @HarnessMin),
	('ReflectorMinimum', @ReflectorMin),
	('HousingMinimum', @HousingMin),
	('LensMinimum', @LensMin),
	('BulbMinimum', @BulbMin),
	('BezelMinimum', @BezelMin),
	
	('RunnerFrequency', @RunnerFreq),
	('TickTime', @Tick),
	('NumberOfAssemblyStations', @NumStations),

	('RookieDefectRate', @RookieDefect),
	('ExperiencedDefectRate', @ExperiencedDefect),
	('SeniorDefectRate', @SeniorDefect),
	
	('AssemblyBaseTime', @AssemblyTime),
	('RookieAssemblyTimeVariance', @RookieVariance),
	('ExperiencedAssemblyTimeVariance', @ExperiencedVariance),
	('SeniorAssemblyTimeVariance', @SeniorVariance),

	('RookieAssemblySpeed', @RookieSpeed),
	('ExperiencedAssemblySpeed', @ExperiencedSpeed),
	('SeniorAssemblySpeed', @SeniorSpeed),
	
	('TestTrayCapacity', @TrayCap);
end