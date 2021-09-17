using System;
using System.Collections.Generic;
using System.IO;

namespace SingletonPattern
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    #region Singleton Database Implementation

    public class SingletonExample : IDatabase
    {
        private static readonly Lazy<SingletonExample> _instance = 
            new(() => new SingletonExample());
        private readonly Dictionary<string, int> _capitals;
        private static int _instanceCount = 0;

        public static int InstanceCount => _instanceCount;

        private SingletonExample()
        {
            _instanceCount++;
            Console.WriteLine("Initializing database");
            _capitals = new();
            string[] capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"
                )
            );

            foreach (var capital in capitals)
            {
                string[] capitalMap = capital.Split(',');
                _capitals.Add(capitalMap[0], int.Parse(capitalMap[1]));
            }
        }

        public static SingletonExample Instance => _instance.Value;

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;

            foreach (string name in names)
            {
                result += SingletonExample.Instance.GetPopulation(name);
            }

            return result;
        }
    }

    #endregion

    #region Normal Database Implementation

    public class NormalDatabase : IDatabase
    {
        private readonly Dictionary<string, int> _capitals;

        private NormalDatabase()
        {
            Console.WriteLine("Initializing database");
            _capitals = new();
            string[] capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"
                )
            );

            foreach (var capital in capitals)
            {
                string[] capitalMap = capital.Split(',');
                _capitals.Add(capitalMap[0], int.Parse(capitalMap[1]));
            }
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    public class DependencyInjectionSingeltonRecordFinder
    {
        private readonly IDatabase _database;

        public DependencyInjectionSingeltonRecordFinder(IDatabase database)
        {
            _database = database;
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;

            foreach (string name in names)
            {
                result += _database.GetPopulation(name);
            }

            return result;
        }
    }

    #endregion

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["city1"] = 5,
                ["city2"] = 7,
                ["city3"] = 6
            }[name];
        }
    }
}
