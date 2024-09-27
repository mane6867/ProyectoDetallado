namespace Fire_Emblem.Effects;

public class NeutralizedBonusEffect: Effect
{
    public NeutralizedBonusEffect(StatType statType) : base(statType, 0) // No afecta un stat específico, neutraliza todos
    {
        EffectType = EffectType.Bonus;  // Esto es un tipo de efecto de combate
        EffectDuration = EffectDuration.WholeBattle;  // Solo durante el combate
        TargetStat = statType;
    }

    public override void Apply(Character attacker, Character defender)
    {
        // Neutralizar todos los bonos del oponente
        defender.NeutralizeBonusSkills();
        defender.BonusNeutralized.Add(TargetStat);

        // Si hay otros bonos en más stats, puedes incluirlos aquí
    }
}