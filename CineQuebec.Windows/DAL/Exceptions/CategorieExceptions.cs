namespace CineQuebec.Windows.DAL.Exceptions
{
    public class CategorieAlreadyExistsException : Exception
    {
        public CategorieAlreadyExistsException(string message) : base(message){}
    }

    public class EmptyCategorieNameException : Exception
    {
        public EmptyCategorieNameException(string message) : base(message){}
    }
}
