namespace Fire_Emblem.Effects;

public class ScalingEffect : Effect
{
    private readonly StatType _targetStat;
    private readonly int _scalingFactor;
    private readonly int _bonusPerFactor;
    public ScalingEffect(StatType targetStat, int scalingFactor, int bonusPerFactor): base(targetStat, 0)
    {
        _targetStat = targetStat;
        _scalingFactor = scalingFactor;
        _bonusPerFactor = bonusPerFactor;
        EffectType = EffectType.Bonus;
        EffectDuration = EffectDuration.WholeBattle;
    }

    public override void Apply(Character character, Character defender)
    {
        int additionalBonus = 0;
        if (_targetStat == StatType.Atk)
        {
            int baseSpeed = character.GetOriginalStat(StatType.Atk);
            additionalBonus = (baseSpeed / _scalingFactor) * _bonusPerFactor;
            character.StatsBonus.Atk += additionalBonus;
            
        }
        if (_targetStat == StatType.Def)
        {
            int baseSpeed = character.GetOriginalStat(StatType.Def);
            additionalBonus = (baseSpeed / _scalingFactor) * _bonusPerFactor;
            character.StatsBonus.Def += additionalBonus;
        }
        if (_targetStat == StatType.Spd)
        {
            int baseSpeed = character.GetOriginalStat(StatType.Spd);
            additionalBonus = (baseSpeed / _scalingFactor) * _bonusPerFactor;
            character.StatsBonus.Spd += additionalBonus;
        }
        if (_targetStat == StatType.Res)
        {
            int baseSpeed = character.GetOriginalStat(StatType.Res);
            additionalBonus = (baseSpeed / _scalingFactor) * _bonusPerFactor;
            character.StatsBonus.Res += additionalBonus;
        }

        SetBonus(additionalBonus);

    }
}