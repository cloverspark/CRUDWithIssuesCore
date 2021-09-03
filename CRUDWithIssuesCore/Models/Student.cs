using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWithIssuesCore.Models
{
    public class Student
    {
        public Student()
        {

        }

        public int StudentId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
