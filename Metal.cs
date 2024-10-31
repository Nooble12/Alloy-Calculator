using System.Collections;

namespace TerraFirmaCraftCalc;

public class Metal
{
    private float percentage;
    private string name;

    public Metal(float inPercentage, string inMetalName)
    {
        percentage = inPercentage;
        name = inMetalName;
    }

    public string GetMetalName()
    {
        return name;
    }

    public float GetMetalPercentage()
    {
        return percentage;
    }

    public string ToString()
    {
        return "Metal: " + name + ", " + "Percent: " + percentage;
    }
}