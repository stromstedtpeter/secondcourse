using System.Text;

namespace secondcourse
{
    // Conclusion of which sorting algorithm to use:
    // Bubblesort and selection sort, are easier to handle and if the list isnt that big use it.
    // Merge sort more complex to implement, but way more efficient. Though extra memory required.
    class Highscore
    {
        private static List<HSItem> HsitemList = [];
        private const string FilePath = "output.csv";
        public static int MaxInList { get; set; }
        public Highscore(int maxInList)
        {
            MaxInList = maxInList;
        }

        public void Add(string name, int points, TimeSpan time)
        {
            if (HsitemList.Count >= MaxInList)
            {
                HSItem lowest = HsitemList.Where(h => h.Points <= points).FirstOrDefault();
                if (lowest != null)
                    HsitemList.Remove(lowest);
            }

            HSItem hs = new(name, points, time);
            HsitemList.Add(hs);
        }

        public void Print()
        {
            var list = Sort(HsitemList);

            int i = 1;

            foreach (var h in list)
            {
                Console.WriteLine($"{i}. {h.Name} {h.Points} på tiden {h.Time.TotalSeconds} sekunder.");
                i++;
            }

            SaveToCSV(list);
        }

        public void PrintWithBubbleSort()
        {
            var list = BubbleSort(HsitemList);

            int i = 1;

            foreach (var h in list)
            {
                Console.WriteLine($"{i}. {h.Name} {h.Points}");
                i++;
            }
        }

        private List<HSItem> Sort(List<HSItem> list)
        {
            var sorted = HsitemList.OrderByDescending(h => h.Points).ToList();

            return sorted;
        }

        // easy to understand and implement
        // compares every element with eachother whic makes an easy algoritm
        // inefficient for big lists, not stable and like bubblesort slow for big lists
        private List<HSItem> SelectionSort(List<HSItem> list)
        {
            int n = list.Count;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (list[j].Time < list[minIndex].Time)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    (list[minIndex], list[i]) = (list[i], list[minIndex]);
                }
            }

            return list;
        }

        // very efficient for big lists
        // stable sorting, good for lists that dont fit memorywise
        // requires extra memory to store temp lists
        // can be more complex to implement than bubble sort and selection sort.
        private List<HSItem> MergeSort(List<HSItem> list)
        {
            // if the list only contains 1 or less, return early
            if (list.Count <= 1)
                return list;

            int mid = list.Count / 2;
            List<HSItem> left = MergeSort(list.Take(mid).ToList());
            List<HSItem> right = MergeSort(list.Skip(mid).ToList());

            return Merge(left, right);
        }

        private List<HSItem> Merge(List<HSItem> left,  List<HSItem> right)
        {
            List<HSItem> result = new List<HSItem>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i].Time < right[j].Time)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }

            // if there are more on either side of the lists.
            while (i < left.Count)
                result.Add(left[i++]);

            while (j < right.Count) 
                result.Add(right[j++]);

            return result;
        }

        // easy to understand and implement
        // slow especially for big lists
        private List<HSItem> BubbleSort(List<HSItem> list)
        {
            int n = list.Count;
            bool finished;

            for (int i = 0; i < n -1; i++)
            {
                finished = false;

                for (int j = 0; j < n - 1; j++)
                {
                    if (list[j].Points <= list[j+1].Points)
                    {
                        var temp = list[j];
                        list[j] = list[j+1];
                        list[j+1] = temp;

                        finished = true;
                    }
                }

                if (!finished)
                {
                    break;
                }
            }

            return list;
        }

        public void ReadFromCSV()
        {   
            StringBuilder sb = new();
            
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Filen finns inte.");
                return;
            }

            if (HsitemList.Count <= 0)
            {
                try
                {
                    var lines = File.ReadAllLines(FilePath);

                    foreach (var line in lines)
                    {
                        var columns = line.Split(',');

                        if (columns.Length == 3)
                        {
                            string name = columns[0];
                            int points = int.Parse(columns[1]);
                            TimeSpan time = TimeSpan.Parse(columns[2]);

                            HSItem item = new(name, points, time);

                            HsitemList.Add(item);
                        }
                    }
                }
                catch (HighscoreException ex)
                {
                    Console.WriteLine($"Det uppstod ett problem {ex.Message}");
                }
            }
        }

        private void SaveToCSV(List<HSItem> list)
        {
            try
            {
                StringBuilder sb = new();

                foreach (HSItem item in list)
                {
                    sb.AppendLine($"{item.Name},{item.Points},{item.Time}");
                }

                File.WriteAllText(FilePath, sb.ToString());
            }
            catch (HighscoreException ex)
            {
                Console.WriteLine($"Det har uppstått ett fel: {ex.Message}");
            }
        }    
    }

    // using the default from Exception instead of adding new.
    class HighscoreException : Exception
    {}

    class HSItem(string name, int points, TimeSpan time)
    {
        public string Name { get; set; } = name;
        public int Points { get; set; } = points;
        public TimeSpan Time { get; set; } = time;
    }
}
