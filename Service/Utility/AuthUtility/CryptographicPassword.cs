namespace dotnet_rpg.Service.Utility.AuthUtility
{
    public class CryptographicPassword
    {
        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }
    }
}