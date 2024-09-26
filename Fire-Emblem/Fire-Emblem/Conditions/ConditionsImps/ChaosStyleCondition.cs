namespace Fire_Emblem.Conditions.ConditionsImps;

public class ChaosStyleCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.isInitiator && 
               (character.BattleContext.attackType == AttackType.Physical && 
                character.BattleContext.actualOpponent.Weapon == WeaponType.Magic) ||
               character.BattleContext.isInitiator && 
               (character.BattleContext.attackType == AttackType.Magical &&  
                character.BattleContext.actualOpponent.Weapon != WeaponType.Magic);
    }
}