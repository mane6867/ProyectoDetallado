namespace Fire_Emblem;
using Fire_Emblem_View;

public class Utilities
{
    private View _view;
    
    public Utilities(View view)
        => _view = view;
    
    public int AskUserToSelectNumber(int minValue, int maxValue)
    {
        int value;
        bool wasParsePossible;
        do
        {
            string? userInput = _view.ReadLine();
            wasParsePossible = int.TryParse(userInput, out value);
        } while (!wasParsePossible || IsValueOutsideTheValidRange(minValue, value, maxValue));

        return value;
    }
    
    public bool IsValueOutsideTheValidRange(int minValue, int value, int maxValue)
        => value < minValue || value > maxValue;
    
    public bool HasDuplicates<T>(List<T> list)
    {
        return list.GroupBy(x => x).Any(g => g.Count() > 1);
    }
}