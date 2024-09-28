
namespace Fire_Emblem.Effects
{
    public class NeutralizeAllBonusEffect : Effect
    {
        public NeutralizeAllBonusEffect() : base(StatType.Atk, 0) 
        {
            EffectType = Fire_Emblem.EffectType.Bonus;  
            EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;  
        }

        public override void Apply(Character attacker, Character defender)
        {
            defender.NeutralizeBonusSkills();
            
            if (!defender.BonusNeutralized.Contains(StatType.Atk)) defender.BonusNeutralized.Add(StatType.Atk);
            if (!defender.BonusNeutralized.Contains(StatType.Spd)) defender.BonusNeutralized.Add(StatType.Spd);
            if (!defender.BonusNeutralized.Contains(StatType.Def)) defender.BonusNeutralized.Add(StatType.Def);
            if (!defender.BonusNeutralized.Contains(StatType.Res)) defender.BonusNeutralized.Add(StatType.Res);
            
        }
    }
}
