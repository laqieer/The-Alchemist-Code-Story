// Decompiled with JetBrains decompiler
// Type: SRPG.BadStatusEffects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public static class BadStatusEffects
  {
    private const string BAD_STATUS_FILE_PATH = "Data/badstatus";
    public static List<BadStatusEffects.Desc> Effects;
    public static GameObject CurseEffect;
    public static string CurseEffectAttachTarget;
    public static string CurseEffectAttachTargetBigUnit;

    public static BadStatusEffects.BadStatusEffectParam LoadBadStatusEffectParam()
    {
      BadStatusEffects.BadStatusEffectParam statusEffectParam = new BadStatusEffects.BadStatusEffectParam();
      return !statusEffectParam.Load() ? (BadStatusEffects.BadStatusEffectParam) null : statusEffectParam;
    }

    [DebuggerHidden]
    public static IEnumerator LoadQuestAssetEffects()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BadStatusEffects.\u003CLoadQuestAssetEffects\u003Ec__Iterator0 effectsCIterator0 = new BadStatusEffects.\u003CLoadQuestAssetEffects\u003Ec__Iterator0();
      return (IEnumerator) effectsCIterator0;
    }

    [DebuggerHidden]
    public static IEnumerator LoadEffects(QuestAssets assets)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BadStatusEffects.\u003CLoadEffects\u003Ec__Iterator1()
      {
        assets = assets
      };
    }

    [DebuggerHidden]
    public static IEnumerator LoadBigUnitEffects()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BadStatusEffects.\u003CLoadBigUnitEffects\u003Ec__Iterator2 effectsCIterator2 = new BadStatusEffects.\u003CLoadBigUnitEffects\u003Ec__Iterator2();
      return (IEnumerator) effectsCIterator2;
    }

    public static void UnloadEffects()
    {
      BadStatusEffects.Effects = (List<BadStatusEffects.Desc>) null;
      BadStatusEffects.CurseEffect = (GameObject) null;
    }

    public static bool IsLoaded()
    {
      return BadStatusEffects.Effects != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) BadStatusEffects.CurseEffect, (UnityEngine.Object) null);
    }

    public class Desc
    {
      public EUnitCondition Key;
      public GameObject Effect;
      public string NameEffectBigUnit;
      public GameObject EffectBigUnit;
      public string AttachTarget;
      public string AttachTargetBigUnit;
      public Color BlendColor;
      public string AnimationName;
      public string NameEffect;

      public string EffectPath => "Effects/" + this.NameEffect;

      public string EffectBigUnitPath => "Effects/" + this.NameEffectBigUnit;
    }

    public class BadStatusEffectParam
    {
      public int colID;
      public int colColor;
      public int colAnimation;
      public int colEffect;
      public int colAttachTarget;
      public int colAttachTargetBigUnit;
      public int colEffectBigUnit;
      public List<BadStatusEffects.Desc> effects;

      public bool Load()
      {
        string empty = string.Empty;
        string str;
        if (Application.isPlaying)
        {
          str = AssetManager.LoadTextData("Data/badstatus");
        }
        else
        {
          TextAsset textAsset = Resources.Load<TextAsset>("Data/badstatus");
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) textAsset, (UnityEngine.Object) null))
            return false;
          str = textAsset.text;
        }
        if (string.IsNullOrEmpty(str))
          return false;
        this.Parse(str);
        return true;
      }

      private void Parse(string value)
      {
        StringReader stringReader = new StringReader(value);
        string[] array = stringReader.ReadLine().Split('\t');
        this.colID = Array.IndexOf<string>(array, "ID");
        this.colColor = Array.IndexOf<string>(array, "COLOR");
        this.colAnimation = Array.IndexOf<string>(array, "ANIMATION");
        this.colEffect = Array.IndexOf<string>(array, "EFFECT");
        this.colAttachTarget = Array.IndexOf<string>(array, "TARGET");
        this.colAttachTargetBigUnit = Array.IndexOf<string>(array, "TARGET_BIGUNIT");
        this.colEffectBigUnit = Array.IndexOf<string>(array, "EFFECT_BIGUNIT");
        this.effects = new List<BadStatusEffects.Desc>();
        string str;
        while ((str = stringReader.ReadLine()) != null)
        {
          if (!string.IsNullOrEmpty(str))
          {
            string[] strArray = str.Split('\t');
            EUnitCondition eunitCondition;
            try
            {
              eunitCondition = (EUnitCondition) Enum.Parse(typeof (EUnitCondition), strArray[this.colID], true);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
              continue;
            }
            BadStatusEffects.Desc desc = new BadStatusEffects.Desc();
            desc.Key = eunitCondition;
            desc.AnimationName = strArray[this.colAnimation];
            desc.AttachTarget = strArray[this.colAttachTarget];
            desc.AttachTargetBigUnit = strArray[this.colAttachTargetBigUnit];
            desc.BlendColor = string.IsNullOrEmpty(strArray[this.colColor]) ? Color.clear : Color32.op_Implicit(GameUtility.ParseColor(strArray[this.colColor]));
            if (!string.IsNullOrEmpty(strArray[this.colEffect]))
              desc.NameEffect = strArray[this.colEffect];
            if (!string.IsNullOrEmpty(strArray[this.colEffectBigUnit]))
              desc.NameEffectBigUnit = strArray[this.colEffectBigUnit];
            this.effects.Add(desc);
          }
        }
      }
    }
  }
}
