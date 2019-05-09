using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mordenkainen2.Models;
using System.Data.SqlClient;
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
        //I probably should make someething elegant that detects what's changed and updated it individually
        public bool UpdateCharacter(CharacterSheetViewModel sheet)
        {
            return true;
        }
    }
}
