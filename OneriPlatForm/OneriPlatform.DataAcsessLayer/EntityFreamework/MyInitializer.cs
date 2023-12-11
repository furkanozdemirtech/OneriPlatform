using System.Data.Entity;

namespace OneriPlatform.DataAcsessLayer.EntityFreamework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            context.SaveChanges();
        }
    }
}
