using System;
using System.Collections.Generic;
using System.IO;

namespace SingleResponsibilityPattern
{
    class Program
    {
        public class Journal
        {
            private readonly List<string> entries = new();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count;
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            #region Problem Area

            // ============================================================
            /* Adding methods like these starts violating the 
             * Single Responsibility Principle. We are adding to much
             * responsibility to the Journal class. This class is now manages
             * the entries of a journal and the persistence of it. The persistence
             * should be a separate class.
             */

            public void Save(string filename)
            {
                File.WriteAllText(filename, ToString());
            }

            public static Journal Load(string filename)
            {
                return null;
            }

            public static Journal Load(Uri uri)
            {
                return null;
            }

            // ============================================================

            #endregion Problem Area
        }

        public class JournalPersistence
        {
            public static void Save(string filename, Journal journal)
            {
                File.WriteAllText(filename, journal.ToString());
            }

            public static Journal Load(string filename)
            {
                return null;
            }

            public static Journal Load(Uri uri)
            {
                return null;
            }
        }

        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("I woke up.");
            journal.AddEntry("I drank coffee.");
            Console.WriteLine(journal);
        }
    }
}
