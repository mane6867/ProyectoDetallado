using System.Runtime.InteropServices;
using Fire_Emblem_View;

using Newtonsoft.Json;
using System.Text.Json;

using System.Text.RegularExpressions;
namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;

    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        List<Character> player1;
        List<Character> player2;

    }

    static bool ContieneRepetidos<T>(List<T> lista)
    {
        // Utilizamos LINQ para agrupar los elementos de la lista por su valor y luego verificamos si algún grupo tiene más de un elemento
        return lista.GroupBy(x => x).Any(g => g.Count() > 1);
    }

    public void filesDisplayToChooseFrom()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        string[] files = Directory.GetFiles(_teamsFolder);
        for (int i = 0; i < files.Length; i++)
        {
            _view.WriteLine(i + ": " + Path.GetFileName(files[i]));
        }
    }

    public int AskUserToSelectNumber(int minValue, int maxValue)
    {
        //_view.WriteLine($"(Ingresa un número entre {minValue} y {maxValue})");
        int value;
        bool wasParsePossible;
        do
        {
            string? userInput = _view.ReadLine();
            wasParsePossible = int.TryParse(userInput, out value);
        } while (!wasParsePossible || IsValueOutsideTheValidRange(minValue, value, maxValue));

        return value;
    }

    public bool IsValueOutsideTheValidRange(int minValue, int value, int maxValue)
        => value < minValue || value > maxValue;


    public string[] GetInfoChooseFile()
    {

        //bool valido = ValidarEquipo(infoArchivoEquipo).valido;
        string pathChoosenFile = AskToChoosePathFile();
        string[] infoTeamsChoosenFile = File.ReadAllLines(pathChoosenFile);
        return infoTeamsChoosenFile;

    }

    public string AskToChoosePathFile()
    {
        string[] files = Directory.GetFiles(_teamsFolder);
        int min = 0;
        int max = files.Length - 1;
        int numberOfSelectedFile = AskUserToSelectNumber(min, max);
        return files[numberOfSelectedFile];
    }

    public static bool HasMaxTwoSkills(List<string> listOfSkills)
    {
        return listOfSkills.Count() <= 2;
    }

    public List<Character> CreateCharactersFromNames(List<string> dataCharactersPlayer)
    {
        List<string> nameOfCharactersToCreateList = new List<string>();
        List<Character> characters = new List<Character>();
        string myJson = File.ReadAllText("characters.json");
        var totalCharactersList = JsonConvert.DeserializeObject<List<Character>>(myJson);
        // acá debo extraer las habilidades
        
        foreach (var dataCharacter in dataCharactersPlayer)
        {
            string name;
            List<string> namesSkills = new List<string>();
            int index =  dataCharacter.IndexOf('(');
            if (index != -1)
            {
                // Extraer lo que está antes del paréntesis
                name = dataCharacter.Substring(0, index).Trim();
                string skillString = dataCharacter.Substring(index + 1, dataCharacter.Length - index - 2);
                //Console.WriteLine("EL STRING DE LAS SKILLS ES"+ skillString);
                namesSkills = skillString.Split(',')
                    .Select(h => h.Trim()) // Limpiar espacios
                    .ToList(); // Convertir en lista
            }
            else
            {
                // Si no hay paréntesis, se agrega la línea completa
                name =  dataCharacter.Trim();
            }
            
            Character character = totalCharactersList.FirstOrDefault(c => c.Name == name);
            
            character.AddSkills(namesSkills);
            characters.Add(character);
        }
        
        //for (int i = 0; i < nameOfCharactersToCreateList.Count; i++)
        //{
        //    Character unidad = totalCharactersList.FirstOrDefault(c => c.Name == nameOfCharactersToCreateList[i]);
        //    
        //    
        //}
        

        return characters;

    }
    public void Play()
    {
        filesDisplayToChooseFrom();
        string[] infoFileSelectedTeam = GetInfoChooseFile();

        if (!ValidateChoosenInfo(infoFileSelectedTeam))
        {
            _view.WriteLine("Archivo de equipos no válido");
        }
        else
        {

            List<Character> team1 =
                CreateCharactersFromNames(ObtainDataCharactersFromTeams(infoFileSelectedTeam).dataCharactersPlayer1);
            List<Character> team2 =
                CreateCharactersFromNames(ObtainDataCharactersFromTeams(infoFileSelectedTeam).dataCharactersPlayer2);
            List<List<Character>> teams = new List<List<Character>>();
            teams.Add(team1);
            teams.Add(team2);

            Battle batalla = new Battle(_view, teams);
            batalla.Fight(1);
        }
    }
    public static bool InfoContainTwoPlayers(string[] info)
        =>  info.Contains("Player 2 Team") && info.Contains("Player 1 Team");
    

    public static bool IsNameInNames(string name, List<string> names)
        => names.Contains(name);

    public static (List<string> dataCharactersPlayer1, List<string> dataCharactersPlayer2)
        ObtainDataCharactersFromTeams(string[] infoTeams)
    {
        bool leyendoJugador1 = false;
        bool leyendoJugador2 = false;

        List<string> dataCharactersPlayer1 = new List<string>();
        List<string> dataCharactersPlayer2 = new List<string>();

        foreach (var linea in infoTeams)
        {
            if (linea.StartsWith("Player 1"))
            {
                leyendoJugador1 = true;
            }
            else if (linea.StartsWith("Player 2"))
            {
                leyendoJugador1 = false;
            }
            else
            {
                (leyendoJugador1 ? dataCharactersPlayer1 : dataCharactersPlayer2).Add(linea.Trim());
            }
        }
        return (dataCharactersPlayer1, dataCharactersPlayer2);
    }

    public  bool isValidTeam(List<string> dataCharacters)
    {
        bool areSkillsValid = true;
        string patron = @"\((.*?)\)";
        List<string> namesCharacters = [];

        foreach (var dataCharacter in dataCharacters)
        {
            string nameCharacterOnReview = dataCharacter;
            List<string> skills = new List<string>();
            Match containHabilities = Regex.Match(dataCharacter, patron);
            if (containHabilities.Success)
            {
                nameCharacterOnReview =
                    dataCharacter.Substring(0, containHabilities.Index).Trim(); // Guarda la parte antes del paréntesis
                string habilidades =
                    dataCharacter.Remove(0, nameCharacterOnReview.Length + 1)
                        .TrimStart(); // Extrae los valores entre paréntesis y los divide por coma
                habilidades = habilidades.Trim().Trim('(', ')');
                // Divide el string utilizando la coma como separador
                skills = habilidades.Split(',').Select(s => s.Trim()).ToList();
            }

            namesCharacters.Add(nameCharacterOnReview);

            if (!HasMaxTwoSkills(skills) || ContieneRepetidos(skills))
            { 
                areSkillsValid = false;
            }
        }
        bool areNamesRepeated = ContieneRepetidos(namesCharacters);
        bool hasMinOneCharacter = namesCharacters.Count > 0;
        bool hasMaxThreeCharacters = namesCharacters.Count <= 3;
        return !areNamesRepeated && areSkillsValid && hasMinOneCharacter && hasMaxThreeCharacters;


    }
    public  bool ValidateTeams(List<string> dataCharactersPlayer1, List<string> dataCharactersPlayer2)
    {
        return isValidTeam(dataCharactersPlayer1) && isValidTeam(dataCharactersPlayer2);
    }

    public bool ValidateChoosenInfo(string[] infoTeams)
    {
        (List<string> dataCharactersPlayer1, List<string> dataCharactersPlayer2) =
            ObtainDataCharactersFromTeams(infoTeams);
        return InfoContainTwoPlayers(infoTeams) && ValidateTeams(dataCharactersPlayer1, dataCharactersPlayer2);

    }
}