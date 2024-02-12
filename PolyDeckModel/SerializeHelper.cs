using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PolyDeckModel
{
    public static class SerializeHelper
    {
        public static string DEVICES_STORAGE_DIRECTORY = "../Devices";
        public static string DEVICES_STORAGE_FILE = DEVICES_STORAGE_DIRECTORY + "/devices.json";

        public static string IMAGE_DIRECTORY = DEVICES_STORAGE_DIRECTORY + "/images";
        public static string DEFAULT_IMAGE = IMAGE_DIRECTORY + "/default_image.png";

        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
    }
}
