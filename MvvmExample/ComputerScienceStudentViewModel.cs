using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// notifies when a property changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The student that this class wraps
        /// </summary>
        private Student _student {get; init;}

        /// <summary>
        /// The student's fitstname
        /// </summary>
        public string FirstName => _student.FirstName;

        /// <summary>
        /// The Student's last name
        /// </summary>
        public string LastName => _student.LastName;

        /// <summary>
        /// The student's GPA
        /// </summary>
        public double GPA => _student.GPA;

        /// <summary>The student's computer science GPA</summary>
        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                  
                }
                return points / hours;
            }
        }
        /// <summary>
        /// The student's course records
        /// </summary>
        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        /// <summary>
        /// Event Handler for handling pass-forward events for the student
        /// </summary>
        /// <param name="sender">The sudetn that is changing</param>
        /// <param name="e">HandleSan event args describing the change</param>
        private void StudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }
        /// <summary>
        /// creates a new ComputerSicenceStudentViewModel
        /// </summary>
        /// <param name="student">The student wrapped in this view</param>
        public ComputerScienceStudentViewModel(Student student)
        {
            _student = student;
            student.PropertyChanged += StudentPropertyChanged;
        }
    }
}
