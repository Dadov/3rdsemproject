using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectricCarGUI
{
    public class RegexChecker
    {
        public bool checkNumber(String s)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(s);
        }

        public bool checkUserName(String s)
        {
            Regex regex = new Regex(@"/^[a-zA-Z0-9_-]{3,16}$/"); //between 3-16 characters, using underscores and hyphens
            return regex.IsMatch(s);
        }

        public bool checkPassword(String s)
        {
            Regex regex = new Regex(@"((?=.*\)(?=.*[a-z]).{6,20})"); //must contain 1 number and 1 letter and be between 6-20 characters
            return regex.IsMatch(s);
        }

        public bool checkEmail(String s)
        {
            Regex regex = new Regex(@"/^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/");
            return regex.IsMatch(s);
        }

        public bool checkDecimal(String s)
        {
            Regex regex = new Regex(@"^\d+[\.,]?\d*$");
            return regex.IsMatch(s);
        }
    }
}
