// Decompiled with JetBrains decompiler
// Type: SRPG.BadStatusEffects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public static class BadStatusEffects
  {
    public static List<BadStatusEffects.Desc> Effects;
    public static GameObject CurseEffect;
    public static string CurseEffectAttachTarget;
    public static string CurseEffectAttachTargetBigUnit;

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
      return (IEnumerator) new BadStatusEffects.\u003CLoadEffects\u003Ec__Iterator1() { assets = assets };
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
      if (BadStatusEffects.Effects != null)
        return (UnityEngine.Object) BadStatusEffects.CurseEffect != (UnityEngine.Object) null;
      return false;
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
    }
  }
}
