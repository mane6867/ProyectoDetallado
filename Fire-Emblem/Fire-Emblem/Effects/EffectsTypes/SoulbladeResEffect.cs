namespace Fire_Emblem.Effects;

public class SoulbladeResEffect : Effect
{
    public SoulbladeResEffect(): base(StatType.Atk, 0)
    {
        effectDuration = EffectDuration.WholeBattle;
    }
    public override void Apply(Character attacker, Character defender)
    {
        int defBase = defender.Stats.Def; // Defensa base del rival
        int resBase = defender.Stats.Res; // Resistencia base del rival
        int promedio = (defBase + resBase) / 2; // Promedio entre Def y Res
        Console.WriteLine("EL DEF BASE ES "+ defBase);
        Console.WriteLine("EL RES BASE ES "+ resBase);


        int bonusRes = promedio - resBase; // Y
        
        if (bonusRes > 0)
        {
            Console.WriteLine("Se considerará como bonus el efecto en res");
            EffectType = EffectType.Bonus;
            defender.StatsBonus.Res += bonusRes;
        }
        else
        {
            Console.WriteLine("Se considerará como penalty el efecto en res");
            EffectType = EffectType.Penalty;
            defender.StatsPenalties.Res -= bonusRes;
        }
    }
}