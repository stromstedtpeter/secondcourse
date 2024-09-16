namespace secondcourse
{
    abstract class Question
    {
        public string QuestionText { get; set; }
        public abstract void AskQuestion();
        public abstract bool CheckAnswer(IAnswer answer);
    }

    class TextQuestion : Question
    {
        public string CorrectAnswer { get; set; }

        public override void AskQuestion()
        {
            Console.WriteLine(QuestionText);
        }

        public override bool CheckAnswer(IAnswer answer)
        {
            return answer is TextAnswer textAnswer && textAnswer.AnswerText.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }

    class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }

        public override void AskQuestion()
        {
            Console.WriteLine(QuestionText);
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
        }

        public override bool CheckAnswer(IAnswer answer)
        {
            return answer is MultipleChoiceAnswer mcAnswer && mcAnswer.SelectedOption.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }

    interface IAnswer
    {
        // Marker interface
    }

    class TextAnswer : IAnswer
    {
        public string AnswerText { get; set; }
    }

    class MultipleChoiceAnswer : IAnswer
    {
        public string SelectedOption { get; set; }
    }

    class Quiz
    {
        private static List<Question> questions = new List<Question>();

        public static void MainMenu()
        {
            bool running = true;

            do
            {
                Console.WriteLine("1. Spela spelet");
                Console.WriteLine("\n666. Admin");
                Console.WriteLine("\n0. Avsluta programmet");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Det måste vara en siffra du matar in");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        StartGame();
                        break;
                    case 666:
                        Console.Clear();
                        AdminMenu();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Hej då...");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Felaktig input... välj ett ifrån menyn");
                        break;
                }

            } while (running);
        }

        private static void AdminMenu()
        {
            Console.Clear();
            bool running = true;

            do
            {
                Console.WriteLine("1. Lägg till ny fråga.");
                Console.WriteLine("0. Återgå till huvudmenyn.");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Du måste ange en siffra.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        QuestionMenu();
                        break;
                    case 0:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Felaktig siffra...");
                        break;
                }

            } while (running);
        }

        private static void QuestionMenu()
        {
            bool running = true;
            Console.Clear();

            do
            {
                Console.WriteLine("Vilken typ av fråga vill du skapa?");
                Console.WriteLine("1. Fritextsfråga");
                Console.WriteLine("2. Flervalsalternativ");
                Console.WriteLine("\n0. För att återgå till adminmenyn");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Du måste ange en siffra, tack.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateTextQuestion();
                        break;
                    case 2:
                        CreateMultipleChoiceQuestion();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Felaktig siffra...");
                        break;
                }

            } while (running);
        }

        private static void CreateTextQuestion()
        {
            Console.Write("Ange frågetext: ");
            string questionText = Console.ReadLine();

            Console.Write("Ange rätt svar: ");
            string correctAnswer = Console.ReadLine();

            questions.Add(new TextQuestion
            {
                QuestionText = questionText,
                CorrectAnswer = correctAnswer
            });

            Console.WriteLine("Fritextfråga skapad!");
        }

        private static void CreateMultipleChoiceQuestion()
        {
            Console.Write("Ange frågetext: ");
            string questionText = Console.ReadLine();

            Console.Write("Ange alternativ (separera med kommatecken): ");
            string[] options = Console.ReadLine().Split(',');

            Console.Write("Ange rätt svar: ");
            string correctAnswer = Console.ReadLine();

            questions.Add(new MultipleChoiceQuestion
            {
                QuestionText = questionText,
                Options = options.ToList(),
                CorrectAnswer = correctAnswer
            });

            Console.WriteLine("Flervalsfråga skapad!");
        }

        private static void StartGame()
        {
            if (questions.Count == 0)
            {
                Console.WriteLine("Det finns inga frågor att spela med. Vänligen skapa några frågor först.");
                return;
            }

            Random random = new Random();
            List<Question> usedQuestions = new List<Question>();

            while (true)
            {
                if (usedQuestions.Count == questions.Count)
                {
                    Console.WriteLine("Alla frågor har ställts. Avslutar spelet.");
                    break;
                }

                Question question;
                do
                {
                    question = questions[random.Next(questions.Count)];
                } while (usedQuestions.Contains(question));

                usedQuestions.Add(question);

                question.AskQuestion();
                Console.Write("Skriv ditt svar: ");

                IAnswer answer;

                if (question is TextQuestion)
                {
                    string answerText = Console.ReadLine();
                    answer = new TextAnswer { AnswerText = answerText };
                }
                else if (question is MultipleChoiceQuestion)
                {
                    string selectedOption = Console.ReadLine();
                    answer = new MultipleChoiceAnswer { SelectedOption = selectedOption };
                }
                else
                {
                    Console.WriteLine("Frågetyp inte hanterad.");
                    continue;
                }

                if (question.CheckAnswer(answer))
                {
                    Console.WriteLine("Rätt svar!");
                }
                else
                {
                    Console.WriteLine("Fel svar. Försök igen!");
                }
            }
        }
    }
}
