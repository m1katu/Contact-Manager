using Contact_Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Contact Manager. Please use your keyboard to input any data. Decide what to do using numerical inputs.\n'0' lets you return to this screen.");
            KontaktService kontaktService = new KontaktService();
            Run(true, kontaktService);
        }

        public static void Run(bool running, KontaktService service)
        {
            while (running)
            {
                Console.WriteLine(">=== Contact Manager ===<");
                Console.WriteLine("1. Add contact");
                Console.WriteLine("2. List contacts");
                Console.WriteLine("3. Delete contact");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        return;
                    case 1:
                        GetContactInput(service);
                        break;
                    case 2:
                        ListContacts(service);
                        break;
                    case 3:
                        DeleteContact(service);
                        break;
                    case 4: running = false;
                        break;
                    default: Console.WriteLine("Your input doesn't match any of the given options. Please enter a valid number for your choice.");
                        break;
                }
            }

            service.SaveKontakte();
            //TODO: Save contacts to JSON file
            Console.WriteLine("Thank you for using the Contact Manager."); //Program ends here.
        }

        public static void GetContactInput(KontaktService service)
        {
            //TODO:Exit possibility

            string input = "";

            Console.Write("First Name: ");
            input = Console.ReadLine();

            if (CancelInput(input)) return;
            string firstName = input;

            Console.Write("Last Name: ");
            input = Console.ReadLine();

            if (CancelInput(input)) return;
            string lastName = input;

            Console.Write("Email: ");
            input = Console.ReadLine();

            if (CancelInput(input)) return;
            string email = input;

            Console.Write("Phone: ");
            input = Console.ReadLine();

            if (CancelInput(input)) return;
            string phone = input;

            service.AddKontakt(firstName, lastName, email, phone);
            Console.WriteLine("Contact added.");

            //Todo : Handle empty inputs and Exit possibility
        }
        public static void ListContacts(KontaktService service)
        {
            if (service.Kontakte.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }
            foreach (var contact in service.Kontakte)
            {
                Console.WriteLine(contact.ToString());
            }

        }

        public static void DeleteContact(KontaktService service)
        {
            //Check if there are any contacts to delete in first place
            if (service.Kontakte.Count == 0)
            {
                Console.WriteLine("No contacts yet.");
                return;
            }
            Console.Write("Enter the ID of the contact to delete: ");
            string input = Console.ReadLine();
            if (CancelInput(input)) return;
            int id = Convert.ToInt32(input);
            bool success = service.RemoveKontakt(id);
            //if (success)
            //{
            //    Console.WriteLine("Contact deleted.");
            //}
            //else
            //{
            //    Console.WriteLine("Contact not found.");
            //}
            //Code above handled in Service class' method.

            //Todo : Handle invalid input (non-integer) and Exit possibility
        }

        public static bool CancelInput(string input)
        {
            if (input == "0")
            {
                Console.WriteLine("Cancelled process.");
                return true;
            }
            return false;
        }

    }
}


/* Why did i mix languages. whyyyy. Stoopid german */
