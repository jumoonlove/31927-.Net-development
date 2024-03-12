using System;
using System.Collections.Generic;

namespace DotnetHospitalManagementSystem
{
    internal class Administrator : IMainMenu // derived from interface class
    {
        private List<Doctor> doctors; // List of added doctors
        private List<Patient> patients; // List of added patients

        public Administrator(List<Doctor> doctors, List<Patient> patients)
        {
            this.doctors = doctors;
            this.patients = patients;
        }

        public void MainMenu()
        {
            while (true)
            {
                try
                {
                Console.Clear();
                GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Administrator Menu");
                Console.WriteLine("Welcome to Dotnet Hospital Management System Admin\n");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List All Doctors");
                Console.WriteLine("2. Check Doctor Details");
                Console.WriteLine("3. List All Patients");
                Console.WriteLine("4. Check Patient Details");
                Console.WriteLine("5. Add Patient");
                Console.WriteLine("6. Add Doctor");
                Console.WriteLine("7. Logout");
                Console.WriteLine("8. Exit");

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();

                    switch (choice)
                    {
                        case '1':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "All Doctors");
                            ListAllDoctors();
                            break;
                        case '2':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Doctor Details");
                            CheckDoctorDetails();
                            break;
                        case '3':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "All Patients");
                            ListAllPatients();
                            break;
                        case '4':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Patient Details");
                            CheckPatientDetails();
                            break;
                        case '5':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Add Patient");
                            AddPatient();
                            break;
                        case '6':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Add Doctor");
                            AddDoctor();
                            break;
                        case '7':
                            return;
                        case '8':
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break; // default value for incorrect input
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error occurred: {e.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void ListAllDoctors()
        {
            try
            {
                Console.WriteLine("\nAll doctors registered to the Dotnet Hospital Management System\n");
                foreach (Doctor doctor in doctors) // iterate through all doctors created by add doctor method
                {
                    Console.WriteLine(doctor.ToString());
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error occurred: {e.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void CheckDoctorDetails()
        {
            try
            {
                Console.WriteLine("\nPlease Enter the ID of the doctor whose details you are checking.");
                int id = Convert.ToInt32(Console.ReadLine()); // Convert string to int

                Doctor foundDoctor = null;

                foreach (Doctor doctor in doctors) // loop to find a corresponded doctor from input id
                {
                    if (doctor.ID == id)
                    {
                        foundDoctor = doctor;
                        break;
                    }
                }

                if (foundDoctor != null)
                {
                    Console.WriteLine($"\nDetails for {foundDoctor.Name}\n");
                    Console.WriteLine(foundDoctor.ToString());
                }
                else
                {
                    Console.WriteLine("No doctor found with the given ID.");
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
        }

        private void ListAllPatients()
        {
            try
            {
                Console.WriteLine("\nAll patients registered to the Dotnet Hospital Management System\n");
                foreach (Patient patient in patients)  // iterate through all patients created by add patient method
                {
                    Console.WriteLine(patient.ToString());
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error occurred: {e.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void CheckPatientDetails()
        {
            try
            {
                Console.WriteLine("\nPlease Enter the ID of the patient whose details you are checking.");
                int id = Convert.ToInt32(Console.ReadLine());
                Patient foundPatient = null;

                foreach (Patient patient in patients) // loop to find a corresponded patient from input id
                {
                    if (patient.ID == id)
                    {
                        foundPatient = patient;
                        break;
                    }
                }

                if (foundPatient != null)
                {
                    Console.WriteLine($"\nDetails for {foundPatient.Name}\n");
                    Console.WriteLine(foundPatient.ToString());
                }
                else
                {
                    Console.WriteLine("No doctor found with the given ID.");
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
        }
        private void AddPatient()
        {
            try
            {
                Console.WriteLine("Registering a new patient with the Dotnet Hospital Management System");

                // get inputs for details of patient from user to add patients
                Console.Write("First Name: ");
                string FirstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string LastName = Console.ReadLine();

                Console.Write("Email: ");
                string Email = Console.ReadLine();

                Console.Write("Phone: ");
                string Phone = Console.ReadLine();

                Console.Write("Street Number: ");
                string StreetNumber = Console.ReadLine();

                Console.Write("Street: ");
                string Street = Console.ReadLine();

                Console.Write("City: ");
                string City = Console.ReadLine();

                Console.Write("State: ");
                string State = Console.ReadLine();

                // Concatenate the address, name details and store them into local variables
                string Address = $"{StreetNumber} {Street} {City} {State}";
                string Name = $"{FirstName} {LastName}";
                int newPatientID = ValidationUtility.GenerateID();
                int newPatientPassword = ValidationUtility.GeneratePass(); // generate random id and password, using method from utility class
                Patient newPatient = new Patient(newPatientID, newPatientPassword, Name, Address, Email, Phone);

                patients.Add(newPatient);

                // Write this data to Patients.txt
                FileManager.AppendAllText($"Patient_{newPatient.ID}.txt", $"{newPatient.ID},{newPatient.Password},{newPatient.Name},{newPatient.Address},{newPatient.Email},{newPatient.Phone}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
        }

        private void AddDoctor()
        {
            try
            {
                Console.WriteLine("Registering a new doctor with the Dotnet Hospital Management System");

                Console.Write("First Name: ");
                string FirstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string LastName = Console.ReadLine();

                Console.Write("Email: ");
                string Email = Console.ReadLine();

                Console.Write("Phone: ");
                string Phone = Console.ReadLine();

                Console.Write("Street Number: ");
                string StreetNumber = Console.ReadLine();

                Console.Write("Street: ");
                string Street = Console.ReadLine();

                Console.Write("City: ");
                string City = Console.ReadLine();

                Console.Write("State: ");
                string State = Console.ReadLine();

                // Concatenate the address, name details
                string Address = $"{StreetNumber} {Street} {City} {State}";
                string Name = $"{FirstName} {LastName}";

                int newDoctorID = ValidationUtility.GenerateID();
                int newDoctorPassword = ValidationUtility.GeneratePass();
                Doctor newDoctor = new Doctor(newDoctorID, newDoctorPassword, Name, Address, Email, Phone, patients);

                doctors.Add(newDoctor);

                // Also, write this data to Doctors.txt
                FileManager.AppendAllText($"Doctor_{newDoctor.ID}.txt", $"{newDoctor.ID},{newDoctor.Password},{newDoctor.Name},{newDoctor.Address},{newDoctor.Email},{newDoctor.Phone}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
        }
    }
}
