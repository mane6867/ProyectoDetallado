namespace Fire_Emblem.Conditions.ConditionsImps;

public class CloseDefCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return !character.BattleContext.isInitiator && 
               (character.BattleContext.actualOpponent.Weapon == WeaponType.Sword || 
                character.BattleContext.actualOpponent.Weapon == WeaponType.Lance ||
                character.BattleContext.actualOpponent.Weapon == WeaponType.Axe) ;
    }
}