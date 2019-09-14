using System.Threading.Tasks;
using UnityEngine;

public class CaptureService
{
    public async Task<Texture2D> CaptureCard(RectTransform card)
    {
        int x = 0;
        int y = 0;
        int width = Screen.width;
        int height = Screen.height;

        var capturedRect = new Rect(x, y, width, height);
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        await new WaitForEndOfFrame();

        texture.name = card.name;
        texture.ReadPixels(capturedRect, 0, 0);
        texture.Apply();

        Debug.Log("Captured card " + card);

        return texture;
    }
}
