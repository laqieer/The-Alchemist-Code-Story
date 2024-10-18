// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterDB
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CharacterDB
  {
    public const string BodyPath = "CH/BODY/";
    public const string BodyTexturePath = "CH/BODYTEX/";
    public const string HeadPath = "CH/HEAD/";
    public const string HairPath = "CH/HAIR/";
    public const string HeadAttachmentPath = "CH/HEADOPT/";
    public const string BodyAttachmentPath = "CH/BODYOPT/";
    public static readonly string DatabasePath = "Data/CHDB";
    private static List<CharacterDB.Character> mCharacters = new List<CharacterDB.Character>();
    private static bool mDBLoaded;

    public static GameObject ComposeCharacter(string characterID, string jobID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character == null)
      {
        Debug.LogError((object) ("Character '" + characterID + "' not found."));
        return (GameObject) null;
      }
      for (int index = 0; index < character.Jobs.Count; ++index)
      {
        if (!(character.Jobs[index].JobID == jobID))
          ;
      }
      Debug.LogError((object) ("Character '" + characterID + "'can't be '" + jobID + "'."));
      return (GameObject) null;
    }

    public static void UnloadAll()
    {
      CharacterDB.mDBLoaded = false;
      CharacterDB.mCharacters.Clear();
    }

    public static void ReloadDatabase()
    {
      CharacterDB.UnloadAll();
      CharacterDB.LoadDatabase();
    }

    public static CharacterDB.Character ReserveCharacter(string characterID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character == null)
      {
        character = new CharacterDB.Character(characterID);
        CharacterDB.mCharacters.Add(character);
      }
      return character;
    }

    public static CharacterDB.Character FindCharacter(string characterID)
    {
      CharacterDB.LoadDatabase();
      int hashCode = characterID.GetHashCode();
      for (int index = CharacterDB.mCharacters.Count - 1; index >= 0; --index)
      {
        if (CharacterDB.mCharacters[index].HashID == hashCode && CharacterDB.mCharacters[index].CharacterID == characterID)
          return CharacterDB.mCharacters[index];
      }
      return (CharacterDB.Character) null;
    }

    public static CharacterDB.Job FindCharacter(string characterID, string jobResourceID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character == null)
        return (CharacterDB.Job) null;
      if (string.IsNullOrEmpty(jobResourceID))
        jobResourceID = "none";
      for (int index = 0; index < character.Jobs.Count; ++index)
      {
        if (character.Jobs[index].JobID == jobResourceID)
          return character.Jobs[index];
      }
      return character.Jobs[0];
    }

    private static string SerializeColor(Color32 color)
    {
      return color.r.ToString() + "," + (object) color.g + "," + (object) color.b;
    }

    public static void LoadDatabase()
    {
      if (CharacterDB.mDBLoaded)
        return;
      CharacterDB.mDBLoaded = true;
      string s = AssetManager.LoadTextData(CharacterDB.DatabasePath);
      if (string.IsNullOrEmpty(s))
      {
        Debug.LogError((object) "Failed to load CharacterDB");
      }
      else
      {
        char[] chArray = new char[1]{ '\t' };
        CharacterDB.mCharacters.Clear();
        using (StringReader stringReader = new StringReader(s))
        {
          string str1 = stringReader.ReadLine();
          if (string.IsNullOrEmpty(str1))
            return;
          string[] array = str1.Split(chArray);
          int index1 = Array.IndexOf<string>(array, "ID");
          int index2 = Array.IndexOf<string>(array, "JOB");
          int index3 = Array.IndexOf<string>(array, "BODY");
          int index4 = Array.IndexOf<string>(array, "BODY_TEXTURE");
          int index5 = Array.IndexOf<string>(array, "BODY_ATTACHMENT");
          int index6 = Array.IndexOf<string>(array, "HEAD");
          int index7 = Array.IndexOf<string>(array, "HEAD_ATTACHMENT");
          int index8 = Array.IndexOf<string>(array, "HAIR");
          int index9 = Array.IndexOf<string>(array, "HAIR_COLOR1");
          int index10 = Array.IndexOf<string>(array, "HAIR_COLOR2");
          int index11 = Array.IndexOf<string>(array, "PREFIX");
          int index12 = Array.IndexOf<string>(array, "MOVABLE");
          int index13 = Array.IndexOf<string>(array, "IS_JOB_DOWNSTAND");
          int index14 = Array.IndexOf<string>(array, "IS_JOB_DODGE");
          int index15 = Array.IndexOf<string>(array, "IS_JOB_DAMAGE");
          int index16 = Array.IndexOf<string>(array, "IS_JOB_STEP");
          int index17 = Array.IndexOf<string>(array, "IS_JOB_FALLLOOP");
          int index18 = Array.IndexOf<string>(array, "IS_JOB_JUMPLOOP");
          int index19 = Array.IndexOf<string>(array, "IS_JOB_PICKUP");
          int index20 = Array.IndexOf<string>(array, "IS_JOB_FREEZE");
          int index21 = Array.IndexOf<string>(array, "IS_JOB_GROGGY");
          int index22 = Array.IndexOf<string>(array, "IS_NO_TURN");
          string str2;
          while ((str2 = stringReader.ReadLine()) != null)
          {
            string[] columns = str2.Split(chArray);
            if (str2.Length > 0)
            {
              CharacterDB.Character character = CharacterDB.ReserveCharacter(columns[index1]);
              CharacterDB.Job job = new CharacterDB.Job()
              {
                JobID = columns[index2]
              };
              job.HashID = job.JobID.GetHashCode();
              job.HairName = columns[index8];
              job.BodyName = columns[index3];
              job.BodyTextureName = columns[index4];
              job.BodyAttachmentName = columns[index5];
              job.HeadName = columns[index6];
              job.HeadAttachmentName = columns[index7];
              job.HairColor0 = GameUtility.ParseColor(columns[index9]);
              job.HairColor1 = GameUtility.ParseColor(columns[index10]);
              job.Movable = true;
              if (index11 != -1)
                job.AssetPrefix = columns[index11];
              int result;
              if (index12 != -1 && int.TryParse(columns[index12], out result))
                job.Movable = result != 0;
              CharacterDB.GetColumnsBool(columns, index13, ref job.IsJobDownstand);
              CharacterDB.GetColumnsBool(columns, index14, ref job.IsJobDodge);
              CharacterDB.GetColumnsBool(columns, index15, ref job.IsJobDamage);
              CharacterDB.GetColumnsBool(columns, index16, ref job.IsJobStep);
              CharacterDB.GetColumnsBool(columns, index17, ref job.IsJobFallloop);
              CharacterDB.GetColumnsBool(columns, index18, ref job.IsJobJumploop);
              CharacterDB.GetColumnsBool(columns, index19, ref job.IsJobPickup);
              CharacterDB.GetColumnsBool(columns, index20, ref job.IsJobFreeze);
              CharacterDB.GetColumnsBool(columns, index21, ref job.IsJobGroggy);
              CharacterDB.GetColumnsBool(columns, index22, ref job.IsNoTurn);
              character.Jobs.Add(job);
            }
          }
        }
      }
    }

    private static void GetColumnsBool(string[] columns, int index, ref bool result)
    {
      int result1;
      if (columns == null || 0 > index || index >= columns.Length || !int.TryParse(columns[index], out result1))
        return;
      result = result1 != 0;
    }

    [Serializable]
    public class Job
    {
      public int HashID;
      public string JobID;
      public string AssetPrefix;
      [StringIsResourcePath(typeof (GameObject), "CH/BODY/")]
      public string BodyName;
      [StringIsResourcePath(typeof (GameObject), "CH/BODYOPT/")]
      public string BodyAttachmentName;
      [StringIsResourcePath(typeof (Texture2D), "CH/BODYTEX/")]
      public string BodyTextureName;
      [StringIsResourcePath(typeof (GameObject), "CH/HEAD/")]
      public string HeadName;
      [StringIsResourcePath(typeof (GameObject), "CH/HAIR/")]
      public string HairName;
      [StringIsResourcePath(typeof (GameObject), "CH/HEADOPT/")]
      public string HeadAttachmentName;
      public Color32 HairColor0;
      public Color32 HairColor1;
      public bool Movable;
      public bool IsJobDownstand;
      public bool IsJobDodge;
      public bool IsJobDamage;
      public bool IsJobStep;
      public bool IsJobFallloop;
      public bool IsJobJumploop;
      public bool IsJobPickup;
      public bool IsJobFreeze;
      public bool IsJobGroggy;
      public bool IsNoTurn;

      public Job()
      {
      }

      public Job(string jobID)
      {
        this.JobID = jobID;
        this.HashID = this.JobID.GetHashCode();
        this.AssetPrefix = (string) null;
        this.BodyName = (string) null;
        this.BodyAttachmentName = (string) null;
        this.BodyTextureName = (string) null;
        this.HeadName = (string) null;
        this.HairName = (string) null;
        this.HeadAttachmentName = (string) null;
        this.HairColor0 = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
        this.HairColor1 = this.HairColor0;
        this.Movable = true;
      }
    }

    [Serializable]
    public class Character
    {
      public int HashID;
      public string CharacterID;
      public List<CharacterDB.Job> Jobs = new List<CharacterDB.Job>();

      public Character(string characterID)
      {
        this.CharacterID = characterID;
        this.HashID = this.CharacterID.GetHashCode();
      }

      public int IndexOfJob(string jobID)
      {
        for (int index = 0; index < this.Jobs.Count; ++index)
        {
          if (this.Jobs[index].JobID == jobID)
            return index;
        }
        return -1;
      }
    }
  }
}
