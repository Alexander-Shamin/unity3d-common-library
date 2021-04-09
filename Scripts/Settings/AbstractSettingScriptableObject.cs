using UnityEngine;

namespace Common
{
    /// <summary>
    /// Абстрактный класс настроек, хранимый в виде ScriptableObject
    /// 
    /// Для использования необходимо:
    /// - наследоваться от данного класса;
    /// - добавить необходимые значения;
    /// - реализовать метод UpdateSettings(AbstractSettings s) и реализовать механизм обновления данных через s
    /// - добавить создание через [CreateAssetMenu(fileName = "Name", menuName = "Common/Settings/Name")]
    /// </summary>
    public abstract class AbstractSettingScriptableObject : EventScriptableObject
    {
        [SerializeField]
        protected AbstractSettings settings = null;

        private void OnEnable()
        {
            if(settings != null)
                settings.OnSettingsChanged += AbstractSettings_OnSettingsChanged;
        }

        private void OnDisable()
        {
            if(settings != null)
                settings.OnSettingsChanged -= AbstractSettings_OnSettingsChanged;
        }

        private void AbstractSettings_OnSettingsChanged()
        {
           if(UpdateSettings(settings)) 
            Raise();
        }
        
        public void Save()
        {
            settings?.Save();
        }

        public void Load()
        {
            settings?.Load();
        }

        /// <summary>
        /// Абстрактная функция обновления настроек
        /// </summary>
        /// <param name="s">Интерфейс доступа к настройкам</param>
        /// <returns>
        /// - true -> выслать событие обновления данных
        /// - false -> не высылать событие обновления данных
        /// </returns>
        protected abstract bool UpdateSettings(AbstractSettings s);

    } // class
}
