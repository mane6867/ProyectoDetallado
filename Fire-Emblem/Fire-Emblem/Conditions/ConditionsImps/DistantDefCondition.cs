namespace Fire_Emblem.Conditions.ConditionsImps;

public class DistantDefCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return !character.BattleContext.isInitiator && 
               (character.BattleContext.actualOpponent.Weapon == WeaponType.Magic || 
                character.BattleContext.actualOpponent.Weapon == WeaponType.Bow) ;
    }
}