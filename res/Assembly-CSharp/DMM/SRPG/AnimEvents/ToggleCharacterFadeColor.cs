// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleCharacterFadeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ToggleCharacterFadeColor : AnimEvent
  {
    [SerializeField]
    private Color FadeColor = Color.white;
    private Color StartColor = Color.white;
    private UnitController mUnitController;
    private GeneratedCharacter mGeneratedCharacter;

    public override void OnStart(GameObject go)
    {
      this.mGeneratedCharacter = go.GetComponent<GeneratedCharacter>();
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        this.StartColor = this.mGeneratedCharacter.GetColor();
      }
      else
      {
        this.mUnitController = go.GetComponentInParent<UnitController>();
        if (!Object.op_Equality((Object) this.mUnitController, (Object) null))
          return;
        this.StartColor = this.mUnitController.GetColor();
      }
    }

    public override void OnEnd(GameObject go)
    {
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        this.mGeneratedCharacter.SetColor(this.FadeColor);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mUnitController, (Object) null))
          return;
        this.mUnitController.SetColor(this.FadeColor);
      }
    }

    public override void OnTick(GameObject go, float ratio) => this.SetRenderMode(ratio);

    private void SetRenderMode(float ratio)
    {
      Color color = Color.Lerp(this.StartColor, this.FadeColor, ratio);
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        this.mGeneratedCharacter.SetColor(color);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mUnitController, (Object) null))
          return;
        this.mUnitController.SetColor(color);
      }
    }
  }
}
