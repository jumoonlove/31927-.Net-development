using System.Collections.Generic;

namespace DotnetHospitalManagementSystem
{
    internal class Identity // Base Class for doctor and patient classes
    {
        // set modifier to be public, so that children classes can access to these
        public int ID { get; set; }
        public int Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Identity(int id, int password, string name, string address, string email, string phone)
        {
            this.ID = id;
            this.Password = password;
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Phone = phone;
            Appointments = new List<Appointment>();
        }
    }
}