using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NDDR
{
    public class ConfigProvider
    {
        public static IConfiguration Config
        {
            get
            {
                var builder = new ConfigurationBuilder();
                //builder.SetBasePath(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName, "SupplyChain.API"));
                builder.AddJsonFile("appsettings.json");
                return builder.Build();
            }
        }
    }
}
