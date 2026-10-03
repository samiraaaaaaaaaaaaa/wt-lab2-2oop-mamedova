using System;

namespace wt_lab2_2oop_mamedova.Exceptions
{
    public class HotelValidationException : Exception
    {
        public HotelValidationException(string message) : base(message)
        {
        }
    }
}