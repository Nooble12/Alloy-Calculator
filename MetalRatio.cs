namespace TerraFirmaCraftCalc;

public class MetalRatio
{
    private readonly string _name;
    private readonly int _numberOfBars;
    private float _barPercent;
    private readonly int _totalBarVolume;

    public MetalRatio(string inName, int inNumberOfBars, float inBarPercent, int inTotalBarVolume)
    {
        _name = inName;
        _numberOfBars = inNumberOfBars;
        _barPercent = inBarPercent;
        _totalBarVolume = inTotalBarVolume;
    }

    public int GetTotalBarVolume()
    {
        return _totalBarVolume;
    }

    public void SetIngotPercentage(float inPercent)
    {
        _barPercent = inPercent;
    }

    public override string ToString()
    {
        return _name + ": " + "\n" + "Number of Bar: " + _numberOfBars + "\n" + "Percent: " + _barPercent + "%" + "\n" + "Volume: " + _totalBarVolume + "mb" + "\n";
    }
}