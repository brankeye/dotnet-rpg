namespace dotnet_rpg.Service.Enums
{
    /// <summary>
    /// Service-level error codes for exceptions
    /// </summary>
    public enum ErrorCode
    {
        ///<summary>The error occurred during validation</summary>
        ValidationError = 1000,
        
        ///<summary>The error occurred in the service layer</summary>
        ServerError = 2000,
        
        ///<summary>The error occurred in the infrastructure layer</summary>
        DataError = 3000
    }
}