
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Becerra.Carder
{
    public class CardParser
    {
        public CardParser()
        {

        }

        public CardModel Parse(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            
            string id = string.Empty;
            CardModel model = new CardModel();

            string[] rawSections = text.Split('\n');

            for (int i = 0; i < rawSections.Length; i++)
            {
                string sectionText = rawSections[i];

                if (IsMetadata(sectionText))
                {
                    ApplyMetadata(sectionText, model);
                }
                else
                {
                    var section = ParseSection(rawSections[i]);

                    if (section != null)
                    {
                        model.sections.Add(section);
                    }
                }
            }
            
            return model;
        }

        private CardSection ParseSection(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            
            return new CardSection();
        }

        private bool IsMetadata(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            if (IsNameMetadata(text)) return true;

            string[] split = text.Split(':');

            if (split.Length < 2) return false;

            string key = split[0];

            switch (key)
            {
                case "source":
                case "front":
                case "back":
                case "categories":
                case "tags":
                return true;
            }

            return false;
        }

        private void ApplyMetadata(string text, CardModel model)
        {
            if (string.IsNullOrEmpty(text)) return;

            if (IsNameMetadata(text))
            {
                model.name = GetNameMetadata(text);
            }
            else
            {
                string key = GetMetadataKey(text);
                string value = GetMetadataValue(text);

                switch (key)
                {
                    case "categories":
                    {
                        model.categories = GetValuesList(value);
                        break;
                    }
                    case "tags":
                    {
                        model.tags = GetValuesList(value);
                        break;
                    }
                    case "front":
                    {
                        model.frontImage = value;
                        break;
                    }
                    case "back":
                    {
                        model.backImage = value;
                        break;
                    }
                    case "source":
                    {
                        model.source = value;
                        break;
                    }
                }
            }
        }

        private bool IsNameMetadata(string text)
        {
            return text.Contains("#") && text.Contains("##") == false;
        }

        private string GetNameMetadata(string text)
        {
            var regex = new Regex("(?<=# ).*");
            var match = regex.Match(text);

            return match.Value;
        }

        private string GetMetadataKey(string text)
        {
            var regex = new Regex(".*(?=: )");
            var match = regex.Match(text);

            return match.Value;
        }
        
        private string GetMetadataValue(string text)
        {
            var regex = new Regex("(?<=: ).*");
            var match = regex.Match(text);

            return match.Value;
        }

        private List<string> GetValuesList(string text)
        {
            var trimmedText = TrimValuesList(text);

            return new List<string>(trimmedText.Split(','));
        }
        
        private string TrimValuesList(string text)
        {
            var regex = new Regex(@"(?<=,)\s");

            return regex.Replace(text, string.Empty);
        }
    }
}