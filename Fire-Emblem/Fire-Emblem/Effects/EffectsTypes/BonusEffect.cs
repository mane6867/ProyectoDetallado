using Fire_Emblem_View;

namespace Fire_Emblem.Effects;

public class BonusEffect : Effect { 
    public readonly StatType TargetStat; 
    private readonly int _bonus;
    public int Bonus => _bonus;
    public BonusEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        TargetStat = targetStat; 
        EffectType = EffectType.Bonus;
        EffectDuration = EffectDuration.WholeBattle;
        _bonus = bonus;
    }

public override void Apply ( Character character , Character defender) {

    if (TargetStat == StatType.Atk)
    {
        character.Stats.Atk += _bonus; ;
    }

    if (TargetStat == StatType.Def)
    {
        character.Stats.Def += _bonus ;
        
    }

    if ( TargetStat == StatType.Res ) character.Stats.Res += _bonus ;

    if ( TargetStat == StatType.Spd ) character.Stats.Spd += _bonus ; 
} }