// Decompiled with JetBrains decompiler
// Type: SRPG.EventStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventStandChara : MonoBehaviour
  {
    public static List<EventStandChara> Instances = new List<EventStandChara>();
    public string CharaID;
    public bool mClose;
    private float[] PositionX;
    public const float FADEIN_TIME = 0.3f;
    public const float FADEOUT_TIME = 0.5f;
    private float FadeTime;
    private bool IsFading;
    private EventStandChara.StateTypes mState;

    public bool Fading => this.IsFading;

    public EventStandChara.StateTypes State
    {
      get => this.mState;
      set => this.mState = value;
    }

    public static EventStandChara Find(string id)
    {
      for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
      {
        if (EventStandChara.Instances[index].CharaID == id)
          return EventStandChara.Instances[index];
      }
      return (EventStandChara) null;
    }

    public static void DiscardAll()
    {
      for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventStandChara.Instances[index]).gameObject.activeInHierarchy)
          Object.Destroy((Object) ((Component) EventStandChara.Instances[index]).gameObject);
      }
      EventStandChara.Instances.Clear();
    }

    public static string[] GetCharaIDs()
    {
      List<string> stringList = new List<string>();
      for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
        stringList.Add(EventStandChara.Instances[index].CharaID);
      return stringList.ToArray();
    }

    private void Awake()
    {
      EventStandChara.Instances.Add(this);
      this.FadeTime = 0.0f;
      this.IsFading = false;
    }

    private void OnDestroy()
    {
      EventStandChara.Instances.Remove(this);
      RawImage component = ((Component) this).gameObject.GetComponent<RawImage>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.texture = (Texture) null;
        Object.Destroy((Object) component);
      }
      this.mState = EventStandChara.StateTypes.Inactive;
    }

    public void Open(float fade = 0.3f)
    {
      this.mClose = false;
      this.StartFadeIn(fade);
    }

    public void Close(float fade = 0.5f)
    {
      this.mClose = true;
      this.StartFadeOut(fade);
    }

    public void StartFadeIn(float fade)
    {
      this.IsFading = true;
      this.FadeTime = fade;
      this.mState = EventStandChara.StateTypes.FadeIn;
      if ((double) this.FadeTime > 0.0)
        return;
      Color color = ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color;
      ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color = new Color(color.r, color.g, color.b, 1f);
    }

    public void StartFadeOut(float fade)
    {
      this.IsFading = true;
      this.FadeTime = fade;
      this.mState = EventStandChara.StateTypes.FadeOut;
      if ((double) this.FadeTime > 0.0)
        return;
      Color color = ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color;
      ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color = new Color(color.r, color.g, color.b, 0.0f);
    }

    private void Update()
    {
      if (!this.IsFading)
        return;
      this.FadeTime -= Time.deltaTime;
      if ((double) this.FadeTime <= 0.0)
      {
        this.FadeTime = 0.0f;
        this.IsFading = false;
      }
      else
      {
        if (this.mState == EventStandChara.StateTypes.FadeIn)
          this.FadeIn(this.FadeTime);
        if (this.mState != EventStandChara.StateTypes.FadeOut)
          return;
        this.FadeOut(this.FadeTime);
      }
    }

    private void FadeIn(float time)
    {
      Color color = ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color;
      ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0.0f, time));
    }

    private void FadeOut(float time)
    {
      Color color = ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color;
      ((Graphic) ((Component) this).gameObject.GetComponent<RawImage>()).color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.0f, 1f, time));
    }

    public void InitPositionX(RectTransform canvasRect)
    {
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      float[] numArray = new float[5];
      Rect rect1 = component.rect;
      numArray[0] = (float) ((double) ((Rect) ref rect1).width / 2.0);
      Rect rect2 = canvasRect.rect;
      numArray[1] = (float) ((double) ((Rect) ref rect2).width / 2.0);
      Rect rect3 = canvasRect.rect;
      double width1 = (double) ((Rect) ref rect3).width;
      Rect rect4 = component.rect;
      double num1 = (double) ((Rect) ref rect4).width / 2.0;
      numArray[2] = (float) (width1 - num1);
      Rect rect5 = component.rect;
      numArray[3] = (float) (-(double) ((Rect) ref rect5).width / 2.0);
      Rect rect6 = canvasRect.rect;
      double width2 = (double) ((Rect) ref rect6).width;
      Rect rect7 = component.rect;
      double num2 = (double) ((Rect) ref rect7).width / 2.0;
      numArray[4] = (float) (width2 + num2);
      this.PositionX = numArray;
    }

    public float GetPositionX(int index)
    {
      return index >= 0 && index < this.PositionX.Length ? this.PositionX[index] : 0.0f;
    }

    public enum PositionTypes
    {
      Left,
      Center,
      Right,
      OverLeft,
      OverRight,
    }

    public enum StateTypes
    {
      FadeIn,
      Active,
      FadeOut,
      Inactive,
    }
  }
}
