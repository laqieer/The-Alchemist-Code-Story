// Decompiled with JetBrains decompiler
// Type: LogKit.Writter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MiniJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace LogKit
{
  public class Writter
  {
    private static readonly DateTime timeStampDelta = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private readonly List<Buffer> mBuffers = new List<Buffer>();
    private readonly string key;

    public Writter(string key) => this.key = key;

    public void Push(Buffer buffer)
    {
      this.RegistDeviceID(buffer);
      lock ((object) this.mBuffers)
        this.mBuffers.Add(buffer);
    }

    public void RegistDeviceID(Buffer buffer)
    {
      try
      {
        buffer.mDeviceID = Logger.DeviceID;
      }
      catch
      {
      }
    }

    public void Flush()
    {
      lock ((object) this.mBuffers)
      {
        for (int index1 = 0; index1 < this.mBuffers.Count; ++index1)
        {
          Buffer mBuffer = this.mBuffers[index1];
          List<object> objectList = new List<object>(mBuffer.Count);
          for (int index2 = 0; index2 < mBuffer.Count; ++index2)
          {
            Log log = mBuffer[index2];
            objectList.Add((object) new Dictionary<string, object>()
            {
              {
                "id",
                (object) log.ID.ToString()
              },
              {
                "tag",
                (object) string.Format("{0}.{1}", (object) this.key, (object) log.Tag)
              },
              {
                "timestamp",
                (object) (log.Date - Writter.timeStampDelta).TotalSeconds
              },
              {
                "log_level",
                (object) log.LogLevel
              },
              {
                "device_id",
                (object) mBuffer.mDeviceID
              },
              {
                "os",
                (object) "windows"
              },
              {
                "platform",
                (object) Logger.Platform
              },
              {
                "log_version",
                (object) 2
              },
              {
                "user_info",
                (object) log.UserInfo
              }
            });
          }
          Guid id = mBuffer[0].ID;
          byte[] bytes = Encoding.UTF8.GetBytes(Json.Serialize((object) objectList));
          using (FileStream fileStream = new FileStream(Logger.GetLogFilePath(this.key, id), FileMode.CreateNew))
          {
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
          }
          mBuffer.Clear();
          mBuffer.Release();
        }
        this.mBuffers.Clear();
      }
    }
  }
}
