using TMPro;
using UnityEngine;

namespace Becerra.Carder
{
    public class CardActionTitleView : MonoBehaviour
    {
        public TextMeshProUGUI label;
        public GameObject oneActionIcon;
        public GameObject twoActionsIcon;
        public GameObject threeActionsIcon;
        public GameObject freeActionIcon;
        public GameObject reactionIcon;
        
        public void Show(string titleName, ActionCostType[] costs)
        {
            label.text = titleName;
            
            oneActionIcon.SetActive(HasCost(ActionCostType.OneAction, costs));
            twoActionsIcon.SetActive(HasCost(ActionCostType.TwoActions, costs));
            threeActionsIcon.SetActive(HasCost(ActionCostType.ThreeActions, costs));
            freeActionIcon.SetActive(HasCost(ActionCostType.Free, costs));
            reactionIcon.SetActive(HasCost(ActionCostType.Reaction, costs));
            
            transform.SetAsLastSibling();
        }

        private bool HasCost(ActionCostType cost, ActionCostType[] costs)
        {
            foreach (var value in costs)
            {
                if (cost == value) return true;
            }

            return false;
        }
    }
}