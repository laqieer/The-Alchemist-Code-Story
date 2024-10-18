// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawInfo : MonoBehaviour
  {
    [SerializeField]
    private Text mRuneName;
    [SerializeField]
    private ImageArray mRuneSlotNumber;
    [SerializeField]
    private Text mRuneSetEffName;
    [SerializeField]
    private Text mRuneSlotNumberText;
    private BindRuneData mRuneData;

    public void Awake()
    {
    }

    public void SetDrawParam(BindRuneData rune_data)
    {
      if (rune_data == null)
        return;
      this.mRuneData = rune_data;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mRuneData == null)
        return;
      RuneData rune = this.mRuneData.Rune;
      if (rune == null)
        return;
      RuneParam runeParam = rune.RuneParam;
      if (runeParam == null)
        return;
      ItemParam itemParam = runeParam.ItemParam;
      if (itemParam == null)
        return;
      RuneSetEff runeSetEff = runeParam.RuneSetEff;
      if (runeSetEff == null)
        return;
      if (Object.op_Implicit((Object) this.mRuneName))
        this.mRuneName.text = itemParam.name;
      if (Object.op_Implicit((Object) this.mRuneSlotNumber))
        this.mRuneSlotNumber.ImageIndex = (int) (byte) runeParam.slot_index;
      if (Object.op_Implicit((Object) this.mRuneSetEffName))
        this.mRuneSetEffName.text = runeSetEff.name;
      if (!Object.op_Implicit((Object) this.mRuneSlotNumberText))
        return;
      this.mRuneSlotNumberText.text = LocalizedText.Get("sys.RUNE_SLOT_TEXT_" + (object) RuneSlotIndex.IndexToSlot((int) (byte) runeParam.slot_index));
    }
  }
}
