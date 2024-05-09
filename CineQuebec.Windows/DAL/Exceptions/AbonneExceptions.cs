namespace CineQuebec.Windows.DAL.Exceptions
{
    public class InexistingUserException : Exception 
    {
        public InexistingUserException(string message) : base(message){}
    }
    
    public class UserAlreadyHasPreferenceException : Exception
    {
        public UserAlreadyHasPreferenceException(string message) : base(message) {}
    }
}

