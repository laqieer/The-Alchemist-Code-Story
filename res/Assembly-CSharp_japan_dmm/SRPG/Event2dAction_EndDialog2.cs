// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndDialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/会話/閉じる(2D)", "表示されている吹き出しを閉じます", 5592405, 4473992)]
  public class Event2dAction_EndDialog2 : EventAction
  {
    [StringIsActorID]
    public string ActorID;
    private EventDialogBubbleCustom mBubble;
    public bool Async;
    public float FadeTime = 0.2f;
    private float fadingTime;
    private bool IsFading;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<CanvasGroup> fadeInParticleList = new List<CanvasGroup>();

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.ActorID))
      {
        for (int index = EventDialogBubbleCustom.Instances.Count - 1; index >= 0; --index)
          EventDialogBubbleCustom.Instances[index].Close();
      }
      else
      {
        this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
        if (Object.op_Inequality((Object) this.mBubble, (Object) null))
          this.mBubble.Close();
      }
      this.fadeInList.Clear();
      this.fadeInParticleList.Clear();
      this.IsFading = false;
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        foreach (EventStandCharaController2 instance in EventStandCharaController2.Instances)
        {
          if (!instance.IsClose)
          {
            Color white = Color.white;
            if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(instance.CharaID))
              white = Event2dAction_OperateStandChara.CharaColorDic[instance.CharaID];
            foreach (GameObject standChara in instance.StandCharaList)
            {
              if (Color.op_Inequality(((Graphic) standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color, white))
              {
                this.fadeInList.Add(standChara);
                this.IsFading = true;
              }
            }
            foreach (Component componentsInChild in ((Component) instance).gameObject.GetComponentsInChildren<GameObjectID>())
            {
              CanvasGroup component = componentsInChild.GetComponent<CanvasGroup>();
              if (Object.op_Inequality((Object) component, (Object) null) && (double) component.alpha != 1.0)
                this.fadeInParticleList.Add(component);
            }
          }
        }
      }
      if (!this.IsFading)
      {
        this.ActivateNext();
      }
      else
      {
        this.fadingTime = this.FadeTime;
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      if (!this.IsFading)
        return;
      this.fadingTime -= Time.deltaTime;
      if ((double) this.fadingTime <= 0.0)
      {
        this.fadingTime = 0.0f;
        this.IsFading = false;
        if (this.Async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      this.FadeIn(this.fadingTime);
    }

    private void FadeIn(float time)
    {
      float num1 = time / this.FadeTime;
      Color color1 = Color.Lerp(Color.white, Color.grey, num1);
      foreach (GameObject fadeIn in this.fadeInList)
      {
        EventStandChara2 component = fadeIn.GetComponent<EventStandChara2>();
        string charaId = fadeIn.GetComponentInParent<EventStandCharaController2>().CharaID;
        Color white = Color.white;
        if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
          white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
        Color color2 = Color.op_Multiply(white, color1);
        Color color3 = ((Graphic) component.BodyObject.GetComponent<RawImage>()).color;
        if ((double) ((Color) ref color3).maxColorComponent <= (double) ((Color) ref color2).maxColorComponent)
        {
          ((Graphic) component.FaceObject.GetComponent<RawImage>()).color = color2;
          ((Graphic) component.BodyObject.GetComponent<RawImage>()).color = color2;
        }
      }
      float num2 = Mathf.Lerp(1f, 0.0f, num1);
      foreach (CanvasGroup fadeInParticle in this.fadeInParticleList)
      {
        if ((double) fadeInParticle.alpha <= (double) num2)
          fadeInParticle.alpha = num2;
      }
    }
  }
}
