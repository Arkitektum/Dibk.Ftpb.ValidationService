using Dibk.Ftpb.Validation.Application.Config;
using Dibk.Ftpb.Validation.Application.Models;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class InputDataService : IInputDataService
    {
        private static readonly Regex _dataFormatRegex = new(@"^(?=.*dataFormatId=""(?<dataFormatId>\d+)"")(?=.*dataFormatVersion=""(?<dataFormatVersion>\d+)"").*$", RegexOptions.Compiled);

        public InputData GetInputData(string xmlString)
        {
            var config = GetInputDataConfig(xmlString);
            var xmlStream = StringToStream(xmlString);

            return new InputData(config, xmlStream);
        }

        public InputData GetInputData(Stream xmlStream)
        {
            var xmlString = StreamToString(xmlStream);
            xmlStream.Seek(0, SeekOrigin.Begin);

            var config = GetInputDataConfig(xmlString);

            return new InputData(config, xmlStream);
        }

        private static InputDataConfig GetInputDataConfig(string xmlString)
        {
            var match = _dataFormatRegex.Match(xmlString);

            if (!match.Success)
                return null;

            var dataFormatId = match.Groups["dataFormatId"].Value;
            var dataFormatVersion = match.Groups["dataFormatVersion"].Value;

            return InputDataSetup.Config
                .SingleOrDefault(inputData => inputData.DataFormatId == dataFormatId && inputData.DataFormatVersion == dataFormatVersion);
        }

        private static string StreamToString(Stream xmlStream)
        {
            using var reader = new StreamReader(xmlStream);

            return reader.ReadToEnd();
        }

        private static Stream StringToStream(string xmlString)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(xmlString));
        }
    }
}
