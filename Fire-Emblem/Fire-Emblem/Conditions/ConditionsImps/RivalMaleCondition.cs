namespace Fire_Emblem.Conditions.ConditionsImps;

public class RivalMaleCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.ActualOpponent.Gender == GenderType.Male;
    }
}