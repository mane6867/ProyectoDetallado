using Fire_Emblem;
using Fire_Emblem.Effects;

//public class LunaEffect : Effect
//{
//    public LunaEffect()
//    {
//        EffectType = EffectType.Penalty;
//        EffectDuration = EffectDuration.WholeBattle;
//    }
//    public override void Apply(Character character)
//    {
//        if (character.BattleContext.isFirstAttack && character.BattleContext.actualOpponent != null)
//        {
//            // Ignorar la mitad de la defensa y resistencia del oponente
//            int reducedDef = Convert.ToInt32(Math.Floor(character.BattleContext.actualOpponent.Stats.Def / 2.0));
//            int reducedRes = Convert.ToInt32(Math.Floor(character.BattleContext.actualOpponent.Stats.Res / 2.0));
//
//            // Aplicar el penalty
//            character.BattleContext.actualOpponent.Stats.Def -= reducedDef;
//            character.BattleContext.actualOpponent.Stats.Res -= reducedRes;
//        }
//    }
//}