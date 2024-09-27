using System.Runtime.InteropServices;

namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class Battle
{
    private View _view;
    private Utilities _utilities;
    private List<List<Character>> _teams;
    private Character _attackerCharacter;
    
    private Character _defenderCharacter;
    private bool _attackerIsAbleToFollowUp = false;
    private bool _defenderIsAbleToFollowUp = false;

    public Battle(View view, List<List<Character>> teams)
    {
        _view = view;
        _utilities = new Utilities(view);
        _teams = teams;


    }

    private (int attackerIndex, int defenderIndex) GetIndexAttackerDefender(int round)
    {
        int attackerIndex = round % 2 == 0 ? 2 : 1;
        int defenderIndex = 3 - attackerIndex;
        return (attackerIndex, defenderIndex);
    }

    private void PrintCaseOfAdvantage()
    {
        if (_attackerCharacter.HasAdvantage(_defenderCharacter)) _view.WriteLine(_attackerCharacter.Name + 
            " (" + _attackerCharacter.Weapon + ") tiene ventaja con respecto a " + _defenderCharacter.Name +
            " (" + _defenderCharacter.Weapon + ")");
        
        else if (_defenderCharacter.HasAdvantage(_attackerCharacter)) _view.WriteLine(_defenderCharacter.Name +
            " (" + _defenderCharacter.Weapon + ") tiene ventaja con respecto a " + _attackerCharacter.Name + 
            " (" + _attackerCharacter.Weapon + ")");
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
    }

    private void PrintDamage(int damage)
    {
        _view.WriteLine(_attackerCharacter.Name + " ataca a "+ _defenderCharacter.Name + " con " + damage +
                        " de daño");
    }

    private void ApplyDamageToCharacter(int damage)
    {
        _attackerCharacter.ApplyDamage(damage, _defenderCharacter);
    }

    private void HandleDamageFight()
    {
        int damage = _attackerCharacter.CalculateDamage(_defenderCharacter);
        PrintDamage(damage);
        ApplyDamageToCharacter(damage);
    }

    private bool IsDuelOver()
    {
        return _teams.Any(team => team.Any(character => character.Stats.Hp == 0));
    }

    private void DeleteCharactersFromTeams()
    {
        foreach (var team in _teams)
        {
            team.RemoveAll(character => character.Stats.Hp == 0);
        }
    }
    private void SwapCharacters()
    {
        (_attackerCharacter, _defenderCharacter) = (_defenderCharacter, _attackerCharacter);
    }
    

    private void SimulateApplySkills()
    {
        
        _attackerCharacter.SimulateApplySkills(_defenderCharacter);
        _defenderCharacter.SimulateApplySkills(_attackerCharacter);
    }

    private void PrintSkillsStatus()
    {
        _attackerCharacter.PrintSkillsEffects(_view);
        _attackerCharacter.PrintSkillsNeutralized(_view);
        _defenderCharacter.PrintSkillsEffects(_view);
        _defenderCharacter.PrintSkillsNeutralized(_view);
    }

    private void ResetStatsIfNeutralized()
    {
        _attackerCharacter.ResetStatsIfNeutralized();
        _defenderCharacter.ResetStatsIfNeutralized();
    }

    private void ApplyDefinitiveSkills()
    {
        _attackerCharacter.ApplyDefinitiveSkills();
        _defenderCharacter.ApplyDefinitiveSkills();
    }

    private void GetFollowUpStatusForDuelists()
    {
        _attackerIsAbleToFollowUp = _attackerCharacter.IsAbleToFollowUp(_defenderCharacter);
        _defenderIsAbleToFollowUp = _defenderCharacter.IsAbleToFollowUp(_attackerCharacter);
    }
    private void HandleDuelFight()
    {
        HandleDamageFight();
        if (IsDuelOver()) return; 
        SwapCharacters(); 
        HandleDamageFight();
    }

    private void HandleFollowUp()
    {
        if (!IsDuelOver())
        {
            if (_attackerIsAbleToFollowUp)
            {
                SwapCharacters();
                HandleDamageFight();
            }
            else if (_defenderIsAbleToFollowUp) HandleDamageFight();
            else _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }
    private void HandleDuel()
    {
        SimulateApplySkills();
        PrintCaseOfAdvantage();
        PrintSkillsStatus();
        ResetStatsIfNeutralized();
        ApplyDefinitiveSkills();
        GetFollowUpStatusForDuelists();
        HandleDuelFight();
        HandleFollowUp();
        DeleteCharactersFromTeams();
        
        
    }

    private bool IsFightOver()
    {
        return IsTeam1Empty() || IsTeam2Empty();
    }

    private bool IsTeam1Empty()
    {
        return  _teams[0].Count == 0;
    }
    private bool IsTeam2Empty()
    {
        return  _teams[1].Count == 0;
    }

    private void ReportRoundOutcome(int indexAttacker, string nameAttacker)
    {
        var winnerTeam = _teams[indexAttacker-1];
        var winnerCharacter = winnerTeam.FirstOrDefault(character => character.Name == nameAttacker);

        if (winnerCharacter != _attackerCharacter) SwapCharacters();
        
        _view.WriteLine(_attackerCharacter.Name +" (" + _attackerCharacter.Stats.Hp + ") : " +
                        _defenderCharacter.Name + " (" + _defenderCharacter.Stats.Hp + ")" );
    }
    private void ReportWhoWon()
    {
        if (IsTeam1Empty()) _view.WriteLine("Player 2 ganó");
        else _view.WriteLine("Player 1 ganó");
    }
    private void UpdateLastOpponents()
    {
        _attackerCharacter.SetLastOpponent(_defenderCharacter);
        _defenderCharacter.SetLastOpponent(_attackerCharacter);
    }

    private void ChooseUnits(int attackerIndex, int defenderIndex)
    {
        _attackerCharacter = AskUserToSelectUnit(attackerIndex);
        _defenderCharacter = AskUserToSelectUnit(defenderIndex);
    }

    private void SetAttributesOpponents()
    {
        _attackerCharacter.SetActualOpponent(_defenderCharacter);
        _defenderCharacter.SetActualOpponent(_attackerCharacter);
        _attackerCharacter.IsInitiator();
        _defenderCharacter.IsNotInitiator();
    }

    private void RestoreAttributesForNextRound()
    {
        _attackerCharacter.RestoreAttributesForNextRound();
        _defenderCharacter.RestoreAttributesForNextRound();
    }

    private void PrintRoundStart(int round, int attackerIndex)
    {
        _view.WriteLine("Round " + round + ": " + _attackerCharacter.Name + " (Player "+ attackerIndex+") comienza");
    }

    private void HandleMoreRounds(int round)
    {
        if (!IsFightOver())
        {
            UpdateLastOpponents();
            RestoreAttributesForNextRound();
            Fight(round + 1);
        }
        else ReportWhoWon();
    }
    public bool Fight(int round)
    {
        (int attackerIndex, int defenderIndex) = GetIndexAttackerDefender(round);
        ChooseUnits(attackerIndex, defenderIndex);
        SetAttributesOpponents();
        string attackerName = _attackerCharacter.Name;
        
        PrintRoundStart(round, attackerIndex);
        HandleDuel();
        ReportRoundOutcome(attackerIndex, attackerName);
        HandleMoreRounds(round);
        return true;
    }

    
    public Character AskUserToSelectUnit(int indexPlayer)
    {
        _view.WriteLine("Player "+ indexPlayer +" selecciona una opción");

        int index = 0;
        foreach (Character player in _teams[indexPlayer-1])
        {
            string namePlayer = player.Name;
            _view.WriteLine(index + ": "+ namePlayer);
            index++;
        }
        int indexSelected = _utilities.AskUserToSelectNumber(0, index);
        
        Character playerSelected = _teams[indexPlayer - 1][indexSelected];

        return playerSelected;
    }

}