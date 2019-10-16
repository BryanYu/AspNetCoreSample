using System;
using System.Collections;
using System.Collections.Generic;

namespace RazorPageWIthEFCore.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        
    }
}
