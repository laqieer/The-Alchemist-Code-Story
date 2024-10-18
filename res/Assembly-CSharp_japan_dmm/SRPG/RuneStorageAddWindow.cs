// Decompiled with JetBrains decompiler
// Type: SRPG.RuneStorageAddWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneStorageAddWindow : MonoBehaviour
  {
    [SerializeField]
    private Text m_BeforeStorageSize;
    [SerializeField]
    private Text m_AfterStorageSize;
    [SerializeField]
    private Text m_Message;

    private void Start()
    {
      int currentRuneStorageSize = MonoSingleton<GameManager>.Instance.Player.CurrentRuneStorageSize;
      int num = currentRuneStorageSize + MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneStorageExpansion;
      int storageExpansion = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneStorageExpansion;
      int runeStorageMax = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneStorageMax;
      int runeStorageCoinCost = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneStorageCoinCost;
      if (Object.op_Inequality((Object) this.m_Message, (Object) null))
        this.m_Message.text = LocalizedText.Get("sys.RUNE_STORAGE_ADD_CONFIRM_TEXT", (object) runeStorageCoinCost, (object) storageExpansion, (object) runeStorageMax);
      if (Object.op_Inequality((Object) this.m_BeforeStorageSize, (Object) null))
        this.m_BeforeStorageSize.text = currentRuneStorageSize.ToString();
      if (!Object.op_Inequality((Object) this.m_AfterStorageSize, (Object) null))
        return;
      this.m_AfterStorageSize.text = num.ToString();
    }
  }
}
