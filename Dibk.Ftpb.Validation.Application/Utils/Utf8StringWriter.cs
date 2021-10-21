using System.IO;
using System.Text;

namespace Dibk.Ftpb.Validation.Application.Utils
{
    public sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
