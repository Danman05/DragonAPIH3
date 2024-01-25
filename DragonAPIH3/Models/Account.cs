namespace DragonAPIH3
{
    public class Account
    {

        // Properties
        public string Username { get; set; }
        public string PrivateKey { get; set; }

        // Constructor
        public Account(string username, string privateKey)
        {
            Username = username;
            PrivateKey = privateKey;
        }
    }
}
