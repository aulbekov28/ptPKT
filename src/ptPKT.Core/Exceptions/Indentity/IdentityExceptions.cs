using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ptPKT.Core.Exceptions.Indentity
{
    public class IdentityException : Exception
    {
    }

    public class AppUserNotFoundException : IdentityException
    {
    }

    public class AppUserIncorrectPasswordException : IdentityException
    {
    }
    public class EmailNotConfirmedException : IdentityException
    {
    }

    public class UserIsLockedException : IdentityException
    {
    }

    public class AppUserIdentityException : Exception
    {
        public AppUserIdentityException()
        {
        }

        public AppUserIdentityException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
