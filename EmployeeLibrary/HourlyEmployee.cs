using System;

namespace EmployeeLibrary
{
    public class HourlyEmployee : Employee
    {
        //  Constructor
        public HourlyEmployee(string firstName,
                              string lastName,
                              int empNumber,
                              decimal hoursWorked,
                              decimal hourlyRate)
                        : base(firstName, lastName, empNumber)
        {
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
        }

        //  Getters and Setters
        decimal HoursWorked { get; set; }

        decimal HourlyRate { get; set; }

        public override string displayText()
        {
            return base.displayText()   +
                   "\r\nHours Worked: " + HoursWorked.ToString("n2") +
                   "\r\nHourly Rate:  " + HourlyRate.ToString("c");
        }
    }
}
