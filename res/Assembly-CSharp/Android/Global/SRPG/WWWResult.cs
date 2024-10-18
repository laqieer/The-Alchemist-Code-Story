// Decompiled with JetBrains decompiler
// Type: SRPG.WWWResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public struct WWWResult
  {
    private WWW mResult;
    private string mResultValue;

    public WWWResult(WWW www)
    {
      this.mResult = www;
      this.mResultValue = (string) null;
    }

    public WWWResult(string result)
    {
      this.mResult = (WWW) null;
      this.mResultValue = result;
    }

    public string text
    {
      get
      {
        if (this.mResult != null)
          return this.mResult.text;
        return this.mResultValue;
      }
    }
  }
}
