using System.IO;
using System.Xml.Serialization;

namespace TerraFirmaCraftCalc;

public class DataManager
{
    public DataManager() 
    { } 
    public void SerializeData(List<Metal> inMetals, string alloyName, int inMaxVolume) 
    {
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string saveFolder = Path.Combine(projectDirectory, "SaveFiles");
        
        if (!Directory.Exists(saveFolder))
        {
                Directory.CreateDirectory(saveFolder);
        }
        
            string filePath = Path.Combine(saveFolder, alloyName + ".xml");
        
            // Create an AlloyData object
            PresetData alloyData = new PresetData(alloyName, inMetals, inMaxVolume);
        
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PresetData));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, alloyData);
            }
            Console.WriteLine("Saved " + alloyName + " to " + saveFolder);
        }

        public PresetData DeserializeData(string inFilePath)
        {
            PresetData deserializedAlloyData;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PresetData));
            using (FileStream fs = new FileStream(inFilePath, FileMode.Open))
            {
                deserializedAlloyData = (PresetData)xmlSerializer.Deserialize(fs);
            }

            return deserializedAlloyData;
        }
}