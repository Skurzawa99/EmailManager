using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace EmailMenager.Models.Domains
{
    public class Email
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole tytuł jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Subject { get; set; }

        [Display(Name = "Treść")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Pole nadawca jest wymagane")]
        [Display(Name = "Nazwa nadawcy")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Pole odbiorca jest wymagane")]
        [RegularExpression(@"^(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))*$", ErrorMessage ="nieprawidłowy adres email")]
        [Display(Name = "Email odbiorcy")]
        public string ReciverEmail { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime PostDate { get; set; }

        public string FileName { get; set; }

        public byte[] FileUpload { get; set; }


        public Config Config { get; set; }
    }
}