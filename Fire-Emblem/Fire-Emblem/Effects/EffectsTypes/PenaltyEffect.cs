namespace Fire_Emblem.Effects;

public class PenaltyEffect : Effect { 
    private readonly StatType _targetStat; 
    public PenaltyEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        _targetStat = targetStat; 
        EffectType = Fire_Emblem.EffectType.Penalty;
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
        _bonus = bonus;
    }

    public override void Apply ( Character character, Character defender) {

        if (_targetStat == StatType.Atk) character.StatsPenalties.Atk += _bonus;
        if ( _targetStat == StatType.Def ) character.StatsPenalties.Def += _bonus ;
        if ( _targetStat == StatType.Res ) character.StatsPenalties.Res += _bonus ;
        if ( _targetStat == StatType.Spd ) character.StatsPenalties.Spd += _bonus ; 
    }
    
}