namespace Fire_Emblem.Effects;

public class PercentageBonusEffect: Effect
{
    private readonly StatType _targetStat; 
    private readonly double _percentageBonus;

    public PercentageBonusEffect ( StatType targetStat , double bonus): base(targetStat, 0)
    { 
        _targetStat = targetStat; 
        _percentageBonus = bonus;
        EffectType = Fire_Emblem.EffectType.Bonus;
        EffectDuration = Fire_Emblem.EffectDuration.FirstAttack;
    }
    public override void Apply ( Character character, Character defender)
    {
        int bonus = 0;

        if (_targetStat == StatType.Atk)
        {
            character.StatsBonus.Atk += Convert.ToInt32(Math.Floor(character.Stats.Atk * _percentageBonus)) ;
            bonus = Convert.ToInt32(Math.Floor(character.Stats.Atk * _percentageBonus));
        }

        if (_targetStat == StatType.Def)
        {
            character.StatsBonus.Def += Convert.ToInt32(Math.Floor(character.Stats.Def * _percentageBonus)) ;
            bonus = Convert.ToInt32(Math.Floor(character.Stats.Def * _percentageBonus));
        }

        if (_targetStat == StatType.Res)
        {
            character.StatsBonus.Res += Convert.ToInt32(Math.Floor(character.Stats.Res * _percentageBonus));
            bonus = Convert.ToInt32(Math.Floor(character.Stats.Res * _percentageBonus));
        }

        if (_targetStat == StatType.Spd)
        {
            character.StatsBonus.Spd += Convert.ToInt32(Math.Floor(character.Stats.Spd * _percentageBonus));
            bonus = Convert.ToInt32(Math.Floor(character.Stats.Spd * _percentageBonus));
        }

        SetBonus(bonus);
    }

    
}