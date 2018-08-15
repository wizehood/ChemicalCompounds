namespace Junior.DataAccessLayer.Migrations
{
    using Junior.DataAccessLayer.Context;
    using Junior.SharedModels.DomainModels;
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
        //    //Insert elements
        //    var elements = new List<Element>()
        //    {
        //        new Element()
        //        {
        //            Name = "Hydrogen"
        //        },
        //        new Element()
        //        {
        //            Name = "Nitrogen"
        //        },
        //        new Element()
        //        {
        //            Name = "Carbon"
        //        },
        //        new Element()
        //        {
        //            Name = "Sodium"
        //        },
        //        new Element()
        //        {
        //            Name = "Oxygen"
        //        },
        //    };
        //    context.Elements.AddRange(elements);

        //    //Insert compound types
        //    var types = new List<CompoundType>()
        //    {
        //        new CompoundType()
        //        {
        //            Name = "Covalent"
        //        },
        //        new CompoundType()
        //        {
        //            Name = "Ionic"
        //        },
        //        new CompoundType()
        //        {
        //            Name = "Organic"
        //        },
        //        new CompoundType()
        //        {
        //            Name = "Inorganic"
        //        },
        //    };
        //    context.CompoundTypes.AddRange(types);

        //    context.SaveChanges();
        }
    }
}
