using System.Reflection.Metadata;
using System.Text;

namespace TerraFirmaCraftCalc;

public class CalculateMetalRatio
{
    private List<Metal> metalList = new List<Metal>();
    private List<MetalRatio> metalRatioList = new List<MetalRatio>();
    private int maxVolume;
    private int barVolume = 144; //milibuckets
    private int totalBarVolume;
    private bool checkFailed = false;
    
    public CalculateMetalRatio(List<Metal> inMetalList, int inMaxVolume)
    {
        metalList = inMetalList;
        maxVolume = inMaxVolume;
        CalculateRatios();
    }

    private float GetIngotPercentage(int indivBarVolume)
    {
        return (float)indivBarVolume / totalBarVolume * 100;
    }
    
    private void CalculateRatios()
    {
        totalBarVolume = 0;
        for (int i = 0; i < metalList.Count; i++)
        {
            float metalAmount = maxVolume * metalList[i].GetAverageMetalPercentage() / 100;
            
            int barAmount = (int)Math.Floor(metalAmount / barVolume);
            
            int volumeOfSingleBarType = barAmount * barVolume;
            
            totalBarVolume += volumeOfSingleBarType;
            
            metalRatioList.Add(new MetalRatio(metalList[i].GetMetalName(), barAmount, 0, volumeOfSingleBarType));
        }
        
        for (int i = 0; i < metalRatioList.Count; i++)
        {
            int volumeOfSingleBarType = metalRatioList[i].GetTotalBarVolume();
            float percentage = GetIngotPercentage(volumeOfSingleBarType);
            if (percentage >= metalList[i].GetMinMetalPercentage() && percentage <= metalList[i].GetMaxMetalPercentage())
            {
                metalRatioList[i].SetIngotPercentage(percentage);   
            }
            else
            {
                checkFailed = true;
                Console.WriteLine("Could not find combination of metal ratio");
            }
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        if (checkFailed == false)
        {
            for (int i = 0; i < metalRatioList.Count; i++)
            {
                builder.Append(metalRatioList[i].ToString() + "\n");
            }
            builder.Append("Total Volume: " + totalBarVolume + "mb");   
        }

        // Append failure message if checkFailed is true
        if (checkFailed)
        {
            builder.Append("\nWarning: Could not find combination of metal ratio.");
        }

        return builder.ToString();
    }
}