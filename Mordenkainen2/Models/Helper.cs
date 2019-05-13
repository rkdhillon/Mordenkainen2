using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Mordenkainen2.Models
{
    public class Helper
    {
        
        //first attempt at updating the CharacterSheetViewModel objects.s
        public static bool SaveSheetProperty(PropertyInfo prop, Context context)
        {
            var type = prop.PropertyType.Name;

            //using the switch statement for differnt table updates for different updates

            switch (type)
            {
                case "CharacterSheet":
                    //convert the type of item to Character sheet
                    var model = Convert.ChangeType(prop, typeof(CharacterSheet));
                    //Attach character sheet with a casted variable.
                    //Using attach instead of update because attach marks all properties to unmodified.
                    context.CharacterSheet.Attach((CharacterSheet)model);
                    //search through all the properties of the Model
                    foreach (var item in model.GetType().GetProperties())
                    {
                        if (item != null)
                        {
                            context.Entry(model).Property(item.PropertyType.Name).IsModified = true;
                        }
                    }
                    break;
                case "SavingThrows":
                    model = Convert.ChangeType(prop, typeof(SavingThrows));
                    context.SavingThrows.Attach((SavingThrows)model);
                    foreach (var item in model.GetType().GetProperties())
                    {
                        if(item != null)
                        {
                            context.Entry(model).Property(item.PropertyType.Name).IsModified = true;
                        }
                    }
                    
                    break;
                case "Skills":
                    model = Convert.ChangeType(prop, typeof(Skills));
                    context.Skills.Attach((Skills)model);
                    break;
                case "Money":
                    model = Convert.ChangeType(prop, typeof(Money));
                    context.Money.Attach((Money)model);
                    break;
                case "Proficiencies":
                    model = Convert.ChangeType(prop, typeof(Proficiencies));
                    context.Proficiencies.Attach((Proficiencies)model);
                    break;
                case "Appearance":
                    model = Convert.ChangeType(prop, typeof(Appearance));
                    context.Appearance.Attach((Appearance)model);
                    break;
                case "SpellBook":
                    model = Convert.ChangeType(prop, typeof(Spellbook));
                    context.Spellbook.Attach((Spellbook)model);
                    break;
                default:
                    context.Attach(prop);
                    break;
            }

            return true;
        }

        //this changes the property values of the appropriate model and changes the property to modified,
        //which means it will be updated on savechanges.
        public static void ModifySheetProperty(Type type, Context context, string[] names, 
            Object value, int? selectCharacterID)
        {
            //create a new instance of the type of object passed in.
            var model = Activator.CreateInstance(type);

            //change model object CharacterID property value to selectCharacterID value input. Originaly 
            //from sessions variable.
            model.GetType().GetProperty("CharacterID")
                .SetValue(model, Convert.ChangeType(selectCharacterID, model.GetType().GetProperty("CharacterID").PropertyType), null);

            //mark all properties of as unmodified, therefore won't be saved.
            context.Attach(model);
        
            // for each property in the type model object get the properties
            foreach (var item in model.GetType().GetProperties())
            {
                //if property name matches name ajax object
                if (item.PropertyType.Name == names[1])
                {
                    //set the value of the property in the model object with a value that has been changed
                    //to match the property type, so string for string... hopefully.
                    item.SetValue(model, Convert.ChangeType(value, item.PropertyType), null);
                }
                //mark property as modified.
                context.Entry(model).Property(names[1]).IsModified = true;
            }
        }
    }
}
