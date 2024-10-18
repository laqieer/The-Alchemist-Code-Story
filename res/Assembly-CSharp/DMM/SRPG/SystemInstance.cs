// Decompiled with JetBrains decompiler
// Type: SRPG.SystemInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/System/SystemInstance")]
  public class SystemInstance : MonoSingleton<SystemInstance>
  {
    private static System.Type[] SYSTEM_INSTANCE = new System.Type[3]
    {
      typeof (GameManager),
      typeof (TimeManager),
      typeof (Network)
    };

    protected override void Initialize()
    {
      int length = SystemInstance.SYSTEM_INSTANCE.Length;
      for (int index = 0; index < length; ++index)
      {
        System.Type type = SystemInstance.SYSTEM_INSTANCE[index];
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = ((Component) this).transform;
        ((UnityEngine.Object) gameObject.transform).name = type.Name;
        gameObject.AddComponent(type);
      }
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }
  }
}
