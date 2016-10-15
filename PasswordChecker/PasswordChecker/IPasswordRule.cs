using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChecker
{

    /// <summary>
    /// Defines an interface for a rule that passwords need to be checked against
    /// </summary>
    /// 
    public interface IPasswordRule
    {

        /// <summary>
        /// Gets if the code checking the password against the list of rules should stop
        /// checking if this rule fails
        /// </summary>
        /// <remarks>
        /// By setting this property to true, no additional rules should be checked
        /// </remarks>
        bool BreakOnFailure { get; }


        /// <summary>
        /// Check to see if this password for this username passes or fails this rule
        /// </summary>
        /// <param name="username">A String of the username the password is for</param>
        /// <param name="password">A String of the password to be checked</param>
        /// <returns>A RuleResult indicating if te password passed or failed this rule</returns>
        RuleResult CheckPassword(String username, String password);
       

    }
}
