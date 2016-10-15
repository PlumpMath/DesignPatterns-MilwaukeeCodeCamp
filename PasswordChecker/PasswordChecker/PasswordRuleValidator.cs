using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChecker
{

    /// <summary>
    /// Implmentation of the password checker using a simple rule pattern
    /// </summary>
    public class PasswordRuleValidator : IPasswordChecker
    {

        public PasswordRuleValidator()
        {
            this.passwordRules = new List<IPasswordRule>();    
        }

        private List<IPasswordRule> passwordRules;


        /// <summary>
        /// Add a rule to the list of rules used to validate a password against
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(IPasswordRule rule)
        {
            this.passwordRules.Add(rule);
        }


        /// <summary>
        /// Validates the password meets all of our rules
        /// </summary>
        /// <remarks>
        /// This method just iterates through all of the IPasswordRule objects in the list,
        /// running ech rule.  Now this method (and class) is really more in a position of
        /// managing the entire process and not in implementing each rule.  This is better
        /// from a separation of concerns point of view and makes things easier to test (i.e.
        /// we can test each rule individually and test the overall algorithm independantly
        /// 
        /// <para>
        /// If we want to add, remove or change the order of the rules, this is much easier as well.
        /// And we can now define our list of rules outside of this class.  For an example, take a look
        /// at the PasswordRuleValidatorTests class in the test project.
        /// </para>
        /// </remarks>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public PasswordResult ValidatePassword(string username, string password)
        {
            List<RuleResult> results = new List<RuleResult>();

            foreach (var rule in this.passwordRules)
            {
                RuleResult ruleResult = rule.CheckPassword(username, password);

                if ( !ruleResult.Passed )
                {
                    PasswordResult passwordResults = new PasswordResult() { ValidPassword = false };
                    passwordResults.Messages.Add(ruleResult.Message);
                    return passwordResults;
                }
            }

            return new PasswordResult() { ValidPassword = true };

        }








    }
}
