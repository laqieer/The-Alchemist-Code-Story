// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ParticleGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ParticleGenerator : AnimEventWithTarget
  {
    public GameObject Template;
    public bool Attach;
    public bool NotParticle;
    public bool Preload;
    private GameObject generateObject;

    public void PreLoad(GameObject go)
    {
      if (!this.Preload)
        return;
      this.Generate(go);
    }

    private void Generate(GameObject go)
    {
      this.generateObject = (GameObject) null;
      if (Object.op_Equality((Object) this.Template, (Object) null))
        return;
      this.generateObject = Object.Instantiate<GameObject>(this.Template);
      this.generateObject.SetActive(false);
      if (!this.Attach || string.IsNullOrEmpty(this.BoneName))
        return;
      Transform transform = GameUtility.findChildRecursively(go.transform, this.BoneName);
      if (this.BoneName == "CAMERA" && Object.op_Implicit((Object) Camera.main))
        transform = ((Component) Camera.main).transform;
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      this.generateObject.transform.SetParent(transform);
    }

    public override void OnStart(GameObject go)
    {
      if (!this.Preload)
        this.Generate(go);
      if (Object.op_Equality((Object) this.generateObject, (Object) null))
        return;
      this.generateObject.SetActive(this.Template.activeSelf);
      Vector3 spawnPos;
      Quaternion spawnRot;
      this.CalcPosition(go, this.Template, out spawnPos, out spawnRot);
      Transform transform = this.generateObject.transform;
      transform.position = spawnPos;
      transform.rotation = spawnRot;
      if (!this.NotParticle)
        GameUtility.RequireComponent<OneShotParticle>(this.generateObject);
      if ((double) go.transform.lossyScale.x * (double) transform.lossyScale.z < 0.0)
      {
        Vector3 localScale = transform.localScale;
        localScale.z *= -1f;
        transform.localScale = localScale;
      }
      this.OnGenerate(this.generateObject);
      if ((double) this.End > (double) this.Start + 0.10000000149011612)
      {
        DestructTimer destructTimer = GameUtility.RequireComponent<DestructTimer>(this.generateObject);
        if (Object.op_Implicit((Object) destructTimer))
          destructTimer.Timer = this.End - this.Start;
      }
      this.generateObject = (GameObject) null;
    }

    protected virtual void OnGenerate(GameObject go)
    {
    }
  }
}
