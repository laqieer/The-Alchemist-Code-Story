// Decompiled with JetBrains decompiler
// Type: SRPG.Image_ReplaceSprite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class Image_ReplaceSprite : Image
  {
    [SerializeField]
    public string mMasterIname;
    [SerializeField]
    public SpriteSheet mSpriteSheet;
    private int mCheckTime = -1;

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      this.ReplaceSprite();
      this.mCheckTime = TimeManager.ServerTime.Minute;
    }

    private void Update()
    {
      if (this.mCheckTime == TimeManager.ServerTime.Minute)
        return;
      this.ReplaceSprite();
      this.mCheckTime = TimeManager.ServerTime.Minute;
    }

    private void ReplaceSprite()
    {
      List<SRPG.ReplaceSprite> repraseSprite = MonoSingleton<GameManager>.Instance.MasterParam.RepraseSprite;
      if (repraseSprite == null)
        return;
      foreach (SRPG.ReplaceSprite replace_sprite in repraseSprite)
      {
        if (this.mMasterIname == replace_sprite.mIname)
          this.ReplaceSprite(replace_sprite);
      }
    }

    private void ReplaceSprite(SRPG.ReplaceSprite replace_sprite)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSpriteSheet, (UnityEngine.Object) null))
        return;
      string name1 = (string) null;
      string name2 = (string) null;
      foreach (ReplacePeriod replacePeriod in replace_sprite.mPeriod)
      {
        if (string.IsNullOrEmpty(replacePeriod.mBeginAt) && string.IsNullOrEmpty(replacePeriod.mEndAt))
          name1 = replacePeriod.mSpriteName;
        else if (this.IsPeriod(replacePeriod.mBeginAt, replacePeriod.mEndAt))
          name2 = replacePeriod.mSpriteName;
      }
      Sprite sprite = (Sprite) null;
      if (!string.IsNullOrEmpty(name2))
        sprite = this.mSpriteSheet.GetSprite(name2);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) sprite, (UnityEngine.Object) null) && !string.IsNullOrEmpty(name1))
        sprite = this.mSpriteSheet.GetSprite(name1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) sprite, (UnityEngine.Object) null))
        return;
      this.sprite = sprite;
    }

    private bool IsPeriod(string startTime, string endTime)
    {
      long num1;
      try
      {
        num1 = !(startTime == string.Empty) ? TimeManager.FromDateTime(DateTime.Parse(startTime)) : long.MinValue;
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("mStartTime is parse failed.");
        return false;
      }
      long num2;
      try
      {
        num2 = !(endTime == string.Empty) ? TimeManager.FromDateTime(DateTime.Parse(endTime)) : long.MaxValue;
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("mEndTime is parse failed.");
        return false;
      }
      long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
      return num1 <= num3 && num3 <= num2;
    }
  }
}
