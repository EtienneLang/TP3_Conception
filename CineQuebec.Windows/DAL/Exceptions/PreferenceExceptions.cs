using System.Windows.Automation;

namespace CineQuebec.Windows.DAL.Exceptions
{
    public class TooManyRealisateurException : Exception
    {
        public TooManyRealisateurException(string message) : base(message){}
    }

    public class TooManyActeurException : Exception
    {
        public TooManyActeurException(string message) : base(message){}
    }

    public class TooManyCategorieException : Exception
    {
        public TooManyCategorieException(string message) : base(message){}
    }
}
