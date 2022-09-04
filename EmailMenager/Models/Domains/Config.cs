using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailMenager.Models.Domains
{
    public class Config
    {
        public Config()
        {
            Emails = new Collection<Email>();
        }
        public int Id { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Pole email jest wymagane")]
        public string Email { get; set; }

        [Display(Name = "Hasło do konta email")]
        [Required(ErrorMessage = "Pole hasło jest wymagane")]
        public string PasswordEmail { get; set; }

        [Display(Name = "Host do konta email")]
        [Required(ErrorMessage = "Pole host jest wymagane")]
        public string Host { get; set; }

        [Display(Name = "Port do konta email")]
        [Required(ErrorMessage = "Pole port jest wymagane")]
        public int Port { get; set; }

        [Display(Name = "Włącz Ssl")]
        public bool EnableSsl { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedDate { get; set; }

        public ICollection<Email> Emails { get; set; }
    }
}