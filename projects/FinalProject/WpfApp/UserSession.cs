using ShoeShopLibrary.Models;

namespace WpfApp
{
    public class UserSession
    {
        public User User { get; private set; }
        private static readonly UserSession _instance = new();
        private UserSession() { }
        public static UserSession Instance => _instance;

        public void SetCurrentUser(User user)
        {
            User = user;
        }

        public void Clear()
        {
            User = null!;
        }
    }
}
