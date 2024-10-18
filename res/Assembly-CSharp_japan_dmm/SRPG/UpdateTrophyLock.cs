// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class UpdateTrophyLock
  {
    private int lock_count = 1;

    public bool IsLock => 0 < this.lock_count;

    public void LockClear() => this.lock_count = 0;

    public void Lock() => ++this.lock_count;

    public void Unlock()
    {
      if (0 >= this.lock_count)
        return;
      --this.lock_count;
    }
  }
}
