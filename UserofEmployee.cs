using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class UserofEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Access {  get; set; }

        public UserofEmployee() { }

        public UserofEmployee(int id, string name, string username, string access)
        {
            Id = id;
            Name = name;
            Username = username;
            Access = access;
        }
    }
}
