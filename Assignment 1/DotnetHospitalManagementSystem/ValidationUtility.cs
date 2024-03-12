using System;

namespace DotnetHospitalManagementSystem
{
    internal class ValidationUtility
    {
        private static Random random = new Random();
        public static int GenerateID()
        {
            return random.Next(9999, 100000000); // generate random 5-8 digits id
        }
        public static int GeneratePass()
        {
            return random.Next(1000, 10000); // generate random 4 digits password
        }
        /* I found the way to mask password after I read this post
         * https://stackoverflow.com/review/suggested-edits/20911937 */
        public static string SecurePassword()
        {
            string pass = ""; // initialize the empty string to store password user inputs
            while (true) // infinite loop
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // read a key from console without displaying it

                if (keyInfo.Key == ConsoleKey.Enter) // check if user presses enter
                {
                    Console.WriteLine();
                    return pass; // then store it in pass variable
                }
                else if (keyInfo.Key == ConsoleKey.Backspace) // check if user presses backspace
                {
                    if (pass.Length > 0) // if pass has some characters
                    {
                        pass = pass.Remove(pass.Length - 1); // remove from the last charactor
                        Console.Write("\b \b"); // remove it from console as well. * \b \b is the console way to backspace *
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar)) // if user did not press any control keys (such as backspace and enter)
                {
                    pass += keyInfo.KeyChar; // store it into string pass
                    Console.Write("*"); // and display asterisk ('*') on console
                }
            }
        }
        public static string ValidCredentials(int id, string password)
        {
            // Check admin credentials first
            if (ValidAdminCredentials(id, password))
            {
                return "Administrator";
            }

            // Check doctor credentials
            if (ValidDoctorCredentials(id, password))
            {
                return "Doctor";
            }

            // Check patient credentials
            if (ValidPatientCredentials(id, password))
            {
                return "Patient";
            }

            return null;
        }

        public static bool ValidAdminCredentials(int id, string password)
        {
            try
            {
                // Read from admin text files and check
                string adminData = FileManager.ReadAllText("Admin.txt");
                string[] parts = adminData.Split(',');
                return Convert.ToInt32(parts[0]) == id && parts[1] == password;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }

        public static bool ValidDoctorCredentials(int id, string password)
        {
            try
            {
                // Read from doctor text files and check
                string doctorData = FileManager.ReadAllText($"Doctor_{id}.txt");
                if (doctorData != null)
                {
                    string[] parts = doctorData.Split(',');
                    return Convert.ToInt32(parts[0]) == id && parts[1] == password;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }

        public static bool ValidPatientCredentials(int id, string password)
        {
            try
            {
                // Read from patient text files and check
                string patientData = FileManager.ReadAllText($"Patient_{id}.txt");
                if (patientData != null)
                {
                    string[] parts = patientData.Split(',');
                    return Convert.ToInt32(parts[0]) == id && parts[1] == password;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return false;
            }
        }

    }
}
