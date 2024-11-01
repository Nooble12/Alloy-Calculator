using System.Collections;

namespace TerraFirmaCraftCalc;

public class Metal
{
    private float averagePercent;
    private float minPercentage;
    private float maxPercentage;
    private string name;

    public Metal(float inAveragePercent, string inMetalName, float inMinPercent, float inMaxPercent)
    {
        averagePercent = inAveragePercent;
        minPercentage = inMinPercent;
        maxPercentage = inMaxPercent;
        name = inMetalName;
    }

    public string GetMetalName()
    {
        return name;
    }

    public float GetAverageMetalPercentage()
    {
        return averagePercent;
    }
    
    public float GetMinMetalPercentage()
    {
        return minPercentage;
    }
    
    public float GetMaxMetalPercentage()
    {
        return maxPercentage;
    }

    public string ToString()
    {
        return "Metal: " + name + ", " + "Percent: " + averagePercent;
    }
}