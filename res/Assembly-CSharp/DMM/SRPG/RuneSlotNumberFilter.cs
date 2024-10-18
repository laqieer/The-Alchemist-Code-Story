// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSlotNumberFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneSlotNumberFilter : MonoBehaviour
  {
    [SerializeField]
    private RuneManager mRuneManager;
    [SerializeField]
    private ImageArray[] mRuneSlotNumberImg;
    [SerializeField]
    private ImageArray mRuneSlotAllImg;

    private void Awake()
    {
    }

    public void Initialize()
    {
    }

    public void SelectedSlot(RuneSlotIndex slot_index)
    {
      if (this.mRuneSlotNumberImg == null || Object.op_Equality((Object) this.mRuneSlotAllImg, (Object) null))
        return;
      if ((byte) 0 <= (byte) slot_index && (byte) slot_index <= (byte) 5)
      {
        this.mRuneSlotAllImg.ImageIndex = 0;
        for (int index = 0; index < this.mRuneSlotNumberImg.Length; ++index)
          this.mRuneSlotNumberImg[index].ImageIndex = index == (int) (byte) slot_index ? 1 : 0;
      }
      else
      {
        this.mRuneSlotAllImg.ImageIndex = 1;
        for (int index = 0; index < this.mRuneSlotNumberImg.Length; ++index)
          this.mRuneSlotNumberImg[index].ImageIndex = 0;
      }
    }
  }
}
