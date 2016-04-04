using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_Code
{
    class Program
    {
        private const string NamesFile = "Names.txt";
        private const string SurnamesFile = "Surnames.txt";
        
        private const string OutputFile = "Output.txt";
        private const string StatisticsFile = "Statistics.txt";
        private const int FullNamesNumber = 10000;
        static List<string> namesList;
        static List<string> surnamesList;
        static HashSet<string> uniqueFullNames;
        static Dictionary<string, int> fullnameStatistics;

        static void Main(string[] args)
        {
            namesList = new List<string>();
            surnamesList = new List<string>();
            uniqueFullNames = new HashSet<string>();
            fullnameStatistics = new Dictionary<string, int>();
            ReadNames();
            ReadSurnames();
            Mix();
            WriteStatistics();
            Console.ReadLine();
        }

        private static void ReadNames()
        {
            ReadLines(NamesFile, namesList);
        }

        private static void ReadSurnames()
        {
            ReadLines(SurnamesFile, surnamesList);
        }

        private static void ReadLines(string path, List<string> outList)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    outList.Add(line);
                }
            }
        }

        private static void Mix()
        {
            using (StreamWriter writer = new StreamWriter(OutputFile))
            {
                Random rand = new Random();

                for (int i = 0; i < FullNamesNumber; i++)
                {
                    string fullName;
                    int namesCount = namesList.Count;
                    int surNames = surnamesList.Count;
                    int currentName = rand.Next(namesCount);
                    int currentSurname = rand.Next(surNames);
                    fullName = String.Format("{0} {1}", namesList[currentName], surnamesList[currentSurname]);
                    //if (!uniqueFullNames.Contains(fullName))
                    //{
                    //    writer.WriteLine(fullName);
                    //    uniqueFullNames.Add(fullName);
                    //}
                    //writer.WriteLine(fullName);
                    if (fullnameStatistics.ContainsKey(fullName))
                    {
                        int currentValue = fullnameStatistics[fullName];
                        
                        fullnameStatistics[fullName] = currentValue + 1;
                    }
                    else
                    {
                        fullnameStatistics[fullName] = 1;
                    }
                    writer.WriteLine(fullName);
                }
            }            
        }
        private static void WriteStatistics()
        {
            using (StreamWriter writer = new StreamWriter(StatisticsFile))
            {
                foreach (KeyValuePair<string, int> entry in fullnameStatistics)
                {
                    writer.WriteLine("{0}-{1}", entry.Key, entry.Value);
                }
            }
        }
    }
}
