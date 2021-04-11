using UnityEngine;
using UnityEngine.Events;

namespace Common
{

	/// <summary>
	/// Абстрактный класс хранения значений настроек
	/// </summary>
	/// <remarks>
	/// Представляемый функционал:
	///  - событие изменения настроек
	///  - функции запроса и установки настроек
	///  - функции сохранения и загрузки настроек
	/// </remarks>
	public abstract class AbstractSettings : ScriptableObject
	{
		public event UnityAction OnSettingsChanged;
		protected void InvokeOnSettingsChanged()
		{
			OnSettingsChanged?.Invoke();
		}

		public abstract T GetValue<T>(string name, T defaultValue = default, TypeScopeSettings scope = TypeScopeSettings.Global);
		public T GetValue<T>(string name, string module, T defaultValue = default, TypeScopeSettings scope = TypeScopeSettings.Global)
		{
			return GetValue<T>(module + "_" + name, default, scope);
		}

		public abstract void SetValue<T>(string name, T value, TypeScopeSettings scope = TypeScopeSettings.Global);
		public void SetValue<T>(string name, string module, T value, TypeScopeSettings scope = TypeScopeSettings.Global)
		{
			SetValue<T>(module + "_" + name, value, scope);
		}

		public abstract void Save();
		public abstract void Load();
	} // class
}
