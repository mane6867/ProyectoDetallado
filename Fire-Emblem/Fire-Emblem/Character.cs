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
            BattleContext.weaponType = value; // Actualizar automáticamente el contexto de batalla
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

    public void ApplySkills(Character defender)
    {
        //Console.WriteLine("ESTOY EN APPLY SKILLS, LOS BONUS DE " + Name + " ESTÁN NEUTRALIZADOS? "+ AreBonusSkillsNeutralized);

            foreach (var skill in _skills[EffectType.Bonus])
            {
                skill.ApplyIfApplicable(this, defender);
                //Console.WriteLine("Se está revisando las skills de "+ Name);
            }
        

            foreach (var skill in _skills[EffectType.PenaltyOwn])
            {
                skill.ApplyIfApplicable(this, defender);
            }
        
    }

    public void ResetStatsIfNeutralized()
    {
        if (AreBonusSkillsNeutralized || ArePenaltiesSkillsNeutralized)
        {
            SetStatsToOriginalStats();
        }
    }

    public void PrintSkillsNeutralized(View view)
    {
        Console.WriteLine("se está en el print de neutralized");
        Console.WriteLine("los bonus están neutralizados "+ AreBonusSkillsNeutralized + " de " + Name);
        foreach (var entry in _skills)
        {
            Console.WriteLine($"Tipo de efecto: {entry.Key}, Número de habilidades: {entry.Value.Count}");
            foreach (var s in entry.Value)
            {
                Console.WriteLine($"  - Habilidad: {s}");
            }
        }
        if (AreBonusSkillsNeutralized)
        {
            view.WriteLine("Los bonus de Atk de " + Name + " fueron neutralizados");
            view.WriteLine("Los bonus de Spd de " + Name + " fueron neutralizados");
            view.WriteLine("Los bonus de Def de " + Name + " fueron neutralizados");
            view.WriteLine("Los bonus de Res de " + Name + " fueron neutralizados");

        }
        if (ArePenaltiesSkillsNeutralized)
        {
            view.WriteLine("Los penaltys de Atk de " + Name + " fueron neutralizados");
            view.WriteLine("Los penaltys de Spd de " + Name + " fueron neutralizados");
            view.WriteLine("Los penaltys de Def de " + Name + " fueron neutralizados");
            view.WriteLine("Los penaltys de Res de " + Name + " fueron neutralizados");
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
        Console.WriteLine("se está viendo si"+ Name+ "puede hacer follow up con " + Stats.Spd + " spd por sobre la del rival que es" + target.Stats.Spd);
        Console.WriteLine(Convert.ToInt32(Stats.Spd) > Convert.ToInt32(target.Stats.Spd) + 4);
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

    public int CalculateDamage(Character target)
    {
        double WTB;
        if (HasDisadvantage(target))
        {
            WTB = 0.8;
        }
        
        else if (HasAdvantage(target) )
        {
            WTB = 1.2;
        }
        else
        {
            WTB = 1;
        }
        
        int valor_resta = 1000;
        // si atacante tiene arma física
        if (Weapon == WeaponType.Sword || Weapon == WeaponType.Lance || Weapon == WeaponType.Bow || Weapon == WeaponType.Axe)
        {
            valor_resta = Convert.ToInt32(target.Stats.Def);
        }
        else if (Weapon == WeaponType.Magic)
        {
            valor_resta = Convert.ToInt32(target.Stats.Res);
        }
        
        // se calcula el daño
        double daño_double = Convert.ToInt32(Stats.Atk) * WTB - valor_resta;
        
        // si es negativo es 0
        if (daño_double < 0)
        {
            daño_double = 0;
        }

        int daño = Convert.ToInt32(Math.Floor(daño_double));
        
        return daño;
    }
    
    public void ApplyDamage(int dano, Character target) 
    {
        //Console.WriteLine("La antigua Hp ES" + target.Stats.Hp + target.Name);
        target.Stats.Hp = Convert.ToInt32(target.Stats.Hp) - dano;
        //Console.WriteLine("La nueva Hp ES" + target.Stats.Hp + target.Name);
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
        Console.WriteLine("inició el duelo por parte de " + Name);
        
    }
    
    public void IsNotInitiator()
    {
        BattleContext.isInitiator = false;
        Console.WriteLine("se coloca que es falso que se inició el duelo por parte de " + Name);
    }
    public void SetWeaponBattle()
    {
        BattleContext.weaponType = Weapon;
    }

    public void SetAttackType()
    {
        if (Weapon == WeaponType.Magic)
        {
            BattleContext.attackType = AttackType.Magical;
        }
        else
        {
            BattleContext.attackType = AttackType.Physical;
        }
    }

    public void AddSkills(List<string> namesSkills)
    {
        Console.WriteLine("SE ESTÁ EN ADD SKILLS");
        //Console.WriteLine("El primer nombre a  AGREGAR es " +namesSkills[0]);
        foreach (string nameSkill in namesSkills)
        {
            //Console.WriteLine(nameSkill);
            Skill skill = _skillFactory.Create(nameSkill);
            EffectType effectType = skill.Effect.EffectType;
            _skills[effectType].Add(skill);
            Console.WriteLine("al señor " + Name + "se le ha agregado la skill de efecto " + effectType.ToString() );
            

        }
    }
    public IReadOnlyList<Skill> GetSkills()
    {
        var allSkills = new List<Skill>();

        foreach (var skillList in _skills.Values)
        {
            allSkills.AddRange(skillList); // Agrega todas las habilidades de la lista actual
        }

        return allSkills.AsReadOnly();
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
        //Console.WriteLine("se está en el lugar para ver si se imprimen las skills");
        //Console.WriteLine("el valor de bonus def es" + StatsBonus.Def);
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
            Console.WriteLine("se setea a 0 las penaltys de atk-------------------");
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