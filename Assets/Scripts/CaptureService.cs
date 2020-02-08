using System.Threading.Tasks;
using UnityEngine;

public class CaptureService
{
    public async Task<Texture2D> CaptureCard(RectTransform card)
    {
        int width = Mathf.CeilToInt(card.sizeDelta.x);
        int height = Mathf.CeilToInt(card.sizeDelta.y);
        int x = Mathf.CeilToInt(Screen.width * 0.5f + card.rect.x);
        int y = Mathf.CeilToInt(Screen.height * 0.5f + card.rect.y);
        
        var capturedRect = new Rect(x, y, width, height);

        Debug.Log("Card rect " + card.rect);
        Debug.Log("Captured rect " + capturedRect);
        
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        await new WaitForEndOfFrame();

        texture.name = card.name;
        texture.ReadPixels(capturedRect, 0, 0);
        texture.Apply();

        Debug.Log("Captured card " + card);

        return texture;
    }
}
