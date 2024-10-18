// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.Helper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  public static class Helper
  {
    public const string ScriptVersion = "1.6.10";

    public static string GetName(Platform platform)
    {
      return platform.ToString();
    }

    public static string GetErrorMessage(ErrorCode code)
    {
      string str = string.Empty;
      switch (code)
      {
        case ErrorCode.None:
          str = "No Error";
          break;
        case ErrorCode.LoadFailed:
          str = "Loading failed.  Codec not supported or video resolution too high or insufficient system resources.";
          if (SystemInfo.operatingSystem.StartsWith("Windows XP") || SystemInfo.operatingSystem.StartsWith("Windows Vista"))
          {
            str += " NOTE: Windows XP and Vista don't have native support for H.264 codec.  Consider using an older codec such as DivX or installing 3rd party codecs such as LAV Filters.";
            break;
          }
          break;
        case ErrorCode.DecodeFailed:
          str = "Decode failed.  Possible codec not supported, video resolution too high or insufficient system resources.";
          break;
      }
      return str;
    }

    public static string[] GetPlatformNames()
    {
      return new string[8]{ Helper.GetName(Platform.Windows), Helper.GetName(Platform.MacOSX), Helper.GetName(Platform.iOS), Helper.GetName(Platform.tvOS), Helper.GetName(Platform.Android), Helper.GetName(Platform.WindowsPhone), Helper.GetName(Platform.WindowsUWP), Helper.GetName(Platform.WebGL) };
    }

    [Conditional("ALWAYS_FALSE")]
    public static void LogInfo(string message, Object context = null)
    {
      if (context == (Object) null)
        Debug.Log((object) ("[AVProVideo] " + message));
      else
        Debug.Log((object) ("[AVProVideo] " + message), context);
    }

    public static string GetTimeString(float totalSeconds, bool showMilliseconds = false)
    {
      int num1 = Mathf.FloorToInt(totalSeconds / 3600f);
      float num2 = (float) ((double) num1 * 60.0 * 60.0);
      int num3 = Mathf.FloorToInt((float) (((double) totalSeconds - (double) num2) / 60.0));
      float num4 = num2 + (float) num3 * 60f;
      int num5 = Mathf.FloorToInt(totalSeconds - num4);
      string str;
      if (num1 <= 0)
      {
        if (showMilliseconds)
        {
          int num6 = (int) (((double) totalSeconds - (double) Mathf.Floor(totalSeconds)) * 1000.0);
          str = string.Format("{0:00}:{1:00}:{2:000}", (object) num3, (object) num5, (object) num6);
        }
        else
          str = string.Format("{0:00}:{1:00}", (object) num3, (object) num5);
      }
      else if (showMilliseconds)
      {
        int num6 = (int) (((double) totalSeconds - (double) Mathf.Floor(totalSeconds)) * 1000.0);
        str = string.Format("{2}:{0:00}:{1:00}:{3:000}", (object) num3, (object) num5, (object) num1, (object) num6);
      }
      else
        str = string.Format("{2}:{0:00}:{1:00}", (object) num3, (object) num5, (object) num1);
      return str;
    }

    public static Orientation GetOrientation(float[] t)
    {
      Orientation orientation = Orientation.Landscape;
      if ((double) t[0] == 0.0 && (double) t[1] == 1.0 && ((double) t[2] == -1.0 && (double) t[3] == 0.0))
        orientation = Orientation.Portrait;
      else if ((double) t[0] == 0.0 && (double) t[1] == -1.0 && ((double) t[2] == 1.0 && (double) t[3] == 0.0))
        orientation = Orientation.PortraitFlipped;
      else if ((double) t[0] == 1.0 && (double) t[1] == 0.0 && ((double) t[2] == 0.0 && (double) t[3] == 1.0))
        orientation = Orientation.Landscape;
      else if ((double) t[0] == -1.0 && (double) t[1] == 0.0 && ((double) t[2] == 0.0 && (double) t[3] == -1.0))
        orientation = Orientation.LandscapeFlipped;
      return orientation;
    }

    public static Matrix4x4 GetMatrixForOrientation(Orientation ori)
    {
      Matrix4x4 matrix4x4_1 = Matrix4x4.TRS(new Vector3(0.0f, 1f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90f), Vector3.one);
      Matrix4x4 matrix4x4_2 = Matrix4x4.TRS(new Vector3(1f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90f), Vector3.one);
      Matrix4x4 matrix4x4_3 = Matrix4x4.TRS(new Vector3(1f, 1f, 0.0f), Quaternion.identity, new Vector3(-1f, -1f, 1f));
      Matrix4x4 matrix4x4_4 = Matrix4x4.identity;
      switch (ori)
      {
        case Orientation.LandscapeFlipped:
          matrix4x4_4 = matrix4x4_3;
          break;
        case Orientation.Portrait:
          matrix4x4_4 = matrix4x4_1;
          break;
        case Orientation.PortraitFlipped:
          matrix4x4_4 = matrix4x4_2;
          break;
      }
      return matrix4x4_4;
    }

    public static void SetupStereoMaterial(Material material, StereoPacking packing, bool displayDebugTinting)
    {
      material.DisableKeyword("STEREO_CUSTOM_UV");
      material.DisableKeyword("STEREO_TOP_BOTTOM");
      material.DisableKeyword("STEREO_LEFT_RIGHT");
      material.DisableKeyword("MONOSCOPIC");
      switch (packing)
      {
        case StereoPacking.TopBottom:
          material.EnableKeyword("STEREO_TOP_BOTTOM");
          break;
        case StereoPacking.LeftRight:
          material.EnableKeyword("STEREO_LEFT_RIGHT");
          break;
        case StereoPacking.CustomUV:
          material.EnableKeyword("STEREO_CUSTOM_UV");
          break;
      }
      if (displayDebugTinting)
        material.EnableKeyword("STEREO_DEBUG");
      else
        material.DisableKeyword("STEREO_DEBUG");
    }

    public static void SetupAlphaPackedMaterial(Material material, AlphaPacking packing)
    {
      material.DisableKeyword("ALPHAPACK_TOP_BOTTOM");
      material.DisableKeyword("ALPHAPACK_LEFT_RIGHT");
      material.DisableKeyword("ALPHAPACK_NONE");
      if (packing == AlphaPacking.None)
        return;
      if (packing != AlphaPacking.TopBottom)
      {
        if (packing != AlphaPacking.LeftRight)
          return;
        material.EnableKeyword("ALPHAPACK_LEFT_RIGHT");
      }
      else
        material.EnableKeyword("ALPHAPACK_TOP_BOTTOM");
    }

    public static void SetupGammaMaterial(Material material, bool playerSupportsLinear)
    {
      if (QualitySettings.activeColorSpace == ColorSpace.Linear && !playerSupportsLinear)
        material.EnableKeyword("APPLY_GAMMA");
      else
        material.DisableKeyword("APPLY_GAMMA");
    }

    public static int ConvertTimeSecondsToFrame(float seconds, float frameRate)
    {
      return Mathf.FloorToInt(frameRate * seconds);
    }

    public static float ConvertFrameToTimeSeconds(int frame, float frameRate)
    {
      float num = 1f / frameRate;
      return (float) ((double) frame * (double) num + (double) num * 0.5);
    }

    public static void DrawTexture(Rect screenRect, Texture texture, ScaleMode scaleMode, AlphaPacking alphaPacking, Material material)
    {
      if (Event.current.type != UnityEngine.EventType.Repaint)
        return;
      float width1 = (float) texture.width;
      float height1 = (float) texture.height;
      switch (alphaPacking)
      {
        case AlphaPacking.TopBottom:
          height1 *= 0.5f;
          break;
        case AlphaPacking.LeftRight:
          width1 *= 0.5f;
          break;
      }
      float num1 = width1 / height1;
      Rect sourceRect = new Rect(0.0f, 0.0f, 1f, 1f);
      switch (scaleMode)
      {
        case ScaleMode.ScaleAndCrop:
          float num2 = screenRect.width / screenRect.height;
          if ((double) num2 > (double) num1)
          {
            float height2 = num1 / num2;
            sourceRect = new Rect(0.0f, (float) ((1.0 - (double) height2) * 0.5), 1f, height2);
            break;
          }
          float width2 = num2 / num1;
          sourceRect = new Rect((float) (0.5 - (double) width2 * 0.5), 0.0f, width2, 1f);
          break;
        case ScaleMode.ScaleToFit:
          float num3 = screenRect.width / screenRect.height;
          if ((double) num3 > (double) num1)
          {
            float num4 = num1 / num3;
            screenRect = new Rect(screenRect.xMin + (float) ((double) screenRect.width * (1.0 - (double) num4) * 0.5), screenRect.yMin, num4 * screenRect.width, screenRect.height);
            break;
          }
          float num5 = num3 / num1;
          screenRect = new Rect(screenRect.xMin, screenRect.yMin + (float) ((double) screenRect.height * (1.0 - (double) num5) * 0.5), screenRect.width, num5 * screenRect.height);
          break;
      }
      Graphics.DrawTexture(screenRect, texture, sourceRect, 0, 0, 0, 0, GUI.color, material);
    }

    public static Texture2D GetReadableTexture(Texture inputTexture, bool requiresVerticalFlip, Orientation ori, Texture2D targetTexture)
    {
      Texture2D texture2D = targetTexture;
      RenderTexture active = RenderTexture.active;
      int width = inputTexture.width;
      int height = inputTexture.height;
      RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
      if (ori == Orientation.Landscape)
      {
        if (!requiresVerticalFlip)
        {
          Graphics.Blit(inputTexture, temporary);
        }
        else
        {
          GL.PushMatrix();
          RenderTexture.active = temporary;
          GL.LoadPixelMatrix(0.0f, (float) temporary.width, 0.0f, (float) temporary.height);
          Rect sourceRect = new Rect(0.0f, 0.0f, 1f, 1f);
          Graphics.DrawTexture(new Rect(0.0f, -1f, (float) temporary.width, (float) temporary.height), inputTexture, sourceRect, 0, 0, 0, 0);
          GL.PopMatrix();
          GL.InvalidateState();
        }
      }
      if ((Object) texture2D == (Object) null)
        texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
      RenderTexture.active = temporary;
      texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) width, (float) height), 0, 0, false);
      texture2D.Apply(false, false);
      RenderTexture.ReleaseTemporary(temporary);
      RenderTexture.active = active;
      return texture2D;
    }

    private static int ParseTimeToMs(string text)
    {
      int num1 = 0;
      string[] strArray = text.Split(new char[2]{ ':', ',' });
      if (strArray.Length == 4)
      {
        int num2 = int.Parse(strArray[0]);
        int num3 = int.Parse(strArray[1]);
        int num4 = int.Parse(strArray[2]);
        num1 = int.Parse(strArray[3]) + (num4 + (num3 + num2 * 60) * 60) * 1000;
      }
      return num1;
    }

    public static List<Subtitle> LoadSubtitlesSRT(string data)
    {
      List<Subtitle> subtitleList = (List<Subtitle>) null;
      if (!string.IsNullOrEmpty(data))
      {
        data = data.Trim();
        string[] strArray1 = data.Split(new string[4]{ "\n\r", "\r\n", "\n", "\r" }, StringSplitOptions.None);
        if (strArray1.Length >= 3)
        {
          subtitleList = new List<Subtitle>(256);
          int num1 = 0;
          int num2 = 0;
          Subtitle subtitle1 = (Subtitle) null;
          for (int index = 0; index < strArray1.Length; ++index)
          {
            switch (num2)
            {
              case 0:
                subtitle1 = new Subtitle();
                subtitle1.index = num1;
                break;
              case 1:
                string[] strArray2 = strArray1[index].Split(new string[1]{ " --> " }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray2.Length == 2)
                {
                  subtitle1.timeStartMs = Helper.ParseTimeToMs(strArray2[0]);
                  subtitle1.timeEndMs = Helper.ParseTimeToMs(strArray2[1]);
                  break;
                }
                break;
              default:
                if (!string.IsNullOrEmpty(strArray1[index]))
                {
                  if (num2 == 2)
                  {
                    subtitle1.text = strArray1[index];
                    break;
                  }
                  Subtitle subtitle2 = subtitle1;
                  subtitle2.text = subtitle2.text + "\n" + strArray1[index];
                  break;
                }
                break;
            }
            if (string.IsNullOrEmpty(strArray1[index]) && num2 > 1)
            {
              subtitleList.Add(subtitle1);
              num2 = 0;
              ++num1;
              subtitle1 = (Subtitle) null;
            }
            else
              ++num2;
          }
          if (subtitle1 != null)
            subtitleList.Add(subtitle1);
        }
        else
          Debug.LogWarning((object) "[AVProVideo] SRT format doesn't appear to be valid");
      }
      return subtitleList;
    }
  }
}
