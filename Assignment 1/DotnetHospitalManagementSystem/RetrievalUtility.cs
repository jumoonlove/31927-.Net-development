using System;
using System.Collections.Generic;
using System.IO;

namespace DotnetHospitalManagementSystem
{
    internal class RetrievalUtility
    {
        public static Doctor RetrieveDoctorByID(int id) // retrieve doctor data by corresponded id
        {
            try
            {
                string doctorData = FileManager.ReadAllText($"Doctor_{id}.txt");

                if (doctorData == null) // Check for null
                {
                    return null;
                }

                string[] parts = doctorData.Split(',');
                return new Doctor(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), parts[2], parts[3], parts[4], parts[5], new List<Patient>());
            }
            catch (Exception e) 
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public static Patient RetrievePatientByID(int id) // retrieve patient data by corresponded id
        {
            try
            {
                string patientData = FileManager.ReadAllText($"Patient_{id}.txt");

                if (patientData == null) // check for null
                {
                    return null;
                }

                string[] parts = patientData.Split(',');
                return new Patient(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), parts[2], parts[3], parts[4], parts[5]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public static List<Appointment> RetrieveAppointmentsByPatientID(int patientID)
        {
            try
            {
                List<Appointment> appointments = new List<Appointment>();
                foreach (string file in Directory.GetFiles(".", $"Appointment_*.txt"))

                /* use directory.GetFiles method to Iterate through all text files
                 https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=net-7.0 */

                {
                    string appointmentData = File.ReadAllText(file);
                    string[] parts = appointmentData.Split(',');
                    if (Convert.ToInt32(parts[2]) == patientID) // if patient ID is the same as the patient id in appointment text file, return add content into the list
                    {
                        appointments.Add(new Appointment(Convert.ToInt32(parts[0]), RetrieveDoctorByID(Convert.ToInt32(parts[1])), RetrievePatientByID(Convert.ToInt32(parts[2])), parts[3]));
                    }
                }
                return appointments;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
        public static List<Appointment> RetrieveAppointmentsByDoctorID(int doctorID)
        {
            try
            {
                List<Appointment> appointments = new List<Appointment>();
                foreach (string file in Directory.GetFiles(".", $"Appointment_*.txt")) // Iterate through all appointment text files
                {
                    string appointmentData = File.ReadAllText(file); // store it
                    string[] parts = appointmentData.Split(',');
                    if (Convert.ToInt32(parts[1]) == doctorID) // if doctor ID is the same as the doctor id in appointment text file, return add content into the list
                    {
                        appointments.Add(new Appointment(Convert.ToInt32(parts[0]), RetrieveDoctorByID(Convert.ToInt32(parts[1])), RetrievePatientByID(Convert.ToInt32(parts[2])), parts[3]));
                    }
                }
                return appointments;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public static Doctor RetrieveRegisteredDoctorByPatientID(int patientID)
        {
            try
            {
                foreach (string file in Directory.GetFiles(".", $"Appointment_*.txt")) // Iterate through all appointment text files
                {
                    string appointmentData = File.ReadAllText(file);
                    string[] parts = appointmentData.Split(',');
                    if (Convert.ToInt32(parts[2]) == patientID) // if patient ID is the same as the doctor id in appointment text file,
                    {
                        int doctorID = Convert.ToInt32(parts[1]); // return corresponded doctor id
                        return RetrieveDoctorByID(doctorID); // then retrieve registered doctor with instance of patient
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public static List<Patient> RetrieveRegisteredPatientByDoctorID(int doctorID)
        {
            try
            {
                List<Patient> registeredPatients = new List<Patient>();

                // Iterate through all appointment text files
                foreach (string file in Directory.GetFiles(".", $"Appointment_*.txt"))
                {
                    string appointmentData = File.ReadAllText(file);
                    string[] parts = appointmentData.Split(',');
                    if (Convert.ToInt32(parts[1]) == doctorID) // if doctor ID is the same as the patient id in text file,
                    {
                        int patientID = Convert.ToInt32(parts[2]); // return corresponded pateint id
                        Patient patient = RetrievePatientByID(patientID); // retrieve registered patient, then store it into patient variable

                        if (patient != null)
                        {
                            bool patientExists = false;

                            // Check if this patient already exists in the list
                            foreach (Patient existingPatient in registeredPatients)
                            {
                                if (existingPatient.ID == patient.ID)
                                {
                                    patientExists = true;
                                    break;
                                }
                            }

                            if (!patientExists) // if it doesn't exist, add them into registeredPatients list
                            {
                                registeredPatients.Add(patient);
                            }
                        }
                    }
                }
                return registeredPatients;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }


        public static List<Doctor> RetrieveAllDoctors()
        {
            try
            {
                List<Doctor> doctors = new List<Doctor>();
                foreach (string file in Directory.GetFiles(".", "Doctor_*.txt")) // iterate through all doctor text files
                {
                    int id = Convert.ToInt32(file.Split('_')[1].Replace(".txt", "")); // procedure to get id from text file name
                    doctors.Add(RetrieveDoctorByID(id)); // pass the IDs to retrieve all doctor details, and store them in doctors list
                }
                return doctors;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        public static List<Patient> RetrieveAllPatients()
        {
            try
            {
                List<Patient> patients = new List<Patient>();
                foreach (string file in Directory.GetFiles(".", "Patient_*.txt"))
                {
                    int id = Convert.ToInt32(file.Split('_')[1].Replace(".txt", ""));
                    patients.Add(RetrievePatientByID(id));
                }
                return patients;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
    }
}
