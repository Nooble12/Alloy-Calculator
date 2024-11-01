using System.Xml.Serialization;
using TerraFirmaCraftCalc;

[XmlRoot("AlloyData")]
public class PresetData
{
    [XmlElement("AlloyName")]
    public string AlloyName { get; set; }
    
    [XmlElement("MaximumVolume")]
    public int MaximumVolume { get; set; }

    [XmlArray("Metals")]
    [XmlArrayItem("Metal")]
    public List<Metal> Metals { get; set; }

    public PresetData() 
    {
        Metals = new List<Metal>();
    }

    public PresetData(string alloyName, List<Metal> metals, int maxVolume)
    {
        AlloyName = alloyName;
        Metals = metals;
        MaximumVolume = maxVolume;
    }
}