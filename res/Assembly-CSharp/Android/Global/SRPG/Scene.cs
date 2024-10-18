// Decompiled with JetBrains decompiler
// Type: SRPG.Scene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public abstract class Scene : MonoBehaviour
  {
    protected bool IsLoaded { get; set; }

    protected void Awake()
    {
      MonoSingleton<SystemInstance>.Instance.Ensure();
      GameUtility.RemoveDuplicatedMainCamera();
    }
  }
}
