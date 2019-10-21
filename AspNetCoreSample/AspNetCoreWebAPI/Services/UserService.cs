using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebAPI.Services
{
    public class UserService : IAUserInterface, IBUserInterface
    {
        public string HelloA()
        {
            return "HelloA";
        }

        public string HelloB()
        {
            return "HelloB";
        }
    }
}
