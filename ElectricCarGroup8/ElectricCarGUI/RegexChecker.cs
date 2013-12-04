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

        public bool checkDate(string s)
        {
            Regex regex = new Regex(@"(\d{1,2})/(\d{1,2})/(\d{4})");
            return regex.IsMatch(s);
        }

        public bool checkTime(string s)
        {
            Regex regex = new Regex(@"^(?=\d)(?:(?!(?:(?:0?[5-9]|1[0-4])(?:\.|-|\/)10(?:\.|-|\/)(?:1582))|(?:(?:0?[3-9]|1[0-3])(?:\.|-|\/)0?9(?:\.|-|\/)(?:1752)))(31(?!(?:\.|-|\/)(?:0?[2469]|11))|30(?!(?:\.|-|\/)0?2)|(?:29(?:(?!(?:\.|-|\/)0?2(?:\.|-|\/))|(?=\D0?2\D(?:(?!000[04]|(?:(?:1[^0-6]|[2468][^048]|[3579][^26])00))(?:(?:(?:\d\d)(?:[02468][048]|[13579][26])(?!\x20BC))|(?:00(?:42|3[0369]|2[147]|1[258]|09)\x20BC))))))|2[0-8]|1\d|0?[1-9])([-.\/])(1[012]|(?:0?[1-9]))\2((?=(?:00(?:4[0-5]|[0-3]?\d)\x20BC)|(?:\d{4}(?:$|(?=\x20\d)\x20)))\d{4}(?:\x20BC)?)(?:$|(?=\x20\d)\x20))?((?:(?:0?[1-9]|1[012])(?::[0-5]\d){0,2}(?:\x20[aApP][mM]))|(?:[01]\d|2[0-3])(?::[0-5]\d){1,2})?$");
            return regex.IsMatch(s);
        }
    }
}
