using Fire_Emblem_View;
using Fire_Emblem.Skills;

namespace Fire_Emblem;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Character
{
    public string Name { get; set; }
    
    private string DeathQuote { get; set; }
    public Stats Stats { get; set; } = new Stats();
    public Stats StatsBonus  { get; set; } = new Stats();
    public Stats StatsPenalties  { get; set; }  = new Stats();
    public BattleContext BattleContext { get; set; } = new BattleContext();
    private readonly SkillFactory _skillFactory = new SkillFactory();
    private Dictionary<EffectType, List<Skill>> _skills;
    public bool AreBonusSkillsNeutralized { get; private set; } = false;
    public bool ArePenaltiesSkillsNeutralized  { get; set; } = false;
    
    [JsonConverter(typeof(StringEnumConverter))]
    public GenderType Gender { get; set; }

    private readonly Stats _originalStats;
    
    [JsonConverter(typeof(StringEnumConverter))]
    private WeaponType _weapon;
    public WeaponType Weapon 
    {
        get => _weapon;
        set
        {
            _weapon = value;
            BattleContext.weaponType = value;
            SetAttackType();

        }
    }
    
    [JsonProperty("HP")]
    private string HpString
    {
        set
        {
            Stats.HpMax = int.Parse(value);
            Stats.Hp = int.Parse(value);
        }
    }
    
    
    [JsonProperty("Atk")]
    private string AtkString
    {
        set
        {
            Stats.Atk = int.Parse(value);
            _originalStats.Atk = int.Parse(value);
        }
    }

    [JsonProperty("Spd")]
    private string SpdString
    {
        set
        {
            Stats.Spd = int.Parse(value);
            _originalStats.Spd = int.Parse(value);
        }
    }

    [JsonProperty("Def")]
    private string DefString
    {
        set
        {
            Stats.Def = int.Parse(value);
            _originalStats.Def = int.Parse(value);
        }
    }

    [JsonProperty("Res")]
    private string ResString
    {
        set
        {
            Stats.Res = int.Parse(value); 
            _originalStats.Res = int.Parse(value);
        }
    }
    public Character()
    {
        _originalStats = new Stats();
        InitializeSkills();
    }

    private void InitializeSkills()
    {
        _skills = new Dictionary<EffectType, List<Skill>>
        {
            { EffectType.Bonus, new List<Skill>() },
            { EffectType.PenaltyOwn, new List<Skill>() },
            { EffectType.PenaltyRival, new List<Skill>() }
        };
    }
    public int GetOriginalStat(StatType statType)
    {
        return statType switch
        {
            StatType.Atk => _originalStats.Atk,
            StatType.Res => _originalStats.Res,
            StatType.Spd => _originalStats.Spd,
            _ => _originalStats.Def 
        };
    }
    private void ApplySkillsOfType(EffectType type, Character defender)
    {
        foreach (var skill in _skills[type])
        {
            skill.ApplyIfApplicable(this, defender);
        }
    }

    public void ApplySkills(Character defender)
    {
        ApplySkillsOfType(EffectType.Bonus, defender);
        ApplySkillsOfType(EffectType.PenaltyOwn, defender);
        
    }

    public void ResetStatsIfNeutralized()
    {
        if (AreBonusSkillsNeutralized || ArePenaltiesSkillsNeutralized)
        {
            SetStatsToOriginalStats();
        }
    }
    private void PrintNeutralizedStats(View view, string skillType)
    {
        view.WriteLine($"Los {skillType}s de Atk de {Name} fueron neutralizados");
        view.WriteLine($"Los {skillType}s de Spd de {Name} fueron neutralizados");
        view.WriteLine($"Los {skillType}s de Def de {Name} fueron neutralizados");
        view.WriteLine($"Los {skillType}s de Res de {Name} fueron neutralizados");
    }

    public void PrintSkillsNeutralized(View view)
    {
        if (AreBonusSkillsNeutralized)
        {
            PrintNeutralizedStats(view, "bonu");
        }

        if (ArePenaltiesSkillsNeutralized)
        {
            PrintNeutralizedStats(view, "penalty");
        }
        
    }
    public void NeutralizeBonusSkills()
    {
        AreBonusSkillsNeutralized = true;
        //Console.WriteLine(" SE CAMBIA A TRUE LOS BONUS NEUTRALIZADORES DE "+ Name);
    }
    public void UnNeutralizeBonusSkills()
    {
        AreBonusSkillsNeutralized = false;
        //Console.WriteLine(" SE CAMBIA A FALSE LOS BONUS NEUTRALIZADORES DE "+ Name);
    }
    public void SetTrueArePenaltysSkillsNeutralized()
    {
        ArePenaltiesSkillsNeutralized = true;
    }
    public void SetFalseArePenaltysSkillsNeutralized()
    {
        ArePenaltiesSkillsNeutralized = false;
    }
    
