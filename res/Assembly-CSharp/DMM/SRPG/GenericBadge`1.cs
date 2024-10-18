// Decompiled with JetBrains decompiler
// Type: SRPG.GenericBadge`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
