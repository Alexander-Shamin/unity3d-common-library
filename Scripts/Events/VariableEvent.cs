using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

	public abstract class VariableEvent<T> : GameEvent
	{
		/// <summary>
		/// Value for template
		/// </summary>
		[SerializeField]
		private T _value = default;

		public T V
		{
			get { return _value; }
			private set { _value = value; }
		}
	}
}
