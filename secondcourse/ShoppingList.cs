namespace secondcourse
{
    internal class ShoppingList
    {
        public static void Start()
        {
            List<string> stringList = new List<string>();

            do
            {
                Console.Clear();
                Console.WriteLine("Listans meny:");
                Console.WriteLine("1. Visa listans innehåll.");
                Console.WriteLine("2. Lägg till en artikel");
                Console.WriteLine("3. Ta bort en artikel");
                Console.WriteLine("4. Sök efter en artikel");
                Console.WriteLine("\n0. För att avsluta");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("en siffra som sagt tack.");
                    returnBack();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        showList(stringList);
                        returnBack();
                        break;
                    case 2:
                        Console.Clear();
                        addArticle(stringList);
                        returnBack();
                        break;
                    case 3:
                        Console.Clear();
                        removeAnArticle(stringList);
                        returnBack();
                        break;
                    case 4:
                        Console.Clear();
                        searchForArticle(stringList);
                        returnBack();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Hej då");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("1-4 eller 0 tack.");
                        break;
                }

            } while (true);
        }

        static void showList(List<string> stringList)
        {
            if (stringList.Count <= 0)
            {
                Console.WriteLine("Listan är tom, fyll på den först.");
                return;
            }

            int i = 1;

            foreach (var item in stringList)
            {
                Console.WriteLine($"{i}. {item}");
                i++;
            }
        }

        static void addArticle(List<string> stringList)
        {
            Console.Write("Vad heter artikeln du vill lägga till?: ");
            string article = Console.ReadLine();
            stringList.Add(article);
            Console.Clear();
            Console.WriteLine($"{article} har blivit tillagd.");
        }

        static void removeAnArticle(List<string> stringList)
        {
            Console.Write("Vad vill du ta bort ifrån listan?: ");
            string article = Console.ReadLine();
            if (stringList.Contains(article))
            {
                stringList.Remove(article);
                Console.WriteLine($"{article} har tagits bort");
            }
            else
            {
                Console.WriteLine($"{article} finns inte på listan");
            }
        }

        static void searchForArticle(List<string> stringList)
        {
            Console.Write("Vad vill du hitta?: ");
            string article = Console.ReadLine();
            if (stringList.Contains(article))
            {
                Console.WriteLine($"Listan innehåller {article}");
            }
            else
            {
                Console.WriteLine($"{article} finns ej på listan...");
            }
        }

        static void returnBack()
        {
            Console.WriteLine("\nTryck på valfri knapp för att återgå.");
            Console.ReadKey();
        }
    }
}