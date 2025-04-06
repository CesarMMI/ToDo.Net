namespace api.Exceptions
{
    public class UnauthorizedException(string message) : AppException(message)
    {
        public override int StatusCode => StatusCodes.Status401Unauthorized;
    }

}
