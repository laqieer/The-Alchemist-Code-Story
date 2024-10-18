// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.UnityResolveryResolverGetFormatterHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  internal static class UnityResolveryResolverGetFormatterHelper
  {
    private static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
    {
      {
        typeof (Vector2),
        (object) new Vector2Formatter()
      },
      {
        typeof (Vector3),
        (object) new Vector3Formatter()
      },
      {
        typeof (Vector4),
        (object) new Vector4Formatter()
      },
      {
        typeof (Quaternion),
        (object) new QuaternionFormatter()
      },
      {
        typeof (Color),
        (object) new ColorFormatter()
      },
      {
        typeof (Bounds),
        (object) new BoundsFormatter()
      },
      {
        typeof (Rect),
        (object) new RectFormatter()
      },
      {
        typeof (Vector2?),
        (object) new StaticNullableFormatter<Vector2>((IMessagePackFormatter<Vector2>) new Vector2Formatter())
      },
      {
        typeof (Vector3?),
        (object) new StaticNullableFormatter<Vector3>((IMessagePackFormatter<Vector3>) new Vector3Formatter())
      },
      {
        typeof (Vector4?),
        (object) new StaticNullableFormatter<Vector4>((IMessagePackFormatter<Vector4>) new Vector4Formatter())
      },
      {
        typeof (Quaternion?),
        (object) new StaticNullableFormatter<Quaternion>((IMessagePackFormatter<Quaternion>) new QuaternionFormatter())
      },
      {
        typeof (Color?),
        (object) new StaticNullableFormatter<Color>((IMessagePackFormatter<Color>) new ColorFormatter())
      },
      {
        typeof (Bounds?),
        (object) new StaticNullableFormatter<Bounds>((IMessagePackFormatter<Bounds>) new BoundsFormatter())
      },
      {
        typeof (Rect?),
        (object) new StaticNullableFormatter<Rect>((IMessagePackFormatter<Rect>) new RectFormatter())
      },
      {
        typeof (Vector2[]),
        (object) new ArrayFormatter<Vector2>()
      },
      {
        typeof (Vector3[]),
        (object) new ArrayFormatter<Vector3>()
      },
      {
        typeof (Vector4[]),
        (object) new ArrayFormatter<Vector4>()
      },
      {
        typeof (Quaternion[]),
        (object) new ArrayFormatter<Quaternion>()
      },
      {
        typeof (Color[]),
        (object) new ArrayFormatter<Color>()
      },
      {
        typeof (Bounds[]),
        (object) new ArrayFormatter<Bounds>()
      },
      {
        typeof (Rect[]),
        (object) new ArrayFormatter<Rect>()
      },
      {
        typeof (Vector2?[]),
        (object) new ArrayFormatter<Vector2?>()
      },
      {
        typeof (Vector3?[]),
        (object) new ArrayFormatter<Vector3?>()
      },
      {
        typeof (Vector4?[]),
        (object) new ArrayFormatter<Vector4?>()
      },
      {
        typeof (Quaternion?[]),
        (object) new ArrayFormatter<Quaternion?>()
      },
      {
        typeof (Color?[]),
        (object) new ArrayFormatter<Color?>()
      },
      {
        typeof (Bounds?[]),
        (object) new ArrayFormatter<Bounds?>()
      },
      {
        typeof (Rect?[]),
        (object) new ArrayFormatter<Rect?>()
      },
      {
        typeof (List<Vector2>),
        (object) new ListFormatter<Vector2>()
      },
      {
        typeof (List<Vector3>),
        (object) new ListFormatter<Vector3>()
      },
      {
        typeof (List<Vector4>),
        (object) new ListFormatter<Vector4>()
      },
      {
        typeof (List<Quaternion>),
        (object) new ListFormatter<Quaternion>()
      },
      {
        typeof (List<Color>),
        (object) new ListFormatter<Color>()
      },
      {
        typeof (List<Bounds>),
        (object) new ListFormatter<Bounds>()
      },
      {
        typeof (List<Rect>),
        (object) new ListFormatter<Rect>()
      },
      {
        typeof (List<Vector2?>),
        (object) new ListFormatter<Vector2?>()
      },
      {
        typeof (List<Vector3?>),
        (object) new ListFormatter<Vector3?>()
      },
      {
        typeof (List<Vector4?>),
        (object) new ListFormatter<Vector4?>()
      },
      {
        typeof (List<Quaternion?>),
        (object) new ListFormatter<Quaternion?>()
      },
      {
        typeof (List<Color?>),
        (object) new ListFormatter<Color?>()
      },
      {
        typeof (List<Bounds?>),
        (object) new ListFormatter<Bounds?>()
      },
      {
        typeof (List<Rect?>),
        (object) new ListFormatter<Rect?>()
      },
      {
        typeof (AnimationCurve),
        (object) new AnimationCurveFormatter()
      },
      {
        typeof (RectOffset),
        (object) new RectOffsetFormatter()
      },
      {
        typeof (Gradient),
        (object) new GradientFormatter()
      },
      {
        typeof (WrapMode),
        (object) new WrapModeFormatter()
      },
      {
        typeof (GradientMode),
        (object) new GradientModeFormatter()
      },
      {
        typeof (Keyframe),
        (object) new KeyframeFormatter()
      },
      {
        typeof (Matrix4x4),
        (object) new Matrix4x4Formatter()
      },
      {
        typeof (GradientColorKey),
        (object) new GradientColorKeyFormatter()
      },
      {
        typeof (GradientAlphaKey),
        (object) new GradientAlphaKeyFormatter()
      },
      {
        typeof (Color32),
        (object) new Color32Formatter()
      },
      {
        typeof (LayerMask),
        (object) new LayerMaskFormatter()
      },
      {
        typeof (WrapMode?),
        (object) new StaticNullableFormatter<WrapMode>((IMessagePackFormatter<WrapMode>) new WrapModeFormatter())
      },
      {
        typeof (GradientMode?),
        (object) new StaticNullableFormatter<GradientMode>((IMessagePackFormatter<GradientMode>) new GradientModeFormatter())
      },
      {
        typeof (Keyframe?),
        (object) new StaticNullableFormatter<Keyframe>((IMessagePackFormatter<Keyframe>) new KeyframeFormatter())
      },
      {
        typeof (Matrix4x4?),
        (object) new StaticNullableFormatter<Matrix4x4>((IMessagePackFormatter<Matrix4x4>) new Matrix4x4Formatter())
      },
      {
        typeof (GradientColorKey?),
        (object) new StaticNullableFormatter<GradientColorKey>((IMessagePackFormatter<GradientColorKey>) new GradientColorKeyFormatter())
      },
      {
        typeof (GradientAlphaKey?),
        (object) new StaticNullableFormatter<GradientAlphaKey>((IMessagePackFormatter<GradientAlphaKey>) new GradientAlphaKeyFormatter())
      },
      {
        typeof (Color32?),
        (object) new StaticNullableFormatter<Color32>((IMessagePackFormatter<Color32>) new Color32Formatter())
      },
      {
        typeof (LayerMask?),
        (object) new StaticNullableFormatter<LayerMask>((IMessagePackFormatter<LayerMask>) new LayerMaskFormatter())
      },
      {
        typeof (AnimationCurve[]),
        (object) new ArrayFormatter<AnimationCurve>()
      },
      {
        typeof (RectOffset[]),
        (object) new ArrayFormatter<RectOffset>()
      },
      {
        typeof (Gradient[]),
        (object) new ArrayFormatter<Gradient>()
      },
      {
        typeof (WrapMode[]),
        (object) new ArrayFormatter<WrapMode>()
      },
      {
        typeof (GradientMode[]),
        (object) new ArrayFormatter<GradientMode>()
      },
      {
        typeof (Keyframe[]),
        (object) new ArrayFormatter<Keyframe>()
      },
      {
        typeof (Matrix4x4[]),
        (object) new ArrayFormatter<Matrix4x4>()
      },
      {
        typeof (GradientColorKey[]),
        (object) new ArrayFormatter<GradientColorKey>()
      },
      {
        typeof (GradientAlphaKey[]),
        (object) new ArrayFormatter<GradientAlphaKey>()
      },
      {
        typeof (Color32[]),
        (object) new ArrayFormatter<Color32>()
      },
      {
        typeof (LayerMask[]),
        (object) new ArrayFormatter<LayerMask>()
      },
      {
        typeof (WrapMode?[]),
        (object) new ArrayFormatter<WrapMode?>()
      },
      {
        typeof (GradientMode?[]),
        (object) new ArrayFormatter<GradientMode?>()
      },
      {
        typeof (Keyframe?[]),
        (object) new ArrayFormatter<Keyframe?>()
      },
      {
        typeof (Matrix4x4?[]),
        (object) new ArrayFormatter<Matrix4x4?>()
      },
      {
        typeof (GradientColorKey?[]),
        (object) new ArrayFormatter<GradientColorKey?>()
      },
      {
        typeof (GradientAlphaKey?[]),
        (object) new ArrayFormatter<GradientAlphaKey?>()
      },
      {
        typeof (Color32?[]),
        (object) new ArrayFormatter<Color32?>()
      },
      {
        typeof (LayerMask?[]),
        (object) new ArrayFormatter<LayerMask?>()
      },
      {
        typeof (List<AnimationCurve>),
        (object) new ListFormatter<AnimationCurve>()
      },
      {
        typeof (List<RectOffset>),
        (object) new ListFormatter<RectOffset>()
      },
      {
        typeof (List<Gradient>),
        (object) new ListFormatter<Gradient>()
      },
      {
        typeof (List<WrapMode>),
        (object) new ListFormatter<WrapMode>()
      },
      {
        typeof (List<GradientMode>),
        (object) new ListFormatter<GradientMode>()
      },
      {
        typeof (List<Keyframe>),
        (object) new ListFormatter<Keyframe>()
      },
      {
        typeof (List<Matrix4x4>),
        (object) new ListFormatter<Matrix4x4>()
      },
      {
        typeof (List<GradientColorKey>),
        (object) new ListFormatter<GradientColorKey>()
      },
      {
        typeof (List<GradientAlphaKey>),
        (object) new ListFormatter<GradientAlphaKey>()
      },
      {
        typeof (List<Color32>),
        (object) new ListFormatter<Color32>()
      },
      {
        typeof (List<LayerMask>),
        (object) new ListFormatter<LayerMask>()
      },
      {
        typeof (List<WrapMode?>),
        (object) new ListFormatter<WrapMode?>()
      },
      {
        typeof (List<GradientMode?>),
        (object) new ListFormatter<GradientMode?>()
      },
      {
        typeof (List<Keyframe?>),
        (object) new ListFormatter<Keyframe?>()
      },
      {
        typeof (List<Matrix4x4?>),
        (object) new ListFormatter<Matrix4x4?>()
      },
      {
        typeof (List<GradientColorKey?>),
        (object) new ListFormatter<GradientColorKey?>()
      },
      {
        typeof (List<GradientAlphaKey?>),
        (object) new ListFormatter<GradientAlphaKey?>()
      },
      {
        typeof (List<Color32?>),
        (object) new ListFormatter<Color32?>()
      },
      {
        typeof (List<LayerMask?>),
        (object) new ListFormatter<LayerMask?>()
      },
      {
        typeof (Vector2Int),
        (object) new Vector2IntFormatter()
      },
      {
        typeof (Vector3Int),
        (object) new Vector3IntFormatter()
      },
      {
        typeof (RangeInt),
        (object) new RangeIntFormatter()
      },
      {
        typeof (RectInt),
        (object) new RectIntFormatter()
      },
      {
        typeof (BoundsInt),
        (object) new BoundsIntFormatter()
      },
      {
        typeof (Vector2Int?),
        (object) new StaticNullableFormatter<Vector2Int>((IMessagePackFormatter<Vector2Int>) new Vector2IntFormatter())
      },
      {
        typeof (Vector3Int?),
        (object) new StaticNullableFormatter<Vector3Int>((IMessagePackFormatter<Vector3Int>) new Vector3IntFormatter())
      },
      {
        typeof (RangeInt?),
        (object) new StaticNullableFormatter<RangeInt>((IMessagePackFormatter<RangeInt>) new RangeIntFormatter())
      },
      {
        typeof (RectInt?),
        (object) new StaticNullableFormatter<RectInt>((IMessagePackFormatter<RectInt>) new RectIntFormatter())
      },
      {
        typeof (BoundsInt?),
        (object) new StaticNullableFormatter<BoundsInt>((IMessagePackFormatter<BoundsInt>) new BoundsIntFormatter())
      },
      {
        typeof (Vector2Int[]),
        (object) new ArrayFormatter<Vector2Int>()
      },
      {
        typeof (Vector3Int[]),
        (object) new ArrayFormatter<Vector3Int>()
      },
      {
        typeof (RangeInt[]),
        (object) new ArrayFormatter<RangeInt>()
      },
      {
        typeof (RectInt[]),
        (object) new ArrayFormatter<RectInt>()
      },
      {
        typeof (BoundsInt[]),
        (object) new ArrayFormatter<BoundsInt>()
      },
      {
        typeof (Vector2Int?[]),
        (object) new ArrayFormatter<Vector2Int?>()
      },
      {
        typeof (Vector3Int?[]),
        (object) new ArrayFormatter<Vector3Int?>()
      },
      {
        typeof (RangeInt?[]),
        (object) new ArrayFormatter<RangeInt?>()
      },
      {
        typeof (RectInt?[]),
        (object) new ArrayFormatter<RectInt?>()
      },
      {
        typeof (BoundsInt?[]),
        (object) new ArrayFormatter<BoundsInt?>()
      },
      {
        typeof (List<Vector2Int>),
        (object) new ListFormatter<Vector2Int>()
      },
      {
        typeof (List<Vector3Int>),
        (object) new ListFormatter<Vector3Int>()
      },
      {
        typeof (List<RangeInt>),
        (object) new ListFormatter<RangeInt>()
      },
      {
        typeof (List<RectInt>),
        (object) new ListFormatter<RectInt>()
      },
      {
        typeof (List<BoundsInt>),
        (object) new ListFormatter<BoundsInt>()
      },
      {
        typeof (List<Vector2Int?>),
        (object) new ListFormatter<Vector2Int?>()
      },
      {
        typeof (List<Vector3Int?>),
        (object) new ListFormatter<Vector3Int?>()
      },
      {
        typeof (List<RangeInt?>),
        (object) new ListFormatter<RangeInt?>()
      },
      {
        typeof (List<RectInt?>),
        (object) new ListFormatter<RectInt?>()
      },
      {
        typeof (List<BoundsInt?>),
        (object) new ListFormatter<BoundsInt?>()
      }
    };

    internal static object GetFormatter(Type t)
    {
      object obj;
      return UnityResolveryResolverGetFormatterHelper.formatterMap.TryGetValue(t, out obj) ? obj : (object) null;
    }
  }
}
