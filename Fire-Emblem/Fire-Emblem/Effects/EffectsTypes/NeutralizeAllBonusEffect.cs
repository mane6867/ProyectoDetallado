
namespace Fire_Emblem.Effects
{
    public class NeutralizeAllBonusEffect : Effect
    {
        public NeutralizeAllBonusEffect() : base(StatType.Atk, 0) // No afecta un stat específico, neutraliza todos
        {
            EffectType = EffectType.Bonus;  // Esto es un tipo de efecto de combate
            EffectDuration = EffectDuration.WholeBattle;  // Solo durante el combate
        }

        public override void Apply(Character attacker, Character defender)
        {
            // Neutralizar todos los bonos del oponente
            defender.NeutralizeBonusSkills();
            
            if (!defender.BonusNeutralized.Contains(StatType.Atk)) defender.BonusNeutralized.Add(StatType.Atk);
            if (!defender.BonusNeutralized.Contains(StatType.Spd)) defender.BonusNeutralized.Add(StatType.Spd);
            if (!defender.BonusNeutralized.Contains(StatType.Def)) defender.BonusNeutralized.Add(StatType.Def);
            if (!defender.BonusNeutralized.Contains(StatType.Res)) defender.BonusNeutralized.Add(StatType.Res);

            // Si hay otros bonos en más stats, puedes incluirlos aquí
        }
    }
}
