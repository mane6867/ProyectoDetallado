namespace Fire_Emblem.Conditions.ConditionsImps;

public class UsesMagicCondition : Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.attackType == AttackType.Magical;
    }
}