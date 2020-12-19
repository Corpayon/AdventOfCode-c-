using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2
{
    class Program
    {
        static void Main(string[] args)
        {

         
        }


            public static void AdventOfCode10()
        {     
            List<int> adapter = File.ReadAllText(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile10.txt")
                .Split(Environment.NewLine)
                .Select(x => Convert.ToInt32(x))
                .ToList();
            
            adapter.Sort();
            long result = 1;
            int count = 0;
            ArrayList a = new ArrayList();
            a.Add(0);



            for (int i = 0; i < adapter.Count-3; i++)
            {
                for(int j = 1; j < 3; j++)
                {
                     count = 1;
                    if( 3 > adapter[i+j] - adapter[i])
                    {
                       // a.IndexOf(i) += count;
                        count++;
                    }
                }
                a.Add(0);
                
                result = result * count;

            }
            Console.WriteLine(result);
            }

            public static void AdventOfCode09()
        {
            int preamble = 25; 
            List<long> list = new List<long>();
            long [] numbers = File.ReadAllText(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile9.txt")
                .Split(Environment.NewLine)
                .Select(x => Convert.ToInt64(x))
                .ToArray();
            for(int i = 0; i < preamble; i++)
            {
                list.Add(numbers[i]);
            }
            for (int x = 0; x < numbers.Length-5; x++)
            {
                long check = numbers[preamble+x];
              //  Console.WriteLine(check);
                bool findPair = false;
                for (int i = x; i < preamble+x-1; i++)
                {
                    for (int j = i + 1; j < preamble + x; j++)
                    {
                        if (check == numbers[i] + numbers[j])
                        {
                            findPair = true;
                                     
                        }
                    }
                }
                if(findPair == false)
                {
                    for (int zz = 0; zz < list.Count; zz++)
                    {
                        Console.WriteLine(zz);
                        long sum = list.ToArray()[zz];

                        for (int u = zz+1; u <  list.Count; u++)
                        {
                            sum += list.ToArray()[u];
                            
                           
                            if (sum > check)
                            {
                                break;
                            }
                            if (check == sum)
                            {
                                List<long> list2 = new List<long>(list.GetRange(u, zz));
                                Console.WriteLine(list2.Max()+list2.Min());
                                
                                return;
                            }
                        }
                    }
                   
                }
                list.Add(check);

            }
        }


        public static void AdventOfCode08()
        {
            string line;
            string[,] instruction = new string[20, 3];
            int result = 0;
            int z = 0;
            int column = 0;

            StreamReader file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile8.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] sSplit = line.Split(' ');
                instruction[column, 0] = sSplit[0];
                instruction[column, 1] = sSplit[1];
                instruction[column, 2] = "0";
                column++;
            }
            bool work = true;
            column = 0;
            int lascolumn = 0;


            while (true)
            {

                Console.WriteLine(instruction[column, 0] + " : " + instruction[column, 1] + " : " + instruction[column, 2]);
                int value = Convert.ToInt32(instruction[column, 1]);
                string current = instruction[column, 0];
               


                if (instruction[column, 2].Equals("1"))
                {
                    Console.WriteLine(lascolumn);
                    work = false;


                    break;
                }
                lascolumn = column;

                instruction[column, 2] = "1";

                switch (current)
                {
                    case "acc":
                        result += value;
                        column++;
                        break;
                    case "jmp":
                        column += value;
                        break;
                    case "nop":
                        column++;
                        break;
                }
            }

            Console.WriteLine(result);
        }
        public static void AdventOfCode07()
        {
            // int result = 0;
            string line;
            HashSet<string> result = new HashSet<string> { };
            Regex regex = new Regex(@"shiny gold");

            StreamReader file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile7.txt");
            string[] text = File.ReadAllLines(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile7.txt");
            //    string text2 = File.ReadAllText(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile7.txt");
            Dictionary<string, string> allBags = new Dictionary<string, string>();

            for (int i = 0; i < text.Length; i++)
            {
                string[] z = text[i].Replace("bags", "").Replace("bag", "").Split("contain");
                allBags.Add(z[0], z[1]);
            }
            Dictionary<string, string> allBags2 = new Dictionary<string, string>(allBags);
            int count = allBags.Count;
            bool doWork = true;
            while (doWork)
            {
                string e = "";
                doWork = false;

                foreach (KeyValuePair<string, string> entry in allBags)
                {

                    if (entry.Value.Contains("plaid bronze") || entry.Key.Contains("plaid bronze"))
                    {
                        Console.WriteLine(entry.Key + " : " + entry.Value);
                    }

                    if (regex.IsMatch(entry.Value))
                    {
                        regex = new Regex(regex + " | " + entry.Key.Trim());
                        result.Add(entry.Key);
                        allBags.Remove(entry.Key);
                        doWork = true;

                    }
                }


                count--;
            }

            Console.WriteLine(result.Count);

            //  allBags2.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
            //  Console.WriteLine(text2);
        }

        public static void AdventOfCode06()
        {
            int result1 = 0;
            int result2 = 0;
            string[] text = File.ReadAllText(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile6.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < text.Length; i++)
            {
                //Part 1
                result1 += new HashSet<char>(text[i].Replace(Environment.NewLine, "")).Count;

                //Part 2
                string s = text[i].Replace(Environment.NewLine, ";");
                int groupSize = s.ToCharArray().Count(x => x == ';') + 1;

                foreach (char c in new HashSet<char>(text[i].Replace(Environment.NewLine, "")))
                {
                    result2 += groupSize == s.Count(x => x == c) ? 1 : 0;
                }

            }
            Console.WriteLine("Part1: " + result1);
            Console.WriteLine("Part2: " + result2);
        }


        public static void AdventOfCode05_2()
        {
            string line;
            var map = new Dictionary<string, string>();
            StreamReader file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\Binärzahlen.txt");
            while ((line = file.ReadLine()) != null)
            {
                string fmt = "0000000.##";
                string[] a = line.Split('=');
                int bin = Convert.ToInt32(a[1]);
                map.Add(a[0], bin.ToString(fmt));
            }
            file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile5.txt");
            string readText = File.ReadAllText(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile5.txt").Replace('F', '1').Replace('B', '0').Replace("\n", ";");
            Console.WriteLine(readText);
            //  map.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
        }



        public static void AdventOfCode05()
        {
            string line;
            double result = 0;
            StreamReader file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile5.txt");
            List<double> array = new List<double>();
            while ((line = file.ReadLine()) != null)
            {
                char[] seat = line.ToCharArray();
                double borderTop = 127; double borderBottom = 0;
                double borderLeft = 0; double borderRight = 7;
                double check = 1;
                for (int i = 0; i < seat.Length; i++)
                {
                    if (seat[i] == 'F')
                    {
                        borderTop = Math.Floor((borderTop + borderBottom) / 2);
                    }
                    else if (seat[i] == 'B')
                    {
                        borderBottom = Math.Ceiling((borderTop + borderBottom) / 2);
                    }
                    else if (seat[i] == 'R')
                    {
                        borderLeft = Math.Ceiling((borderRight + borderLeft) / 2);
                    }
                    else if (seat[i] == 'L')
                    {
                        borderRight = Math.Floor((borderRight + borderLeft) / 2);
                    }
                }
                check *= (seat[6] == 'F' ? borderTop : borderBottom) * 8;
                check += seat[9] == 'R' ? borderLeft : borderRight;
                result = check > result ? check : result;
                array.Add(check);
            }
            array.Sort();
            double y = 6;
            foreach (double d in array)
            {
                if (d - y != 0)
                {
                    Console.WriteLine("my seat: " + (d - 1));
                    break;
                }
                y++;
            }
            Console.WriteLine("highest seat id: " + result);
        }


        public static void AdventOfCode04()
        {
            string input = makeString(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile4.txt");

            int result = 0;

            var values = new[] { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" };


            List<string> array = new List<string>(input.Split(";"));
            foreach (string s in array)
            {
                bool check = true;
                if (values.All(s.Contains))
                {
                    string[] a = s.Split(" ");

                    for (int i = 0; i < a.Length; i++)
                    {
                        string current = a[i];

                        string[] currCase = current.Split(":");

                        switch (currCase[0])
                        {
                            case "byr":
                                check = IsInRangeInclusive(1920, 2002, Convert.ToInt32(currCase[1]));
                                break;
                            case "iyr":
                                check = IsInRangeInclusive(2010, 2020, Convert.ToInt32(currCase[1]));
                                break;
                            case "eyr":
                                check = IsInRangeInclusive(2020, 2030, Convert.ToInt32(currCase[1]));
                                break;
                            case "hgt":
                                if (currCase[1].Contains("cm"))
                                {
                                    currCase[1] = currCase[1].Replace("cm", "");
                                    check = IsInRangeInclusive(150, 193, Convert.ToInt32(currCase[1]));
                                }
                                else
                                {
                                    currCase[1] = currCase[1].Replace("in", "");
                                    check = IsInRangeInclusive(59, 76, Convert.ToInt32(currCase[1]));
                                }
                                break;
                            case "hcl":
                                Regex regex = new Regex(@"#[a-f0-9]{6}");
                                check = regex.IsMatch(currCase[1]);
                                break;
                            case "ecl":
                                string u = currCase[1];
                                var eyeColor = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                                check = eyeColor.Contains(currCase[1]);
                                break;
                            case "pid":
                                Console.WriteLine(s);
                                try
                                {
                                    Convert.ToInt32(currCase[1]);
                                    check = currCase[1].Length == 9 ? true : false;
                                }
                                catch (Exception)
                                {
                                    check = false;
                                }
                                break;
                            case "cid":
                                break;
                            default:
                                break;
                        }
                        if (check == false)
                        {
                            break;
                        }
                    }
                    result += check ? 1 : 0;
                }
            }
            Console.WriteLine(result);
        }

        public static void AdventOfCode03()
        {
            string line;
            long result = 1;
            // int counter = 3; Task 1
            int counterForDown2 = 0;
            int[] counts = { 1, 3, 5, 7, 1 };
            int[] right = { 1, 3, 5, 7, 1 };
            int[] down = { 1, 1, 1, 1, 2 };
            int[] result2 = { 0, 0, 0, 0, 0 };
            StreamReader file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile2.txt");
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                for (int i = 0; i < counts.Length - 1; i++)
                {

                    result2[i] += Convert.ToChar(line.ToCharArray().GetValue(right[i] % line.Length)) == '#' ? 1 : 0;

                    right[i] += counts[i];
                }
                if (counterForDown2 % 2 != 0)
                {


                    result2[4] += Convert.ToChar(line.ToCharArray().GetValue(right[4] % line.Length)) == '#' ? 1 : 0;
                    right[4]++;
                }
                counterForDown2++;

                /* Task 1
                result += Convert.ToChar(line.ToCharArray().GetValue(counter % line.Length)) == '#' ? 1 : 0;
                counter += 3;
                */
            }
            result2.ToList().ForEach(f => result *= f);
            Console.WriteLine(result);
        }

        public static void AdvetnOfCode02()
        {
            string line;
            int result = 0;
            StreamReader file = new StreamReader(@"C:\Users\steve\source\repos\AdventOfCode2\AdventOfCode2\TextFile1.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] array = line.Split(new char[] { '-', ':', ' ' }, StringSplitOptions.None);
                int borderLeft = Convert.ToInt32(array[0]);
                int borderRight = Convert.ToInt32(array[1]);
                char letter = Convert.ToChar(array[2]);
                char[] l = array[4].ToCharArray();
                if (l[borderRight - 1] == letter ^ l[borderLeft - 1] == letter)
                {
                    result++;
                }
                /*   Task 1
                   int count = array[4].Count(f => f == letter);
                   if (count >= borderLeft && count <= borderRight)
                   {            
                       result++;
                   }
                   */
            }
            Console.WriteLine(result);

        }

        public static string makeString(string fileName)
        {
            string line;
            string result = "";
            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Equals(""))
                {
                    result += "; ";
                }
                else
                {
                    result += line + " ";
                }
            }
            return result;
        }

        public static bool IsInRangeInclusive(int min, int max, int value)
        {
            return min <= value && max >= value;
        }

    }




}
