namespace Fire_Emblem.Conditions.ConditionsImps;

public class NotCond: Condition
{
    private readonly Condition _condition;

    public NotCond(Condition condition)
        => _condition = condition;

    public override bool DoesHold(Character character, Character defender)
        => !_condition.DoesHold(character, defender);
}