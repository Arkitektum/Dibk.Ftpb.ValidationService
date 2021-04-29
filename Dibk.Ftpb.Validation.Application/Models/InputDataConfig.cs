using Dibk.Ftpb.Validation.Application.Constants;
using System;

namespace Dibk.Ftpb.Validation.Application.Models
{
    public class InputDataConfig
    {
        public DataType DataType { get; private set; }
        public string DataFormatId { get; private set; }
        public string DataFormatVersion { get; private set; }
        public Type DeserializedType { get; private set; } 

        public InputDataConfig(DataType dataType, string dataFormatId, string dataFormatVersion, Type deserializedType)
        {
            DataType = dataType;
            DataFormatId = dataFormatId;
            DataFormatVersion = dataFormatVersion;
            DeserializedType = deserializedType;
        }
    }
}
