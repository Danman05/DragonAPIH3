using DragonAPIH3.Models;

namespace DragonAPIH3.Services
{
    public class LoginService
    {
        private AccountData _accData = new AccountData();

        /// <summary>
        /// Verifies login
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool VerifyLogin(Account account)
        {
            try
            {
                // Find account by username
                Account? foundAccount = _accData.ExistingAccount(account.Username);

                // Account exist check
                if (foundAccount == null) { return false; }

                // Private keys match check
                if (foundAccount.PrivateKey != account.PrivateKey) { return false; }

                // Login verified
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
