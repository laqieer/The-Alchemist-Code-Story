// Decompiled with JetBrains decompiler
// Type: SRPG.GameObjectCallBack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  public class GameObjectCallBack : MonoBehaviour
  {
    private UnityEvent mOnDestroy = new UnityEvent();
    private UnityEvent mOnEnable = new UnityEvent();
    private UnityEvent mOnDisable = new UnityEvent();
    private UnityEvent mOnStart = new UnityEvent();

    public UnityEvent onDestroy => this.mOnDestroy;

    public UnityEvent onEnable => this.mOnEnable;

    public UnityEvent onDisable => this.mOnDisable;

    public UnityEvent onStart => this.mOnStart;

    private void Awake()
    {
    }

    private void Start()
    {
      if (this.mOnStart == null)
        return;
      this.mOnStart.Invoke();
    }

    private void OnEnable()
    {
      if (this.mOnEnable == null)
        return;
      this.mOnEnable.Invoke();
    }

    private void OnDisable()
    {
      if (this.mOnDisable == null)
        return;
      this.mOnDisable.Invoke();
    }

    private void OnDestroy()
    {
      this.mOnDestroy.Invoke();
      ((UnityEventBase) this.mOnDestroy).RemoveAllListeners();
      ((UnityEventBase) this.mOnEnable).RemoveAllListeners();
      ((UnityEventBase) this.mOnDisable).RemoveAllListeners();
      ((UnityEventBase) this.mOnStart).RemoveAllListeners();
    }

    private void OnApplicationQuit()
    {
      ((UnityEventBase) this.mOnDestroy).RemoveAllListeners();
      ((UnityEventBase) this.mOnEnable).RemoveAllListeners();
      ((UnityEventBase) this.mOnDisable).RemoveAllListeners();
      ((UnityEventBase) this.mOnStart).RemoveAllListeners();
    }
  }
}
