using System;

namespace algebra_quiz_game
{
    class Program
    {
        static void Main(string[] args)
        {
            double score = 0; Difficulty level = 0;
            Menu(score, level);
        }
        static void Menu(double score, Difficulty level)
        {
            int page;
            Console.WriteLine("Score: {0}, Difficulty: {1}", score, level);
            checkpage(out page);
            if (page == 0)
            { play(level, score); }
            else if (page == 1)
            { setup(score, level); }
            else if (page == 2)
            { }
        }
        static void setup(double score, Difficulty level)
        {
            int levelcode;
            do
            {
                levelcode = int.Parse(Console.ReadLine());
                if (levelcode > 2 || levelcode < 0)
                { Console.WriteLine("Please input 0 - 2."); }
            } while (levelcode > 2 || levelcode < 0);
            level = (Difficulty)levelcode;
            Menu(score,level);
        }
        static void play(Difficulty level, double score)
        {
            int d = (int)level; double Ans, Qc = 0, Qa;
            Problem[] RandomProblems = GenerateRandomProblems(d * 2 + 3);
            long starttime = DateTimeOffset.Now.ToUnixTimeSeconds();
            for (int j = 0; j < RandomProblems.Length; j++)
            {
                Console.Write(RandomProblems[j].Message);
                Console.Write(" ");
                Ans = int.Parse(Console.ReadLine());
                if (Ans == RandomProblems[j].Answer)
                { Qc = Qc + 1; }
                else { Qc = Qc + 0; }
                Console.WriteLine();
            }
            long endtime = DateTimeOffset.Now.ToUnixTimeSeconds();
            long dtime = endtime - starttime;
            Qa = d * 2 + 3;
            score = score + ((Qc / Qa) * ((25 - Math.Pow(d, 2)) / Math.Max(dtime, 25 - Math.Pow(d, 2))) * (Math.Pow(2 * d + 1, 2)));
            Menu(score, level);
        }
        static void checkpage(out int page)
        {
            do
            {
                page = int.Parse(Console.ReadLine());
                if(page > 2 || page < 0)
                { Console.WriteLine("Please input 0 - 2."); }
            } while (page > 2 || page < 0);

        }
        enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }
        static Problem[] GenerateRandomProblems(int numProblem)
        {
            Problem[] randomProblems = new Problem[numProblem];

            Random rnd = new Random();
            int x, y;

            for (int i = 0; i < numProblem; i++)
            {
                x = rnd.Next(50);
                y = rnd.Next(50);
                if (rnd.NextDouble() >= 0.5)
                    randomProblems[i] =
                    new Problem(String.Format("{0} + {1} = ?", x, y), x + y);
                else
                    randomProblems[i] =
                    new Problem(String.Format("{0} - {1} = ?", x, y), x - y);
            }

            return randomProblems;
        }
        struct Problem
        {
            public string Message;
            public int Answer;

            public Problem(string message, int answer)
            {
                Message = message;
                Answer = answer;
            }
        }
    }
}
