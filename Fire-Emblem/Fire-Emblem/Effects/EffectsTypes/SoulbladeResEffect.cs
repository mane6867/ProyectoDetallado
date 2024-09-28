namespace Fire_Emblem.Effects;

public class SoulbladeResEffect : Effect
{
    public SoulbladeResEffect(): base(StatType.Atk, 0)
    {
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
    }
    public override void Apply(Character attacker, Character defender)
    {
        int defBase = defender.Stats.Def; // Defensa base del rival
        int resBase = defender.Stats.Res; // Resistencia base del rival
        int promedio = (defBase + resBase) / 2; // Promedio entre Def y Res


        int bonusRes = promedio - resBase; // Y
        
        if (bonusRes > 0)
        {

            EffectType = Fire_Emblem.EffectType.Bonus;
            defender.StatsBonus.Res += bonusRes;
        }
        else
        {

            EffectType = Fire_Emblem.EffectType.Penalty;
            defender.StatsPenalties.Res -= bonusRes;
        }
    }
}