using System.Net;

namespace Project.Application.Common.Errors;
internal class DuplicationEmailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email already Exists.";
}