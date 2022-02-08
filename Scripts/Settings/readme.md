# Settings

# Общие сведения
Настройка приложения осуществляется в Unity Editor. Обращение напрямую к файлам конфигурации допускается только в при работе в релизных версиях. Конфигурация осуществляется в ScriptableObject и RemoteConfig windows.


# Области видимости
В системе 3 области видимости настроек (`SettingScope`):
+ local: настройки, которые должны хранится только на данной машине. Место хранения `local_settings.json`
+ global: настройки, которые являются общими для всех реализаций. Место хранения `settings.json`
+ remote: настройки, которые являются общими для всех реализаций. Место хранения `RemoteConfig`

В локальных настройках существует флаг `dominationLocalSettings`, если он установлен в `true` система ищет настройки сначала в локальных настройках, затем в глобальных. 

Приоритет применения настроек: remote -> global -> local
Следует учитывать, что удаленные настройки могут быть доступны не всегда, поэтому любые данные, пришедщие из этого источника принудительно записываются в global хранилище. Флаг `dominationLocalSettings` меняет приоритет следующим образом: local -> global == remote.

# Использование системы конфигурации разработчиком

Все неоходимые данные хранятся в `ScriptableObject`. Каждый класс хранения данных создается с наследованием от `AbstractSettingSO`. При наследовании необходимо реализовать 3 метода (пример ниже):
- `GetSettings` - автоматический доступ к данным, хранимым в хранилищах настроек
- `SetSettings` - автоматическая запись данных в хранилище настроек
- `InvokeOnSettingsChanged` - метод для добавления в интерфейс кнопки отправки события
``` cs

	protected override bool GetSettings(AbstractSettingsStorageSO s)
	{
		bool somethingChanged = false;
		somethingChanged |= s.GetValueAndCheck(nameof(LightIntensity), ref LightIntensity LightIntensity, SettingPolicy.Local);

		somethingChanged |= s.GetValueAndCheck(nameof(CustomerInformation), ref CustomerInformation, CustomerInformation, SettingPolicy.Local);

		somethingChanged |= s.GetValueAndCheck(nameof(LocationInformation), ref LocationInformation, LocationInformation, SettingPolicy.Local);

		somethingChanged |= s.GetValueAndCheck(nameof(VersionInformation), ref VersionInformation, VersionInformation, SettingPolicy.Global);
		return somethingChanged;
	}

	protected override void SetSettings(AbstractSettingsStorageSO s)
	{
		s.SetValue(nameof(LightIntensity), LightIntensity, scope: SettingPolicy.Local);
		s.SetValue(nameof(CustomerInformation), CustomerInformation, scope: SettingPolicy.Local);
		s.SetValue(nameof(LocationInformation), LocationInformation, scope: SettingPolicy.Local);
		s.SetValue(nameof(VersionInformation), VersionInformation, scope: SettingPolicy.Global);
	}

	public override void InvokeOnSettingsChanged() { InvokeOnSettingsChanged(this); }
```

