namespace Fire_Emblem.Effects;

public class NeutralizePenaltiesEffect:Effect
{
    public NeutralizePenaltiesEffect() : base(StatType.Atk, 0) // No afecta un stat específico, neutraliza todos
    {
        EffectType = EffectType.Bonus;  // Esto es un tipo de efecto de combate
        EffectDuration = EffectDuration.WholeBattle;  // Solo durante el combate
    }

    public override void Apply(Character attacker, Character defender)
    {
        // Neutralizar todos los bonos del oponente
        attacker.NeutralizePenaltiesSkills();

        // Si hay otros bonos en más stats, puedes incluirlos aquí
    }
}