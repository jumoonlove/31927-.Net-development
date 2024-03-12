namespace DotnetHospitalManagementSystem
{
    internal class Appointment
    {
        public int ID { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public string Description { get; set; }

        // Constructor
        public Appointment(int id, Doctor doctor, Patient patient, string description)
        {
            this.ID = id;
            this.Doctor = doctor;
            this.Patient = patient;
            this.Description = description;
        }

        // ToString to display appointment details
        public override string ToString()
        {
            return $"Appointment ID: {ID}, Doctor: {Doctor.Name}, Patient: {Patient.Name}, Description: {Description}";
        }
    }

}
