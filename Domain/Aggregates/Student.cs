using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    [Context("DefaultMSSQLContext")]
    public class Student
    {
        public Student()
        {
            Course = new List<Course>();
            SubjectTest = new List<SubjectTest>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string UUID { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Gender { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public List<Course> Course { get; set; }
        public List<SubjectTest> SubjectTest { get; set; }
    }
    public class Course
    {
        public Course()
        {
            Teacher = new List<Teacher>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseName { get; set; }
        //public string CreditHours { get; set; }
        public List<Teacher> Teacher { get; set; }
    }
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public long? CourseId { get; set; }
        public string Name { get; set; }
    }
    public class DatabaseAttribute : Attribute
    {
        public string DatabaseName = "";
        public DatabaseAttribute(string _databaseName)
        {
            DatabaseName = _databaseName;
        }
    }
}
