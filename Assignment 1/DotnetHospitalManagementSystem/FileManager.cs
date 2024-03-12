using System;
using System.IO;

namespace DotnetHospitalManagementSystem
{
    internal static class FileManager
    {
        public static void WriteAllText(string filename, string content) // method to write text files
        {
            try
            {
                File.WriteAllText(filename, content);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"file is not existed: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An I/O error occurred: {e.Message}");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access to the file is denied: {e.Message}");
            }
        }
        public static string ReadAllText(string filename) // method to read text files
        {
            try
            {
                return File.ReadAllText(filename);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (IOException e)
            {
                Console.WriteLine($"An I/O error occurred: {e.Message}");
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access to the file is denied: {e.Message}");
                return null;
            }
        }

        // Method to append data to a file
        public static void AppendAllText(string filename, string content) // method to append text files
        {
            try
            {
                File.AppendAllText(filename, content);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"file is not existed: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An I/O error occurred: {e.Message}");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access to the file is denied: {e.Message}");
            }
        }
    }
}
