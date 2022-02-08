using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
#if USE_REMOTE_CONFIG
public class RemoteConfigUpdater : Singelton<RemoteConfigUpdater, DontDestroyOnLoadEnable>
{
  [SerializeField]
  private RemoteConfigScriptableObject _rc = null;

  [SerializeField]
  private float _secondsUpdateSettings = 900.0f;
  void Start()
  {
    Assert.IsNotNull(_rc);
    InvokeRepeating("FetchSettings", _secondsUpdateSettings, _secondsUpdateSettings);
  }

  private void FetchSettings() { _rc?.FetchRemoteSettings(); }
}  // class
#endif
}
