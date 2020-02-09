using TMPro;
using UnityEngine;

namespace Becerra.Carder
{
    public class CardBodyTextView : MonoBehaviour
    {
        public TextMeshProUGUI label;
        
        public void ShowText(string text)
        {
            // TODO: Replace Markdown tags with BBCode tags
            
            label.text = text;
            
            transform.SetAsLastSibling();
        }
    }
}