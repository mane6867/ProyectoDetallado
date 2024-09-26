namespace Fire_Emblem.Conditions.ConditionsImps;

public class AndCondition : Condition
{
    private readonly Condition _firstCond;
    private readonly Condition _secondCond;

    public AndCondition(Condition firstCond, Condition secondCond)
    {
        _firstCond = firstCond;
        _secondCond = secondCond;
    }

    public override bool DoesHold(Character character, Character defender)
        => _firstCond.DoesHold(character, defender) && _secondCond.DoesHold(character, defender);
}