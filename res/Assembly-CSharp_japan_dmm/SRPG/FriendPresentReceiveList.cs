// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentReceiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class FriendPresentReceiveList
  {
    private List<FriendPresentReceiveList.Param> m_List = new List<FriendPresentReceiveList.Param>();

    public FriendPresentReceiveList.Param this[int index]
    {
      get => index < this.m_List.Count ? this.m_List[index] : (FriendPresentReceiveList.Param) null;
    }

    public int count => this.m_List.Count;

    public List<FriendPresentReceiveList.Param> list => this.m_List;

    public FriendPresentReceiveList.Param[] array => this.m_List.ToArray();

    public void Clear() => this.m_List.Clear();

    public FriendPresentReceiveList.Param GetParam(string iname)
    {
      return this.m_List.Find((Predicate<FriendPresentReceiveList.Param>) (prop => prop.iname == iname));
    }

    public void Deserialize(FriendPresentReceiveList.Json[] jsons)
    {
      if (jsons == null)
        throw new InvalidJSONException();
      for (int index = 0; index < jsons.Length; ++index)
      {
        FriendPresentReceiveList.Json json = jsons[index];
        if (json != null)
        {
          FriendPresentReceiveList.Param obj = this.GetParam(json.pname);
          if (obj != null)
          {
            ++obj.num;
            if (obj.uids.FindIndex((Predicate<string>) (prop => prop == json.fuid)) == -1)
              obj.uids.Add(json.fuid);
          }
          else
            this.m_List.Add(new FriendPresentReceiveList.Param()
            {
              present = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(json.pname),
              iname = json.pname,
              num = 1,
              uids = new List<string>((IEnumerable<string>) new string[1]
              {
                json.fuid
              })
            });
        }
      }
    }

    [Serializable]
    public class Json
    {
      public string uid;
      public string fuid;
      public string pname;
    }

    public class Param
    {
      public FriendPresentItemParam present;
      public string iname;
      public int num;
      public List<string> uids;
    }
  }
}
