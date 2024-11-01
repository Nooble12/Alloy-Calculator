using System.Reflection.Metadata;
using System.Text;

namespace TerraFirmaCraftCalc;

public class CalculateMetalRatio
{
    private readonly List<Metal> _metalList;
    private readonly List<MetalRatio> _metalRatioList = new List<MetalRatio>();
    private readonly int _maxVolume;
    private readonly int _singleBarVolume = 144; //mili buckets
    private int _totalBarVolume;
    private bool _checkFailed;
    
    public CalculateMetalRatio(List<Metal> inMetalList, int inMaxVolume)
    {
        _metalList = inMetalList;
        _maxVolume = inMaxVolume;
        CalculateRatios();
    }

    private float GetIngotPercentage(int indivBarVolume)
    {
        return (float)indivBarVolume / _totalBarVolume * 100;
    }
    
    private void CalculateRatios()
    {
        _totalBarVolume = 0;
        for (int i = 0; i < _metalList.Count; i++)
        {
            float metalAmount = _maxVolume * _metalList[i].AveragePercent / 100;
            
            int barAmount = (int)Math.Floor(metalAmount / _singleBarVolume);
            
            int volumeOfSingleBarType = barAmount * _singleBarVolume;
            
            _totalBarVolume += volumeOfSingleBarType;
            
            _metalRatioList.Add(new MetalRatio(_metalList[i].Name, barAmount, 0, volumeOfSingleBarType));
        }
        
        for (int i = 0; i < _metalRatioList.Count; i++)
        {
            int volumeOfSingleBarType = _metalRatioList[i].GetTotalBarVolume();
            float percentage = GetIngotPercentage(volumeOfSingleBarType);
            if (percentage >= _metalList[i].MinPercentage && percentage <= _metalList[i].MaxPercentage)
            {
                _metalRatioList[i].SetIngotPercentage(percentage);   
            }
            else
            {
                _checkFailed = true;
                Console.WriteLine("Could not find combination of metal ratio");
            }
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        if (_checkFailed == false)
        {
            for (int i = 0; i < _metalRatioList.Count; i++)
            {
                builder.Append(_metalRatioList[i].ToString() + "\n");
            }
            builder.Append("Total Volume: " + _totalBarVolume + "mb");   
        }

        // Append failure message if checkFailed is true
        if (_checkFailed)
        {
            builder.Append("\nWarning: Could not find combination of metal ratio.");
        }

        return builder.ToString();
    }
}