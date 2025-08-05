namespace SFA.DAS.AssessorService.ExternalApi.Client.Helpers
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public static class CsvFileHelper<T>
    {
        public static IEnumerable<TRecord> GetFromFile<TRecord>(string filePath, ClassMap<TRecord> map = null)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                using (TextReader textReader = new StreamReader(stream))
                {
                    var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        BadDataFound = null,
                        ReadingExceptionOccurred = null
                    };

                    using (var csv = new CsvReader(textReader, config))
                    {
                        if (map != null)
                        {
                            csv.Context.RegisterClassMap(map);
                        }

                        return csv.GetRecords<TRecord>().ToList();
                    }
                }
            }
            catch (SystemException)
            {
                return new List<TRecord>();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }

        public static void SaveToFile(string filePath, IEnumerable<T> records)
        {
            try
            {
                using (TextWriter textWriter = File.CreateText(filePath))
                {
                    var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);

                    using (var csv = new CsvWriter(textWriter, config))
                    {
                        csv.WriteHeader<T>();
                        csv.NextRecord();
                        csv.WriteRecords(records);
                    }
                }
            }
            catch (SystemException)
            {
                // ignore
            }
        }
    }
}
