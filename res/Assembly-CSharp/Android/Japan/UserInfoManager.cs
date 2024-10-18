// Decompiled with JetBrains decompiler
// Type: UserInfoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class UserInfoManager : MonoSingleton<UserInfoManager>
{
  private Dictionary<string, object> info;

  private string InfoFilePath
  {
    get
    {
      string path = AppPath.temporaryCachePath + "/user";
      try
      {
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
      }
      catch
      {
      }
      return path + "/user_info.json";
    }
  }

  public object GetValue(string key)
  {
    if (this.info == null)
      this.info = this.Load();
    object obj;
    if (!this.info.TryGetValue(key, out obj))
      return (object) null;
    return obj;
  }

  public void SetValue(string key, object value, bool isSave = true)
  {
    if (this.info == null)
      this.info = this.Load();
    this.info[key] = value;
    if (!isSave)
      return;
    this.Save();
  }

  private Dictionary<string, object> Load()
  {
    try
    {
      return (Dictionary<string, object>) MiniJSON.Json.Deserialize(File.ReadAllText(this.InfoFilePath, Encoding.UTF8));
    }
    catch
    {
      return new Dictionary<string, object>();
    }
  }

  private bool Save()
  {
    try
    {
      File.WriteAllText(this.InfoFilePath, MiniJSON.Json.Serialize((object) this.info), Encoding.UTF8);
      return true;
    }
    catch
    {
      return false;
    }
  }

  public void Delete()
  {
    if (!Directory.Exists(AppPath.temporaryCachePath + "/user") || !File.Exists(this.InfoFilePath))
      return;
    File.Delete(this.InfoFilePath);
  }
}
