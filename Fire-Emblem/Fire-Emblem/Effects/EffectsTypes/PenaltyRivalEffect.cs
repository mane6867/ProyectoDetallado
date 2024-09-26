namespace Fire_Emblem.Effects;

public class PenaltyRivalEffect : Effect { 
    private readonly StatType _targetStat; 
    public PenaltyRivalEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        _targetStat = targetStat; 
        EffectType = EffectType.PenaltyRival;
        EffectDuration = EffectDuration.WholeBattle;
        _bonus = bonus;
        Console.WriteLine("en el inicializador se coloca que el valor de penalty es "+ _bonus);
    }

    public override void Apply ( Character character, Character defender) {

        if (_targetStat == StatType.Atk)
        {
            defender.StatsPenalties.Atk += _bonus;
            Console.WriteLine("SE LE baja el atk DE  PENALTY " + _bonus + " A " + defender.Name);
        }
        if ( _targetStat == StatType.Def ) defender.StatsPenalties.Def += _bonus ;
        if ( _targetStat == StatType.Res ) defender.StatsPenalties.Res += _bonus ;
        if ( _targetStat == StatType.Spd ) defender.StatsPenalties.Spd += _bonus ; 
    }
    
}