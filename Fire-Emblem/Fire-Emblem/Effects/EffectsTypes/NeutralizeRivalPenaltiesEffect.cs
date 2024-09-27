namespace Fire_Emblem.Effects;

public class NeutralizeRivalPenaltiesEffect:Effect
{
    public NeutralizeRivalPenaltiesEffect() : base(StatType.Atk, 0) // No afecta un stat específico, neutraliza todos
    {
        EffectType = EffectType.Bonus;  // Esto es un tipo de efecto de combate
        EffectDuration = EffectDuration.WholeBattle;  // Solo durante el combate
    }

    public override void Apply(Character attacker, Character defender)
    {
        // Neutralizar todos los bonos del oponente
        defender.NeutralizePenaltiesSkills();
        defender.PenaltiesNeutralized.Add(StatType.Atk);
        defender.PenaltiesNeutralized.Add(StatType.Spd);
        defender.PenaltiesNeutralized.Add(StatType.Def);
        defender.PenaltiesNeutralized.Add(StatType.Res);

        // Si hay otros bonos en más stats, puedes incluirlos aquí
    }
}