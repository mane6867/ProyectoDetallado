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
        Console.WriteLine("se le suma en el efecto  3 puntos a stats Penaltys");
        if (statType == StatType.Atk) attacker.BattleContext.actualOpponent.StatsPenalties.Atk += 3;
        if (statType == StatType.Spd) attacker.BattleContext.actualOpponent.StatsPenalties.Spd += 3;
        if (statType == StatType.Res) attacker.BattleContext.actualOpponent.StatsPenalties.Res += 3;
        if (statType == StatType.Def) attacker.BattleContext.actualOpponent.StatsPenalties.Def += 3;
    }

    //private void NeutralizedRival(Character attacker, StatType statType)
    //{
    //    if (statType == StatType.Atk) attacker.BattleContext.actualOpponent.BonusNeutralized.Add(StatType.Atk);
    //    if (statType == StatType.Spd) attacker.BattleContext.actualOpponent.BonusNeutralized.Add(StatType.Spd);
    //    if (statType == StatType.Res) attacker.BattleContext.actualOpponent.BonusNeutralized.Add(StatType.Res);
    //    if (statType == StatType.Def) attacker.BattleContext.actualOpponent.BonusNeutralized.Add(StatType.Def);
    //}
    
    public override void Apply(Character character, Character defender)
    {
        PenalizedRival(character, _statTypePenalty1);
        PenalizedRival(character, _statTypePenalty2);
        //NeutralizedRival(character, _statTypeNeutralized1);
        //NeutralizedRival(character, _statTypeNeutralized2);
    }

}