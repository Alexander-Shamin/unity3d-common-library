using System.IO;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// Упрощение загрузки файлов (не через Unity)
	/// </summary>
	public class FileLoader
	{
		/// <summary>
		/// Из-за разного расположения файлов - необходим простой инструмент 
		/// для их загрузки.
		/// В Editor path рассчитвается от Asset
		/// В Release от exe
		/// </summary>
		public static string DataPath()
		{
#if UNITY_EDITOR
			return Application.dataPath;
#else
			return Path.GetFullPath(".");
#endif
		}


		/// <summary>
		/// Расширение DataPath для формирования пути к файлу
		/// </summary>
		/// <param name="path"> путь к файлу (от Assets) без начального символа
		/// разделителя папки </param>
		public static string DataPath(string path)
		{
			return DataPath() + Path.DirectorySeparatorChar + path;
		}

		/// <summary>
		/// Загрузить Sprite из файла
		/// </summary>
		/// <param name="path">путь от Assets</param>
		/// <returns></returns>
		public static Sprite LoadSprite(string path_)
		{
			string path = DataPath(path_);

			Texture2D tex = null;
			byte[] fileData;

			if (System.IO.File.Exists(path))
			{
				fileData = System.IO.File.ReadAllBytes(path);
				tex = new Texture2D(2, 2);
				//..this will auto-resize the texture dimensions.
				tex.LoadImage(fileData);
			}
			else
			{
				Debug.LogError($"not find file {path}");
			}
			if (tex != null)
			{
				Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width,
										tex.height), new Vector2(0.5f, 0.5f));
				return sprite;
			}
			else
				return null;
		}

		/// <summary>
		/// Загрузка текстуры из файла
		/// </summary>
		/// <param name="path">путь к текстуре от Assets</param>
		public static Texture LoadTextureFromFile(string path_)
		{
			string path = DataPath(path_);

			Texture2D tex = null;
			byte[] fileData;

			if (System.IO.File.Exists(path))
			{
				fileData = System.IO.File.ReadAllBytes(path);
				tex = new Texture2D(2, 2);
				//..this will auto-resize the texture dimensions.
				tex.LoadImage(fileData);
			}
			else
			{
				Debug.LogError($"not find file {path}");
			}
			return tex;
		}

	} // class
}
