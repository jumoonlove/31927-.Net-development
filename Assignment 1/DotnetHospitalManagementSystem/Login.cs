using System;
using System.Collections.Generic;

namespace DotnetHospitalManagementSystem
{
    internal class Login
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Login");

                    Console.Write("\nID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Password: ");
                    string password = ValidationUtility.SecurePassword(); // mask the input password

                    string role = ValidationUtility.ValidCredentials(id, password); // return corresponded role with input id and password

                    if (role == "Administrator") // if it is admin, get all the data about doctors and patients for admin function
                    {
                        List<Doctor> allDoctors = RetrievalUtility.RetrieveAllDoctors();
                        List<Patient> allPatients = RetrievalUtility.RetrieveAllPatients();
                        Administrator admin = new Administrator(allDoctors, allPatients);
                        admin.MainMenu();
                    }
                    else if (role == "Doctor") // if it is doctor, get all the data about doctor details
                    {
                        Doctor doctor = RetrievalUtility.RetrieveDoctorByID(id);
                        if (doctor != null)
                        {
                            doctor.MainMenu();
                        }
                        else
                        {
                            Console.WriteLine("Error retrieving doctor details. Please try again.");
                        }
                    }
                    else if (role == "Patient") // if it is patient, get all the data about patient details
                    {
                        Patient patient = RetrievalUtility.RetrievePatientByID(id);
                        if (patient != null)
                        {
                            patient.MainMenu();
                        }
                        else
                        {
                            Console.WriteLine("Error retrieving patient details. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Credentials. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to retry...");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to retry...");
                    Console.ReadKey();
                }
            }
        }
    }
}