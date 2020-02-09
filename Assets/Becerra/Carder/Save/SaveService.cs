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

            System.IO.File.WriteAllBytes(path, bytes);

            Debug.Log("Saved texture " + texture + " at " + path);
        }
    }
}
