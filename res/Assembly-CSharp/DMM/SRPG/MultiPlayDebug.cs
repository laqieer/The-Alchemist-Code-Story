// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayDebug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiPlayDebug : MonoBehaviour
  {
    public GameObject debuginfo;
    public Button m_DebugBtn;
    private static MultiPlayDebug mInstance;

    public static MultiPlayDebug Instance => MultiPlayDebug.mInstance;

    private void Awake()
    {
    }

    private void Start() => ((Component) this.m_DebugBtn).gameObject.SetActive(false);

    private void OnDestroy()
    {
    }

    private void OnClick()
    {
    }
  }
}
