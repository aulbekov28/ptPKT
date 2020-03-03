using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ptPKT.Core.Exceptions.Indentity
{
    public class AppUserNotFoundException : Exception
    {
    }

    public class AppUserIncorrectPasswordException : Exception
    {
    }

    public class AppUserCreationException : Exception
    {
        public AppUserCreationException()
        {
        }

        public AppUserCreationException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
