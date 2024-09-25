using Fire_Emblem.Conditions.ConditionsImps;
using Fire_Emblem.Effects;

namespace Fire_Emblem.Skills;

public class SkillFactory
{
    public Skill Create(string name)
    {
        if (name == "HP +15")
            return new Skill(
                new TrueCondition(),
                new BonusEffect(StatType.Hp, 15));
        if (name == "Fair Fight")
        {
            return new Skill(
                new InitiateAttackCondition(), // No necesita parámetros
                new BonusEffect(StatType.Atk, 6));
        }
        if (name == "Will to Win")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp,StatType.HpMax, 0.5),
                new BonusEffect(StatType.Atk, 8));
        }
        if (name == "Single-Minded")
        {
            return new Skill(
                new SameOpponentCondition(),
                new BonusEffect(StatType.Atk, 8));
        }
        
        if (name == "Ignis")
        {
            return new Skill(
                new FirstAttackCondition(),
                new PercentageBonusEffect(StatType.Atk, 0.5));
        }

        if (name == "Perceptive")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(  // Efecto compuesto
                    new BonusEffect(StatType.Spd, 12),  // Otorga Spd+12
                    new ScalingEffect(StatType.Spd, 4, 1)));
        }

        if (name == "Tome Precision")
        {
            return new Skill(
                new UsesMagicCondition(), 
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 6), 
                    new BonusEffect(StatType.Spd, 6)));
        }

        if (name == "Attack +6")
        {
            return new Skill(
                new TrueCondition(), 
                new BonusEffect(StatType.Atk, 6));
        }
        if (name == "Speed +5")
        {
            return new Skill(
                new TrueCondition(), 
                new BonusEffect(StatType.Spd, 5));
        }
        if (name == "Defense +5")
        {
            return new Skill(
                new TrueCondition(), 
                new BonusEffect(StatType.Def, 5));
        }
        if (name == "Wrath")
        {
            return new Skill(
                new TrueCondition(), 
                new WrathEffect());
        }
        if (name == "Resolve")
        {
            return new Skill(new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.75), 
                new CompositeEffect(
                    new BonusEffect(StatType.Def, 7),
                    new BonusEffect(StatType.Res, 7)));
        }

        if (name == "Resistance +5")
        {
            return new Skill(
                new TrueCondition(),
                new BonusEffect(StatType.Res, 5));
        }
        if (name == "Atk/Def +5")
        {
            return new Skill(
                new TrueCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 5), 
                    new BonusEffect(StatType.Def, 5))
                );
        }
        if (name == "Atk/Res +5")
        {
            return new Skill(
                new TrueCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 5), 
                    new BonusEffect(StatType.Res, 5))
            );
        }
        if (name == "Spd/Res +5")
        {
            return new Skill(
                new TrueCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Spd, 5), 
                    new BonusEffect(StatType.Res, 5))
            );
        }
        if (name == "Deadly Blade")
        {
            return new Skill(
                new AndCondition(new InitiateAttackCondition(), new UsesWeaponCondition(WeaponType.Sword)),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 8), 
                    new BonusEffect(StatType.Spd, 8))
            );
        }
        if (name == "Death Blow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new BonusEffect(StatType.Atk, 8)
            );
        }
        if (name == "Armored Blow")
        {
            
            Skill skill = new Skill(
                new InitiateAttackCondition(),
                new BonusEffect(StatType.Def, 8)
            );
            //Console.WriteLine($"Created skill: {name}, TargetStat: {skill.Effect.TargetStat}");
            return skill;
        }
        if (name == "Darting Blow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new BonusEffect(StatType.Spd, 8)
            );
        }
        if (name == "Warding Blow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new BonusEffect(StatType.Res, 8)
            );
        }
        if (name == "Swift Sparrow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 6), 
                    new BonusEffect(StatType.Spd,6))
                
            );
        }
        if (name == "Sturdy Blow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 6), 
                    new BonusEffect(StatType.Def,6))
                
            );
        }
        if (name == "Mirror Strike")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 6), 
                    new BonusEffect(StatType.Res,6))
            );
        }
        if (name == "Steady Blow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Spd, 6), 
                    new BonusEffect(StatType.Def,6))
            );
        }
        if (name == "Swift Strike")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Spd, 6), 
                    new BonusEffect(StatType.Res,6))
            );
        }
        if (name == "Bracing Blow")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new CompositeEffect(
                    new BonusEffect(StatType.Def, 6), 
                    new BonusEffect(StatType.Res,6))
            );
        }
        if (name == "Brazen Atk/Spd")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.8),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10), 
                    new BonusEffect(StatType.Spd,10))
            );
        }
        if (name == "Brazen Atk/Def")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.8),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10), 
                    new BonusEffect(StatType.Def,10))
            );
        }
        if (name == "Brazen Atk/Res")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.8),
                new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10), 
                    new BonusEffect(StatType.Res,10))
            );
        }
        if (name == "Brazen Spd/Def")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.8),
                new CompositeEffect(
                    new BonusEffect(StatType.Spd, 10), 
                    new BonusEffect(StatType.Def,10))
            );
        }
        if (name == "Brazen Spd/Res")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.8),
                new CompositeEffect(
                    new BonusEffect(StatType.Spd, 10), 
                    new BonusEffect(StatType.Res,10))
            );
        }
        if (name == "Brazen Def/Res")
        {
            return new Skill(
                new LessOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 0.8),
                new CompositeEffect(
                    new BonusEffect(StatType.Def, 10), 
                    new BonusEffect(StatType.Res,10))
            );
        }

        if (name == "Fire Boost")
        {
            return new Skill(
                new GreaterOrEqualNumberCondition(StatType.Hp, StatType.Hp, 3),
                new BonusEffect(StatType.Atk, 6));
        }
        if (name == "Wind Boost")
        {
            return new Skill(
                new GreaterOrEqualNumberCondition(StatType.Hp, StatType.Hp, 3),
                new BonusEffect(StatType.Spd, 6));
        }
        if (name == "Earth Boost")
        {
            return new Skill(
                new GreaterOrEqualNumberCondition(StatType.Hp, StatType.Hp, 3),
                new BonusEffect(StatType.Def, 6));
        }
        if (name == "Water Boost")
        {
            return new Skill(
                new GreaterOrEqualNumberCondition(StatType.Hp, StatType.Hp, 3),
                new BonusEffect(StatType.Res, 6));
        }
        if (name == "Chaos Style")
        {
            return new Skill(
                new ChaosStyleCondition(),
                new BonusEffect(StatType.Spd, 3));
        }

        if (name == "Blinding Flash")
        {
            return new Skill(
                new InitiateAttackCondition(),
                new PenaltyRivalEffect(StatType.Spd, 4));
        }
        if (name == "Not *Quite*")
        {
            return new Skill(
                new TrueCondition(),
                new PenaltyRivalEffect(StatType.Atk, 4));
        }
        if (name == "Stunning Smile")
        {
            return new Skill(
                new RivalMaleCondition(),
                new PenaltyRivalEffect(StatType.Spd, 8));
        }
        if (name == "Disarming Sigh")
        {
            return new Skill(
                new RivalMaleCondition(),
                new PenaltyRivalEffect(StatType.Atk, 8));
        }
        if (name == "Charmer")
        {
            return new Skill(
                new SameOpponentCondition(),
                new CompositeEffect(new PenaltyRivalEffect(StatType.Atk, 3), new PenaltyRivalEffect(StatType.Spd, 3 ))
                );
        }
        //if (name == "Luna")
        //{
        //    return new Skill(
        //        new FirstAttackCondition(),
        //        new LunaEffect());
        //}
        if (name == "Belief in Love")
        {
            return new Skill(
                new OrCondition(new InitiateRivalAttackCondition(), new GreaterOrEqualPercentageCondition(StatType.Hp, StatType.HpMax, 1)),
                new CompositeEffect(
                    new PenaltyRivalEffect(StatType.Atk, 5),
                    new PenaltyRivalEffect(StatType.Def, 5)));
        }

        if (name == "Beorc's Blessing")
            return new Skill(new TrueCondition(), new NeutralizeBonusesEffect());
        if (name == "Sword Agility")
            return new Skill(
                new UsesWeaponCondition(WeaponType.Sword),
                new CompositeEffect(
                    new BonusEffect(StatType.Spd, 12),
                    new PenaltyEffect(StatType.Atk, 6)));
        if (name == "Lance Power")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Lance), new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10),
                    new PenaltyEffect(StatType.Def, 10)));
        }
        if (name == "Sword Power")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Sword), new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10),
                    new PenaltyEffect(StatType.Def, 10)));
        }
        if (name == "Bow Power")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Sword), new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10),
                    new PenaltyEffect(StatType.Res, 10)));
        }
        if (name == "Lance Agility")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Lance), new CompositeEffect(
                    new BonusEffect(StatType.Spd, 12),
                    new PenaltyEffect(StatType.Atk, 6)));
        }
        if (name == "Axe Power")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Axe), new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10),
                    new PenaltyEffect(StatType.Def, 10)));
        }
        if (name == "Bow Agility")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Bow), new CompositeEffect(
                    new BonusEffect(StatType.Spd, 12),
                    new PenaltyEffect(StatType.Atk, 6)));
        }
        if (name == "Sword Focus")
        {
            return new Skill(
                new UsesWeaponCondition(WeaponType.Sword), new CompositeEffect(
                    new BonusEffect(StatType.Atk, 10),
                    new PenaltyEffect(StatType.Res, 10)));
        }
        if (name == "Fort. Def/Res")
        {
            return new Skill(
                new TrueCondition(), new CompositeEffect(
                    new BonusEffect(StatType.Def, 6),
                    new BonusEffect(StatType.Res, 6),
                    new PenaltyEffect(StatType.Atk, 2)));
        }
        if (name == "Life and Death")
        {
            return new Skill(
                new TrueCondition(), new CompositeEffect(
                    new BonusEffect(StatType.Atk, 6),
                    new BonusEffect(StatType.Spd, 6),
                    new PenaltyEffect(StatType.Def, 5),
                    new PenaltyEffect(StatType.Res, 5)));
        }
        if (name == "Solid Ground")
        {
            return new Skill(
                new TrueCondition(), new CompositeEffect(
                    new BonusEffect(StatType.Def, 6),
                    new BonusEffect(StatType.Atk, 6),
                    new PenaltyEffect(StatType.Res, 5)));
        }
        if (name == "Still Water")
        {
            return new Skill(
                new TrueCondition(), new CompositeEffect(
                    new BonusEffect(StatType.Res, 6),
                    new BonusEffect(StatType.Atk, 6),
                    new PenaltyEffect(StatType.Def, 5)));
        }

        if (name == "Close Def")
        {
            return new Skill(
                new AndCondition(
                    new NotCond(new UsesMagicCondition()),
                    new NotCond(new UsesWeaponCondition(WeaponType.Bow))),
                new CompositeEffect(
                    new BonusEffect(StatType.Def, 8),
                    new BonusEffect(StatType.Res, 8),
                    new NeutralizeBonusesEffect()));
        }
        
        
        
        
        
        throw new ApplicationException () ;

    }
}