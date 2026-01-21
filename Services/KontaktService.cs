using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Contact_Manager.Models;
using System.IO;

namespace Contact_Manager.Services
{
    internal class KontaktService
    {
        public static int IDCounter = 1;
        public List<Kontakt> Kontakte = new List<Kontakt>();
        public KontaktService()
        {
            LoadKontakte();
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

                int newID = IDCounter++; //Assign ID and directly increment counter
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

        #region JSON Save/Load Methods
        public void SaveKontakte() // Serializes the contact list to a JSON file to save data locally
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(Kontakte, options);
            //Console.WriteLine(jsonString);
            FileStream fs = new FileStream("kontakte.json", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(jsonString);
            sw.Close();
        }

        public void LoadKontakte() // Deserializes the contact list from a local JSON file to load data 
        { 
            if (File.Exists("kontakte.json"))
            {
                string jsonString = File.ReadAllText("kontakte.json");
                if (string.IsNullOrEmpty(jsonString))
                {
                    return;
                }
                Kontakte = JsonSerializer.Deserialize<List<Kontakt>>(jsonString) ?? new List<Kontakt>();
                //Set IDCounter to highest existing ID +1
                if (Kontakte.Count > 0) 
                {
                    IDCounter = Kontakte.Max(x => x.Id) + 1;
                    //Console.WriteLine(IDCounter);
                }
            }
        }
        #endregion
    }
}
