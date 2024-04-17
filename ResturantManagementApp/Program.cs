namespace ResturantManagementLibrary
{
    class Program
    {
        public static void Main()
        {
            LoginMenu loginMenu = LoginMenu.GetInstance();
            loginMenu.StartLoginMenu();
        }
    }
}
