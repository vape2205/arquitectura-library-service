namespace library.api.Security
{
    public class AccessTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string PublicKey { get; set; }
    }
}
