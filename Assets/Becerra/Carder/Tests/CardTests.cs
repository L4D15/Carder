using NUnit.Framework;
using UnityEngine;

 namespace Becerra.Carder.Tests
 {
     public class Parser
     {
        public CardParser parser;
        public string fullCardText = "\n# CARD_NAME\n\ncategories: CATEGORY_A, CATEGORY_B\ntags: TAG_A, TAG_B\nsource: SOURCE_ID\nfront: FRONT_IMAGE\nback: BACK_IMAGE\n\nThis is a card doing stuff. Cool uh?\n---\n\n## ACTION_TITLE; 1,2,3,r,f\n---\n__Requisite:__ I need some stuff to work.\n---\nThis is the stuff I do.\n---\n\nBut I can do more things, like all of this:\n- Wo\n- lo\n- lo\n- You are converted.\n\nI can even print text in __bold__, **italic** or __**bold italic too**__.";

        [SetUp]
        public void Setup()
        {
            parser = new CardParser();
        }

        [Test]
        public void _1_ParseEmpty()
        {
             string text = string.Empty;
             CardModel model = parser.Parse(text);

             Assert.IsNull(model);
        }
        
        [Test]
        public void _2_Name()
        {
            string text = fullCardText;
            CardModel model = parser.Parse(text);
            
            Assert.IsNotNull(model);
            Assert.AreEqual("CARD_NAME", model.name);
        }

        [Test]
        public void _3_FrontImage()
        {
            string text = fullCardText;
            CardModel model = parser.Parse(text);
            
            Assert.AreEqual("FRONT_IMAGE", model.frontImage);
        }

        [Test]
        public void _4_BackImage()
        {
            string text = fullCardText;
            CardModel model = parser.Parse(text);

            Assert.AreEqual("BACK_IMAGE", model.backImage);
        }

        [Test]
        public void _5_Categories()
        {
            string text = fullCardText;
            CardModel model = parser.Parse(text);
            
            Assert.IsTrue(model.categories.Contains("CATEGORY_A"));
            Assert.IsTrue(model.categories.Contains("CATEGORY_B"));
        }

        [Test]
        public void _6_Tags()
        {
            string text = fullCardText;
            CardModel model = parser.Parse(text);
            
            Assert.IsTrue(model.tags.Contains("TAG_A"));
            Assert.IsTrue(model.tags.Contains("TAG_B"));
        }

        [Test]
        public void _7_Source()
        {
            string text = fullCardText;
            CardModel model = parser.Parse(text);
            
            Assert.AreEqual("SOURCE_ID", model.source);
        }
     }
 }