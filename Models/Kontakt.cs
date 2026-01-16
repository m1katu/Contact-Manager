using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager.Models
{
    internal class Kontakt
    {
        public int Id { get; set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Telefonnummer { get; set; } = string.Empty;
        private string email = string.Empty;
        public string Email {
            get => email;
            set
            {
                if (!value.Contains("@"))
                {
                    throw new ArgumentException("Ungültige E-Mail-Adresse.");
                }
                email = value;
            }
        }

        public override string ToString()
        {
            return $"Kontakt mit ID {Id}: {Vorname} {Nachname} - Tel: {Telefonnummer}, Email: {Email}";
        }
    }
}
