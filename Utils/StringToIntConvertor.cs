namespace Utils;

public class StringToIntConvertor
{
    public static List<int> ConvertStringToInt(List<string> stringList)
    {

        List<int> intList = new List<int>();

        foreach(var s in stringList)
        {
            if(!int.TryParse(s, out int id))
            {
                continue;
            }
            else
            {
                intList.Add(id);
            }
        }

        return intList;
    }
}