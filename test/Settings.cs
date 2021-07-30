using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test
{
    public class Settings
    {
        public string lastRawfilePath { get => lastRawFilePath; }
        string[] settings;
        string lastRawFilePath;
        public Settings()
        {
            settings = File.ReadAllLines("settings.ini");

            
            
            lastRawFilePath = settings[1];


        }

        public void saveRawFileLastPath(string sPath)
        {
            if (Directory.Exists(sPath))
            {
                settings[1] = sPath;
                
                savefile();
            }
        }

        private void savefile()
        {
            if (File.Exists("settings.ini"))
            {
                File.Delete("settings.ini");
            }

            File.WriteAllLines("settings.ini", settings);
        }
    }
}
