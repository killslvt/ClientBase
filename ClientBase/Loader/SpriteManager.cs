using UnityEngine;

namespace ClientBase.Loader
{
    internal static class SpriteManager
    {
        public static Sprite LoadSpriteFromDisk(this string path, int width = 512, int height = 512)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            byte[] array = System.IO.File.ReadAllBytes(path);
            if (array == null || array.Length == 0)
            {
                return null;
            }

            Texture2D texture2D = new Texture2D(width, height);
            if (!ImageConversion.LoadImage(texture2D, array))
            {
                return null;
            }

            Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f), 100000f, 1000u, SpriteMeshType.FullRect, Vector4.zero, generateFallbackPhysicsShape: false);
            sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            return sprite;
        }
    }
}
