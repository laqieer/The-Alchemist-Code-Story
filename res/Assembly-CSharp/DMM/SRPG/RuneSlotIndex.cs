// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSlotIndex
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public struct RuneSlotIndex
  {
    private byte value;
    public const byte All = 255;
    public const byte Min = 0;
    public const byte Max = 5;
    public const byte MaxCount = 6;

    public RuneSlotIndex(byte value)
    {
      if (value != byte.MaxValue && ((byte) 0 > value || value > (byte) 5))
      {
        DebugUtility.LogError("適正でないスロット番号「value=" + (object) value + "」が設定されようとしています");
        value = (byte) 0;
      }
      this.value = value;
    }

    public static implicit operator RuneSlotIndex(byte value) => new RuneSlotIndex(value);

    public static implicit operator byte(RuneSlotIndex value) => value.value;

    public static RuneSlotIndex CreateSlotToIndex(int slot) => new RuneSlotIndex((byte) (slot - 1));

    public static int IndexToSlot(int index) => index + 1;
  }
}
