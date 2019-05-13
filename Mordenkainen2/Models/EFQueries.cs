using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mordenkainen2.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace Mordenkainen2.Models
{
    public class EFQueries
    {
        public static bool RegisterUser(RegisterModel user)
        {
            //check to see if passwords match
            if (Equals(user.Password, user.Vpassword))
            { 
                try
                {
                    //attempt to insert(add) a new user to the database
                    using (var context = new LoginRegisterContext())
                    {
                        context.Add<UserInsert>(new UserInsert
                            { UserEmail = user.Email, UserPass = user.Password });
                        context.SaveChanges();
                        return true;
                    }
                }
                //if insert fails return false
                catch
                {
                    return false;
                }
            }
            return false;
        }

        //attempst to verify if a user exists in the database and return a nullable integer
        public static int? LoginUser(LoginModel login)
        {
            using (var context = new LoginRegisterContext())
            {
                //queries the database to see if email and password match the LoginModel
                int? verify = context.UserInformation
                    .Where(d => d.UserEmail == login.Email && d.UserPass == login.Password)
                    .Select(d => d.UserID)
                    .First();
                //return int or null
                return verify;
            }
        }

        //gets the selected character and hopefully all related tables. might need to use include
        public CharacterSheet GetCharacter(int userID, int charID)
        {
            using (var context = new Context())
            {
                var sheet = context.CharacterSheet
                    .Where(d => d.UserID == userID && d.CharacterID == charID)
                    .First();
                return sheet;
            }
        }

        //reteives list of users characters
        public List<CharacterSelectViewModel> GetSelection(int userID)
        {
            using (var context = new Context())
            {
                var selection = context.CharacterSheet
                    .Where(d => d.UserID == userID)
                    .Select(c => new CharacterSelectViewModel
                    {
                        CharacterID = c.CharacterID,
                        CharacterName = c.CharacterName
                    }).ToList();
                return selection;
            }
        }

        //insert new character
        public bool CreateCharacter(CharacterSheetViewModel sheet)
        {
            try
            {
                using (var context = new Context())
                {
                    //need an add for each table?
                    context.Add(sheet.CharacterSheet);
                    context.Add(sheet.SavingThrows);
                    context.Add(sheet.Skills);
                    context.Add(sheet.Money);
                    context.Add(sheet.Proficiencies);
                    context.Add(sheet.Appearance);
                    context.Add(sheet.Spellbook);
                    context.SaveChanges();
                    return true;
                }
                
            }
            catch
            {
                return false;
            }
        }

        //updates and saves a single property for a single model.
        public static void UpdateCharacterProperty(string ModelAndProp, object value, int? selectCharacterID)
        {
            //get context. The using statement ensures resources are disposed of when they go out of scope.
            using (var context = new Context())
            {
                //split the string coming from the DOM into the model type name and property name
                string[] names = ModelAndProp.Split(".");
                //switch on model type name
                switch (names[0])
                {
                    case "CharacterSheet":
                        //moved to the helper and into a method to reduce code.
                        Helper.ModifySheetProperty(typeof(CharacterSheet), context, names, value, selectCharacterID);
                        break;
                    case "SavingThrows":
                        Helper.ModifySheetProperty(typeof(SavingThrows), context, names, value, selectCharacterID);
                        break;
                    case "Skills":
                        Helper.ModifySheetProperty(typeof(Skills), context, names, value, selectCharacterID);
                        break;
                    case "Money":
                        Helper.ModifySheetProperty(typeof(Money), context, names, value, selectCharacterID);
                        break;
                    case "Proficiencies":
                        Helper.ModifySheetProperty(typeof(Proficiencies), context, names, value, selectCharacterID);
                        break;
                    case "Appearance":
                        Helper.ModifySheetProperty(typeof(Appearance), context, names, value, selectCharacterID);
                        break;
                    case "Spellbook":
                        Helper.ModifySheetProperty(typeof(Spellbook), context, names, value, selectCharacterID);
                        break;
                    default:
                        break;
                }
                context.SaveChangesAsync();
            }
        }

        //I probably should make someething elegant that detects what's changed and updated it individually
        //may be a dead end
        public static bool UpdateCharacter(CharacterSheetViewModel sheet)
        {
            //for each property of this view model, which is a model object in itself
            //try to update that object if not null
            foreach (var item in sheet.GetType().GetProperties())
            {
                try
                {
                    //check that property/Model isn't null
                    if (item is null)
                        return false;
                    using (var context = new Context())
                    {

                        //see helper class
                        Helper.SaveSheetProperty(item, context);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
