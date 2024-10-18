// Decompiled with JetBrains decompiler
// Type: SRPG.DownloadLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace SRPG
{
  public class DownloadLogger : DownloadHandlerScript
  {
    private string mParam = string.Empty;
    private VersusAudienceManager mManager;

    public VersusAudienceManager Manager
    {
      set
      {
        this.mManager = value;
      }
    }

    public string Response
    {
      get
      {
        return this.mParam;
      }
    }

    protected override bool ReceiveData(byte[] data, int dataLength)
    {
      if (data == null || data.Length < 1)
      {
        Debug.Log((object) "LoggingDownloadHandler :: ReceiveData - received a null/empty buffer");
        return false;
      }
      string str1 = Encoding.UTF8.GetString(data, 0, dataLength).Replace("\r\n", string.Empty).Replace("\x0005", string.Empty);
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

    protected override void CompleteContent()
    {
      if (this.mManager != null)
        this.mManager.Disconnect();
      Debug.Log((object) this.mParam);
    }
  }
}
