using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ptPKT.Core.Exceptions.Indentity
{
    public class UserNotFound : Exception
    {
        public UserNotFound()
        {
        }

        public UserNotFound(string message) : base(message)
        {
        }

        public UserNotFound(string message, Exception ex) : base(message, ex)
        {
        }
    }

    public class IncorrectCredentialsException : Exception
    {
        public IncorrectCredentialsException()
        {
        }

        public IncorrectCredentialsException(string message) : base(message)
        {
        }

        public IncorrectCredentialsException(string message, Exception ex) : base(message, ex)
        {
        }
    }

    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException()
        {
        }

        public EmailNotConfirmedException(string message) : base(message)
        {
        }

        public EmailNotConfirmedException(string message, Exception ex) : base(message, ex)
        {
        }
    }

    public class UserIsLockedException : Exception
    {
        public UserIsLockedException()
        {
        }

        public UserIsLockedException(string message) : base(message)
        {
        }

        public UserIsLockedException(string message, Exception ex) : base(message, ex)
        {
        }
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
