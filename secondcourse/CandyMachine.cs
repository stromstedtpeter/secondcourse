namespace secondcourse
{
    class CandyMachine
    {
        private static List<Candy> candies = new List<Candy>
        {
            new Candy { Name = "Japp", Amount = 5},
            new Candy { Name = "Daim", Amount = 3},
            new Candy { Name = "Snickers", Amount = 4},
            new Candy { Name = "Bounty", Amount = 1},
        };

        public static void MainMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Välkommen till Godisautomaten...");
                Console.WriteLine("Välj en utav alla sorter.");

                if (candies.Count == 0)
                {
                    Console.WriteLine("Tyvärr så funkar inte automaten utan godis. ADMIN!");
                }
                else
                {
                    TheCandies();
                }

                Console.Write("\nVilket godis vill du ha? (0 för att avsluta.): ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    break;
                }

                if (choice == 666)
                {
                    AdminView();
                    continue;
                }

                if (candies[choice - 1].Amount != 0)
                {
                    candies[choice - 1].Amount --;
                }
                else
                {
                    Console.WriteLine("Tyvärr den här är slut.");
                    Console.WriteLine("\nTryck på valfri knapp för att välja något annat.");
                    Console.ReadKey();
                }

            } while (true);
        }

        private static void AdminView()
        {
            do
            {
                Console.WriteLine("Administratör vy:\n");

                TheCandies();

                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. för att lägga till godis");
                Console.WriteLine("2. för att ta bort godis.");
                Console.WriteLine("\n0. för att återgå.");
                int choice = int.Parse(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        AddCandy();
                        break;
                    case 2:
                        RemoveCandy();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Felaktig input...");
                        continue;
                }

            } while (true);
        }

        private static void RemoveCandy()
        {
            Console.Clear();
            Console.WriteLine("Vilket godis vill du ta bort?");

            if (candies.Count == 0)
            {
                Console.WriteLine("du kan inte ta bort godis eftersom det är tomt...");
                Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
                Console.ReadKey();
                return;
            }
           
            TheCandies();

            Console.WriteLine("Ange siffran");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= candies.Count)
            {
                candies.RemoveAt(choice - 1);
                Console.WriteLine("Godiset togs bort.");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }

            Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
            Console.ReadKey();
        }

        private static void AddCandy()
        {
            Console.Clear();
            Console.WriteLine("Vad heter godiset du vill lägga till? och antal (, emellan namn och antal)");
            string answer = Console.ReadLine();

            string[] parts = answer.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int amount))
            {
                Candy candy = new Candy
                {
                    Name = parts[0].Trim(),
                    Amount = amount
                };

                candies.Add(candy);
                Console.WriteLine($"Godis {candy.Name} har lagts till...");
            }
            else
            {
                Console.WriteLine("Ogiltigt format försök igen..");
            }

            Console.WriteLine("\n Tryck på valfri knapp för att fortsätta");
            Console.ReadKey();
        }

        private static void TheCandies()
        {
            int i = 1;

            foreach (Candy candy in candies)
            {
                if (candy.Amount == 0)
                {
                    Console.WriteLine($"LUCKA NR: {i}. {candy.Name} ÄR SLUT");
                }
                else
                {
                    Console.WriteLine($"LUCKA NR: {i}. {candy.Name} ANTAL: {candy.Amount}");
                }
                i++;
            }
        }
    }

    class Candy
    {
        public string? Name { get; set; }
        public int Amount {  get; set; }
    }
}
