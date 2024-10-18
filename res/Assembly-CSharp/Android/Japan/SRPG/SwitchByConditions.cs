// Decompiled with JetBrains decompiler
// Type: SRPG.SwitchByConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
