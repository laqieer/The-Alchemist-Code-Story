// Decompiled with JetBrains decompiler
// Type: SRPG.FlagManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public struct FlagManager
  {
    private int box;

    private bool Check(int id)
    {
      if (id < 32)
        return true;
      DebugUtility.LogError("BoolManager: over is max id [" + (object) id + "]");
      return false;
    }

    public void Set(int id, bool flag)
    {
      if (flag)
        this.True(id);
      else
        this.False(id);
    }

    private void True(int id)
    {
      if (!this.Check(id))
        return;
      this.box |= 1 << id;
    }

    private void False(int id)
    {
      if (!this.Check(id))
        return;
      this.box &= ~(1 << id);
    }

    public bool Is(int id) => (this.box & 1 << id) != 0;
  }
}
