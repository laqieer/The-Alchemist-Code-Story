// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Telop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("会話/テロップ(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Telop : EventAction
  {
    private const float DialogPadding = 20f;
    [HideInInspector]
    public string ActorID = "2DPlus";
    [StringIsTextIDPopup(false)]
    public string TextID;
    public bool TextColor;
    public Event2dAction_Telop.TextPositionTypes TextPosition;
    private string mTextData;
    private EventTelopBubble mBubble;
    private LoadRequest mBubbleResource;
    private LoadRequest mPortraitResource;
    private static readonly string AssetPath = "Event2dAssets/TelopBubble";

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_Telop.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble = EventTelopBubble.Find(this.ActorID);
      if (Object.op_Equality((Object) this.mBubble, (Object) null) && this.mBubbleResource != null)
      {
        this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventTelopBubble;
        ((Component) this.mBubble).transform.SetParent((Transform) this.EventRootTransform, false);
        this.mBubble.BubbleID = this.ActorID;
        ((Component) this.mBubble).transform.SetAsLastSibling();
        ((Component) this.mBubble).gameObject.SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
    }

    private Vector2 CalcBubblePosition(Vector3 position)
    {
      Vector2 vector2 = Vector2.op_Implicit(Camera.main.WorldToScreenPoint(position));
      vector2.x /= (float) Screen.width;
      vector2.y /= (float) Screen.height;
      return vector2;
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && !((Component) this.mBubble).gameObject.activeInHierarchy)
      {
        ((Component) this.mBubble).gameObject.SetActive(true);
        RectTransform component = ((Component) this.mBubble).GetComponent<RectTransform>();
        RectTransform rectTransform = component;
        Vector2 vector2_1 = new Vector2(0.5f, 0.5f);
        component.anchorMax = vector2_1;
        Vector2 vector2_2 = vector2_1;
        rectTransform.anchorMin = vector2_2;
        component.pivot = new Vector2(0.5f, 0.5f);
        component.anchoredPosition = new Vector2(0.0f, 0.0f);
      }
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      RectTransform transform1 = ((Component) this.mBubble).transform as RectTransform;
      for (int index = 0; index < EventTelopBubble.Instances.Count; ++index)
      {
        RectTransform transform2 = ((Component) EventTelopBubble.Instances[index]).transform as RectTransform;
        if (Object.op_Inequality((Object) transform1, (Object) transform2))
        {
          Rect rect = transform1.rect;
          if (((Rect) ref rect).Overlaps(transform2.rect))
            EventTelopBubble.Instances[index].Close();
        }
      }
      this.mBubble.TextColor = this.TextColor;
      this.mBubble.TextPosition = this.TextPosition;
      this.mBubble.SetBody(this.mTextData);
      this.mBubble.Open();
    }

    private string GetActorName(string actorID)
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if (Object.op_Inequality((Object) actor, (Object) null))
      {
        TacticsUnitController component = actor.GetComponent<TacticsUnitController>();
        if (Object.op_Inequality((Object) component, (Object) null))
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
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
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
