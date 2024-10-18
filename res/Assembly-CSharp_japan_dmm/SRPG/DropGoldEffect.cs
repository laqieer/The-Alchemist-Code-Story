// Decompiled with JetBrains decompiler
// Type: SRPG.DropGoldEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DropGoldEffect : MonoBehaviour
  {
    public const string GOLD_GAMEOBJECT_NAME = "UI_GOLD";
    [NonSerialized]
    public int Gold;
    private RectTransform m_TargetRect;
    private Unit m_DropOwner;

    public RectTransform TargetRect => this.m_TargetRect;

    public Unit DropOwner
    {
      set => this.m_DropOwner = value;
      get => this.m_DropOwner;
    }

    private void Start()
    {
      GameObject gameObject = GameObjectID.FindGameObject("UI_GOLD");
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        this.m_TargetRect = gameObject.transform as RectTransform;
      SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropGoldEffect.End", (object) this);
      GameUtility.RequireComponent<OneShotParticle>(((Component) this).gameObject);
    }
  }
}
