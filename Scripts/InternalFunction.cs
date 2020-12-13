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

		/// <summary>
		/// Проверка флага и в случае false -> выход из программы
		/// </summary>
		public static void Require(bool flag, string str)
		{
			if (!flag)
			{
				Debug.LogError(str);
#if UNITY_EDITOR
        Debug.DebugBreak();
#else
				Application.Quit();
#endif
			}
		}

		public static void DebugErrorException(System.Exception exc, string str = "Exception! ")
		{
			Debug.LogError(str + exc.Message + exc.StackTrace);
		}

		static public void LogError(string message, MethodBase mb)
		{
			Debug.LogError($"{mb.Name} :: {mb.DeclaringType} :: {message}");
		}
		static public void LogError(string message)
		{
			Debug.LogError($"{message}");
		}

		static public void Log(string message, MethodBase mb)
		{
			Debug.Log($"{mb.Name} :: {mb.DeclaringType} :: {message}");
		}
		static public void Log(string message)
		{
			Debug.Log($"{message}");
		}

		static public void LogWarning(string message, MethodBase mb)
		{
			Debug.LogWarning($"{mb.Name} :: {mb.DeclaringType} :: {message}");
		}

		static public void LogWarning(string message)
		{
			Debug.LogWarning($"{message}");
		}

        static public void LogException(System.Exception exception, MethodBase mb)
        {
            Debug.LogError($"{mb.Name} :: {mb.DeclaringType} :: Exception \n {exception.Message} \n {exception.StackTrace}");
        }

        static public void LogException(System.Exception exception)
        {
            Debug.LogError($"Exception \n {exception.Message} \n {exception.StackTrace}");
        }

        /// <summary>
        /// if object == null LogError(message, mb) => false
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <param name="mb"></param>
        static public bool CheckNull(object obj, string message, MethodBase mb)
        {
            if (obj == null)
            {
                _.LogError(message, mb);
                return true;
            }
            return false;
        }

        /// <summary>
        /// if object == null LogError(message)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        static public bool CheckNull(object obj, string message)
        {
            if (obj == null)
            {
                _.LogError(message);
                return true;
            }
            return false;
        }
        
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
        public static void  CheckPlatformSpecificRenderingDifferences(bool isFlip, RawImage rawImage)
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

        
        public static T  GetComponentInChildred<T>(Component obj, string message, MethodBase mb)
        {
            var component = obj.GetComponentInChildren<T>();
            CheckNull(component, message, mb);
            return component;
        }

    } // class
}
