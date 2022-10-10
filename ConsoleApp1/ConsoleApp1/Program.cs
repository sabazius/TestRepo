using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private readonly int b;

        public Program(int input)
        {
            b = input;
        }
        static void Main(string[] args)
        {
            string str = "Test";
            str ??= "Hello";

            Console.WriteLine(str);
        }
    }
}