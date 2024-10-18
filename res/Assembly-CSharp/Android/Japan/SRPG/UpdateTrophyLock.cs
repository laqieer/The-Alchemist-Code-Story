// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
