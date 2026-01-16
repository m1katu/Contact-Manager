using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_Manager.Models;

namespace Contact_Manager.Services
{
    internal class KontaktService
    {
        public static int IDCounter = 1;
        public List <Kontakt> Kontakte = new List<Kontakt>();
        public KontaktService() 
        {
            //TODO: check for Contact Data (JSON) on startup, load into list
        }

        public void AddKontakt(string pVorname, string pNachname, string pEmail, string pTel)
        {
            //Is Contact already in list?
            Kontakt kExists = Kontakte.Find(x => x.Email == pEmail);

            //YES: Alert user there is a contact with same data, do not add, return
            if (kExists != null)
            {
                Console.WriteLine("Kontakt mit dieser Emailadresse existiert bereits");
                return;
            }
            //NO: Create and add contact, increment IDCounter
            else
            {
                if (string.IsNullOrEmpty(pVorname) || string.IsNullOrEmpty(pNachname) || string.IsNullOrEmpty(pEmail))
                {   //TODO: Better error handling -> Exception
                    Console.WriteLine("Vorname, Nachname und Email dürfen nicht leer sein.");
                    return;
                }

                int newID = IDCounter++;
                Kontakt newContact = new Kontakt
                {
                    Id = newID,
                    Vorname = pVorname,
                    Nachname = pNachname,
                    Email = pEmail,
                    Telefonnummer = pTel
                };
                Kontakte.Add(newContact);
            }
        }

        public List<Kontakt> GetAllKontakte() => Kontakte;

        public bool RemoveKontakt(int pID)
        { 
            Kontakt kToRemove = Kontakte.Find(x => x.Id == pID);
            if (kToRemove != null)
            {
                Kontakte.Remove(kToRemove);
                Console.WriteLine("Kontakt gefunden und aus Liste der Kontakte entfernt.");
                return true;
            }
            Console.WriteLine($"Kontakt mit ID {pID} nicht gefunden.");
            return false;
        }
    }
}
