CREATE DATABASE Morden
Go
Use Morden
Go
CREATE Table UserInformation(
UserID int identity(1,1),
UserEmail varchar(30) not null,
UserPass varchar(20) not null,

CONSTRAINT PK_UserInformation PRIMARY KEY CLUSTERED(UserID)
)
Go
Create Table Class(
ClassID int identity(1,1),
ClassName varchar(20) not null,
ClassDesc varchar(2000) not null,
Proficiencies varchar(4000) not null,
Features varchar(4000) not null,

CONSTRAINT PK_Class PRIMARY KEY CLUSTERED(ClassID)
)
go
Create Table SubClass(
SubClassID int identity(1,1),
SubClassName varchar(20) not null,
SubClassDesc varchar(2000) not null,
SubProficiencies varchar(4000) not null,
SubFeatures varchar(4000) not null,

CONSTRAINT PK_SubClass PRIMARY KEY CLUSTERED(SubClassID)
)
go
Create Table Background(
BackgroundID int identity(1,1),
BackgroundName varchar(20) not null,
BackgroundDesc varchar(2000) not null,
SkillProf varchar(50) not null,
Languages varchar(20) not null,
Equipment varchar(500) not null,

CONSTRAINT PK_Background PRIMARY KEY CLUSTERED(BackgroundID)
)
go
Create Table Race(
RaceID int identity(1,1),
RaceName varchar(30) not null,
RaceDesc varchar(2000) not null,
Bonuses varchar(1000) not null,

CONSTRAINT PK_Race PRIMARY KEY CLUSTERED(RaceID)
)
go
Create Table Alignment(
AlignmentID int identity(1,1),
AlignmentType varchar(20) not null,
AlignmentDesc varchar(100) not null,

CONSTRAINT PK_Alignment PRIMARY KEY CLUSTERED(AlignmentID)
)
Go

Create Table SpellCompendium(
SpellID int identity(1,1),
SpellName varchar(40) not null,
SpellSchool varchar(20) not null,
SpellLevel int not null,
CastingTime varchar(20) not null,
SpellRange varchar(20) not null,
Components varchar(30) not null,
Duration varchar(10) not null,
SpellDesc varchar(1000) not null,
)
Go

Create Table CharacterSheet(
CharacterID int identity(1,1),
UserID int not null,
CharacterName varchar(50) not null,
Class varchar(30),
SubClass varchar(30),
CharLevel tinyint Default 1,
BackgroundID int
references Background(BackgroundID),
PlayerName varchar(50),
/*Thinking of using the race table for loading and initial picking, but saving selection as text so that the
user can edit their race as needed*/
Race varchar(30) Default 'human',
AlignmentID int
references Alignment(AlignmentID),
Experience int default 10,
Strength tinyint default 10,
Dexterity tinyint default 10,
Constitution tinyint default 10,
Intelligence tinyint default 10,
Wisdom tinyint default 10,
Charisma tinyint default 10,
Inspiration tinyint,
ProficiencyBonus tinyint,
PassiveWisdom tinyint,
ArmorClass tinyint default 10,
Initiative tinyint default 0,
Speed tinyint default 30,
HitPoints int default 6,
HitDie tinyint default 1,
HitDieType varchar(10),
DeathSaveSuccess tinyint default 0,
DeathSaveFailure tinyint default 0,
/*Was thinking of having a string that is broken up with a specific character to split on*/
Attacks varchar(max) default 'Attack 1: Rubber Shark nubs;',
/*Was thinking a list of numbers that correspond to spells */
Equipment varchar(max) default 'naked',
PersonalityTraits varchar(1000),
Ideals varchar(1000),
Bonds varchar(1000),
Flaws varchar(1000),
Features varchar(1000),
Traits varchar(1000),


CONSTRAINT PK_CharacterSheet PRIMARY KEY CLUSTERED(CharacterID),
CONSTRAINT FK_CharacterSheet FOREIGN KEY(UserID) REFERENCES UserInformation(UserID),
)
Go
Create Table SavingThrows(
SavingThrowsID int identity(1,1),
CharacterID int,
/*Save number*/
StrSave tinyint not null,
/*Save bool for if proficient*/
StrSaveProf bit not null,
DexSave tinyint not null,
DexSaveProf bit not null,
ConSave tinyint not null,
ConSaveProf bit not null,
IntSave tinyint not null,
IntSaveProf bit not null,
WisSave tinyint not null,
WisSaveProf bit not null,
ChaSave tinyint not null,
ChaSaveProf bit not null,

CONSTRAINT PR_SavingThrows PRIMARY KEY CLUSTERED(SavingThrowsID),
CONSTRAINT FK_CharInSaving FOREIGN KEY(CharacterID) REFERENCES CharacterSheet(CharacterID),
CONSTRAINT U_Saves UNIQUE (CharacterID)
)
Go
Create Table Skills(
SkillsID int identity(1,1),
CharacterID int,
Acrobatics tinyint Default 0,
AnimalHandling tinyint Default 0,
Arcana tinyint Default 0,
Athletics tinyint Default 0,
Deception tinyint Default 0,
History tinyint Default 0,
Insight tinyint Default 0,
Intimidation tinyint Default 0,
Investigation tinyint Default 0,
Medicine tinyint Default 0,
Nature tinyint Default 0,
Perception tinyint Default 0,
Performance tinyint Default 0,
Persuasion tinyint Default 0,
Religion tinyint Default 0,
Sleight tinyint Default 0,
Stealth tinyint Default 0,
Survival tinyint Default 0,

CONSTRAINT PK_Skills PRIMARY KEY CLUSTERED(SkillsID),
CONSTRAINT FK_CharInSKills FOREIGN KEY (CharacterID) REFERENCES CharacterSheet(CharacterID),
CONSTRAINT U_Skills UNIQUE(CharacterID)
)
Go
/*Not Used
Create Table Equipment(
EquipmentID int identity(1,1),
CharacterID int not null,
EquipmentLog 

CONSTRAINT PK_Equipment PRIMARY KEY CLUSTERED(EquipmentID),
CONSTRAINT FK_CharInEquip FOREIGN KEY (CharacterID) REFERENCES CharacterSheet(CharacterID),
)
*/
GO
/*Character specfic table of currency amounts*/
Create Table Money(
MoneyID int identity(1,1),
CharacterID int,
Copper int Default 0,
Silver int Default 0,
Electrum int Default 0,
Gold int Default 0,
Platinum int Default 0,

CONSTRAINT PK_Money PRIMARY KEY CLUSTERED(MoneyID),
CONSTRAINT FK_CharInMoney FOREIGN KEY (CharacterID) REFERENCES CharacterSheet(CharacterID),
CONSTRAINT U_Money UNIQUE(CharacterID)
)
Go

