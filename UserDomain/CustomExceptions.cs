using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDomain;
    public class NotFoundException : AppException
    {
        public NotFoundException(string message = "Resource not found") : base(message, 404) { }
    }
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Not authorized") : base(message, 401) { }
}
public class BadRequestException : AppException
{
    public BadRequestException(string message = "Bad request") : base(message, 400) { }
}
public class InternalServerException : AppException
{
    public InternalServerException(string message = "Server error") : base(message, 500) { }
}
public class DatabaseUnavailableException : AppException
{
    public DatabaseUnavailableException(string message = "Database not connected") : base(message, 503) { }
}
