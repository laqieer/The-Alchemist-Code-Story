// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Telop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("会話/テロップ(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Telop : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/TelopBubble";
    [HideInInspector]
    public string ActorID = "2DPlus";
    private const float DialogPadding = 20f;
    [StringIsTextIDPopup(false)]
    public string TextID;
    public bool TextColor;
    public Event2dAction_Telop.TextPositionTypes TextPosition;
    private string mTextData;
    private EventTelopBubble mBubble;
    private LoadRequest mBubbleResource;
    private LoadRequest mPortraitResource;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_Telop.\u003CPreloadAssets\u003Ec__IteratorC4() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mBubble == (UnityEngine.Object) null))
        return;
      this.mBubble = EventTelopBubble.Find(this.ActorID);
      if ((UnityEngine.Object) this.mBubble == (UnityEngine.Object) null && this.mBubbleResource != null)
      {
        this.mBubble = UnityEngine.Object.Instantiate(this.mBubbleResource.asset) as EventTelopBubble;
        this.mBubble.transform.SetParent(this.ActiveCanvas.transform, false);
        this.mBubble.BubbleID = this.ActorID;
        this.mBubble.transform.SetAsLastSibling();
        this.mBubble.gameObject.SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
    }

    private Vector2 CalcBubblePosition(Vector3 position)
    {
      Vector2 screenPoint = (Vector2) UnityEngine.Camera.main.WorldToScreenPoint(position);
      screenPoint.x /= (float) Screen.width;
      screenPoint.y /= (float) Screen.height;
      return screenPoint;
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null && !this.mBubble.gameObject.activeInHierarchy)
      {
        this.mBubble.gameObject.SetActive(true);
        RectTransform component = this.mBubble.GetComponent<RectTransform>();
        RectTransform rectTransform = component;
        Vector2 vector2_1 = new Vector2(0.5f, 0.5f);
        component.anchorMax = vector2_1;
        Vector2 vector2_2 = vector2_1;
        rectTransform.anchorMin = vector2_2;
        component.pivot = new Vector2(0.5f, 0.5f);
        component.anchoredPosition = new Vector2(0.0f, 0.0f);
      }
      if (!((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null))
        return;
      RectTransform transform1 = this.mBubble.transform as RectTransform;
      for (int index = 0; index < EventTelopBubble.Instances.Count; ++index)
      {
        RectTransform transform2 = EventTelopBubble.Instances[index].transform as RectTransform;
        if ((UnityEngine.Object) transform1 != (UnityEngine.Object) transform2 && transform1.rect.Overlaps(transform2.rect))
          EventTelopBubble.Instances[index].Close();
      }
      this.mBubble.TextColor = this.TextColor;
      this.mBubble.TextPosition = this.TextPosition;
      this.mBubble.SetBody(this.mTextData);
      this.mBubble.Open();
    }

    private string GetActorName(string actorID)
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
      {
        TacticsUnitController component = actor.GetComponent<TacticsUnitController>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          Unit unit = component.Unit;
          if (unit != null)
            return unit.UnitName;
        }
      }
      return actorID;
    }

    public override bool Forward()
    {
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null)
      {
        if (this.mBubble.Finished)
        {
          this.mBubble.Forward();
          this.ActivateNext();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public enum TextPositionTypes
    {
      Left,
      Center,
      Right,
    }

    public enum TextSpeedTypes
    {
      Normal,
      Slow,
      Fast,
    }
  }
}
