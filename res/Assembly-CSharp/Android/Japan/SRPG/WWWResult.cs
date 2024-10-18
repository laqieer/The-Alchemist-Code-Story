// Decompiled with JetBrains decompiler
// Type: SRPG.WWWResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
