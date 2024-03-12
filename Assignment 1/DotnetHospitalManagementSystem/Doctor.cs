using System;
using System.Collections.Generic;
using System.IO;

namespace DotnetHospitalManagementSystem
{
    internal class Doctor : Identity, IMainMenu // derived from Identity class and IMainMenu interface
    {

        private List<Patient> patients; // Private data member list of patients registered with this doctor

        public Doctor(int id, int password, string name, string address, string email, string phone,
                      List<Patient> patients) : base(id, password, name, address, email, phone) // base keyword to call base class constructor
        {
            this.patients = patients;
        }
        public void MainMenu()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Doctor Menu");
                    Console.WriteLine("Welcome to Dotnet Hospital Management System {0}\n", Name);
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. List Doctor Details");
                    Console.WriteLine("2. List Patients");
                    Console.WriteLine("3. List Appointments");
                    Console.WriteLine("4. Check Particular Patient");
                    Console.WriteLine("5. List Appointments With Patient");
                    Console.WriteLine("6. Logout");
                    Console.WriteLine("7. Exit");

                    char choice = Console.ReadKey().KeyChar;
                    Console.Clear();

                    switch (choice)
                    {
                        case '1':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "My Details");
                            ListDoctorDetails();
                            break;
                        case '2':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "My Patients");
                            ListPatients();
                            break;
                        case '3':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "All Appointments");
                            ListAppointments();
                            break;
                        case '4':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Check Patient Details");
                            CheckParticularPatient();
                            break;
                        case '5':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Appointments with");
                            ListAppointmentsWithPatient();
                            break;
                        case '6':
                            return;
                        case '7':
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
        }

        private void ListDoctorDetails()
        {
            try
            {
                Console.WriteLine(this.ToString()); // use overridden ToString method to display informations
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

        private void ListPatients()
        {
            try
            {
                patients = RetrievalUtility.RetrieveRegisteredPatientByDoctorID(ID);
                // retrieve details of registered patients using the method with doctor's ID in utility class

                Console.WriteLine($"\nPatients assigned to {Name}\n");
                foreach (Patient patient in patients)
                {
                    Console.WriteLine(patient.ToString());
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid. Enter a valid ID.");
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
    

        private void ListAppointments()
        {
            try
            {
                Appointments = RetrievalUtility.RetrieveAppointmentsByDoctorID(ID);
                // retrieve details of appointments with registered patients using the method with doctor's ID in utility class

                foreach (Appointment appointment in Appointments)
                {
                    Console.WriteLine(appointment.ToString());
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


        private void CheckParticularPatient()
        {
            Console.WriteLine("\nEnter the ID of the patient to check:");
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());

                Patient patient = RetrievalUtility.RetrievePatientByID(id);
                // Retrieve details of the patient using patient's ID

                // If the patient is not null value, print the details
                if (patient != null)
                {
                    Console.WriteLine(patient.ToString());
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("No patient found with the corresponded ID.");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid. Enter a valid ID.");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Invalid. Enter a valid ID.");
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


        private void ListAppointmentsWithPatient()
        {
            Console.WriteLine("\nEnter Patient ID:");
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Patient patient = RetrievalUtility.RetrievePatientByID(id);
                // Retrieve details of the patient using patient's ID

                if (patient != null)
                // If the patient is not null value,
                {
                    List<Appointment> patientAppointments = RetrievalUtility.RetrieveAppointmentsByPatientID(id);
                    // use method to find appointments with registered doctor with corresponded patient's ID. Store it in the list
                    if (patientAppointments.Count > 0) // if there is/are appointment
                    {
                        Console.WriteLine("Appointments:");
                        foreach (Appointment appointment in patientAppointments)
                        {
                            Console.WriteLine(appointment.ToString()); // print it out using ToString
                        }
                        Console.WriteLine("Press any key to return to menu...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No appointments found with the given patient ID.");
                        Console.WriteLine("Press any key to return to menu...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("No patient found with the given ID.");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid. Enter a valid ID.");
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


        public override string ToString()
        {
            return $"Doctor ID: {ID}, Name: {Name}, Email Address: {Email}, Phone: {Phone}, Address: {Address}";
        }
    }

}
