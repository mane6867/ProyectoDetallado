using Fire_Emblem_View;

namespace Fire_Emblem.Effects;

public class BonusRivalEffect : Effect { 
    private readonly StatType _targetStat; 
    private readonly int _bonus;
    public BonusRivalEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        _targetStat = targetStat; 
        EffectType = Fire_Emblem.EffectType.Bonus;
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
        _bonus = bonus;
    }

    public override void Apply ( Character character , Character defender) {

        if ( _targetStat == StatType.Atk ) defender.StatsBonus.Atk += _bonus ;
        if ( _targetStat == StatType.Def ) defender.StatsBonus.Def += _bonus ;
        if ( _targetStat == StatType.Res ) defender.StatsBonus.Res += _bonus ;
        if ( _targetStat == StatType.Spd ) defender.StatsBonus.Spd += _bonus ; 
    } }