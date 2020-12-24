using UnityEngine;

namespace Common
{
	public abstract class SettingsScriptableObject : ScriptableObject, ISettings
	{
		public abstract void Serialize<T>(T data) where T : class;

		public abstract T Deserialize<T>() where T : class;
	}
}
