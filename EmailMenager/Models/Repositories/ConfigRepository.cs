using EmailMenager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailMenager.Models.Repositories
{
    public class ConfigRepository
    {
        public void Add(Config config)
        {
            using (var context = new ApplicationDbContext())
            {
                config.CreatedDate = DateTime.Now;
                context.Configs.Add(config);
                context.SaveChanges();
            }
        }

        public Config GetConfig()
        {
            using (var context = new ApplicationDbContext())
            {  
                if(context.Configs.Any())
                    return context.Configs.OrderByDescending(x => x.Id).First();
                else
                    return null;
            }
        }

        public List<Config> GetConfigs()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Configs.ToList();
            }
        }
    }
}