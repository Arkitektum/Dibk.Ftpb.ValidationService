using System;
using System.IO;

namespace Dibk.Ftpb.Validation.Application.Models
{
    public class InputData : IDisposable
    {
        public InputDataConfig Config { get; private set; }
        public Stream Data { get; private set; }
        public bool IsValid { get; set; }

        public InputData(InputDataConfig config, Stream data)
        {
            Config = config;
            Data = data;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || Data == null)
                return;

            Data.Dispose();
        }
    }
}
