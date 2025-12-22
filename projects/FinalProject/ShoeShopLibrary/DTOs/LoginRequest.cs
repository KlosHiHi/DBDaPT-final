namespace ShoeShopLibrary.DTOs
{
    /// <summary>
    /// Краткая запись данных для авторизации
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="password">Пароль от аккаунта пользователя</param>
    public class LoginRequest(string login, string password)
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
    }
}
