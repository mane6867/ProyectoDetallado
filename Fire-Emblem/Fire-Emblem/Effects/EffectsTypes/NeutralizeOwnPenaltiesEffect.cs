namespace Fire_Emblem.Effects;

public class NeutralizeOwnPenaltiesEffect: Effect
{
    public NeutralizeOwnPenaltiesEffect() : base(StatType.Atk, 0) 
    {
        EffectType = Fire_Emblem.EffectType.Bonus;  
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;  
    }

    public override void Apply(Character attacker, Character defender)
    {

        attacker.NeutralizePenaltiesSkills();
        attacker.PenaltiesNeutralized.Add(StatType.Atk);
        attacker.PenaltiesNeutralized.Add(StatType.Spd);
        attacker.PenaltiesNeutralized.Add(StatType.Def);
        attacker.PenaltiesNeutralized.Add(StatType.Res);


    }
}