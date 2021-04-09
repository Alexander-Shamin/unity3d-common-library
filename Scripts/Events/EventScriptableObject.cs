using UnityEngine;
using UnityEngine.Events;

namespace Common
{
	[CreateAssetMenu(fileName = "Event Scripatable Object", menuName = "Common/Event/Event Scriptable Object")]
    public class EventScriptableObject : ScriptableObject, IRaiseEvent
    {
		[SerializeField]
		private UnityEvent @event = new UnityEvent();

		public void Raise()
		{
			@event?.Invoke();
		}

		public void RegisterListener(UnityAction action)
		{
			@event?.AddListener(action);
		}

		public void UnregisterListener(UnityAction action)
		{
			@event?.RemoveListener(action);
		}
    } // class
}