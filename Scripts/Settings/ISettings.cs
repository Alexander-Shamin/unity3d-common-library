using UnityEngine;

namespace Common
{
	/// <summary>
	/// Интерфейс сериализации/десериализации 
	/// </summary>
	public interface ISettings
	{
		/// <summary>
		/// Сериализация данных
		/// </summary>
		void Serialize<T>(T data) where T : class;

		/// <summary>
		/// Десериализация данных
		/// </summary>
		T Deserialize<T>() where T : class;
	}
}
