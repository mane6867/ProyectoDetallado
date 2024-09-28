namespace Fire_Emblem.Effects;

public class PenaltyRivalEffect : Effect { 
    private readonly StatType _targetStat; 
    public PenaltyRivalEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        _targetStat = targetStat; 
        EffectType = Fire_Emblem.EffectType.Penalty;
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
        _bonus = bonus;
    }

    public override void Apply ( Character character, Character defender) {

        if (_targetStat == StatType.Atk) defender.StatsPenalties.Atk += _bonus;
        if ( _targetStat == StatType.Def ) defender.StatsPenalties.Def += _bonus ;
        if ( _targetStat == StatType.Res ) defender.StatsPenalties.Res += _bonus ;
        if ( _targetStat == StatType.Spd ) defender.StatsPenalties.Spd += _bonus ; 
    }
    
}