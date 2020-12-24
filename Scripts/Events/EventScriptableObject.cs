using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// Abstract class event with ScriptableObject
	/// </summary>
	public abstract class EventScriptableObject : ScriptableObject
	{
		public abstract void Raise();
	}
}
