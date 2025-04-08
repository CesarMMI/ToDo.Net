namespace api.Exceptions;

public class InternalServerErrorException() : AppException("Internal server error")
{
    public override int StatusCode => StatusCodes.Status500InternalServerError;
}
