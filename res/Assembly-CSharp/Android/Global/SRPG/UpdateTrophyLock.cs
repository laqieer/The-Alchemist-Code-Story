// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class UpdateTrophyLock
  {
    private int lock_count = 1;

    public bool IsLock
    {
      get
      {
        return 0 < this.lock_count;
      }
    }

    public void LockClear()
    {
      this.lock_count = 0;
    }

    public void Lock()
    {
      ++this.lock_count;
    }

    public void Unlock()
    {
      if (0 >= this.lock_count)
        return;
      --this.lock_count;
    }
  }
}
