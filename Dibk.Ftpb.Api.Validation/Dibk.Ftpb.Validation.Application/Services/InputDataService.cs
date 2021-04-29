using Dibk.Ftpb.Validation.Application.Config;
using Dibk.Ftpb.Validation.Application.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class InputDataService : IInputDataService
    {
        private static readonly Regex _dataFormatRegex = new(@"^(?=.*dataFormatId=""(?<dataFormatId>\d+)"")(?=.*dataFormatVersion=""(?<dataFormatVersion>\d+)"").*$", RegexOptions.Compiled | RegexOptions.Multiline);

        public InputData GetInputData(string xmlString)
        {
            var config = GetInputDataConfig(xmlString);
            var xmlStream = StringToStream(xmlString);

            return new InputData(config, xmlStream);
        }

        public InputData GetInputData(IFormFile xmlFile)
        {
            using var fileStream = xmlFile.OpenReadStream();
            var memoryStream = new MemoryStream();
            xmlFile.OpenReadStream().CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var xmlString = StreamToString(memoryStream);
            var config = GetInputDataConfig(xmlString);
            
            return new InputData(config, memoryStream);
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
            using var memoryStream = new MemoryStream();
            xmlStream.CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(memoryStream);

            return reader.ReadToEnd();
        }

        private static MemoryStream StringToStream(string xmlString)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(xmlString));
        }
    }
}
