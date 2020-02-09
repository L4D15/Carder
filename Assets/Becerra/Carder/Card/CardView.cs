using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Becerra.Carder
{
    public class CardView : MonoBehaviour
    {
        public TextMeshProUGUI frontNameLabel;
        public TextMeshProUGUI backNameLabel;

        public Image frontImage;
        public Image backImage;
        
        public Transform frontCategoriesParent;
        public CardCategoryView frontCategoryPrefab;
        
        public Transform backCategoriesParent;
        public CardCategoryView backCategoryPrefab;

        public Transform tagsContainer;
        public CardTagView tagPrefab;

        public TextMeshProUGUI sourceLabel;

        public Transform sectionsContainer;
        public CardBodyTextView bodyTextPrefab;
        public CardSeparatorView separatorPrefab;
        public CardActionTitleView actionTitlePrefab;
        public CardListItemView listItemPrefab;

        private Pool<CardCategoryView> frontCategoriesPool;
        private Pool<CardCategoryView> backCategoriesPool;
        private Pool<CardTagView> tagsPool;
        private Pool<CardBodyTextView> textSectionPool;
        private Pool<CardSeparatorView> separatorSectionPool;
        private Pool<CardActionTitleView> actionTitleSectionPool;
        private Pool<CardListItemView> listItemPool;
        
        public CardModel Model { get; private set; }
        
        public void Initialize()
        {
            frontCategoriesPool = new Pool<CardCategoryView>(frontCategoryPrefab, frontCategoriesParent, 4);
            backCategoriesPool = new Pool<CardCategoryView>(backCategoryPrefab, backCategoriesParent, 4);
            tagsPool = new Pool<CardTagView>(tagPrefab, tagsContainer, 6);
            textSectionPool = new Pool<CardBodyTextView>(bodyTextPrefab, sectionsContainer, 4);
            separatorSectionPool = new Pool<CardSeparatorView>(separatorPrefab, sectionsContainer, 4);
            actionTitleSectionPool = new Pool<CardActionTitleView>(actionTitlePrefab, sectionsContainer, 2);
            listItemPool = new Pool<CardListItemView>(listItemPrefab, sectionsContainer, 10);
        }

        public void Dispose()
        {
            frontCategoriesPool.Dispose();
            backCategoriesPool.Dispose();
            tagsPool.Dispose();
            textSectionPool.Dispose();
            separatorSectionPool.Dispose();
            actionTitleSectionPool.Dispose();
            listItemPool.Dispose();
        }
        
        public void Show(CardModel model)
        {
            Model = model;
            
            ShowName(model.name);
            ShowFrontCategories(model.categories);
            ShowBackCategories(model.categories);
            ShowSource(model.source);
            ShowTags(model.tags);
            ShowSections(model.sections);
        }

        public void Hide()
        {
            Model = null;
            
            frontNameLabel.text = string.Empty;
            backNameLabel.text = string.Empty;
            frontImage.sprite = null;
            backImage.sprite = null;
            
            frontCategoriesPool.Reset();
            backCategoriesPool.Reset();
            tagsPool.Reset();
            textSectionPool.Reset();
            separatorSectionPool.Reset();
            actionTitleSectionPool.Reset();
            listItemPool.Reset();
        }

        private void ShowName(string name)
        {
            frontNameLabel.text = name;
            backNameLabel.text = name;
        }

        private void ShowFrontCategories(IEnumerable<string> categories)
        {
            foreach (var category in categories)
            {
                var categoryView = frontCategoriesPool.Spawn();

                categoryView.Show(category);
            }
        }

        private void ShowBackCategories(IEnumerable<string> categories)
        {
            foreach (var category in categories)
            {
                var categoryView = backCategoriesPool.Spawn();

                categoryView.Show(category);
            }
        }

        private void ShowTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                var tagView = tagsPool.Spawn();
                
                tagView.Show(tag);
            }
        }

        private void ShowFrontImage(string imageName)
        {
            // TODO
        }

        private void ShowBackImage(string imageName)
        {
            // TODO
        }

        private void ShowSource(string source)
        {
            sourceLabel.text = source;
        }

        private void ShowSections(IEnumerable<CardSection> sections)
        {
            foreach (var section in sections)
            {
                if (section is CardSectionText textSection)
                {
                    textSectionPool.Spawn().ShowText(textSection.BodyText);
                }
                else if (section is CardSectionSeparator separatorSection)
                {
                    separatorSectionPool.Spawn().Show(separatorSection);
                }
                else if (section is CardSectionActionTitle titleSection)
                {
                    actionTitleSectionPool.Spawn().Show(titleSection.Title, titleSection.Costs);
                }
                else if (section is CardSectionListItem listItemSection)
                {
                    listItemPool.Spawn().Show(listItemSection.Text);
                }
            }
        }
    }
}