using EmailMenager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailMenager.Models.Repositories
{
    public class EmailRepository
    {
        public void Add(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                email.PostDate = DateTime.Now;
                context.Emails.Add(email);
                context.SaveChanges();
            }
        }

        public object GetEmails()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.ToList();
            }
        }

        public Email GetEmail(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Single(x => x.Id == id);
            }
        }

        public void Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailToDelete = context.Emails
                    .Single(x => x.Id == id);
                context.Emails.Remove(emailToDelete);
                context.SaveChanges();
            }
        }
    }
}