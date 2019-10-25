using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreWebAPI.Providers
{
    public class EFConfigurationSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _optionAction;

        public EFConfigurationSource(Action<DbContextOptionsBuilder> optionAction)
        {
            _optionAction = optionAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EFConfigurationProvider(_optionAction);
        }
    }
}