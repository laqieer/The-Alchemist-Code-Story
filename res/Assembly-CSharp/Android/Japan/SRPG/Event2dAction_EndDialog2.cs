// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndDialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/会話/閉じる(2D)", "表示されている吹き出しを閉じます", 5592405, 4473992)]
  public class Event2dAction_EndDialog2 : EventAction
  {
    public float FadeTime = 0.2f;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<CanvasGroup> fadeInParticleList = new List<CanvasGroup>();
    [StringIsActorID]
    public string ActorID;
    private EventDialogBubbleCustom mBubble;
    public bool Async;
    private float fadingTime;
    private bool IsFading;

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
        if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null)
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
              if (standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().color != white)
              {
                this.fadeInList.Add(standChara);
                this.IsFading = true;
              }
            }
            foreach (Component componentsInChild in instance.gameObject.GetComponentsInChildren<GameObjectID>())
            {
              CanvasGroup component = componentsInChild.GetComponent<CanvasGroup>();
              if ((UnityEngine.Object) component != (UnityEngine.Object) null && (double) component.alpha != 1.0)
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
      float t = time / this.FadeTime;
      Color color1 = Color.Lerp(Color.white, Color.grey, t);
      foreach (GameObject fadeIn in this.fadeInList)
      {
        EventStandChara2 component = fadeIn.GetComponent<EventStandChara2>();
        string charaId = fadeIn.GetComponentInParent<EventStandCharaController2>().CharaID;
        Color white = Color.white;
        if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
          white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
        Color color2 = white * color1;
        if ((double) component.BodyObject.GetComponent<RawImage>().color.maxColorComponent <= (double) color2.maxColorComponent)
        {
          component.FaceObject.GetComponent<RawImage>().color = color2;
          component.BodyObject.GetComponent<RawImage>().color = color2;
        }
      }
      float num = Mathf.Lerp(1f, 0.0f, t);
      foreach (CanvasGroup fadeInParticle in this.fadeInParticleList)
      {
        if ((double) fadeInParticle.alpha <= (double) num)
          fadeInParticle.alpha = num;
      }
    }
  }
}
