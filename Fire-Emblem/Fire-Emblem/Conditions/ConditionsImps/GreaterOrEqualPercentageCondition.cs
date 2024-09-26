namespace Fire_Emblem.Conditions.ConditionsImps;

public class GreaterOrEqualPercentageCondition:Condition
{
    private readonly StatType _targetStat;
    private readonly StatType _threshold;
    private readonly double _percentage;

    public GreaterOrEqualPercentageCondition ( StatType targetStat , StatType threshold, double percentage ) {
        _targetStat = targetStat;
        _threshold = threshold;
        _percentage = percentage;
    }
    public override bool DoesHold(Character character, Character defender)
        => character.Stats.GetStat(_targetStat) >= character.Stats.GetStat(_threshold) * _percentage ;
}
