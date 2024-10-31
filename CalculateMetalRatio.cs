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

    private int GetNumberOfIngot(float metalAmount)
    {
        int barAmount = (int)Math.Floor(metalAmount / barVolume);
        return barAmount;
    }
    private void CalculateRatios()
    {
        totalBarVolume = 0;
        for (int i = 0; i < metalList.Count; i++)
        {
            float metalAmount = maxVolume * metalList[i].GetMetalPercentage() / 100;
            
            int barAmount = (int)Math.Floor(metalAmount / barVolume);
            
            int volumeOfSingleBarType = barAmount * barVolume;
            
            totalBarVolume += volumeOfSingleBarType;
            
            metalRatioList.Add(new MetalRatio(metalList[i].GetMetalName(), barAmount, 0, volumeOfSingleBarType));
        }
        
        for (int i = 0; i < metalRatioList.Count; i++)
        {
            int volumeOfSingleBarType = metalRatioList[i].GetTotalBarVolume();
            float percentage = GetIngotPercentage(volumeOfSingleBarType);
            metalRatioList[i].SetIngotPercentage(percentage);
        }
    }

    public string ToString()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < metalRatioList.Count; i++)
        {
            builder.Append(metalRatioList[i].ToString() + "\n" + "");
        }
        return builder.ToString() + "\n" + "Total Volume: " + totalBarVolume + "mb";
    }
}