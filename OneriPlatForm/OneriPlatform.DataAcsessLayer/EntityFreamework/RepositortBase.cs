namespace OneriPlatform.DataAcsessLayer.EntityFreamework
{
    public class RepositortBase
    {
        protected static DatabaseContext context;
        private static object _lockSync = new object();

        public static DatabaseContext CreateContext()
        {
            if (context == null)
                lock (_lockSync)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();

                    }
                }
            return context;

        }
        public RepositortBase()
        {

            CreateContext();
        }
    }
}
