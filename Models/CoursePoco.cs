﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public class CoursePoco
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EnrollmentPoco> Enrollments { get; set; }
    }
}
