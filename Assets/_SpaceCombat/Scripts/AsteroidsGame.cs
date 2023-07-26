using UnityEngine;

namespace SpaceCombat
{
    public class AsteroidsGame
    {
        public const float ASTEROIDS_MIN_SPAWN_TIME = 5.0f;
        public const float ASTEROIDS_MAX_SPAWN_TIME = 10.0f;

        public const float PLAYER_RESPAWN_TIME = 4.0f;

        public const int SHIP_MAX_PROTECTION = 10;

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