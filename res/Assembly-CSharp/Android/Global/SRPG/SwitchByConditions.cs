// Decompiled with JetBrains decompiler
// Type: SRPG.SwitchByConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class SwitchByConditions : MonoBehaviour
  {
    [SerializeField]
    public int lv = 10;

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.Player.Lv >= this.lv)
        return;
      this.gameObject.SetActive(false);
    }
  }
}
