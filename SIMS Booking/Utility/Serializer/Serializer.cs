using System;
using System.Collections.Generic;
using System.IO;

namespace SIMS_Booking.Utility.Serializer
{
    public class Serializer<T> where T : ISerializable, new()
    {
        private const char Delimiter = '|';

        public void ToCSV(string fileName, List<T> objects)
        {
            if (!File.Exists(fileName))
            {
                File.CreateText(fileName);
            }
            StreamWriter streamWriter = new StreamWriter(fileName);

            foreach (T obj in objects)
            {
                string line = string.Join(Delimiter.ToString(), obj.ToCSV());
                streamWriter.WriteLine(line);
            }
            streamWriter.Close();
        }

        public List<T> FromCSV(string fileName)
        {
            List<T> objects = new List<T>();

            try
            {
                foreach (string line in File.ReadLines(fileName))
                {
                    string[] csvValues = line.Split(Delimiter);
                    T obj = new T();
                    obj.FromCSV(csvValues);
                    objects.Add(obj);
                }
                return objects;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                File.CreateText(fileName).Close();
                return objects;
            }
        }
    }
}
