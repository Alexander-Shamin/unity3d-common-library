using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class EventListener : MonoBehaviour
    {
        [Tooltip("Event object")]
        public EventScriptableObject Event;

		[Tooltip("Response to invoke when Event is raised.")]
		public UnityEvent Response;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            Event?.RegisterListener(OnRaiseEvent);
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        private void OnDisable()
        {
            Event?.UnregisterListener(OnRaiseEvent);
        }

        /// <summary>
        /// This function is called when the event raised in Event
        /// </summary>
        private void OnRaiseEvent()
        {
            Response?.Invoke();
        }

    } // class
}
