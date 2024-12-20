﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveStandChara2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵2/移動(2D)", "立ち絵2を指定した位置に移動させます。", 5592405, 4473992)]
  public class Event2dAction_MoveStandChara2 : EventAction
  {
    public float MoveTime = 1f;
    public string CharaID;
    public EventStandCharaController2.PositionTypes MoveTo;
    private EventStandCharaController2 mStandChara;
    private Vector3 FromPostion;
    private Vector3 ToPostion;
    private float offset;
    private Vector2 FromAnchorMin;
    private Vector2 FromAnchorMax;
    private RectTransform mStandCharaTransform;
    private Vector2 mToAnchor;

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mStandChara == (UnityEngine.Object) null))
        return;
      this.mStandChara = EventStandCharaController2.FindInstances(this.CharaID);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandChara == (UnityEngine.Object) null)
      {
        this.ActivateNext();
      }
      else
      {
        this.mStandCharaTransform = this.mStandChara.GetComponent<RectTransform>();
        this.FromAnchorMin = this.mStandCharaTransform.anchorMin;
        this.FromAnchorMax = this.mStandCharaTransform.anchorMax;
        this.mToAnchor = new Vector2(this.mStandChara.GetAnchorPostionX((int) this.MoveTo), 0.0f);
      }
    }

    public override void Update()
    {
      this.mStandCharaTransform.anchorMin = this.FromAnchorMin + Vector2.Scale(this.mToAnchor - this.FromAnchorMin, new Vector2(this.offset, this.offset));
      this.mStandCharaTransform.anchorMax = this.FromAnchorMax + Vector2.Scale(this.mToAnchor - this.FromAnchorMax, new Vector2(this.offset, this.offset));
      this.offset += Time.deltaTime / this.MoveTime;
      if ((double) this.offset < 1.0)
        return;
      this.offset = 1f;
      RectTransform standCharaTransform = this.mStandCharaTransform;
      Vector2 mToAnchor = this.mToAnchor;
      this.mStandCharaTransform.anchorMax = mToAnchor;
      Vector2 vector2 = mToAnchor;
      standCharaTransform.anchorMin = vector2;
      this.ActivateNext();
    }
  }
}
