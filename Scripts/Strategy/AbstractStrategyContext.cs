using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common
{
	/// <summary>
	/// Базовый класс для реализации стратегий через состояние
	/// 
	/// Необходимо для простой сериализации и управления стратегиями.
	/// Для реализации стратегии необходимо наследовать данный класс.
	/// 
	/// Если стратегия требует создания объекта - то их необходимо уничтожить
	/// через метод BaseStrategy::DestroyCreatedObjects
	/// </summary>
	/// <typeparam name="T"> Состояние (enum) </typeparam>
	/// <typeparam name="V"> Абстрактная стратегия </typeparam>
	[ExecuteAlways]
	public abstract class AbstractStrategyContext<T, V>
			where T : Enum
			where V : BaseStrategy
	{
		/// <summary>
		/// Текущее состояние
		/// 
		/// Необходимо, чтобы управлять временем жизни стратегии
		/// </summary>
		private T _currentState = default;
		public T State { get { return _currentState; } }

		/// <summary>
		/// Текущая стратегия
		/// </summary>
		private V _strategy = null;
		public V Strategy { get { return _strategy; } }

		/// <summary>
		/// Смена стратегии
		/// </summary>
		public void ChangeStrategy(T state, GameObject gameObject)
		{
			// Проверяем на наличие стратегии
			if (_strategy == null)
			{
				_strategy = gameObject.GetComponent<V>();

				if (_strategy == null)
					_strategy = AddStrategyByState(_currentState, gameObject);
			}

			if (!_currentState.Equals(state))
			{
				_currentState = state;
				_strategy.DestroyCreatedObjects();
				UnityEngine.Object.DestroyImmediate(_strategy);
				_strategy = AddStrategyByState(_currentState, gameObject);
			}
		}

		public void DestroyStrategy()
		{
			_strategy.Destroy(true);
		}

		/// <summary>
		/// Перегрузка метода для сопоставления состояния и класса
		/// </summary>
		protected abstract V AddStrategyByState(T state, GameObject gameObject);
	} // class
}
