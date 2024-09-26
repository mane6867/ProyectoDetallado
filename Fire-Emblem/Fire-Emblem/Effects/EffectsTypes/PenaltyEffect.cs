namespace Fire_Emblem.Effects;

public class PenaltyEffect : Effect { 
    private readonly StatType _targetStat; 
    public PenaltyEffect ( StatType targetStat , int bonus): base(targetStat, bonus)
    { 
        _targetStat = targetStat; 
        EffectType = EffectType.PenaltyOwn;
        EffectDuration = EffectDuration.WholeBattle;
        _bonus = bonus;
        Console.WriteLine("en el inicializador se coloca que el valor de penalty es "+ _bonus);
    }

    public override void Apply ( Character character, Character defender) {

        if (_targetStat == StatType.Atk)
        {
            character.StatsPenalties.Atk += _bonus;
            Console.WriteLine("SE LE baja el atk DE  PENALTY " + _bonus + " A " + character.Name);
        }
        if ( _targetStat == StatType.Def ) character.StatsPenalties.Def += _bonus ;
        if ( _targetStat == StatType.Res ) character.StatsPenalties.Res += _bonus ;
        if ( _targetStat == StatType.Spd ) character.StatsPenalties.Spd += _bonus ; 
    }
    
}