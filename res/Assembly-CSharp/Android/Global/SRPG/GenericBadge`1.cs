// Decompiled with JetBrains decompiler
// Type: SRPG.GenericBadge`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  internal class GenericBadge<T> where T : class
  {
    public bool mValue;
    public T mRawData;

    public GenericBadge(T data, bool value = false)
    {
      this.mRawData = data;
      this.mValue = value;
    }
  }
}
