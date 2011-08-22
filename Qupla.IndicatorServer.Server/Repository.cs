using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Newtonsoft.Json;

namespace Qupla.IndicatorServer.Server
{
    public abstract class Repository<T>
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly ILog _log;

        protected Repository (string fileName, ILog log)
        {
            _fileName = fileName;
            _filePath = Path.Combine(Environment.CurrentDirectory, _fileName);
            _log = log;
        }

        protected abstract string Sample { get; }

        public IEnumerable<T> LoadAll()
        {
            _log.DebugFormat("Loading all repository data from {0}", _filePath);
            if (!File.Exists(_fileName))
            {
                return new T[]{};
            }
            try
            {
                using (var streamReader = new StreamReader(_fileName))
                {
                    var jsonTextReader = new JsonTextReader(streamReader);
                    var result = MakeSerializer().Deserialize<IEnumerable<T>>(jsonTextReader);
                    if (result == null)
                        return new T[] { };
                    return result;
                }
            } catch(Exception e)
            {
                _log.Error(string.Format("Failed to load all data from {0}", _filePath), e);
                throw;
            }
        }

        private static JsonSerializer MakeSerializer()
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, };
            return JsonSerializer.Create(settings);
        }

        public void SaveAll(IEnumerable<T> objects)
        {
            _log.DebugFormat("Saving all repository data to {0}", _filePath);
            try
            {
                using (var streamWriter = new StreamWriter(_fileName))
                {
                    var jsonTextWriter = new JsonTextWriter(streamWriter) { Formatting = Formatting.Indented };
                    jsonTextWriter.WriteComment(Sample);
                    MakeSerializer().Serialize(jsonTextWriter, objects);
                }
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Failed to save all data to {0}", _filePath), e);
                throw;
            }
        }

    }
}