namespace Fire_Emblem.Conditions.ConditionsImps;

public class GreaterOrEqualNumberCondition:Condition
{
    private readonly StatType _targetStat;
    private readonly StatType _threshold;
    private readonly int _constant;

    public GreaterOrEqualNumberCondition ( StatType targetStat , StatType threshold, int constant ) {
        _targetStat = targetStat;
        _threshold = threshold;
        _constant = constant;
    }
    public override bool DoesHold(Character character, Character defender)
        => character.Stats.GetStat(_targetStat) >= character.BattleContext.ActualOpponent.Stats.GetStat(_threshold) 
            + _constant ;
}