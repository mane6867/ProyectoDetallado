

namespace Fire_Emblem.Conditions.ConditionsImps;

public class RivalGreaterOrEqualPercentageCondition: Condition
{
    private readonly StatType _targetStat;
    private readonly StatType _threshold;
    private readonly double _percentage;

    public RivalGreaterOrEqualPercentageCondition ( StatType targetStat , StatType threshold, double percentage ) {
        _targetStat = targetStat;
        _threshold = threshold;
        _percentage = percentage;
    }
    public override bool DoesHold(Character character, Character defender)
        => defender.Stats.GetStat(_targetStat) >= defender.Stats.GetStat(_threshold) * _percentage ;
}