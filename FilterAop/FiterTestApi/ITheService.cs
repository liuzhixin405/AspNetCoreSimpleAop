namespace FiterTestApi
{
    public interface ITheService
    {
        string GetSomething();
    }
   
    public class TheService : ITheService
    {
        public string GetSomething()
        {
            return "Something";
        }
    }
}
