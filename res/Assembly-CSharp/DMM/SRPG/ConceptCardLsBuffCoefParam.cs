// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLsBuffCoefParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ConceptCardLsBuffCoefParam
  {
    public const int COEF_DEFAULT = 10000;
    private int mRare;
    private int[] mCoefs;
    private int[] mFriendCoefs;

    public int Rare => this.mRare;

    public int[] Coefs => this.mCoefs;

    public int[] FriendCoefs => this.mFriendCoefs;

    public void Deserialize(JSON_ConceptCardLsBuffCoefParam json)
    {
      if (json == null)
        return;
      this.mRare = json.rare;
      this.mCoefs = (int[]) null;
      if (json.coefs != null && json.coefs.Length != 0)
      {
        this.mCoefs = new int[json.coefs.Length];
        for (int index = 0; index < json.coefs.Length; ++index)
          this.mCoefs[index] = json.coefs[index];
      }
      this.mFriendCoefs = (int[]) null;
      if (json.friend_coefs == null || json.friend_coefs.Length == 0)
        return;
      this.mFriendCoefs = new int[json.friend_coefs.Length];
      for (int index = 0; index < json.friend_coefs.Length; ++index)
        this.mFriendCoefs[index] = json.friend_coefs[index];
    }

    public static void Deserialize(
      ref List<ConceptCardLsBuffCoefParam> list,
      JSON_ConceptCardLsBuffCoefParam[] json)
    {
      if (json == null || json.Length == 0)
        return;
      if (list == null)
        list = new List<ConceptCardLsBuffCoefParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        ConceptCardLsBuffCoefParam cardLsBuffCoefParam = new ConceptCardLsBuffCoefParam();
        cardLsBuffCoefParam.Deserialize(json[index]);
        list.Add(cardLsBuffCoefParam);
      }
    }

    public static int GetCoef(List<ConceptCardLsBuffCoefParam> list, int rare, int bt_limit)
    {
      int coef = 10000;
      if (list != null && bt_limit >= 0)
      {
        ConceptCardLsBuffCoefParam cardLsBuffCoefParam = list.Find((Predicate<ConceptCardLsBuffCoefParam>) (p => p.Rare == rare));
        if (cardLsBuffCoefParam != null && cardLsBuffCoefParam.Coefs != null && bt_limit < cardLsBuffCoefParam.Coefs.Length)
          coef = cardLsBuffCoefParam.Coefs[bt_limit];
      }
      return coef;
    }

    public static int GetFriendCoef(List<ConceptCardLsBuffCoefParam> list, int rare, int bt_limit)
    {
      int friendCoef = 10000;
      if (list != null && bt_limit >= 0)
      {
        ConceptCardLsBuffCoefParam cardLsBuffCoefParam = list.Find((Predicate<ConceptCardLsBuffCoefParam>) (p => p.Rare == rare));
        if (cardLsBuffCoefParam != null && cardLsBuffCoefParam.FriendCoefs != null && bt_limit < cardLsBuffCoefParam.FriendCoefs.Length)
          friendCoef = cardLsBuffCoefParam.FriendCoefs[bt_limit];
      }
      return friendCoef;
    }
  }
}
