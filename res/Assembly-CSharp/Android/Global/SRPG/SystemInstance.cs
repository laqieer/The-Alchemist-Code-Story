// Decompiled with JetBrains decompiler
// Type: SRPG.SystemInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/System/SystemInstance")]
  public class SystemInstance : MonoSingleton<SystemInstance>
  {
    private static System.Type[] SYSTEM_INSTANCE = new System.Type[3]{ typeof (GameManager), typeof (TimeManager), typeof (Network) };

    protected override void Initialize()
    {
      DeployGateSDK.Install();
      int length = SystemInstance.SYSTEM_INSTANCE.Length;
      for (int index = 0; index < length; ++index)
      {
        System.Type type = SystemInstance.SYSTEM_INSTANCE[index];
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = this.transform;
        gameObject.transform.name = type.Name;
        gameObject.AddComponent(type);
      }
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }
  }
}
