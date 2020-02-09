using TMPro;
using UnityEngine;

namespace Becerra.Carder
{
    public class CardListItemView : MonoBehaviour
    {
        public TextMeshProUGUI label;
        
        public void Show(string itemText)
        {
            label.text = itemText;
            
            transform.SetAsLastSibling();
        }
    }
}