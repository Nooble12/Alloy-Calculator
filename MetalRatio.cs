namespace TerraFirmaCraftCalc;

public class MetalRatio
{
    private string name;
    private int numberOfBars;
    private float barPercent;
    private int totalBarVolume;

    public MetalRatio(string inName, int inNumberOfBars, float inBarPercent, int inTotalBarVolume)
    {
        name = inName;
        numberOfBars = inNumberOfBars;
        barPercent = inBarPercent;
        totalBarVolume = inTotalBarVolume;
    }

    public int GetTotalBarVolume()
    {
        return totalBarVolume;
    }

    public void SetIngotPercentage(float inPercent)
    {
        barPercent = inPercent;
    }

    public string ToString()
    {
        return name + ": " + "\n" + "Number of Bar: " + numberOfBars + "\n" + "Percent: " + barPercent + "%" + "\n" + "Volume: " + totalBarVolume + "mb" + "\n";
    }
}