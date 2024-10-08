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
    public List<StatType> BonusNeutralized = new List<StatType>();
    public List<StatType> PenaltiesNeutralized = new List<StatType>();
    
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
            BattleContext.WeaponType = value;
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
            { EffectType.Penalty, new List<Skill>() }
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
    private void SimulateApplySkillsOfType(EffectType type, Character defender)
    {
        foreach (var skill in _skills[type])
        {
            skill.ApplyIfApplicable(this, defender);
        }
    }

    public void SimulateApplySkills(Character defender)
    {
        SimulateApplySkillsOfType(EffectType.Bonus, defender);
        SimulateApplySkillsOfType(EffectType.Penalty, defender);
    }

    private void ApplySkillsNotNeutralized()
    {
        Stats.Atk += StatsBonus.Atk - StatsPenalties.Atk;
        Stats.Spd += StatsBonus.Spd - StatsPenalties.Spd;
        Stats.Res += StatsBonus.Res - StatsPenalties.Res;
        Stats.Def += StatsBonus.Def - StatsPenalties.Def;
    }
    private void ApplySkillsPenaltiesNeutralized()
    {
        Stats.Atk += StatsBonus.Atk;
        Stats.Spd += StatsBonus.Spd;
        Stats.Res += StatsBonus.Res;
        Stats.Def += StatsBonus.Def;
    }

    private void ApplySkillsBonusNeutralized()
    {
        if (BonusNeutralized.Contains(StatType.Atk)) Stats.Atk -= StatsPenalties.Atk;
        else  Stats.Atk += StatsBonus.Atk - StatsPenalties.Atk;
        if (BonusNeutralized.Contains(StatType.Spd)) Stats.Spd -= StatsPenalties.Spd;
        else  Stats.Spd += StatsBonus.Spd - StatsPenalties.Spd;
        if (BonusNeutralized.Contains(StatType.Res)) Stats.Res -= StatsPenalties.Res;
        else  Stats.Res += StatsBonus.Res - StatsPenalties.Res;
        if (BonusNeutralized.Contains(StatType.Def)) Stats.Def -= StatsPenalties.Def;
        else  Stats.Def += StatsBonus.Def - StatsPenalties.Def;
    }
    public void ApplyDefinitiveSkills()
    {
        if (!AreBonusSkillsNeutralized && !ArePenaltiesSkillsNeutralized)
        {
            ApplySkillsNotNeutralized();
        }
        if (!AreBonusSkillsNeutralized && ArePenaltiesSkillsNeutralized)
        {
            ApplySkillsPenaltiesNeutralized();
        }
        if (AreBonusSkillsNeutralized && !ArePenaltiesSkillsNeutralized)
        {
            ApplySkillsBonusNeutralized();
        }
        
    }

    public void ResetStatsIfNeutralized()
    {
        if (AreBonusSkillsNeutralized || ArePenaltiesSkillsNeutralized) SetStatsToOriginalStats();
    }

    private void SortListBonusNeutralized()
    {
        List<StatType> targetOrder = new List<StatType> { StatType.Atk, StatType.Spd, StatType.Def, StatType.Res };
        BonusNeutralized = BonusNeutralized.OrderBy(stat => targetOrder.IndexOf(stat)).ToList();
        
    }
    private void PrintBonusNeutralizedStats(View view)
    {
        SortListBonusNeutralized();
        foreach (var stat in BonusNeutralized)
        {
            view.WriteLine($"Los bonus de "+ stat + " de "+ Name +" fueron neutralizados"); 
        }
    }
    private void PrintPenaltiesNeutralizedStats(View view)
    {
        foreach (var stat in PenaltiesNeutralized)
        {
            view.WriteLine($"Los penalty de "+ stat + " de " + Name + " fueron neutralizados"); 
        }
    }

    public void PrintSkillsNeutralized(View view)
    {
        if (AreBonusSkillsNeutralized)
        {
            PrintBonusNeutralizedStats(view);
        }

        if (ArePenaltiesSkillsNeutralized)
        {
            PrintPenaltiesNeutralizedStats(view);
        }
    }
    public void NeutralizeBonusSkills()
    {
        AreBonusSkillsNeutralized = true;
    }
    public void UnNeutralizeBonusSkills()
    {
        AreBonusSkillsNeutralized = false;
    }
    public void NeutralizePenaltiesSkills()
    {
        ArePenaltiesSkillsNeutralized = true;
    }
    public void UnNeutralizePenaltiesSkills()
    {
        ArePenaltiesSkillsNeutralized = false;
    }
    public void SetTrueArePenaltysSkillsNeutralized()
    {
        ArePenaltiesSkillsNeutralized = true;
    }
    public void SetFalseArePenaltiesSkillsNeutralized()
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
        BattleContext.LastOpponent = character;
    }
    
    public void SetActualOpponent(Character character)
    {
        BattleContext.ActualOpponent = character;
    }

    public void SetIsNotFirstAttack()
    {
        BattleContext.IsFirstAttack = false;
    }

    public void IsInitiator()
    {
        BattleContext.IsInitiator = true;
        
    }
    
    public void IsNotInitiator()
    {
        BattleContext.IsInitiator = false;
    }
    public void SetWeaponBattle()
    {
        BattleContext.WeaponType = Weapon;
    }

    public void SetAttackType()
    {
        if (Weapon == WeaponType.Magic) BattleContext.AttackType = AttackType.Magical;
        else BattleContext.AttackType = AttackType.Physical;
    }

    public void AddSkills(List<string> namesSkills)
    {
        foreach (var nameSkill in namesSkills)
        {
            var skill = _skillFactory.Create(nameSkill);
            var effectType = skill.Effect.EffectType;
            _skills[effectType].Add(skill);
        }
    }

    public void RestoreAllStats()
    {
        SetStatsToOriginalStats();
        StatsBonus.SetDefault();
        StatsPenalties.SetDefault();
        BonusNeutralized = new List<StatType>();
        PenaltiesNeutralized = new List<StatType>();

    }
    public void SetStatsToOriginalStats()
    {
        Stats.Atk = _originalStats.Atk;
        Stats.Spd = _originalStats.Spd;
        Stats.Def = _originalStats.Def;
        Stats.Res = _originalStats.Res;
    }

    private void PrintBonus(View view)
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
    }

    private void PrintPenalties(View view)
    {
        if (StatsPenalties.Atk > 0)
        {
            view.WriteLine(Name + " obtiene Atk-" + StatsPenalties.Atk);
        }
        if (StatsPenalties.Spd > 0)
        {
            view.WriteLine(Name + " obtiene Spd-" + StatsPenalties.Spd);
        }
        if (StatsPenalties.Def > 0)
        {
            view.WriteLine(Name + " obtiene Def-" + StatsPenalties.Def);
        }
        if (StatsPenalties.Res > 0)
        {
            view.WriteLine(Name + " obtiene Res-" + StatsPenalties.Res);
        }
    }
    public void PrintSkillsEffects(View view)
    {
        PrintBonus(view);
        PrintPenalties(view);
    }

    public void RestoreAttributesForNextRound()
    {
        RestoreAllStats();
        UnNeutralizeBonusSkills();
        SetFalseArePenaltiesSkillsNeutralized();
    }
}