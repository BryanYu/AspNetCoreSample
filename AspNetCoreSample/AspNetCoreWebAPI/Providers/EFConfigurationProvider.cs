using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using AspNetCoreWebAPI.Models;

namespace AspNetCoreWebAPI.Providers
{
    public class EFConfigurationProvider : ConfigurationProvider
    {
        public Action<DbContextOptionsBuilder> OptionAction;

        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionAction)
        {
            OptionAction = optionAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<EFConfigurationContext>();

            OptionAction(builder);

            using (var dbContext = new EFConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();

                Data = !dbContext.Values.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : dbContext.Values.ToDictionary(c => c.Id, c => c.Value);
            }
        }

        private IDictionary<string, string> CreateAndSaveDefaultValues(EFConfigurationContext dbContext)
        {
            var configValues = new Dictionary<string, string>
            {
                { "quote1", "I aim to misbehave." },
                { "quote2", "I swallowed a bug." },
                { "quote3", "You can't stop the signal, Mal." }
            };

            dbContext.Values.AddRange(configValues.Select(kvp =>
                new EFConfigurationValue
                {
                    Id = kvp.Key,
                    Value = kvp.Value
                }).ToArray());
            dbContext.SaveChanges();

            return configValues;
        }
    }
}