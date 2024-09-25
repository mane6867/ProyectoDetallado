namespace Fire_Emblem.Conditions.ConditionsImps;

public class OrCondition : Condition
{
    private readonly Condition _firstCond;
    private readonly Condition _secondCond;

    public OrCondition(Condition firstCond, Condition secondCond)
    {
        _firstCond = firstCond;
        _secondCond = secondCond;
    }
    public override bool DoesHold(Character character)
        => _firstCond.DoesHold(character) || _secondCond.DoesHold(character);
}
