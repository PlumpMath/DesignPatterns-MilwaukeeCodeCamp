This example shows another way you can refactor from a series of if statements into some more
maintainable code.  

The example used is a class that checks to see if passwords pass a series of rules such as:
  -- Is the password long enough
  -- Does the password have at least one letter
  -- Does the password have at least one number
  -- Has the password been used before

The idea is to factor out each use case as a class, and have those "rule" classes implement a common
interface.  Then it becomes very simple to just iterate through the rules, checking each one.  This 
separates out the process that manages the algorithm from the implementation of the rules, and makes
it much easier to add or remove rules as needed.

The main classes to look at are:

SimplePasswordChecker -- The original implementation with a bunch of if statements

PasswordRuleValidator -- An implementation using the IPasswordRule interface to iterate through the rules

BetterPasswordRuleValidator -- A revised implementation with functionality so that if a rule fails, the
code will stop checking further rules