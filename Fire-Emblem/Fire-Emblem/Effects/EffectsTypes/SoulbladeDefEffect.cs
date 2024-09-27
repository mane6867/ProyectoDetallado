namespace Fire_Emblem.Effects;

public class SoulbladeDefEffect : Effect
{
    public SoulbladeDefEffect(): base(StatType.Atk, 0)
    {
        effectDuration = EffectDuration.WholeBattle;
    }
    public override void Apply(Character attacker, Character defender)
    {
        int defBase = defender.Stats.Def; // Defensa base del rival
        int resBase = defender.Stats.Res; // Resistencia base del rival
        int promedio = (defBase + resBase) / 2; // Promedio entre Def y Res

        // Calcular bonificaciones o penalizaciones
        int bonusDef = promedio - defBase; // X

        if (bonusDef > 0)
        {
            Console.WriteLine("Se considerará como bonus el efecto en Def");
            EffectType = EffectType.Bonus;
            defender.StatsBonus.Def += bonusDef;
        }
        else
        {
            Console.WriteLine("Se considerará como penalty el efecto en Def");
            EffectType = EffectType.Penalty;
            defender.StatsPenalties.Def -= bonusDef;
        }
        
    }
}