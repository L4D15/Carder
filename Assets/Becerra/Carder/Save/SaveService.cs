using UnityEngine;

namespace Becerra.Save
{
    public class SaveService
    {
        public readonly string savePath;

        public SaveService(string savePath)
        {
            this.savePath = savePath;
        }

        public void SaveTexture(Texture2D texture)
        {
            var bytes = texture.EncodeToPNG();
            string path = savePath + "/" + texture.name + ".png";

            Debug.Log("Saving texture " + texture + " at " + path);
            
            System.IO.File.WriteAllBytes(path, bytes);
        }
    }
}
