namespace ChemicalCompounds.DataAccessLayer.Migrations
{
    using Bogus;
    using ChemicalCompounds.DataAccessLayer.Context;
    using ChemicalCompounds.SharedModels.DomainModels;
    using ChemicalCompounds.SharedModels.Dummy;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ChemicalCompounds.DataAccessLayer.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            //Seed database with components types and elements
            if (context.CompoundTypes.Count() == 0)
            {
                context.CompoundTypes.AddRange(Dummy.CompoundTypes);
            }
            if (context.Elements.Count() == 0)
            {
                context.Elements.AddRange(Dummy.Elements);
            }
            context.SaveChanges();

            //Initial setup
            int compoundCount = 20;
            int maxElementCount = 5;
            int maxElementQuantity = 12;

            //Seed fake compounds
            var compoundMock = new Faker<Compound>()
                .StrictMode(false)
                .RuleFor(u => u.Name, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(u => u.TypeId, f => f.PickRandom<Guid>(context.CompoundTypes.Select(t => t.Id)));

            var compounds = compoundMock.Generate(compoundCount);
            var compoundElements = new List<CompoundElement>();

            //Seed fake compound elements
            foreach (var compound in compounds)
            {
                var elementCount = new Faker().Random.Number(2, maxElementCount);

                var compoundElementMock = new Faker<CompoundElement>()
                   .StrictMode(false)
                   .RuleFor(u => u.ElementId, f => f.PickRandom<Guid>(context.Elements.Select(t => t.Id)))
                   .RuleFor(u => u.CompoundId, f => compound.Id)
                   .RuleFor(u => u.ElementQuantity, f => f.Random.Number(1, maxElementQuantity));

                for (int i = 0; i < elementCount; i++)
                {
                    compoundElements.Add(compoundElementMock.Generate());
                }
            }

            context.Compounds.AddRange(compounds);
            context.CompoundElements.AddRange(compoundElements);
            context.SaveChanges();
        }
    }
}
