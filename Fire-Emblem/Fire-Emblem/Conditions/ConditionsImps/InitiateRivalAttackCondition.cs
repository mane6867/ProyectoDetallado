namespace Fire_Emblem.Conditions.ConditionsImps;

public class InitiateRivalAttackCondition:Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return defender.BattleContext.IsInitiator; 
    }
}