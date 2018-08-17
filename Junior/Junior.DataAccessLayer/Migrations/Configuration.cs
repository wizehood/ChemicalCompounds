namespace Junior.DataAccessLayer.Migrations
{
    using Junior.DataAccessLayer.Context;
    using Junior.SharedModels.DomainModels;
    using Junior.SharedModels.Dummy;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Junior.DataAccessLayer.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            //Uncomment following lines to seed database
            //context.CompoundTypes.AddRange(Dummy.CompoundTypes);
            //context.Elements.AddRange(Dummy.Elements);
            //context.SaveChanges();
        }
    }
}
