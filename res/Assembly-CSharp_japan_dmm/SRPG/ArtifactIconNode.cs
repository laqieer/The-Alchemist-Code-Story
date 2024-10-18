// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactIconNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtifactIconNode : ContentNode
  {
    [SerializeField]
    public ArtifactIcon Icon;
    [SerializeField]
    public GameObject RecommendObject;
    [SerializeField]
    public GameObject SelectObject;
    [SerializeField]
    public GameObject EmptyObject;

    public void Setup(ArtifactData arti_data)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || arti_data == null)
        return;
      ((Component) this.Icon).gameObject.SetActive(true);
      DataSource.Bind<ArtifactData>(((Component) this.Icon).gameObject, arti_data);
      GameParameter.UpdateAll(((Component) this.Icon).gameObject);
    }

    public void Setup(ArtifactParam arti_param)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || arti_param == null)
        return;
      ((Component) this.Icon).gameObject.SetActive(true);
      DataSource.Bind<ArtifactParam>(((Component) this.Icon).gameObject, arti_param);
      GameParameter.UpdateAll(((Component) this.Icon).gameObject);
    }

    public void Empty(bool is_enmpty)
    {
      if (Object.op_Equality((Object) this.EmptyObject, (Object) null) || Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      ((Component) this.Icon).gameObject.SetActive(!is_enmpty);
      this.EmptyObject.SetActive(is_enmpty);
    }

    public void Enable(bool enable)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || !((Component) this.Icon).gameObject.activeSelf)
        return;
      this.Icon.EquipForceMask = !enable;
      Button component = ((Component) this.Icon).GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Selectable) component).interactable = enable;
    }

    public void Select(bool select)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || !((Component) this.Icon).gameObject.activeSelf || Object.op_Equality((Object) this.SelectObject, (Object) null))
        return;
      this.SelectObject.SetActive(select);
    }

    public void Recommend(bool is_recommend)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || !((Component) this.Icon).gameObject.activeSelf || Object.op_Equality((Object) this.RecommendObject, (Object) null))
        return;
      this.RecommendObject.SetActive(is_recommend);
    }
  }
}
