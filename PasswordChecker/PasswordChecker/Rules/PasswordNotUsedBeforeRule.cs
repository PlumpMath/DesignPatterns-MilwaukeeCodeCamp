using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChecker.Rules
{

    /// <summary>
    /// A Rule that says a user cannot re-use a password they previously used
    /// </summary>
    public class PasswordNotUsedBeforeRule : IPasswordRule
    {

        /// <summary>
        /// Returns false--If this rule fails we still want to continue checking other rules
        /// </summary>
        public bool BreakOnFailure { get { return false; } }


        /// <summary>
        /// Implementation of rule that 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public RuleResult CheckPassword(string username, string password)
        {
            if (this.HasPasswordBeenUsedBefore(username, password))
            {
                return RuleResult.CreateFailResult($"You have entered a password that you have used before.  Please enter a new password");
            }

            return RuleResult.CreatePassResult();
        }


        /// <summary>
        /// Checks to see if this password for this username has been used before
        /// </summary>
        /// <remarks>
        /// This method gets the hashes of the prior passwords used by this user, and then
        /// cycles through them to see if when you hash this password it matches one of 
        /// the prior password hashes
        /// </remarks>
        /// <param name="username">A String of the username</param>
        /// <param name="newPassword">A String of the new password</param>
        /// <returns>True if the password has been used by this user before.  False otherwise</returns>
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


        /// <summary>
        /// Helper method to simulate some previously used passwords
        /// </summary>
        /// <remarks>
        /// In a real application, there would probably be a database table that contained the hashes
        /// of the prior passwords used by the user, and this method would look up those prior
        /// password hashes and return them from this method
        /// </remarks>
        /// <param name="username">A Strng of the username</param>
        /// <returns>A list of the hashes of prior passwords the user has used</returns>
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
