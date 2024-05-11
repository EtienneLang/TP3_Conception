namespace CineQuebec.Windows.DAL.Exceptions
{
    public class EmptyNameException : Exception
    {
        public EmptyNameException(string message) : base(message) {}
    }

    public class InvalidNameLengthException : Exception
    {
        public InvalidNameLengthException(string message) : base(message){}
    }

    public class InexistingEntityException : Exception
    {
        public InexistingEntityException(string message) : base(message){}
    }
}
