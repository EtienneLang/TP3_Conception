namespace CineQuebec.Windows.DAL.Exceptions
{
    public class NoteAlreadyExistException : Exception
    {
        public NoteAlreadyExistException(string message) : base(message){}
    }

    public class InvalidNoteValueException : Exception
    {
        public InvalidNoteValueException(string message) : base(message){}
    }
}