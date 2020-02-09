using Becerra.Carder;
using Becerra.Carder.Capture;
using Becerra.Save;
using UnityEngine;

public class Main : MonoBehaviour
{
    public RectTransform cardParent;
    public CardView cardView;

    private CardParser parser;
    private SaveService saveService;
    private CaptureService captureService;
    private TextAsset[] dataFiles;

    public void Awake()
    {
        parser = new CardParser();
        saveService = new SaveService(Application.dataPath + "/Output");
        captureService = new CaptureService();
        
        cardView.Initialize();

        dataFiles = Resources.LoadAll<TextAsset>("Cards");

        StartCapture();
    }

    public void StartCapture()
    {
        Capture();
    }

    private async void Capture()
    {
        foreach (var dataFile in dataFiles)
        {
            string text = dataFile.text;

            var cards = parser.SplitIntoSeparateCards(text);

            foreach (var card in cards)
            {
                HideCard(cardView);
                ShowCard(card);

                var texture = await captureService.CaptureCard(cardView);
                
                saveService.SaveTexture(texture);
            }
        }
    }

    private void ShowCard(string cardText)
    {
        CardController card = new CardController(cardText);
        
        card.Parse(parser);
        
        cardView.Show(card.model);
    }

    private void HideCard(CardView cardView)
    {
        cardView.Hide();
    }
}
