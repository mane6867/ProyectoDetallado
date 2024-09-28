namespace Fire_Emblem.Effects;

public class SoulbladeDefEffect : Effect
{
    public SoulbladeDefEffect(): base(StatType.Atk, 0)
    {
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
    }
    public override void Apply(Character attacker, Character defender)
    {
        int defBase = defender.Stats.Def; 
        int resBase = defender.Stats.Res; 
        int promedio = (defBase + resBase) / 2; 


        int bonusDef = promedio - defBase; 

        if (bonusDef > 0)
        {
            EffectType = Fire_Emblem.EffectType.Bonus;
            defender.StatsBonus.Def += bonusDef;
        }
        else
        {

            EffectType = Fire_Emblem.EffectType.Penalty;
            defender.StatsPenalties.Def -= bonusDef;
        }
        
    }
}