namespace Fire_Emblem.Conditions.ConditionsImps;

public class TrueCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
        => true;
}