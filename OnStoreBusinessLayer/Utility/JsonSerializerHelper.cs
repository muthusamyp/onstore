using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Collections.Concurrent;

namespace OnStoreBusinessLayer
{
    public class JsonSerializerHelper
    {
        public string Serialize(object dataObject)
        {
            string serializedData = string.Empty;
            if (dataObject == null) { return serializedData; }

            try
            {
                MemoryStream dataStream = new MemoryStream();
                StreamWriter streamWriter = new StreamWriter(dataStream);
                JsonSerializer serializer = new JsonSerializer();
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serializer.Serialize(streamWriter, dataObject);
                streamWriter.Flush();
                if (dataStream.CanSeek)
                    dataStream.Position = 0;
   
                StreamReader streamReader = new StreamReader(dataStream);
                serializedData = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                serializedData = string.Empty;
            }

            return serializedData;
        }

        public object Deserialize(string dataObject, Type objectType)
        {
            object deserializedObject = null;
            if (dataObject == null) { return deserializedObject; }

            try
            {
                MemoryStream dataStream = new MemoryStream();
                StreamWriter streamWriter = new StreamWriter(dataStream);
                streamWriter.Write(dataObject);
                streamWriter.Flush();
                if (dataStream.CanSeek)
                    dataStream.Position = 0;

                StreamReader streamReader = new StreamReader(dataStream);
                JsonSerializer serializer = new JsonSerializer();
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                deserializedObject = serializer.Deserialize(streamReader, objectType);
            }
            catch (Exception ex)
            {
                deserializedObject = null;
            }

            return deserializedObject;
        }
    }
}
