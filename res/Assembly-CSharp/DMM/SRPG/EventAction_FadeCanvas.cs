// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_FadeCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("オブジェクト/キャンバスフェード", "Canvasをフェードさせます", 5592405, 4473992)]
  public class EventAction_FadeCanvas : EventAction
  {
    public AnimationCurve Curve = new AnimationCurve(new Keyframe[2]
    {
      new Keyframe(0.0f, 0.0f),
      new Keyframe(1f, 1f)
    });
    public string CanvasID;
    private CanvasGroup[] mCanvasGroup;
    private float mTime;
    public float Time = 1f;

    public override void OnActivate()
    {
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.CanvasID);
      if (gameObjects.Length > 0)
      {
        this.mCanvasGroup = new CanvasGroup[gameObjects.Length];
        for (int index = 0; index < gameObjects.Length; ++index)
          this.mCanvasGroup[index] = GameUtility.RequireComponent<CanvasGroup>(gameObjects[index]);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.deltaTime;
      for (int index = 0; index < this.mCanvasGroup.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mCanvasGroup[index], (Object) null))
        {
          float num1;
          if ((double) this.Time > 0.0)
          {
            AnimationCurve curve = this.Curve;
            double num2 = (double) Mathf.Clamp01(this.mTime / this.Time);
            Keyframe keyframe = this.Curve[this.Curve.length - 1];
            double time = (double) ((Keyframe) ref keyframe).time;
            double num3 = num2 * time;
            num1 = curve.Evaluate((float) num3);
          }
          else
          {
            AnimationCurve curve = this.Curve;
            Keyframe keyframe = this.Curve[this.Curve.length - 1];
            double time = (double) ((Keyframe) ref keyframe).time;
            num1 = curve.Evaluate((float) time);
          }
          this.mCanvasGroup[index].alpha = Mathf.Clamp01(num1);
        }
      }
      if ((double) this.mTime < (double) this.Time)
        return;
      this.ActivateNext();
    }

    public override void GoToEndState() => this.mTime = this.Time;

    public override void SkipImmediate() => this.mTime = this.Time;
  }
}
