using UnityEngine;

namespace Common
{
	/// <summary>
	/// Абстрактный класс настроек, хранимый в виде ScriptableObject
	/// </summary>
	/// <remarks>
	/// Для использования необходимо:
	/// - наследоваться от данного класса;
	/// - добавить необходимые значения;
	/// - реализовать метод UpdateSettings(AbstractSettings s) и реализовать механизм обновления данных через s
	/// - добавить создание через [CreateAssetMenu(fileName = "Name", menuName = "Common/Settings/Name")]
	/// - создать scriptableObject через меню
	/// 
	/// Общая логика настроек следующая:
	/// - сохранение всех настроек в отдельном хранилище (scriptableObject)
	/// - простой доступ из билда и editor
	/// - при старте, проект сам читает настройки из start, плюс есть возможность среагировать на изменение настроек (каллбек)
	/// </remarks>
	public abstract class AbstractSettingScriptableObject : EventScriptableObject
	{
		[SerializeField]
		protected AbstractSettings settings = null;

		/// <summary>
		/// Абстрактная функция обновления настроек
		/// </summary>
		/// <param name="s">Интерфейс доступа к настройкам</param>
		protected abstract void GetSettings(AbstractSettings s);
		/// <summary>
		/// Абстрактная функция обновления настроек, должна быть реализована в классе наследнике
		/// </summary>
		/// <param name="s">Подразумевается, что будет вызваны необходимые установки Set, а затем Save</param>
		protected abstract void SetSettings(AbstractSettings s);

		private void OnEnable()
		{
			if (settings != null)
			{
				settings.OnSettingsChanged += AbstractSettings_OnSettingsChanged;
				settings?.Load(this);
			}
		}

		private void OnDisable()
		{
			if (settings != null)
			{
				settings?.Save();
				settings.OnSettingsChanged -= AbstractSettings_OnSettingsChanged;
			}
		}

		// У нас инверсия считывания данных при старте, чтобы не было множество лишних вызвов, добавил caller
		private void AbstractSettings_OnSettingsChanged(AbstractSettingScriptableObject caller)
		{
			if (caller == null || caller == this)
			{
				GetSettings(settings);
				Raise();
			}
		}

		public void Save()
		{
			settings?.Save();
		}

		public void Load()
		{
			settings?.Load();
		}

		private void OnValidate()
		{
			if (settings != null)
			{
				SetSettings(settings);
				Save();
			}
		}
	} // class
}
