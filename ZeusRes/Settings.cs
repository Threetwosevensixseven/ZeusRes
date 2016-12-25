using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ZeusRes
{
    [XmlRoot("ZeusRes")]
    public class Settings
    {
        private static Settings instance = null;
        public string ZeusLocation { get; set; }

        public static Settings GetInstance()
        {
            if (instance == null)
            {
                instance = Settings.Load();
            }
            return instance;
        }

        public static Settings Load()
        {
            Settings settings;
            try
            {
                TextReader reader = new StreamReader(GetFileName());
                using (reader)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    settings = (Settings)serializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                settings = new Settings();
            }
            return settings;
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            TextWriter writer = new StreamWriter(GetFileName());
            using (writer)
            {
                serializer.Serialize(writer, this);
                writer.Close();
            }
        }

        private static string GetFileName()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Threetwosevensixseven", "ZeusRes");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return Path.Combine(path, "Settings.xml");
        }
    }
}
