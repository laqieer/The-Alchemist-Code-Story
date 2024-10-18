// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadShopBG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Reflection;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("LoadShopBG", 32741)]
  [FlowNode.Pin(1, "Done", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_LoadShopBG : FlowNode
  {
    public string BasePath = "ShopBG";
    public FlowNode_LoadShopBG.PrefabType Type;
    public string TypeString;
    public Transform Parent;
    public bool WorldPositionStays;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      SectionParam currentSectionParam = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam();
      string name = (string) null;
      if (this.Type == FlowNode_LoadShopBG.PrefabType.DirectResourcePath)
        name = this.TypeString;
      else if (currentSectionParam != null)
      {
        if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamBar)
          name = currentSectionParam.bar;
        else if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamShop)
          name = currentSectionParam.shop;
        else if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamInn)
          name = currentSectionParam.inn;
        else if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamMemberName)
        {
          FieldInfo field = currentSectionParam.GetType().GetField(this.TypeString);
          if ((object) field != null && (object) field.FieldType == (object) typeof (string))
            name = (string) field.GetValue((object) currentSectionParam);
        }
      }
      if (name != null && !string.IsNullOrEmpty(this.BasePath))
        name = this.BasePath + "/" + name;
      if (name != null && (UnityEngine.Object) this.Parent != (UnityEngine.Object) null)
      {
        GameObject original = AssetManager.Load<GameObject>(name);
        if ((UnityEngine.Object) original != (UnityEngine.Object) null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
            gameObject.transform.SetParent(this.Parent, this.WorldPositionStays);
        }
      }
      this.ActivateOutputLinks(1);
    }

    public enum PrefabType
    {
      SectionParamBar,
      SectionParamShop,
      SectionParamInn,
      DirectResourcePath,
      SectionParamMemberName,
    }
  }
}
