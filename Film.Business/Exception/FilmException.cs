using System;

namespace Film.Business
{
    public class FilmException : Exception
    {
        public FilmException()
        {
        }

        public FilmException(string message) : base(message)
        {
        }
    }
}
