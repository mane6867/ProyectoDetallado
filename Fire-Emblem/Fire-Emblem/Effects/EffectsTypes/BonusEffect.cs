using Fire_Emblem_View;

namespace Fire_Emblem.Effects;

public class BonusEffect : Effect { 
    public readonly StatType TargetStat; 
    private readonly int _bonus;
    public int Bonus => _bonus;
    public BonusEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        TargetStat = targetStat; 
        EffectType = Fire_Emblem.EffectType.Bonus;
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
        _bonus = bonus;
    }

public override void Apply ( Character character , Character defender) {

    if (TargetStat == StatType.Atk) character.StatsBonus.Atk += _bonus; ;

    if (TargetStat == StatType.Def) character.StatsBonus.Def += _bonus ;

    if ( TargetStat == StatType.Res ) character.StatsBonus.Res += _bonus ;

    if ( TargetStat == StatType.Spd ) character.StatsBonus.Spd += _bonus ;
    if (TargetStat == StatType.Hp)
    { 
        character.Stats.HpMax += _bonus;
        character.Stats.Hp += _bonus ;
    } 
} }