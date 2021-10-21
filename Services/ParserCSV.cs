using CSVProssesing.Models.Person;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSVProssesing.Services
{
    public class ParserCSV
    {
        public static List<Person> CSVParseToModel(string filePath, string delimiter = ",")
        {
            List<Person> people = new List<Person>();
            string[] csvLines;

            try
            {
                csvLines = File.ReadAllLines(filePath);

                string[] personFields;
                for (int i = 0; i < csvLines.Length; i++)
                {
                    personFields = csvLines[i].Split(delimiter);
                    people.Add(new Person
                    {
                        Name = personFields[0],
                        DateOfBirth = Convert.ToDateTime(personFields[1]),
                        Married = Convert.ToBoolean(personFields[2]),
                        Phone = personFields[3],
                        Salary = Convert.ToDecimal(personFields[4])
                    });
                }
            }
            catch (Exception ex)
            {
                // Pass ex for 
                // warning the user about the impossibility of reading the document due to an incorrect format
            }
            return people;
        }
    }
}
