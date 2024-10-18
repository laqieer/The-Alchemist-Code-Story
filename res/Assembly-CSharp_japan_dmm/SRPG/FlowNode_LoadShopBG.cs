// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadShopBG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Shop/LoadShopBG", 32741)]
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Done", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_LoadShopBG : FlowNode
  {
    public FlowNode_LoadShopBG.PrefabType Type;
    public string TypeString;
    public string BasePath = "ShopBG";
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
      if (name != null && Object.op_Inequality((Object) this.Parent, (Object) null))
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(name);
        if (Object.op_Inequality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject1);
          if (Object.op_Inequality((Object) gameObject2, (Object) null))
            gameObject2.transform.SetParent(this.Parent, this.WorldPositionStays);
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
