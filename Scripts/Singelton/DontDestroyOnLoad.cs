using UnityEngine;

namespace Common
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		void Start()
		{
			DontDestroyOnLoad(this);
		}
	}
}
