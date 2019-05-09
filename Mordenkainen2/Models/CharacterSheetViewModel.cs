using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mordenkainen2.Models
{
    public class CharacterSheetViewModel
    {
        public CharacterSheet CharacterSheet { get; set; }
        public SavingThrows SavingThrows { get; set; }
        public Skills Skills { get; set; }
        public Money Money { get; set; }
        public Proficiencies Proficiencies { get; set; }
        public Appearance Appearance { get; set; }
        public Spellbook Spellbook { get; set; }
    }

    public class CharacterSelectViewModel
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
    }
}
