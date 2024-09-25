using Fire_Emblem_View;

namespace Fire_Emblem.Effects;

public class BonusRivalEffect : Effect { 
    private readonly StatType _targetStat; 
    private readonly int _bonus;
    public BonusRivalEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        _targetStat = targetStat; 
        EffectType = EffectType.Bonus;
        EffectDuration = EffectDuration.WholeBattle;
        _bonus = bonus;
    }

    public override void Apply ( Character character , Character defender) {

        if ( _targetStat == StatType.Atk ) defender.Stats.Atk += _bonus ;
        if ( _targetStat == StatType.Def ) defender.Stats.Def += _bonus ;
        if ( _targetStat == StatType.Res ) defender.Stats.Res += _bonus ;
        if ( _targetStat == StatType.Spd ) defender.Stats.Spd += _bonus ; 
    } }