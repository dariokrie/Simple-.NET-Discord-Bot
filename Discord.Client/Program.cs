namespace Discord.Client
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var startup = new Startup();

            startup.StartAsync().GetAwaiter().GetResult();
        }
    }
}