    public bool IsAbleToFollowUp(Character target)
    {
        return Convert.ToInt32(Stats.Spd) > Convert.ToInt32(target.Stats.Spd) + 4;
    }
    
    public bool HasAdvantage(Character target)
    {
        return (Weapon == WeaponType.Sword && target.Weapon == WeaponType.Axe) ||
               (Weapon == WeaponType.Lance && target.Weapon == WeaponType.Sword) ||
               (Weapon == WeaponType.Axe && target.Weapon == WeaponType.Lance);
    }

    public bool HasDisadvantage(Character target)
    {
        return (Weapon == WeaponType.Axe && target.Weapon == WeaponType.Sword) ||
               (Weapon == WeaponType.Sword && target.Weapon == WeaponType.Lance) ||
               (Weapon == WeaponType.Lance && target.Weapon == WeaponType.Axe);
    }

    private double CalculateWtbDamage(Character target)
    {

        if (HasDisadvantage(target)) return 0.8;
        else if (HasAdvantage(target)) return 1.2;
        else return 1;
    }

    private int CalculateReductionDamage(Character target)
    {
        if (Weapon != WeaponType.Magic) return Convert.ToInt32(target.Stats.Def);
        return Convert.ToInt32(target.Stats.Res);
    }
    public int CalculateDamage(Character target)
    {
        
        double wtb = CalculateWtbDamage(target);
        int reduction = CalculateReductionDamage(target);
        double rawDamage = Convert.ToInt32(Stats.Atk) * wtb - reduction;
        return (int)Math.Max(0, Math.Floor(rawDamage));
    }
    
    public void ApplyDamage(int damage, Character target) 
    {
        target.Stats.Hp = Convert.ToInt32(target.Stats.Hp) - damage;
    }

    public void SetLastOpponent(Character character)
    {
        BattleContext.lastOpponent = character;
    }
    
    public void SetActualOpponent(Character character)
    {
        BattleContext.actualOpponent = character;
    }

    public void SetIsNotFirstAttack()
    {
        BattleContext.isFirstAttack = false;
    }

    public void IsInitiator()
    {
        BattleContext.isInitiator = true;
        
    }
    
    public void IsNotInitiator()
    {
        BattleContext.isInitiator = false;
    }
    public void SetWeaponBattle()
    {
        BattleContext.weaponType = Weapon;
    }

    public void SetAttackType()
    {
        if (Weapon == WeaponType.Magic) BattleContext.attackType = AttackType.Magical;
        else BattleContext.attackType = AttackType.Physical;
    }

    public void AddSkills(List<string> namesSkills)
    {
        foreach (string nameSkill in namesSkills)
        {
            Skill skill = _skillFactory.Create(nameSkill);
            EffectType effectType = skill.Effect.EffectType;
            _skills[effectType].Add(skill);
        }
    }

    public void RestoreAllStats()
    {
        SetStatsToOriginalStats();
        StatsBonus.SetDefault();
    }
    public void SetStatsToOriginalStats()
    {
        Stats.Atk = _originalStats.Atk;
        Stats.Spd = _originalStats.Spd;
        Stats.Def = _originalStats.Def;
        Stats.Res = _originalStats.Res;
    }
    public void PrintSkillsEffects(View view)
    {
        if (StatsBonus.Atk > 0)
        {
            view.WriteLine(Name + " obtiene Atk+" + StatsBonus.Atk);

        }
        if (StatsBonus.Spd > 0)
        {
            view.WriteLine(Name + " obtiene Spd+" + StatsBonus.Spd);
        }
        if (StatsBonus.Def > 0)
        {
            view.WriteLine(Name + " obtiene Def+" + StatsBonus.Def);
        }
        if (StatsBonus.Res > 0)
        {
            view.WriteLine(Name + " obtiene Res+" + StatsBonus.Res);
        }
        
        
        if (StatsPenalties.Atk > 0)
        {
            view.WriteLine(Name + " obtiene Atk-" + StatsPenalties.Atk);
            StatsPenalties.Atk = 0;
        }
        if (StatsPenalties.Spd > 0)
        {
            view.WriteLine(Name + " obtiene Spd-" + StatsPenalties.Spd);
            StatsPenalties.Spd = 0;
        }
        if (StatsPenalties.Def > 0)
        {
            view.WriteLine(Name + " obtiene Def-" + StatsPenalties.Def);
            StatsPenalties.Def = 0;
        }
        if (StatsPenalties.Res > 0)
        {
            view.WriteLine(Name + " obtiene Res-" + StatsPenalties.Res);
            StatsPenalties.Res = 0;
        }
    }
}