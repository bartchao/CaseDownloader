using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseProcessor.Model
{
    public class Persons
    {
        private List<KeyValuePair<string, List<string>>> persons = new List<KeyValuePair<string, List<string>>>();
        public string[] GetPersonType()
        {
            List<string> keys = new List<string>();
            foreach (var person in persons)
            {
                keys.Add(person.Key);
            }
            return keys.ToArray();
        }
        public List<KeyValuePair<string, List<string>>> GetPersonName(string type)
        {
            var result = persons.FindAll((item) => item.Key == type);
            return result;
        }
        public void AddPerson(string type, string name)
        {
            persons.Add(new KeyValuePair<string, List<string>>(type, new List<string> { name }));
        }
        public void AddPerson(string name)
        {
            try
            {
                persons.Last().Value.Add(name);
            }catch(Exception e)
            {
                throw new Exception("Persons List doesn't contain any type.");
            }
            
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var person in persons)
            {
                sb.Append(person.Key + ":");
                foreach (var value in person.Value)
                {
                    sb.Append(value + ",");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
