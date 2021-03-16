using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tool
{
    public class XmlClass
    {
        private static string FILENAME = "ImageConfigurations.xml";

        public ImageList Data = new ImageList();

        public void Create()
        {
            using (var stream = new FileStream(FILENAME, FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(ImageList));
                XML.Serialize(stream, Data);
            }
        }

        public bool Exist()
        {
            if (File.Exists(FILENAME)) return true;
            else return false;
        }
    }

    [XmlRoot(ElementName = "ImageConfigurations")]
    public class ImageList
    {
        public List<ImageObject> ListImages = new List<ImageObject>();
    }

    public class ImageObject
    {
        public ImageObject() { }
        public ImageObject(int id, string name, ImageParameters parameters)
        {
            this.ID = id;
            this.Name = name;
            this.Parameters = parameters;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public ImageParameters Parameters { get; set; }
    }

    public class ImageParameters
    {
        public ImageParameters() { }
        public ImageParameters(ImageValues L1, ImageValues L2, ImageValues L3, ImageValues L4, ImageValues L5, ImageValues L6, ImageValues L7, ImageValues G1, ImageValues G2, ImageValues G3, ImageValues G4)
        {
            this.L1Value = L1;
            this.L2Value = L2;
            this.L3Value = L3;
            this.L4Value = L4;
            this.L5Value = L5;
            this.L6Value = L6;
            this.L7Value = L7;
            this.G1Value = G1;
            this.G2Value = G2;
            this.G3Value = G3;
            this.G4Value = G4;
        }

        public ImageValues L1Value { get; set; }
        public ImageValues L2Value { get; set; }
        public ImageValues L3Value { get; set; }
        public ImageValues L4Value { get; set; }
        public ImageValues L5Value { get; set; }
        public ImageValues L6Value { get; set; }
        public ImageValues L7Value { get; set; }
        public ImageValues G1Value { get; set; }
        public ImageValues G2Value { get; set; }
        public ImageValues G3Value { get; set; }
        public ImageValues G4Value { get; set; }
    }

    public class ImageValues
    {
        public ImageValues() { }
        public ImageValues(int value, bool used)
        {
            this.Value = value;
            this.Used = used;
        }

        public int Value { get; set; }
        public bool Used { get; set; }
    }
}
