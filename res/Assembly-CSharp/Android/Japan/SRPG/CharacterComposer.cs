// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterComposer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public struct CharacterComposer
  {
    public GameObject Body;
    public GameObject BodyAttachment;
    public Texture2D BodyTexture;
    public GameObject Head;
    public GameObject HeadAttachment;
    public GameObject Hair;
    public Color32 HairColor0;
    public Color32 HairColor1;

    public bool IsValid
    {
      get
      {
        return (UnityEngine.Object) this.Body != (UnityEngine.Object) null;
      }
    }

    public void LoadImmediately(string characterID, ESex sex, string jobID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character != null)
      {
        int index = character.IndexOfJob(jobID);
        if (string.IsNullOrEmpty(jobID) || index >= 0)
        {
          CharacterDB.Job job = character.Jobs[index];
          if (!string.IsNullOrEmpty(job.BodyName))
          {
            this.Body = Resources.Load<GameObject>("CH/BODY/" + job.BodyName);
            if ((UnityEngine.Object) this.Body == (UnityEngine.Object) null)
            {
              Debug.LogError((object) ("Failed to load " + job.BodyName));
            }
            else
            {
              if (!string.IsNullOrEmpty(job.BodyTextureName))
              {
                this.BodyTexture = Resources.Load<Texture2D>("CH/BODYTEX/" + job.BodyTextureName);
                if ((UnityEngine.Object) this.BodyTexture == (UnityEngine.Object) null)
                {
                  Debug.LogError((object) ("Failed to load " + job.BodyTextureName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.BodyAttachmentName))
              {
                this.BodyAttachment = Resources.Load<GameObject>("CH/BODYOPT/" + job.BodyAttachmentName);
                if ((UnityEngine.Object) this.BodyAttachment == (UnityEngine.Object) null)
                {
                  Debug.LogError((object) ("Failed to load " + job.BodyAttachmentName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.HeadName))
              {
                this.Head = Resources.Load<GameObject>("CH/HEAD/" + job.HeadName);
                if ((UnityEngine.Object) this.Head == (UnityEngine.Object) null)
                {
                  Debug.LogError((object) ("Failed to load " + job.HeadName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.HairName))
              {
                this.Hair = Resources.Load<GameObject>("CH/HAIR/" + job.HairName);
                if ((UnityEngine.Object) this.Hair == (UnityEngine.Object) null)
                {
                  Debug.LogError((object) ("Failed to load " + job.HairName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.HeadAttachmentName))
              {
                this.HeadAttachment = Resources.Load<GameObject>("CH/HEADOPT/" + job.HeadAttachmentName);
                if ((UnityEngine.Object) this.HeadAttachment == (UnityEngine.Object) null)
                {
                  Debug.LogError((object) ("Failed to load " + job.HeadAttachmentName));
                  goto label_24;
                }
              }
              this.HairColor0 = job.HairColor0;
              this.HairColor1 = job.HairColor1;
              return;
            }
          }
        }
        else
        {
          this.Body = Resources.Load<GameObject>("Units/" + sex.ToPrefix() + characterID);
          if ((UnityEngine.Object) this.Body != (UnityEngine.Object) null)
            return;
          Debug.LogError((object) ("Failed to load " + sex.ToPrefix() + characterID));
        }
      }
label_24:
      this.Body = Resources.Load<GameObject>("Units/NULL");
      this.BodyAttachment = (GameObject) null;
      this.BodyTexture = (Texture2D) null;
      this.Head = (GameObject) null;
      this.HeadAttachment = (GameObject) null;
      this.Hair = (GameObject) null;
    }

    public GameObject Compose(Vector3 position, Quaternion rotation)
    {
      if ((UnityEngine.Object) this.Body == (UnityEngine.Object) null)
        return (GameObject) null;
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.Body, position, rotation * this.Body.transform.rotation);
      Transform transform1 = gameObject1.transform;
      if ((UnityEngine.Object) this.BodyTexture != (UnityEngine.Object) null)
      {
        SkinnedMeshRenderer componentInChildren = gameObject1.GetComponentInChildren<SkinnedMeshRenderer>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null && (UnityEngine.Object) componentInChildren.sharedMaterial != (UnityEngine.Object) null)
          componentInChildren.sharedMaterial = new Material(componentInChildren.sharedMaterial)
          {
            mainTexture = (Texture) this.BodyTexture
          };
      }
      Transform childRecursively = GameUtility.findChildRecursively(transform1, "Bip001 Head");
      if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.Head != (UnityEngine.Object) null)
        {
          Transform transform2 = this.Head.transform;
          UnityEngine.Object.Instantiate<GameObject>(this.Head, transform2.localPosition, transform2.localRotation).transform.SetParent(childRecursively, false);
        }
        if ((UnityEngine.Object) this.Hair != (UnityEngine.Object) null)
        {
          Transform transform2 = this.Hair.transform;
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Hair, transform2.localPosition, transform2.localRotation);
          gameObject2.transform.SetParent(childRecursively, false);
          Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < componentsInChildren.Length; ++index)
          {
            if ((componentsInChildren[index] is MeshRenderer || componentsInChildren[index] is SkinnedMeshRenderer) && (UnityEngine.Object) componentsInChildren[index].sharedMaterial != (UnityEngine.Object) null)
            {
              Material material = new Material(componentsInChildren[index].sharedMaterial);
              material.hideFlags = HideFlags.DontSave;
              material.SetColor("_hairColor0", (Color) this.HairColor0);
              material.SetColor("_hairColor1", (Color) this.HairColor1);
              componentsInChildren[index].sharedMaterial = material;
            }
          }
        }
        if ((UnityEngine.Object) this.HeadAttachment != (UnityEngine.Object) null)
        {
          Transform transform2 = this.HeadAttachment.transform;
          UnityEngine.Object.Instantiate<GameObject>(this.HeadAttachment, transform2.localPosition, transform2.localRotation).transform.SetParent(childRecursively, false);
        }
      }
      return gameObject1;
    }
  }
}
