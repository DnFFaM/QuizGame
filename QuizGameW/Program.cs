using System;
using System.IO;
using System.Text.Json;

namespace QuizGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            string filePath;

            if (args.Length < 1)
            {
                Console.WriteLine("Please provide the path to the JSON file:");
                filePath = Console.ReadLine();
            }
            else
            {
                filePath = args[0];
            }

            // Read the JSON file
            string json = File.ReadAllText(filePath);
            QuizDataWrapper quizDataWrapper = JsonSerializer.Deserialize<QuizDataWrapper>(json);

            // Run the quiz
            RunQuiz(quizDataWrapper.questions);
        }

        static void RunQuiz(Question[] questions)
        {
            if (questions == null)
            {
                Console.WriteLine("Quiz data is invalid.");
                return;
            }

            foreach (var question in questions)
            {
                Console.WriteLine(question.question);
                Console.WriteLine("Options:");
                foreach (var option in question.options)
                {
                    Console.WriteLine("- " + option);
                }

                Console.Write("Enter your answer: ");
                string userAnswer = Console.ReadLine();

                if (Array.Exists(question.answers, answer => answer.Equals(userAnswer, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }

                Console.WriteLine();
            }
        }
    }

    public class QuizDataWrapper
    {
        public Question[] questions { get; set; }
    }

    public class Question
    {
        public string question { get; set; }
        public string[] options { get; set; }
        public string[] answers { get; set; }
    }
}
