namespace Fire_Emblem.Conditions.ConditionsImps;

public class RivalMaleCondition: Condition
{
    public override bool DoesHold(Character character)
    {
        return character.BattleContext.actualOpponent.Gender == GenderType.Male;
    }
}