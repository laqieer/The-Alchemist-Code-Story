// Decompiled with JetBrains decompiler
// Type: SRPG.EventStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool Fading
    {
      get
      {
        return this.IsFading;
      }
    }

    public EventStandChara.StateTypes State
    {
      get
      {
        return this.mState;
      }
      set
      {
        this.mState = value;
      }
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
        if (!EventStandChara.Instances[index].gameObject.activeInHierarchy)
          UnityEngine.Object.Destroy((UnityEngine.Object) EventStandChara.Instances[index].gameObject);
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
      Color color = this.gameObject.GetComponent<RawImage>().color;
      this.gameObject.GetComponent<RawImage>().color = new Color(color.r, color.g, color.b, 1f);
    }

    public void StartFadeOut(float fade)
    {
      this.IsFading = true;
      this.FadeTime = fade;
      this.mState = EventStandChara.StateTypes.FadeOut;
      if ((double) this.FadeTime > 0.0)
        return;
      Color color = this.gameObject.GetComponent<RawImage>().color;
      this.gameObject.GetComponent<RawImage>().color = new Color(color.r, color.g, color.b, 0.0f);
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
      Color color = this.gameObject.GetComponent<RawImage>().color;
      this.gameObject.GetComponent<RawImage>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0.0f, time));
    }

    private void FadeOut(float time)
    {
      Color color = this.gameObject.GetComponent<RawImage>().color;
      this.gameObject.GetComponent<RawImage>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.0f, 1f, time));
    }

    public void InitPositionX(RectTransform canvasRect)
    {
      RectTransform component = this.GetComponent<RectTransform>();
      this.PositionX = new float[5]
      {
        component.rect.width / 2f,
        canvasRect.rect.width / 2f,
        canvasRect.rect.width - component.rect.width / 2f,
        (float) (-(double) component.rect.width / 2.0),
        canvasRect.rect.width + component.rect.width / 2f
      };
    }

    public float GetPositionX(int index)
    {
      if (index >= 0 && index < this.PositionX.Length)
        return this.PositionX[index];
      return 0.0f;
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
