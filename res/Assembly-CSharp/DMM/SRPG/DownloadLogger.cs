// Decompiled with JetBrains decompiler
// Type: SRPG.DownloadLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
namespace SRPG
{
  public class DownloadLogger : DownloadHandlerScript
  {
    private VersusAudienceManager mManager;
    private string mParam = string.Empty;

    public VersusAudienceManager Manager
    {
      set => this.mManager = value;
    }

    public string Response => this.mParam;

    protected virtual bool ReceiveData(byte[] data, int dataLength)
    {
      if (data == null || data.Length < 1)
      {
        Debug.Log((object) "LoggingDownloadHandler :: ReceiveData - received a null/empty buffer");
        return false;
      }
      string str1 = Encoding.UTF8.GetString(data, 0, dataLength).Replace("\r\n", string.Empty).Replace("\u0005", string.Empty);
      string[] strArray = str1.Split('\n');
      if (this.mManager != null)
      {
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (!string.IsNullOrEmpty(strArray[index]))
          {
            string str2 = strArray[index];
            char[] chArray = new char[1]{ '\r' };
            foreach (string data1 in str2.Split(chArray))
              this.mManager.Add(data1);
          }
        }
      }
      this.mParam = str1;
      return true;
    }

    protected virtual void CompleteContent()
    {
      if (this.mManager != null)
        this.mManager.Disconnect();
      Debug.Log((object) this.mParam);
    }
  }
}
