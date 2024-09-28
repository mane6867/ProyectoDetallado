namespace Fire_Emblem.Effects;

public class NeutralizedBonusEffect: Effect
{
    public NeutralizedBonusEffect(StatType statType) : base(statType, 0)
    {
        EffectType = Fire_Emblem.EffectType.Bonus;  
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;  
        TargetStat = statType;
    }

    public override void Apply(Character attacker, Character defender)
    {

        defender.NeutralizeBonusSkills();
        if (!defender.BonusNeutralized.Contains(TargetStat)) defender.BonusNeutralized.Add(TargetStat);
        


    }
}