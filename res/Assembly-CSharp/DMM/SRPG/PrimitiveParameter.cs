// Decompiled with JetBrains decompiler
// Type: SRPG.PrimitiveParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PrimitiveParameter : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private PrimitiveParameter.PrimitiveType Type;
    [SerializeField]
    private Text TargetText;

    public void UpdateValue()
    {
      switch (this.Type)
      {
        case PrimitiveParameter.PrimitiveType.Int:
          this.SetText(DataSource.FindDataOfClass<int>(((Component) this).gameObject, 0).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.Long:
          this.SetText(DataSource.FindDataOfClass<long>(((Component) this).gameObject, 0L).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.Float:
          this.SetText(DataSource.FindDataOfClass<float>(((Component) this).gameObject, 0.0f).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.Double:
          this.SetText(DataSource.FindDataOfClass<double>(((Component) this).gameObject, 0.0).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.String:
          this.SetText(DataSource.FindDataOfClass<string>(((Component) this).gameObject, (string) null));
          break;
      }
    }

    private void SetText(string text)
    {
      if (!Object.op_Inequality((Object) this.TargetText, (Object) null))
        return;
      this.TargetText.text = text;
    }

    public enum PrimitiveType
    {
      Int,
      Long,
      Float,
      Double,
      String,
    }
  }
}
