using System.Xml.Serialization;

namespace TerraFirmaCraftCalc;

public class Metal
{
    [XmlElement("AveragePercent")]
    public float AveragePercent { get; set; }

    [XmlElement("MinPercentage")]
    public float MinPercentage { get; set; }

    [XmlElement("MaxPercentage")]
    public float MaxPercentage { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    public Metal(float inAveragePercent, string inMetalName, float inMinPercent, float inMaxPercent)
    {
        AveragePercent = inAveragePercent;
        MinPercentage = inMinPercent;
        MaxPercentage = inMaxPercent;
        Name = inMetalName;
    }

    // Parameterless constructor for serialization
    public Metal() 
    {
    }

    public override string ToString()
    {
        return $"Metal: {Name}, Percent: {AveragePercent}";
    }
}