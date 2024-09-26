namespace Fire_Emblem.Conditions.ConditionsImps;

public class FirstAttackCondition:Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.isFirstAttack;
    }
}