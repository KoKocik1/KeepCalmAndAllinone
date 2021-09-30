using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YamlDotNet.Serialization;

namespace PartyMaker
{
    public class YamlRead
    {
        /// <summary>
        /// Odczytanie konfiguracji z pliku .yaml do obiektu
        /// </summary>
        public static ConfigPostprocessing ReadConfigFile()
        {
            ConfigPostprocessing config = new ConfigPostprocessing();
            var enviroment = System.Environment.CurrentDirectory;
            string configFilePath = Directory.GetParent(enviroment).Parent.FullName + @"\config.yaml";
            try
            {
                string configpath = Environment.CurrentDirectory;
                var fileData = System.IO.File.ReadAllText(configFilePath);
                var deserializer = new Deserializer();
                config = deserializer.Deserialize<ConfigPostprocessing>(fileData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("UWAGA!!! BŁĄD ZCZYTANIA PLIKU KONFIGURACYJNEGO- LOKALIZACJA: " + configFilePath + "||" + ex);
            }
            return config;
        }
    }
    public class ConfigPostprocessing
    {
        public string PathDoZapisu { get; set; } = "";
        public int PoprawkaDVLsecond { get; set; } = 0;
        public int PoprawkaDVLmiliseconds { get; set; } = 0;
        public double KursSatPoprawka { get; set; } = 0;
        public double KursAhrsPoprawka { get; set; } = 0;
        public string ZrodloKursu { get; set; } = "";
        public bool LiczLocalTime { get; set; } = false;
        public List<PrzedzialCzasowy> PrzedzialyCzasowe { get; set; }
        public List<initbark> Initbarks { get; set; }

    }
    public class PrzedzialCzasowy
    {
        public DateTime Poczatek { get; set; }
        public DateTime Koniec { get; set; }
    }
}
