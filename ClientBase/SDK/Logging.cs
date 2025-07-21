using System;
using System.Runtime.InteropServices;

namespace ClientBase.SDK
{
    internal static class Logging
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        public static void InitConsole()
        {
            AllocConsole();
            Console.Title = "ClientBase Console";
            Log("Console initialized.", LType.Success);
        }

        public static void Log(string message, LType type = LType.Info)
        {
            if (type == LType.Info)
                Console.ForegroundColor = ConsoleColor.White;
            else if (type == LType.Warning)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (type == LType.Error)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (type == LType.Success)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (type == LType.Debug)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if (type == LType.Join)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (type == LType.Leave)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("[" + type.ToString() + "]" + " ~ " + message);
            Console.ResetColor();
        }
    }

    internal enum LType
    {
        Info,
        Warning,
        Error,
        Success,
        Debug,
        Join,
        Leave
    }
}
