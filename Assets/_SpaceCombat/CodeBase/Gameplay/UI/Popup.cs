using UnityEngine;
using UnityEngine.UI;

namespace SpaceCombat.Gameplay.UI
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void UpdateText(string text, Color textColor)
        {
            _text.color = textColor;
            _text.text = text;
        }
    }
}