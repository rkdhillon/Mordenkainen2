using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mordenkainen2.Models
{
    public class Context : DbContext
    {
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<SubClass> Subclass { get; set; }
        public DbSet<Background> Background { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<Alignment> Alignment { get; set; }
        public DbSet<SpellCompendium> SpellCompendium { get; set; }
        public DbSet<CharacterSheet> CharacterSheet { get; set; }
        public DbSet<SavingThrows> SavingThrows { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<Proficiencies> Proficiencies { get; set; }
        public DbSet<Appearance> Appearance { get; set; }
        public DbSet<Spellbook> Spellbook { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //change example server location to actual db path.
            optionsBuilder.UseSqlServer(
                "Server=LAPTOP-A1RJ3U46\\SQLEXPRESS;Database=Morden;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

    public class LoginRegisterContext : DbContext
    {
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<UserInsert> UserInsert { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //change example server location to actual db path.
            optionsBuilder.UseSqlServer(
                "Server=LAPTOP-A1RJ3U46\\SQLEXPRESS;Database=Morden;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

    public class UserInformation
    {
        [Key]
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPass { get; set; }

        /*Get all related sheets*/
        public IEnumerable<UserInformation> Sheets { get; set; }
    }
    [Table("UserInformation")]
    public class UserInsert
    {
        public string UserEmail { get; set; }
        public string UserPass { get; set; }
    }

    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDesc { get; set; }
        public string Proficiencies { get; set; }
        public string Features { get; set; }
    }

    public class SubClass
    {
        [Key]
        public int SubClassID { get; set; }
        public string SubClassName { get; set; }
        public string SubClassDesc { get; set; }
        public string SubProficiencies { get; set; }
        public string SubFeatures { get; set; }
    }

    public class Background
    {
        [Key]
        public int BackgroundID { get; set; }
        public string BackgroundName { get; set; }
        public string BackgroundDesc { get; set; }
        public string SkillProf { get; set; }
        public string Languages { get; set; }
        public string Equipment { get; set; }

        ICollection<CharacterSheet> CharacterSheets { get; set; }
    }

    public class Race
    {
        [Key]
        public int RaceID { get; set; }
        public string RaceName { get; set; }
        public string RaceDesc { get; set; }
        public string Bonus { get; set; }
    }

    public class Alignment
    {
        [Key]
        public int AlignmentID { get; set; }
        public string AlignmentType { get; set; }

        ICollection<CharacterSheet> CharacterSheets { get; set; }
    }

    public class SpellCompendium
    {
        [Key]
        public int SpellID { get; set; }
        public string SpellName { get; set; }
        public string SpellSchool { get; set; }
        public int SpellLevel { get; set; }
        public string CastingTime { get; set; }
        public string SpellRange { get; set; }
        public string Components { get; set; }
        public string Duration { get; set; }
        public string SpellDesc { get; set; }

    }

    public class CharacterSheet
    {
        [Key]
        public int CharacterID { get; set; }
        public int UserID { get; set; }
        public string CharacterName { get; set; }
        public Class Class { get; set; }
        public SubClass Subclass { get; set; }
        public byte CharLevel { get; set; }
        /*For referenced tables*/
        [Column("Background")]
        public int BackgoundID { get; set; }
        public Background Background { get; set; }
        public string PlayerName { get; set; }
        public string Race { get; set; }
        [Column("Alignment")]
        public int AlignmentID { get; set; }
        public Alignment Alignment { get; set; }
        public int Experience { get; set; }
        public byte Strength { get; set; }
        public byte Dexterity { get; set; }
        public byte Constitution { get; set; }
        public byte Intelligence { get; set; }
        public byte Wisdom { get; set; }
        public byte Charisma { get; set; }
        /*This is how to get related tables*/
        public Skills Skills { get; set; }
        public SavingThrows SavingThrows { get; set; }
        public byte Inspiration { get; set; }
        public byte ProficiencyBonus { get; set; }
        public byte PassiveWisdom { get; set; }
        public byte ArmorClass { get; set; }
        public byte Initiative { get; set; }
        public byte Speed { get; set; }
        public int HitPoints { get; set; }
        public byte HitDie { get; set; }
        public string HitDieType { get; set; }
        public byte DeathSaveSuccess { get; set; }
        public byte DeathSaveFailure { get; set; }
        public string Attacks { get; set; }
        public string Equipment { get; set; }
        public Money Money { get; set; }
        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }
        public string Features { get; set; }
        public string Traits { get; set; }
        public Appearance Appearance { get; set; }
        public Spellbook Spellbook { get; set; }
        public Proficiencies Proficiencies { get; set; }





        //still don't quite understand why this is needed.
        //Indicates where Foreign Key is from I think
        public UserInformation UserInformation { get; set; }
    }
    public class Skills
    {
        [Key]
        public int SkillsID { get; set; }
        [ForeignKey("CharacterSheet")]
        public int CharacterID { get; set; }
        public int Acrobatics { get; set; }
        public int AnimalHandling { get; set; }
        public int Arcana { get; set; }
        public int Athletics { get; set; }
        public int Deception { get; set; }
        public int History { get; set; }
        public int Insight { get; set; }
        public int Intimidation { get; set; }
        public int Investigation { get; set; }
        public int Medicine { get; set; }
        public int Nature { get; set; }
        public int Perception { get; set; }
        public int Performance { get; set; }
        public int Persuasion { get; set; }
        public int Religion { get; set; }
        public int Sleight { get; set; }
        public int Stealth { get; set; }
        public int Survival { get; set; }

        public CharacterSheet CharacterSheet { get; set; }
    }

    public class SavingThrows
    {
        [Key]
        public int SavingThrowsID { get; set; }
        [ForeignKey("CharacterSheet")]
        public int CharacterID { get; set; }
        public byte StrSave { get; set; }
        public bool StrSaveProf { get; set; }
        public byte DexSave { get; set; }
        public bool DexSaveProf { get; set; }
        public byte ConSave { get; set; }
        public bool ConSaveProf { get; set; }
        public byte IntSave { get; set; }
        public bool IntSaveProf { get; set; }
        public byte WisSave { get; set; }
        public bool WisSaveProf { get; set; }
        public byte ChaSave { get; set; }
        public bool ChaSaveProf { get; set; }

        public CharacterSheet CharacterSheet { get; set; }
    }

    public class Money
    {
        [Key]
        public int MoneyID { get; set; }
        [ForeignKey("CharacterSheet")]
        public int CharacterID { get; set; }
        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Electum { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }

        public CharacterSheet CharacterSheet { get; set; }
    }

    public class Appearance
    {
        [Key]
        public int AppearanceID { get; set; }
        [ForeignKey("CharacterSheet")]
        public int CharacterID { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public int CharWeight { get; set; }
        public string Eyes { get; set; }
        public string Skin { get; set; }
        public string Hair { get; set; }
        public string General { get; set; }

        public CharacterSheet CharacterSheet { get; set; }

    }

    public class Spellbook
    {
        [Key]
        public int SpellBookID { get; set; }
        [ForeignKey("CharacterSheet")]
        public int CharacterID { get; set; }
        public string CastingClass { get; set; }
        public int CastingAbility { get; set; }
        public int SaveDC { get; set; }
        public int SpellAttackBonus { get; set; }
        public string Cantrips { get; set; }
        public string LevelOne { get; set; }
        public string LevelTwo { get; set; }
        public string LevelThree { get; set; }
        public string LevelFour { get; set; }
        public string LevelFive { get; set; }
        public string LevelSix { get; set; }
        public string LevelSeven { get; set; }
        public string LevelEight { get; set; }
        public string LevelNine { get; set; }

        public CharacterSheet CharacterSheet { get; set; }
    }


    public class Proficiencies
    {
        [Key]
        public int ProfID { get; set; }
        [ForeignKey("CharacterSheet")]
        public int CharacterID { get; set; }
        public string Armor { get; set; }
        public string Weapons { get; set; }
        public string Tools { get; set; }

        public CharacterSheet CharacterSheet { get; set; }
    }

}



