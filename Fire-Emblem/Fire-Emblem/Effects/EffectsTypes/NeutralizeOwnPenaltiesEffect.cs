namespace Fire_Emblem.Effects;

public class NeutralizeOwnPenaltiesEffect: Effect
{
    public NeutralizeOwnPenaltiesEffect() : base(StatType.Atk, 0) // No afecta un stat específico, neutraliza todos
    {
        EffectType = EffectType.Bonus;  // Esto es un tipo de efecto de combate
        EffectDuration = EffectDuration.WholeBattle;  // Solo durante el combate
    }

    public override void Apply(Character attacker, Character defender)
    {
        // Neutralizar todos los bonos del oponente
        attacker.NeutralizePenaltiesSkills();
        attacker.PenaltiesNeutralized.Add(StatType.Atk);
        attacker.PenaltiesNeutralized.Add(StatType.Spd);
        attacker.PenaltiesNeutralized.Add(StatType.Def);
        attacker.PenaltiesNeutralized.Add(StatType.Res);

        // Si hay otros bonos en más stats, puedes incluirlos aquí
    }
}