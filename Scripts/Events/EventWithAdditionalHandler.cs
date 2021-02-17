using System;

namespace Common
{
	/// <summary>
	/// Simple event with additional handler then invoke
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EventWithAdditionalHandler<T>
	{
		private event Action<T> _internalEvent;

		public delegate void AdditionalHandlerDelegate(ref T value);
		public AdditionalHandlerDelegate AdditionalHandler;

		/// <summary>
		/// Invoke inserted event
		/// </summary>
		/// <param name="data"></param>
		public void InvokeEvent(T data)
		{
			AdditionalHandler?.Invoke(ref data);
			_internalEvent?.Invoke(data);
		}

		/// <summary>
		/// Overloading for easier access to events +=
		/// </summary>
		public static EventWithAdditionalHandler<T> operator +(EventWithAdditionalHandler<T> eventWithAdditionalHandler, Action<T> action)
		{
			eventWithAdditionalHandler._internalEvent += action;
			return eventWithAdditionalHandler;
		}

		/// <summary>
		/// Overloading for easier access to events -=
		/// </summary>
		public static EventWithAdditionalHandler<T> operator -(EventWithAdditionalHandler<T> eventWithAdditionalHandler, Action<T> action)
		{
			eventWithAdditionalHandler._internalEvent -= action;
			return eventWithAdditionalHandler;
		}
	}
}
