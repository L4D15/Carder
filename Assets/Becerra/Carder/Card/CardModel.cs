using System.Collections.Generic;

namespace Becerra.Carder
{
    public class CardModel
    {
        public List<CardSection> sections;
        public List<string> categories;
        public List<string> tags;

        // Metadata
        public string name;
        public string frontImage;
        public string backImage;
        public string source;
        
        public CardModel()
        {
            this.sections = new List<CardSection>();
            this.categories = new List<string>();
            this.tags = new List<string>();
        }
    }
}