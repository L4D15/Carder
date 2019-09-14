using UnityEngine;

public class Main : MonoBehaviour
{
    public RectTransform cardParent;

    private SaveService saveService;
    private CaptureService captureService;
    private GameObject[] cards;

    public void Awake()
    {
        saveService = new SaveService(Application.dataPath + "/Output");
        captureService = new CaptureService();

        cards = Resources.LoadAll<GameObject>("LargeCards");

        StartCapture();
    }

    public void StartCapture()
    {
        Capture();
    }

    private async void Capture()
    {
        foreach (var card in cards)
        {
            Debug.Log("Loading card " + card.name);

            var cardInScene = Instantiate(card, cardParent);
            cardInScene.name = cardInScene.name.Replace("(Clone)", string.Empty);

            var rect = cardInScene.GetComponent<RectTransform>();

            var texture = await captureService.CaptureCard(rect);

            saveService.SaveTexture(texture);

            Destroy(cardInScene.gameObject);
        }
    }
}
