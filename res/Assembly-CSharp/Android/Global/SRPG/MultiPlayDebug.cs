// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayDebug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayDebug : MonoBehaviour
  {
    public GameObject debuginfo;
    public Button m_DebugBtn;

    private void Start()
    {
      this.m_DebugBtn.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
    }

    private void OnClick()
    {
    }
  }
}
