using System;
using System.Collections.Generic;

namespace DotnetHospitalManagementSystem
{
    internal class Patient : Identity, IMainMenu
    {
        public Doctor RegisteredDoctor; // Patients will have one registered doctor once they book appointment
        public Patient(int id, int password, string name, string address, string email, string phone)
            : base(id, password, name, address, email, phone) // call base class constructor
        {

        }

        public void MainMenu()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Patient Menu");
                    Console.WriteLine("Welcome to Dotnet Hospital Management System {0}\n", Name);
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. List Patient Details");
                    Console.WriteLine("2. List My Doctor Details");
                    Console.WriteLine("3. List All Appointments");
                    Console.WriteLine("4. Book Appointment");
                    Console.WriteLine("5. Exit to login");
                    Console.WriteLine("6. Exit system");

                    char option = Console.ReadKey().KeyChar;
                    Console.Clear();

                    switch (option)
                    {
                        case '1':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "My Details");
                            ListPatientDetails();
                            break;
                        case '2':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "My Doctor");
                            ListDoctorDetails();
                            break;
                        case '3':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "My appointments");
                            ListAllAppointments();
                            break;
                        case '4':
                            GeneralUtility.DisplayMenuTable("Dotnet Hospital Management System", "Book appointments");
                            BookAppointment();
                            break;
                        case '5':
                            return;
                        case '6':
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
            }
        }

        private void ListPatientDetails()
        {
            try
            {
                Console.WriteLine($"\n{Name}'s Details:\n");
                Console.WriteLine($"Patient ID: {ID}");
                Console.WriteLine($"Full Name: {Name}");
                Console.WriteLine($"Address: {Address}");
                Console.WriteLine($"Email: {Email}");
                Console.WriteLine($"Phone: {Phone}");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        private void ListDoctorDetails()
        {
            try
            {
                // get the registered doctor for this patient
                RegisteredDoctor = RetrievalUtility.RetrieveRegisteredDoctorByPatientID(this.ID);

                if (RegisteredDoctor != null)
                {
                    // If a doctor is found, print the doctor's details
                    Console.WriteLine("Doctor Details:");
                    Console.WriteLine(RegisteredDoctor.ToString());
                }
                else
                {
                    // If no doctor is found, print that the patient is not registered with any doctor
                    Console.WriteLine("You are not registered with any doctor.");
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }


        private void ListAllAppointments()
        {
            try
            {
                // get all the appointments made by logined patient
                Appointments = RetrievalUtility.RetrieveAppointmentsByPatientID(ID);

                Console.WriteLine("Appointments:");
                foreach (Appointment appointment in Appointments) // look through all appointments text file to find the ones made with the patient's id
                {
                    Console.WriteLine(appointment.ToString());
                }
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }


        private void BookAppointment()
        {
            try
            {
                // Check for an already registered doctor
                RegisteredDoctor = RetrievalUtility.RetrieveRegisteredDoctorByPatientID(ID);

                // If no doctor is registered, then proceed with registration with doctors
                if (RegisteredDoctor == null)
                {
                    Console.WriteLine("You are not registered with any doctor. Please register first.");

                    // Display list of doctors and make the patient choose one.
                    List<Doctor> allDoctors = RetrievalUtility.RetrieveAllDoctors();
                    int i = 1;
                    foreach (Doctor doctor in allDoctors)
                    {
                        Console.WriteLine($"{i}. {doctor.ToString()}");
                        i++;
                    }

                    Console.WriteLine("Choose a doctor by entering the corresponding number:");
                    try
                    {
                        int choice = Convert.ToInt32(Console.ReadLine());

                        if (choice > 0 && choice <= allDoctors.Count)
                        {
                            RegisteredDoctor = allDoctors[choice - 1]; // store chosen doctor in RegisteredDoctor variable
                            Console.WriteLine($"You have successfully registered with Dr. {RegisteredDoctor.Name}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadKey();
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An error occurred: {e.Message}");
                        Console.ReadKey();
                        return;
                    }
                }

                // Continue with booking the appointment
                Console.WriteLine($"You are booking a new appointment with Dr. {RegisteredDoctor.Name}");
                Console.WriteLine("Enter a description for the appointment:");
                string description = Console.ReadLine();

                Appointment newAppointment = new Appointment(ValidationUtility.GenerateID(), RegisteredDoctor, this, description); // use this keyword to pass the current instance of patient
                Appointments.Add(newAppointment); // generating new appointment and store it into list

                // Save this appointment to its own appointment text file
                string appointmentData = $"{newAppointment.ID},{newAppointment.Doctor.ID},{newAppointment.Patient.ID},{newAppointment.Description}";
                FileManager.WriteAllText($"Appointment_{newAppointment.ID}.txt", appointmentData);

                Console.WriteLine("Appointment booked successfully!");

                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format. Please enter valid data.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
        public override string ToString()
        {
            return $"Patient ID: {ID}, Name: {Name}, Email Address: {Email}, Phone: {Phone}, Address: {Address}";
        }
    }

}