Create table Appearance(
AppearanceID int identity(1,1),
CharacterID int,
Age int not null,
Height varchar(10) not null,
CharWeight int,
Eyes varchar(110) not null,
Skin varchar(100) not null,
Hair varchar(100) not null,
General varchar(max) not null,

CONSTRAINT PK_Appearance PRIMARY KEY CLUSTERED(AppearanceID),
CONSTRAINT FK_CharInAppearance FOREIGN KEY (CharacterID) REFERENCES CharacterSheet(CharacterID),
CONSTRAINT U_Appearance UNIQUE(CharacterID)
)
Go
Create Table SpellBook(
SpellBookID int identity(1,1),
CharacterID int,
CastingClass varchar(10) not null,
CastingAbility int not null,
SaveDC int not null,
SpellAttackBonus int not null,
Cantrips varchar(150),
LevelOne varchar(150),
LevelTwo varchar(150),
LevelThree varchar(150),
LevelFour varchar(150),
LevelFive varchar(150),
LevelSix varchar(150),
LevelSeven varchar(150),
LevelEight varchar(150),
LevelNine varchar(150),


CONSTRAINT PK_SpellBook PRIMARY KEY CLUSTERED(SpellBookID),
CONSTRAINT FK_CharInSpellBook FOREIGN KEY (CharacterID) REFERENCES CharacterSheet(CharacterID),
CONSTRAINT U_SpellBook UNIQUE(CharacterID)
)
Go

Create Table Proficiencies(
ProfID int identity(1,1),
CharacterID int,
Armor varchar(1000),
Weapons varchar(1000),
Tools varchar (1000),


CONSTRAINT PK_Prof PRIMARY KEY CLUSTERED(ProfID),
CONSTRAINT FK_CharInProf FOREIGN KEY (CharacterID) REFERENCES CharacterSheet(CharacterID),
CONSTRAINT U_Prof UNIQUE(CharacterID)
)
Go



Insert into UserInformation
Values('User1@example.com','qwerty')
Insert into UserInformation
Values('User2@example.com','password')

Insert into Class
Values('Barbarian','A fierce warrior with a primitive background who can enter a battle rage',
' ', 'Armor: Weapons:Light armor, Medium armor, shields; 
Tools: Simple weapons, martial weapons; Saving Throws: Strength, Constitution;
 Skills: Choose two from Animal Handling, Athletics, Intimidation, Nature, Perception, and Survival' )

Insert into Race
Values('Gnome', 'An excitement and enthusiasm for live. Gnomes are small with a weight to match',
'Int + 2;Gnome Cunning;Darkvision: 60ft; Languages: Common, Gnomish;')

insert into Background
values('Acolyte', 'You have spent your life in the service of a god/gods and are the intermediary between the divine and mortal',
'Insight, Religion','Two of your choice','A holy symbol, a prayer book, 5 sticks of incense, vestments, common clothes, belt pouch, 15 gp')

insert into Alignment
Values('Chaotic Good','They follow their conscience and try to help out without regard to the rules.')

insert into CharacterSheet
Values(1,'Crush','Barbarian','None',1,1,'Bob','Gnome',1,2000,18,12,14,9,9,11,0,0,0,14,2,30,14,1,'D12',0,0,'Attack 1; Attack2;',
'Weapon1:1-hand axe;Armor:Leather;Boots:Leather;Backpack;','Personality Traits:','Ideals:;','Bonds:','Flaws:;','Features:;','Traits:;')

Insert into SavingThrows
Values(1,4,1,1,1,2,1,0,0,0,0,0,0)

Insert into Skills
Values(1,3,0,0,3,0,0,3,0,0,0,0,4,0,0,0,0,0,5)

Insert into Money
Values(1,0,0,0,10,0 )

Insert into Appearance
Values(1,20,'3.6',45,'Brown','Sun-kissed','Honey colored','Has many scars')

Insert into Proficiencies
Values(1,'Light Armor;','Axes;','None' )

/*Should I make Procedures for most frequently used queries?*/