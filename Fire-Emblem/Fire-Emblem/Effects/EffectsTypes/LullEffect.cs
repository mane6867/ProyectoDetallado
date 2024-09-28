namespace Fire_Emblem.Effects;

public class LullEffect:Effect
{
    private StatType _statTypePenalty1;
    private StatType _statTypePenalty2;
    
    public LullEffect (StatType statTypePenalty1, StatType statTypePenalty2): base(StatType.Atk, 0)
    {
        _statTypePenalty1 = statTypePenalty1;
        _statTypePenalty2 = statTypePenalty2;
        
    }

    private void PenalizedRival(Character attacker, StatType statType)
    {

        if (statType == StatType.Atk) attacker.BattleContext.ActualOpponent.StatsPenalties.Atk += 3;
        if (statType == StatType.Spd) attacker.BattleContext.ActualOpponent.StatsPenalties.Spd += 3;
        if (statType == StatType.Res) attacker.BattleContext.ActualOpponent.StatsPenalties.Res += 3;
        if (statType == StatType.Def) attacker.BattleContext.ActualOpponent.StatsPenalties.Def += 3;
    }
    
    
    public override void Apply(Character character, Character defender)
    {
        PenalizedRival(character, _statTypePenalty1);
        PenalizedRival(character, _statTypePenalty2);
    }

}