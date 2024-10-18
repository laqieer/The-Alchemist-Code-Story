// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/シェイク", "立ち絵2を揺らします", 5592405, 4473992)]
  public class Event2dAction_StandShake : EventAction
  {
    public float Duration = 0.5f;
    public float FrequencyX = 12.51327f;
    public float FrequencyY = 20.4651f;
    public float AmplitudeX = 0.1f;
    public float AmplitudeY = 0.1f;
    public string CharaID;
    public bool Async;
    private EventStandCharaController2 mStandChara;
    private RectTransform mStandCharaTransform;
    private float mSeedX;
    private float mSeedY;
    private float mTime;
    private Vector2 originalPvt;

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
        this.mSeedX = Random.value;
        this.mSeedY = Random.value;
        this.mStandCharaTransform = this.mStandChara.GetComponent<RectTransform>();
        this.originalPvt = this.mStandCharaTransform.pivot;
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      this.mTime += Time.deltaTime;
      if ((double) this.mTime >= (double) this.Duration)
      {
        this.mStandCharaTransform.pivot = this.originalPvt;
        if (this.Async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      else
      {
        float num = 1f - Mathf.Clamp01(this.mTime / this.Duration);
        this.mStandCharaTransform.pivot = new Vector2(this.originalPvt.x + Mathf.Sin((float) (((double) Time.time + (double) this.mSeedX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num, this.originalPvt.y + Mathf.Sin((float) (((double) Time.time + (double) this.mSeedY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num);
      }
    }
  }
}
