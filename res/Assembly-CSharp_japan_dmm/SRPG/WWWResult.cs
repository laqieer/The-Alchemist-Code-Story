// Decompiled with JetBrains decompiler
// Type: SRPG.WWWResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine.Networking;

#nullable disable
namespace SRPG
{
  public struct WWWResult
  {
    private UnityWebRequest mResult;
    private string mResultValue;
    public byte[] rawResult;

    public WWWResult(UnityWebRequest www)
    {
      this.mResult = www;
      this.mResultValue = (string) null;
      this.rawResult = (byte[]) null;
    }

    public WWWResult(string result)
    {
      this.mResult = (UnityWebRequest) null;
      this.mResultValue = result;
      this.rawResult = (byte[]) null;
    }

    public WWWResult(byte[] rawResult)
    {
      this.mResult = (UnityWebRequest) null;
      this.mResultValue = (string) null;
      this.rawResult = rawResult;
    }

    public string text
    {
      get => this.mResult != null ? this.mResult.downloadHandler.text : this.mResultValue;
    }
  }
}
