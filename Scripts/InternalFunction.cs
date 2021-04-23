using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
	/// <summary>
	/// Общие функции
	/// </summary>
	public class _
	{
		public static Texture2D TextureToTexture2D(Texture texture)
		{
			Texture2D texture2D = new Texture2D(texture.width, texture.height,
				TextureFormat.RGB24, false);
			RenderTexture currentRenderTexture = RenderTexture.active;
			RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 24);
			Graphics.Blit(texture, renderTexture);

			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
			texture2D.Apply();

			RenderTexture.active = currentRenderTexture;
			RenderTexture.ReleaseTemporary(renderTexture);
			return texture2D;
		}

		public static Sprite LoadSpriteFromFile(string path)
		{
			Texture2D tex = null;
			byte[] fileData;

			if (System.IO.File.Exists(path))
			{
				fileData = System.IO.File.ReadAllBytes(path);
				tex = new Texture2D(2, 2);
				tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			}
			else
			{
				Debug.LogError($"not find file {path}");
			}
			if (tex != null)
			{
				Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
				return sprite;
			}
			else
				return null;
		}

		public static Texture LoadTextureFromFile(string path)
		{
			Texture2D tex = null;
			byte[] fileData;

			if (System.IO.File.Exists(path))
			{
				fileData = System.IO.File.ReadAllBytes(path);
				tex = new Texture2D(2, 2);
				tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			}
			return tex;
		}

		/// <summary>
		/// Учет платформозависимого рендера
		/// </summary>
		/// <param name="isFlip"></param>
		public static void CheckPlatformSpecificRenderingDifferences(bool isFlip, RawImage rawImage)
		{
			if (isFlip)
			{
				var uvRect = rawImage.uvRect;
				uvRect.height = -1.0f;
				rawImage.uvRect = uvRect;
			}
			else
			{
				var uvRect = rawImage.uvRect;
				uvRect.height = 1.0f;
				rawImage.uvRect = uvRect;
			}
		}
	} // class
}
