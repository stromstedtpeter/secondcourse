/*
    Vi gör Reseloggen mer komplex! Lägg till features från denna lista i vilken ordning du vill:
    Se till att använda fina färger, inte bara standard-grå
    Redan klar igen?
    Grymt jobbat! Du jobbar på snabbt.
*/
using System.Linq;

namespace secondcourse
{
    class Destination(string name)
    {
        public string Name { get; set; } = name;
        public string? Description { get; set; }
        public int VisitedDate { get; set; }
        public int Rating { get; set; }
    }

    class Travellog
    {
        private static List<Destination>? Destinations { get; set; } = [];

        public static void Start()
        {
            do
            {
                Console.WriteLine("1. Lägg till resemål");
                Console.WriteLine("2. Visa detaljer för ett resemål");
                Console.WriteLine("3. Visa alla resemål");
                Console.WriteLine("4. Ta bort resemål");
                Console.WriteLine("5. Visa statistik (antal resmål)");
                Console.WriteLine("6. Sortera resmål");
                Console.WriteLine("0. Avsluta program");

                Console.Write("Vad vill du göra?: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("en siffra som sagt tack.");
                    continue;
                }

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        AddDestination();
                        break;
                    case 2:
                        Show();
                        break;
                    case 3:
                        ListAllDestinations();
                        break;
                    case 4:
                        WhatToRemove();
                        break;
                    case 5:
                        ShowStatistics();
                        break;
                    case 6:
                        SortDestinations();
                        break;
                    case 0:
                        Console.WriteLine("Hej då!.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        break;
                }

            } while (true);
        }

        private static void AddDestination()
        {
            bool accepted = true;
            Destination dest = null;

            do
            {
                Console.Write("Ange ett resmål som du har besökt: ");
                string name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Du måste ange ett namn på resemålet...");
                    continue;
                }

                name = UcFirst(name);
                Destination exist = Destinations.FirstOrDefault(d => d.Name.Equals(name));

                if (exist != null)
                {
                    Console.WriteLine("Tyvärr finns det redan på listan.");
                    continue;
                }

                dest = new Destination(name);
                Destinations?.Add(dest);
                Console.WriteLine($"{dest.Name} har blivit tillagd.");

                accepted = false;

            } while (accepted);

            EditDestination(dest);
        }

        private static void EditDestination(Destination dest)
        {
            bool isEditing = true;

            do
            {
                Console.WriteLine($"Vill du lägga till [i]nformation, [å]r du besökte eller sätta [b]etyg på {dest.Name}?");
                Console.Write("Eller är du nöjd? [a]vsluta: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "a":
                        isEditing = false;
                        break;
                    case "b":
                        Console.Write("Vilket betyg vill du ge? (1-5): ");
                        if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 5)
                        {
                            dest.Rating = rating;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt betyg, försök igen.");
                        }
                        break;
                    case "i":
                        Console.Write($"Ange vilken information du vill om {dest.Name}: ");
                        string description = Console.ReadLine();
                        dest.Description = description;
                        break;
                    case "å":
                        Console.Write($"Ange vilket år du besökte {dest.Name}: ");
                        if (int.TryParse(Console.ReadLine(), out int year))
                        {
                            dest.VisitedDate = year;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt år, försök igen.");
                        }
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

            } while (isEditing);
        }

        private static void ListAllDestinations(List<Destination>? listToDisplay = null)
        {
            var destinationsToDisplay = listToDisplay ?? Destinations;

            if (destinationsToDisplay.Count == 0)
            {
                Console.WriteLine("Du får inte glömma att lägga in resmål...");
            }
            else
            {
                int i = 1;
                foreach (Destination dest in destinationsToDisplay)
                {
                    Console.WriteLine($"{i}. Resmål: {dest.Name}, År besökt: {dest.VisitedDate}, Betyg: {dest.Rating}");
                    i++;
                }
            }
        }

        private static void RemoveDestination()
        {
            ListAllDestinations();
            Console.Write("Vilken vill du ta bort(siffra eller namn)?: ");
            string whatToRemove = Console.ReadLine();
            whatToRemove = UcFirst(whatToRemove);

            if (!int.TryParse(whatToRemove, out int number))
            {
                if (number > 0 && number <= Destinations.Count)
                {
                    Destinations.RemoveAt(number - 1);
                    Console.WriteLine("Resmålet har tagits bort.");
                }
                else
                {
                    Console.WriteLine("Ogiltigt nummer.");
                }
            } 
            else
            {
                Destination remove = Destinations.FirstOrDefault(d => d.Name == whatToRemove);
                if (remove != null)
                {
                    Destinations.Remove(remove);
                }
                else
                {
                    Console.WriteLine("Ogilitigt namn.");
                }
            }
        }

        private static void RemoveAllDestinations()
        {
            Destinations.Clear();
        }

        private static void WhatToRemove()
        {
            do
            {
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. Ta bort 1 resmål");
                Console.WriteLine("2. Ta bort alla");
                Console.WriteLine("\n0. För att återgå");
                Console.Write("\nTa bort 1 resmål eller tömma allt?: ");

                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine("Du måste skriva in en siffra...");
                    continue;
                }

                switch (number)
                {
                    case 1:
                        RemoveDestination();
                        break;
                    case 2:
                        RemoveAllDestinations();
                        break;
                    case 0:
                        return;
                }
            } while (true);
        }

        private static void ShowStatistics()
        {
            Console.WriteLine($"Totalt antal resmål: {Destinations.Count}");
        }

        private static void SortDestinations()
        {
            Console.WriteLine("Hur vill du sortera resmålen?");
            Console.WriteLine("1. Sortera på namn (A-Ö)");
            Console.WriteLine("2. Sortera på år besökt (äldsta till nyaste)");

            if (!int.TryParse(Console.ReadLine(), out int sortChoice))
            {
                Console.WriteLine("Ogiltigt val.");
                return;
            }

            switch (sortChoice)
            {
                case 1:
                    var sortedByName = Destinations.OrderBy(d => d.Name).ToList();
                    ListAllDestinations(sortedByName);
                    break;
                case 2:
                    var sortedByYear = Destinations.OrderBy(d => d.VisitedDate).ToList();
                    ListAllDestinations(sortedByYear);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        private static void Show()
        {
            if (Destinations.Count == 0)
            {
                Console.WriteLine("Inga resmål finns inlagda.");
                return;
            }

            // Visa alla resmål med nummer
            ListAllDestinations();

            Console.Write("Ange numret för det resmål du vill visa detaljer för: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= Destinations.Count)
            {
                // Hämta resmålet baserat på användarens val
                Destination selected = Destinations[choice - 1];

                // Visa detaljerad information om det valda resmålet
                Console.WriteLine("\nDetaljerad information:");
                Console.WriteLine($"Namn: {selected.Name}");
                Console.WriteLine($"År besökt: {(selected.VisitedDate > 0 ? selected.VisitedDate.ToString() : "Ej angivet")}");
                Console.WriteLine($"Beskrivning: {(!string.IsNullOrEmpty(selected.Description) ? selected.Description : "Ej angiven")}");
                Console.WriteLine($"Betyg: {(selected.Rating > 0 ? selected.Rating.ToString() : "Ej betygsatt")}");
            }
            else
            {
                Console.WriteLine("Ogiltigt nummer.");
            }
        }

        /// <summary>
        /// Method for Capitilazing the first letter and making the rest small.
        /// </summary>
        /// <param name="input">String that should be modified</param>
        /// <returns></returns>
        public static string UcFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
