using UnityEngine;
using UnityEngine.Events;

namespace Common
{

	/// <summary>
	/// Абстрактный класс хранения значений настроек
	///  
	/// Представляемый функционал:
	///  - событие изменения настроек
	///  - функции запроса и установки настроек
	///  - функции сохранения и загрузки настроек
	/// </summary>
	public abstract class AbstractSettings : ScriptableObject
	{
		public event UnityAction OnSettingsChanged;
		protected void InvokeOnSettingsChanged()
		{
			OnSettingsChanged?.Invoke();
		}

		public abstract T GetValue<T>(string name, T defaultValue = default);
		public T GetValue<T>(string name, string module)
		{
			return GetValue<T>(module + "_" + name);
		}

		public abstract void SetValue<T>(string name, T value);
		public void SetValue<T>(string name, string module, T value)
		{
			SetValue<T>(module + "_" + name, value);
		}

		public abstract void Save();
		public abstract void Load();
	} // class
}
