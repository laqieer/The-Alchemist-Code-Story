// Decompiled with JetBrains decompiler
// Type: SRPG.BaseObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public abstract class BaseObject
  {
    private bool mInitialized;
    private bool mPaused;

    public bool IsInitialized
    {
      set
      {
        this.mInitialized = value;
      }
      get
      {
        return this.mInitialized;
      }
    }

    public bool IsPaused
    {
      set
      {
        this.mPaused = value;
      }
      get
      {
        return this.mPaused;
      }
    }

    public virtual bool Load()
    {
      return true;
    }

    public virtual void Release()
    {
    }

    public virtual void Update()
    {
    }
  }
}
