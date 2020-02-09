using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Becerra.Carder
{
    public class CardBodyTextView : MonoBehaviour
    {
        public TextMeshProUGUI label;
        
        public void ShowText(string text)
        {
            text = ConvertBold(text);
            text = ConvertItalic(text);
            
            label.text = text;
            
            transform.SetAsLastSibling();
        }

        private string ConvertBold(string sourceText)
        {
            string result = sourceText;

            var regex = new Regex(@"__.*?__");
            var preMarkRegex = new Regex(@"__(?=.+?)");
            var postMarkRegex = new Regex(@"(?<=.+?)__");
            var matches = regex.Matches(sourceText);

            foreach (Match match in matches)
            {
                string originalText = match.Value;
                string convertedText = originalText;
                
                convertedText = preMarkRegex.Replace(convertedText, "<b>");
                convertedText = postMarkRegex.Replace(convertedText, "</b>");
                
                result = result.Replace(originalText, convertedText);
            }

            return result;
        }
        
        private string ConvertItalic(string sourceText)
        {
            string result = sourceText;

            var regex = new Regex(@"\*\*.*?\*\*");
            var preMarkRegex = new Regex(@"\*\*(?=.+?)");
            var postMarkRegex = new Regex(@"(?<=.+?)\*\*");
            var matches = regex.Matches(sourceText);

            foreach (Match match in matches)
            {
                string originalText = match.Value;
                string convertedText = originalText;
                
                convertedText = preMarkRegex.Replace(convertedText, "<i>");
                convertedText = postMarkRegex.Replace(convertedText, "</i>");

                result = result.Replace(originalText, convertedText);
            }

            return result;
        }

        private struct ConversionData
        {
            public int index;
            public string text;
        }
    }
}