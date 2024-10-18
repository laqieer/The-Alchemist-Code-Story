// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FadeCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/キャンバスフェード(2D)", "Canvasをフェードさせます", 5592405, 4473992)]
  public class Event2dAction_FadeCanvas : EventAction
  {
    public AnimationCurve Curve = new AnimationCurve(new Keyframe[2]{ new Keyframe(0.0f, 0.0f), new Keyframe(1f, 1f) });
    public float Time = 1f;
    public string CanvasID;
    private CanvasGroup[] mCanvasGroup;
    private float mTime;
    public bool async;

    public override void OnActivate()
    {
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.CanvasID);
      if (gameObjects.Length > 0)
      {
        this.mCanvasGroup = new CanvasGroup[gameObjects.Length];
        for (int index = 0; index < gameObjects.Length; ++index)
          this.mCanvasGroup[index] = GameUtility.RequireComponent<CanvasGroup>(gameObjects[index]);
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.deltaTime;
      for (int index = 0; index < this.mCanvasGroup.Length; ++index)
      {
        if ((UnityEngine.Object) this.mCanvasGroup[index] != (UnityEngine.Object) null)
        {
          float num = (double) this.Time <= 0.0 ? this.Curve.Evaluate(this.Curve[this.Curve.length - 1].time) : this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time) * this.Curve[this.Curve.length - 1].time);
          this.mCanvasGroup[index].alpha = Mathf.Clamp01(num);
        }
      }
      if ((double) this.mTime < (double) this.Time)
        return;
      if (this.async)
        this.enabled = false;
      else
        this.ActivateNext();
    }
  }
}
