// Decompiled with JetBrains decompiler
// Type: SRPG.SGButtonHighlightController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SGButtonHighlightController : MonoBehaviour
  {
    [SerializeField]
    private GameObject speedupHighlight;
    [SerializeField]
    private GameObject autoHighlight;

    private void Start()
    {
      if ((UnityEngine.Object) this.autoHighlight != (UnityEngine.Object) null && SceneBattle.Instance.Battle.RequestAutoBattle)
        this.autoHighlight.SetActive(false);
      if (!((UnityEngine.Object) this.speedupHighlight != (UnityEngine.Object) null) || !PlayerPrefs.HasKey("SPEED_UP") || PlayerPrefs.GetInt("SPEED_UP") != 1)
        return;
      this.speedupHighlight.SetActive(false);
    }
  }
}
