using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordChecker
{

    /// <summary>
    /// A simple class to validate that a password meets some rules we have established
    /// </summary>
    /// <remarks>
    /// This class implements a password checker just through using a series of if
    /// statements in a single method.  While this works, this leaves a lot to be desired
    /// from a code readability and maintanability point of view.  Most of our rules have
    /// a similar implementation, but not all -- note the rule about not having your
    /// username in your password.  Also, if we want to add or reorder these rules, it
    /// becomes easy to make a mistake when we have all of these if statements like this.
    /// </remarks> 
    public class SimplePasswordChecker : IPasswordChecker
    {

        /// <summary>
        /// Checks if a password is valid (acceptable) by running it through some rules
        /// </summary>
        /// <param name="username">A String of the username the password is for</param>
        /// <param name="password">A String of the password to be checked</param>
        /// <returns>A PasswordResult object indicating if the password meets all of the established rules</returns>
        public PasswordResult ValidatePassword(string username, string password)
        {
            PasswordResult results = new PasswordResult();
            results.ValidPassword = false;

            if ( String.IsNullOrEmpty(password))
            {
                results.Messages.Add("A password value must be provided");
                return results;
            }
            else if ( password.Length < 8 )
            {
                results.Messages.Add("The password must be 8 characters in length or greater");
                return results;
            }
            else if ( !Regex.IsMatch(password, "[A-Z]+", RegexOptions.IgnoreCase) )
            {
                results.Messages.Add("The password must contain at least one letter");
                return results;
            }
            else if (!Regex.IsMatch(password, "[0-9]+") )
            {
                results.Messages.Add("The password must contain at least one number");
                return results;
            }

            String usernameRegex = String.Format("({0})+", username);
            if ( Regex.IsMatch(password, usernameRegex, RegexOptions.IgnoreCase))
            {
                results.Messages.Add( $"Your username of {username} may not be used as part of the password");
                return results;
            }

            if ( this.HasPasswordBeenUsedBefore(username, password) )
            {
                results.Messages.Add($"You have entered a password that you have used before.  Please enter a new password");
                return results;
            }

            return results;
        }


        internal bool HasPasswordBeenUsedBefore(String username, String newPassword)
        {
            List<PasswordHistory> historicalPasswords = this.GetHistoricalPasswords(username);

            foreach (var oldPassword in historicalPasswords)
            {
                var hashedPassword = PasswordUtil.HashPassword(oldPassword.Salt, newPassword);
                if (hashedPassword == oldPassword.PasswordHash)
                    return true;
            }

            return false;
        }


        internal List<PasswordHistory> GetHistoricalPasswords(String username)
        {
            return new List<PasswordHistory>()
            {
                new PasswordHistory() { Salt = "ABCDEFGH", PasswordHash = "3130E56DC7A51A6B768442B80643C6AC2E4A9CA881DC7C995172E2A448F326A2" },
                new PasswordHistory() { Salt = "EFGHIJKL", PasswordHash = "6DF2A7FD740D357AA4122792D47BCB31A9AB88AD2A52E9E011979EAC537F9A16" },
                new PasswordHistory() { Salt = "IJKLMNOP", PasswordHash = "040942AA8AF939B60A4156FEC8620C7EDDA287BBF1AEC7E6DD06270D81837B9D" }
            };
        }





    }
}
