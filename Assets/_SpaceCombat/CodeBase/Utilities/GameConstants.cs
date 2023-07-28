using UnityEngine;

namespace SpaceCombat.Utilities
{
    public class GameConstants
    {
        public const string PLAYER_READY = "IsPlayerReady";
        public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";
        public const string SHIP_PROTECTION = "PlayerHealthPoints";
        public const string WINNER_NUMBER = "WinPlayerNumber";

        public static Color GetColor(int colorChoice)
        {
            switch (colorChoice)
            {
                case 0: return Color.red;
                case 1: return Color.green;
                case 2: return Color.blue;
                case 3: return Color.yellow;
                case 4: return Color.cyan;
                case 5: return Color.grey;
                case 6: return Color.magenta;
                case 7: return Color.white;
            }

            return Color.black;
        }

        public static string GetColorName(int colorChoice)
        {
            switch (colorChoice)
            {
                case 0: return "Red";
                case 1: return "Green";
                case 2: return "Blue";
                case 3: return "Yellow";
                case 4: return "Cyan";
                case 5: return "Grey";
                case 6: return "Magneta";
                case 7: return "White";
            }

            return "Unknown";
        }
    }
}