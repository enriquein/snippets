using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Settings
{
    public class Settings
    {
        protected static string _fileName = "Settings.xml";

        /// <summary>
        /// Default constructor provided only for XML Serialization.
        /// For instantiation please use Load() instead.
        /// </summary>
        public Settings() {}

        public void Save()
        {
            string path = Path.Combine(Program.AppDataPath, _fileName);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                xs.Serialize(fs, this);
            }
        }

        public static Settings Load()
        {
            string path = Path.Combine(Program.AppDataPath, _fileName);
            Settings settingsToReturn;
            FileStream fs = null;

            try
            {
                fs = new FileStream(path, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                settingsToReturn = (Settings)xs.Deserialize(fs);
            }
            catch (FileNotFoundException)
            {
                settingsToReturn = new Settings();
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return settingsToReturn;
        }
    }
}
