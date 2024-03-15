using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskStack
{
    class Task1
    {
        static string text = "";
        private static async Task<int> santxAnalyz()
        {
            string[] sCounter = text.Split('!', '.', '?');
            await Task.Delay(1);
            return sCounter.GetLength(0);
        }
        private static async Task<int> charAnalyz()
        {
            await Task.Delay(1);
            return text.Length;
        }
        private static async Task<int> wordAnalyz()
        {
            string[] wCounter = text.Split(' ', ',', '.', '!', '?');
            await Task.Delay(1);
            return wCounter.GetLength(0);
        }
        private static async Task<int[]> SpecAnalyz()
        {
            int[] specCounter = new int[2];
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '!')
                {
                    specCounter[0]++;
                }
                else if (text[i] == '?')
                {
                    specCounter[1]++;
                }
            }
            await Task.Delay(1);
            return specCounter;
        }

        private static async Task TotalSave()
        {
            string total = "";
            total += $"\nSantaxes: {santxAnalyz().Result}";
            total += $"\nChars: {charAnalyz().Result}";
            total += $"\nWords: {wordAnalyz().Result}";
            int[] spec = SpecAnalyz().Result;
            total += $"\n[!]: {spec[0]}\t[?]: {spec[1]}";
            await Task.Delay(5000);
            File.WriteAllText("D:\\saveLog.txt", total);
        }
        private static async Task TotalOutput()
        {
            string total = "";
            total += $"\nSantaxes: {santxAnalyz().Result}";
            total += $"\nChars: {charAnalyz().Result}";
            total += $"\nWords: {wordAnalyz().Result}";
            int[] spec = SpecAnalyz().Result;
            total += $"\n[!]: {spec[0]}\t[?]: {spec[1]}";
            await Task.Delay(5000);
            Console.WriteLine(total);
        }

        private static async Task additionalTask2(bool output)
        {
            await Task.Delay(1);
            char choise = ' ';
            Console.WriteLine("Enter 1 for cancel analyze\nEnter 2 for restart analyze");
            choise = Console.ReadLine()[0];
            if (output)
            {
                if (choise == 1)
                {
                    await TotalOutput();
                    // вот есть токены отмены, но их привязать к функциям, подобным моим - я не нашел как
                    // А есть повторный вызов функции... И он рушит саму функцию! Я этим костылём не горжусь, но оно работает...
                }
                else if (choise == 2)
                {
                    TotalOutput().Dispose();

                }
            }
            else
            {
                if (choise == 1)
                {
                    await TotalSave();
                }
                else if (choise == 2)
                {
                    TotalSave().Dispose();
                }
            }
        }
        public static async Task task1()
        {
            char choise = '0';
            Console.WriteLine("Enter text:\n");
            string text = Console.ReadLine();
            Console.WriteLine("Choose:\n1 - output to screen\n2 - save to file");
            choise = (Console.ReadLine())[0];
            if (choise == '1')
            {
                Task.WaitAny(TotalOutput(), additionalTask2(true));
                Console.WriteLine("Done!");
            }
            else if (choise == '2')
            {
                Task.WaitAny(TotalOutput(), additionalTask2(false));
                await TotalSave();
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }
    }
}

namespace HW_ProgSys_5
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await TaskStack.Task1.task1();
        }
    }
}
