namespace Fire_Emblem.Conditions.ConditionsImps;

public class CloseDefCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return !character.BattleContext.IsInitiator && 
               (defender.Weapon == WeaponType.Sword || 
                defender.Weapon == WeaponType.Lance ||
                defender.Weapon == WeaponType.Axe) ;
    }
}