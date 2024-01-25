namespace DragonAPIH3.Models
{
    public class AccountData
    {
        // Fields
        private static List<Account> _accounts = new List<Account>()
        {
            new Account("Dragon", "12"),
            new Account("DragonOwner", "123"),
            new Account("SongWriterDragon", "1234"),
            new Account("MusicLoverDragon", "12345")
        };

        // Properties 
        public IEnumerable<Account> Accounts { get { return _accounts; } }

        /// <summary>
        /// Adds user to list of users. Username is unique
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public bool AddAccount(Account acc)
        {
            try
            {
                if (string.IsNullOrEmpty(acc.Username) || string.IsNullOrEmpty(acc.PrivateKey)) { return false; }
                if (ExistingAccount(acc.Username) != null) { return false; }

                Account newAccount = new Account(acc.Username, acc.PrivateKey);
                _accounts.Add(newAccount);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to find account from username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Account or Null</returns>
        public Account? ExistingAccount(string username)
        {
            return _accounts.FirstOrDefault(existingAccount => existingAccount.Username == username);
        }
    }
}
