// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentWishList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class FriendPresentWishList
  {
    private FriendPresentItemParam[] m_Array = new FriendPresentItemParam[3];

    public FriendPresentItemParam this[int index]
    {
      get
      {
        return this.m_Array != null && index < this.m_Array.Length ? this.m_Array[index] : (FriendPresentItemParam) null;
      }
    }

    public int count => this.m_Array != null ? this.m_Array.Length : 0;

    public FriendPresentItemParam[] array => this.m_Array;

    public void Clear()
    {
      for (int index = 0; index < this.m_Array.Length; ++index)
        this.m_Array[index] = (FriendPresentItemParam) null;
    }

    public void Set(string iname, int priority)
    {
      this.m_Array[priority] = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(iname);
    }

    public void Deserialize(FriendPresentWishList.Json[] jsons)
    {
      if (jsons == null)
        throw new InvalidJSONException();
      this.Clear();
      for (int index = 0; index < jsons.Length; ++index)
      {
        FriendPresentWishList.Json json = jsons[index];
        if (json != null)
        {
          if (json.priority > 0 && json.priority <= this.m_Array.Length)
          {
            FriendPresentItemParam presentItemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(json.iname);
            if (presentItemParam != null)
              this.m_Array[json.priority - 1] = presentItemParam;
          }
          else
            DebugUtility.LogError(string.Format("ウィッシュリスト優先の範囲は 1 ~ {0} まで > {1}", (object) this.m_Array.Length, (object) json.priority));
        }
      }
    }

    [Serializable]
    public class Json
    {
      public string iname;
      public int priority;
    }
  }
}
