using System;
using System.IO;

namespace DMSGalaxy.ViewModel.Common
{
    public class CommonUtils
    {

        public byte[] BufferFromImage(string path)
        {
            var uri = new Uri(path, UriKind.Relative);

            var streamInfo = System.Windows.Application.GetResourceStream(uri);
            Stream stream = streamInfo.Stream;
            Byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }
            return buffer;
        }

    }
}
