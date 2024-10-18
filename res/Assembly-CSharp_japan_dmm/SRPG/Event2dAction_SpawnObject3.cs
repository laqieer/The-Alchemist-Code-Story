// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnObject3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/オブジェクト/配置2(2D)", "シーンにオブジェクトを配置します。", 5592405, 4473992)]
  public class Event2dAction_SpawnObject3 : EventAction
  {
    public const string ResourceDir = "Event2dAssets/";
    [StringIsResourcePathPopup(typeof (GameObject), "Event2dAssets/")]
    public string ResourceID;
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    [HideInInspector]
    public bool Persistent;
    [HideInInspector]
    public Vector2 Position;
    private GameObject mGO;
    public Event2dAction_SpawnObject3.SiblingOrder Order;
    [HideInInspector]
    public string CharaID;
    [HideInInspector]
    public Event2dAction_SpawnObject3.ActorChildOrder ChildOrder;
    private RectTransform rectTransform;
    private Vector2 pvt = new Vector2(0.5f, 0.5f);

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_SpawnObject3.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && Object.op_Inequality(this.mResourceLoadRequest.asset, (Object) null))
      {
        GameObject asset = this.mResourceLoadRequest.asset as GameObject;
        RectTransform component = asset.GetComponent<RectTransform>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.pvt = component.pivot;
        this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, Vector3.zero, asset.transform.rotation) as GameObject;
        if (!string.IsNullOrEmpty(this.ObjectID))
          GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.Root)
        {
          if (this.Persistent && Object.op_Inequality((Object) TacticsSceneSettings.Instance, (Object) null))
            this.mGO.transform.SetParent(((Component) TacticsSceneSettings.Instance).transform, true);
        }
        else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.ChildOfActor)
        {
          if (!string.IsNullOrEmpty(this.CharaID))
          {
            EventStandCharaController2 instances = EventStandCharaController2.FindInstances(this.CharaID);
            if (Object.op_Inequality((Object) instances, (Object) null))
            {
              this.mGO.transform.SetParent(((Component) instances).gameObject.transform, true);
              if (this.ChildOrder == Event2dAction_SpawnObject3.ActorChildOrder.Over)
                this.mGO.transform.SetAsLastSibling();
              else
                this.mGO.transform.SetAsFirstSibling();
              this.rectTransform = this.mGO.GetComponent<RectTransform>();
              if (Object.op_Inequality((Object) this.rectTransform, (Object) null))
              {
                this.rectTransform.pivot = this.pvt;
                this.rectTransform.anchoredPosition = Vector2.zero;
                RectTransform rectTransform = this.rectTransform;
                Vector2 vector2_1 = this.convertPosition(this.Position);
                this.rectTransform.anchorMax = vector2_1;
                Vector2 vector2_2 = vector2_1;
                rectTransform.anchorMin = vector2_2;
              }
            }
          }
        }
        else
        {
          float num1 = 0.0f;
          for (int index = 0; index < ((Component) this.EventRootTransform).transform.childCount; ++index)
          {
            Transform child = ((Transform) this.EventRootTransform).GetChild(index);
            if (Object.op_Inequality((Object) ((Component) child).GetComponent<EventDialogBubbleCustom>(), (Object) null))
            {
              num1 = ((Component) child).transform.position.z;
              break;
            }
          }
          this.mGO.transform.SetParent((Transform) this.EventRootTransform, true);
          this.mGO.transform.SetAsLastSibling();
          if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnDialog)
          {
            Vector3 position = this.mGO.transform.position;
            position.z = num1 - 1f;
            this.mGO.transform.position = position;
          }
          else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnStandChara)
          {
            int num2 = -1;
            for (int index = 0; index < ((Transform) this.EventRootTransform).childCount; ++index)
            {
              if (Object.op_Inequality((Object) ((Component) ((Transform) this.EventRootTransform).GetChild(index)).GetComponent<EventDialogBubbleCustom>(), (Object) null))
              {
                num2 = index;
                break;
              }
            }
            if (num2 > 0)
              this.mGO.transform.SetSiblingIndex(num2);
            if ((double) num1 < -1.0)
            {
              Vector3 position = this.mGO.transform.position;
              position.z = num1 + 1f;
              this.mGO.transform.position = position;
            }
          }
          else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnBackGround)
          {
            int num3 = -1;
            for (int index = 0; index < ((Transform) this.EventRootTransform).childCount; ++index)
            {
              if (Object.op_Inequality((Object) ((Component) ((Transform) this.EventRootTransform).GetChild(index)).GetComponent<EventStandCharaController2>(), (Object) null))
              {
                num3 = index;
                break;
              }
            }
            if (num3 > 0)
              this.mGO.transform.SetSiblingIndex(num3);
          }
          this.rectTransform = this.mGO.GetComponent<RectTransform>();
          if (Object.op_Inequality((Object) this.rectTransform, (Object) null))
          {
            this.rectTransform.pivot = this.pvt;
            this.rectTransform.anchoredPosition = Vector2.zero;
            RectTransform rectTransform = this.rectTransform;
            Vector2 vector2_3 = this.convertPosition(this.Position);
            this.rectTransform.anchorMax = vector2_3;
            Vector2 vector2_4 = vector2_3;
            rectTransform.anchorMin = vector2_4;
          }
        }
        this.Sequence.SpawnedObjects.Add(this.mGO);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mGO, (Object) null) || this.Persistent && !Object.op_Equality((Object) this.mGO.transform.parent, (Object) null))
        return;
      Object.Destroy((Object) this.mGO);
    }

    private Vector2 convertPosition(Vector2 pos)
    {
      return Vector2.Scale(Vector2.op_Addition(pos, Vector2.one), new Vector2(0.5f, 0.5f));
    }

    public enum SiblingOrder
    {
      Root,
      OnDialog,
      OnStandChara,
      OnBackGround,
      ChildOfActor,
    }

    public enum ActorChildOrder
    {
      Over,
      Under,
    }
  }
}
