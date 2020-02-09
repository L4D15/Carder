using System.Text.RegularExpressions;

namespace Becerra.Carder
{
    public class CardSectionActionTitle : CardSection
    {
        private const string TitleRegularExpression = @"(?<=## ).*(?=;)";    // Matches between ## and ; (non inclusive, last one optional)
        private const string CostsRegularExpression = @"(?<=;\s+).*";

        public string Title { get; private set; }
        public ActionCostType[] Costs { get; private set; }
        
        public CardSectionActionTitle(string rawText)
            : base(rawText)
        {
            var regex = new Regex(TitleRegularExpression);
            var result = regex.Match(rawText);

            Title = result.Value;
            
            regex = new Regex(CostsRegularExpression);
            result = regex.Match(rawText);

            var listOfCosts = CardParser.GetValuesList(result.Value);
            Costs = new ActionCostType[listOfCosts.Count];

            for (int i = 0; i < listOfCosts.Count; i++)
            {
                Costs[i] = FromStringToCost(listOfCosts[i]);
            }
        }

        private ActionCostType FromStringToCost(string text)
        {
            switch (text)
            {
                case "f": return ActionCostType.Free;
                case "1": return ActionCostType.OneAction;
                case "2": return ActionCostType.TwoActions;
                case "3": return ActionCostType.ThreeActions;
                case "r": return ActionCostType.Reaction;
            }

            return ActionCostType.Unknown;
        }

        public static bool IsOfType(string text)
        {
            var regex = new Regex(TitleRegularExpression);
            var result = regex.Match(text);

            return result.Success;
        }
    }
}