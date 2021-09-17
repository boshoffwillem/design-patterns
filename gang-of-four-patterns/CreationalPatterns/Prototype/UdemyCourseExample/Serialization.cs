using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace UdemyCourseExample
{
    public static class SerializationExtensionMethods
    {
        /// <summary>
        /// Any class that you want to serialize using this method
        /// must have a parameterless constructor.
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SerializationDeepCopyXml<T>(this T self)
        {
            using var stream = new MemoryStream();
            var s = new XmlSerializer(typeof(T));
            s.Serialize(stream, self);
            stream.Position = 0;
            return (T)s.Deserialize(stream);
        }
    }

    [Serializable]
     public class Person4
    {
        public string[] Names { get; set; }

        public Address4 Address { get; set; }

        public Person4()
        {

        }

        public Person4(string[] names, Address4 address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    [Serializable]
    public class Address4
    {
        public string StreetName { get; set; }

        public int HouseNumber { get; set; }

        public Address4()
        {

        }

        public Address4(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }
}