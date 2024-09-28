namespace Fire_Emblem.Effects;

public class NeutralizeRivalPenaltiesEffect:Effect
{
    public NeutralizeRivalPenaltiesEffect() : base(StatType.Atk, 0) 
    {
        EffectType = Fire_Emblem.EffectType.Bonus;  
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;  
    }

    public override void Apply(Character attacker, Character defender)
    {
        defender.NeutralizePenaltiesSkills();
        defender.PenaltiesNeutralized.Add(StatType.Atk);
        defender.PenaltiesNeutralized.Add(StatType.Spd);
        defender.PenaltiesNeutralized.Add(StatType.Def);
        defender.PenaltiesNeutralized.Add(StatType.Res);
    }
}