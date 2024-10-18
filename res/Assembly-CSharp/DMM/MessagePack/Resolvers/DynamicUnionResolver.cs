// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.DynamicUnionResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class DynamicUnionResolver : IFormatterResolver
  {
    public static readonly DynamicUnionResolver Instance = new DynamicUnionResolver();
    private const string ModuleName = "MessagePack.Resolvers.DynamicUnionResolver";
    private static readonly DynamicAssembly assembly;
    private static readonly Regex SubtractFullNameRegex = new Regex(", Version=\\d+.\\d+.\\d+.\\d+, Culture=\\w+, PublicKeyToken=\\w+");
    private static int nameSequence = 0;
    private static readonly System.Type refByte = typeof (byte[]).MakeByRefType();
    private static readonly System.Type refInt = typeof (int).MakeByRefType();
    private static readonly System.Type refKvp = typeof (KeyValuePair<int, int>).MakeByRefType();
    private static readonly MethodInfo getFormatterWithVerify = ((IEnumerable<MethodInfo>) System.Reflection.ReflectionExtensions.GetRuntimeMethods(typeof (FormatterResolverExtensions))).First<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == "GetFormatterWithVerify"));
    private static readonly Func<System.Type, MethodInfo> getSerialize = (Func<System.Type, MethodInfo>) (t => System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackFormatter<>).MakeGenericType(t), "Serialize", new System.Type[4]
    {
      DynamicUnionResolver.refByte,
      typeof (int),
      t,
      typeof (IFormatterResolver)
    }));
    private static readonly Func<System.Type, MethodInfo> getDeserialize = (Func<System.Type, MethodInfo>) (t => System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackFormatter<>).MakeGenericType(t), "Deserialize", new System.Type[4]
    {
      typeof (byte[]),
      typeof (int),
      typeof (IFormatterResolver),
      DynamicUnionResolver.refInt
    }));
    private static readonly FieldInfo runtimeTypeHandleEqualityComparer = System.Reflection.ReflectionExtensions.GetRuntimeField(typeof (RuntimeTypeHandleEqualityComparer), "Default");
    private static readonly ConstructorInfo intIntKeyValuePairConstructor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (KeyValuePair<int, int>)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 2));
    private static readonly ConstructorInfo typeMapDictionaryConstructor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>>)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x =>
    {
      ParameterInfo[] parameters = x.GetParameters();
      return parameters.Length == 2 && (object) parameters[0].ParameterType == (object) typeof (int);
    }));
    private static readonly MethodInfo typeMapDictionaryAdd = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>>), "Add", new System.Type[2]
    {
      typeof (RuntimeTypeHandle),
      typeof (KeyValuePair<int, int>)
    });
    private static readonly MethodInfo typeMapDictionaryTryGetValue = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>>), "TryGetValue", new System.Type[2]
    {
      typeof (RuntimeTypeHandle),
      DynamicUnionResolver.refKvp
    });
    private static readonly ConstructorInfo keyMapDictionaryConstructor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (Dictionary<int, int>)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x =>
    {
      ParameterInfo[] parameters = x.GetParameters();
      return parameters.Length == 1 && (object) parameters[0].ParameterType == (object) typeof (int);
    }));
    private static readonly MethodInfo keyMapDictionaryAdd = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (Dictionary<int, int>), "Add", new System.Type[2]
    {
      typeof (int),
      typeof (int)
    });
    private static readonly MethodInfo keyMapDictionaryTryGetValue = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (Dictionary<int, int>), "TryGetValue", new System.Type[2]
    {
      typeof (int),
      DynamicUnionResolver.refInt
    });
    private static readonly MethodInfo objectGetType = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (object), "GetType", System.Type.EmptyTypes);
    private static readonly MethodInfo getTypeHandle = System.Reflection.ReflectionExtensions.GetRuntimeProperty(typeof (System.Type), "TypeHandle").GetGetMethod();
    private static readonly MethodInfo intIntKeyValuePairGetKey = System.Reflection.ReflectionExtensions.GetRuntimeProperty(typeof (KeyValuePair<int, int>), "Key").GetGetMethod();
    private static readonly MethodInfo intIntKeyValuePairGetValue = System.Reflection.ReflectionExtensions.GetRuntimeProperty(typeof (KeyValuePair<int, int>), "Value").GetGetMethod();
    private static readonly ConstructorInfo invalidOperationExceptionConstructor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (InvalidOperationException)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x =>
    {
      ParameterInfo[] parameters = x.GetParameters();
      return parameters.Length == 1 && (object) parameters[0].ParameterType == (object) typeof (string);
    }));
    private static readonly ConstructorInfo objectCtor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (object)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0));

    private DynamicUnionResolver()
    {
    }

    static DynamicUnionResolver()
    {
      DynamicUnionResolver.assembly = new DynamicAssembly("MessagePack.Resolvers.DynamicUnionResolver");
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return DynamicUnionResolver.FormatterCache<T>.formatter;
    }

    private static TypeInfo BuildType(System.Type type)
    {
      TypeInfo typeInfo = System.Reflection.ReflectionExtensions.GetTypeInfo(type);
      UnionAttribute[] array = typeInfo.GetCustomAttributes<UnionAttribute>().OrderBy<UnionAttribute, int>((Func<UnionAttribute, int>) (x => x.Key)).ToArray<UnionAttribute>();
      if (array.Length == 0)
        return (TypeInfo) null;
      if (!typeInfo.IsInterface && !typeInfo.IsAbstract)
        throw new MessagePackDynamicUnionResolverException("Union can only be interface or abstract class. Type:" + type.Name);
      HashSet<int> intSet = new HashSet<int>();
      HashSet<System.Type> typeSet = new HashSet<System.Type>();
      foreach (UnionAttribute unionAttribute in array)
      {
        if (!intSet.Add(unionAttribute.Key))
          throw new MessagePackDynamicUnionResolverException("Same union key has found. Type:" + type.Name + " Key:" + (object) unionAttribute.Key);
        if (!typeSet.Add(unionAttribute.SubType))
          throw new MessagePackDynamicUnionResolverException("Same union subType has found. Type:" + type.Name + " SubType: " + (object) unionAttribute.SubType);
      }
      System.Type type1 = typeof (IMessagePackFormatter<>).MakeGenericType(type);
      TypeBuilder type2 = DynamicUnionResolver.assembly.DefineType("MessagePack.Formatters." + DynamicUnionResolver.SubtractFullNameRegex.Replace(type.FullName, string.Empty).Replace(".", "_") + "Formatter" + (object) Interlocked.Increment(ref DynamicUnionResolver.nameSequence), TypeAttributes.Public | TypeAttributes.Sealed, (System.Type) null, new System.Type[1]
      {
        type1
      });
      ConstructorBuilder method1 = type2.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, System.Type.EmptyTypes);
      FieldBuilder typeToKeyAndJumpMap = type2.DefineField("typeToKeyAndJumpMap", typeof (Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>>), FieldAttributes.Private | FieldAttributes.InitOnly);
      FieldBuilder keyToJumpMap = type2.DefineField("keyToJumpMap", typeof (Dictionary<int, int>), FieldAttributes.Private | FieldAttributes.InitOnly);
      ILGenerator ilGenerator1 = method1.GetILGenerator();
      DynamicUnionResolver.BuildConstructor(type, array, (ConstructorInfo) method1, typeToKeyAndJumpMap, keyToJumpMap, ilGenerator1);
      MethodBuilder method2 = type2.DefineMethod("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, typeof (int), new System.Type[4]
      {
        typeof (byte[]).MakeByRefType(),
        typeof (int),
        type,
        typeof (IFormatterResolver)
      });
      ILGenerator ilGenerator2 = method2.GetILGenerator();
      DynamicUnionResolver.BuildSerialize(type, array, method2, typeToKeyAndJumpMap, ilGenerator2);
      MethodBuilder method3 = type2.DefineMethod("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, type, new System.Type[4]
      {
        typeof (byte[]),
        typeof (int),
        typeof (IFormatterResolver),
        typeof (int).MakeByRefType()
      });
      ILGenerator ilGenerator3 = method3.GetILGenerator();
      DynamicUnionResolver.BuildDeserialize(type, array, method3, keyToJumpMap, ilGenerator3);
      return System.Reflection.ReflectionExtensions.CreateTypeInfo(type2);
    }

    private static void BuildConstructor(
      System.Type type,
      UnionAttribute[] infos,
      ConstructorInfo method,
      FieldBuilder typeToKeyAndJumpMap,
      FieldBuilder keyToJumpMap,
      ILGenerator il)
    {
      il.EmitLdarg(0);
      il.Emit(OpCodes.Call, DynamicUnionResolver.objectCtor);
      il.EmitLdarg(0);
      il.EmitLdc_I4(infos.Length);
      il.Emit(OpCodes.Ldsfld, DynamicUnionResolver.runtimeTypeHandleEqualityComparer);
      il.Emit(OpCodes.Newobj, DynamicUnionResolver.typeMapDictionaryConstructor);
      int num1 = 0;
      foreach (UnionAttribute info in infos)
      {
        il.Emit(OpCodes.Dup);
        il.Emit(OpCodes.Ldtoken, info.SubType);
        il.EmitLdc_I4(info.Key);
        il.EmitLdc_I4(num1);
        il.Emit(OpCodes.Newobj, DynamicUnionResolver.intIntKeyValuePairConstructor);
        il.EmitCall(DynamicUnionResolver.typeMapDictionaryAdd);
        ++num1;
      }
      il.Emit(OpCodes.Stfld, (FieldInfo) typeToKeyAndJumpMap);
      il.EmitLdarg(0);
      il.EmitLdc_I4(infos.Length);
      il.Emit(OpCodes.Newobj, DynamicUnionResolver.keyMapDictionaryConstructor);
      int num2 = 0;
      foreach (UnionAttribute info in infos)
      {
        il.Emit(OpCodes.Dup);
        il.EmitLdc_I4(info.Key);
        il.EmitLdc_I4(num2);
        il.EmitCall(DynamicUnionResolver.keyMapDictionaryAdd);
        ++num2;
      }
      il.Emit(OpCodes.Stfld, (FieldInfo) keyToJumpMap);
      il.Emit(OpCodes.Ret);
    }

    private static void BuildSerialize(
      System.Type type,
      UnionAttribute[] infos,
      MethodBuilder method,
      FieldBuilder typeToKeyAndJumpMap,
      ILGenerator il)
    {
      Label label1 = il.DefineLabel();
      Label label2 = il.DefineLabel();
      il.EmitLdarg(3);
      il.Emit(OpCodes.Brtrue_S, label1);
      il.Emit(OpCodes.Br, label2);
      il.MarkLabel(label1);
      LocalBuilder keyPair = il.DeclareLocal(typeof (KeyValuePair<int, int>));
      il.EmitLoadThis();
      il.EmitLdfld((FieldInfo) typeToKeyAndJumpMap);
      il.EmitLdarg(3);
      il.EmitCall(DynamicUnionResolver.objectGetType);
      il.EmitCall(DynamicUnionResolver.getTypeHandle);
      il.EmitLdloca(keyPair);
      il.EmitCall(DynamicUnionResolver.typeMapDictionaryTryGetValue);
      il.Emit(OpCodes.Brfalse, label2);
      LocalBuilder local = il.DeclareLocal(typeof (int));
      il.EmitLdarg(2);
      il.EmitStloc(local);
      DynamicUnionResolver.EmitOffsetPlusEqual(il, (Action) null, (Action) (() =>
      {
        il.EmitLdc_I4(2);
        il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.WriteFixedArrayHeaderUnsafe);
      }));
      DynamicUnionResolver.EmitOffsetPlusEqual(il, (Action) null, (Action) (() =>
      {
        il.EmitLdloca(keyPair);
        il.EmitCall(DynamicUnionResolver.intIntKeyValuePairGetKey);
        il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.WriteInt32);
      }));
      Label label3 = il.DefineLabel();
      // ISSUE: object of a compiler-generated type is created
      \u003C\u003E__AnonType0<Label, UnionAttribute>[] array = ((IEnumerable<UnionAttribute>) infos).Select<UnionAttribute, \u003C\u003E__AnonType0<Label, UnionAttribute>>((Func<UnionAttribute, \u003C\u003E__AnonType0<Label, UnionAttribute>>) (x => new \u003C\u003E__AnonType0<Label, UnionAttribute>(il.DefineLabel(), x))).ToArray<\u003C\u003E__AnonType0<Label, UnionAttribute>>();
      il.EmitLdloca(keyPair);
      il.EmitCall(DynamicUnionResolver.intIntKeyValuePairGetValue);
      il.Emit(OpCodes.Switch, ((IEnumerable<\u003C\u003E__AnonType0<Label, UnionAttribute>>) array).Select<\u003C\u003E__AnonType0<Label, UnionAttribute>, Label>((Func<\u003C\u003E__AnonType0<Label, UnionAttribute>, Label>) (x => x.Label)).ToArray<Label>());
      il.Emit(OpCodes.Br, label3);
      foreach (\u003C\u003E__AnonType0<Label, UnionAttribute> anonType0 in array)
      {
        // ISSUE: variable of a compiler-generated type
        \u003C\u003E__AnonType0<Label, UnionAttribute> item = anonType0;
        il.MarkLabel(item.Label);
        DynamicUnionResolver.EmitOffsetPlusEqual(il, (Action) (() =>
        {
          il.EmitLdarg(4);
          il.Emit(OpCodes.Call, DynamicUnionResolver.getFormatterWithVerify.MakeGenericMethod(item.Attr.SubType));
        }), (Action) (() =>
        {
          il.EmitLdarg(3);
          if (System.Reflection.ReflectionExtensions.GetTypeInfo(item.Attr.SubType).IsValueType)
            il.Emit(OpCodes.Unbox_Any, item.Attr.SubType);
          else
            il.Emit(OpCodes.Castclass, item.Attr.SubType);
          il.EmitLdarg(4);
          il.Emit(OpCodes.Callvirt, DynamicUnionResolver.getSerialize(item.Attr.SubType));
        }));
        il.Emit(OpCodes.Br, label3);
      }
      il.MarkLabel(label3);
      il.EmitLdarg(2);
      il.EmitLdloc(local);
      il.Emit(OpCodes.Sub);
      il.Emit(OpCodes.Ret);
      il.MarkLabel(label2);
      il.EmitLdarg(1);
      il.EmitLdarg(2);
      il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.WriteNil);
      il.Emit(OpCodes.Ret);
    }

    private static void EmitOffsetPlusEqual(ILGenerator il, Action loadEmit, Action emit)
    {
      il.EmitLdarg(2);
      if (loadEmit != null)
        loadEmit();
      il.EmitLdarg(1);
      il.EmitLdarg(2);
      emit();
      il.Emit(OpCodes.Add);
      il.EmitStarg(2);
    }

    private static void BuildDeserialize(
      System.Type type,
      UnionAttribute[] infos,
      MethodBuilder method,
      FieldBuilder keyToJumpMap,
      ILGenerator il)
    {
      Label label1 = il.DefineLabel();
      il.EmitLdarg(1);
      il.EmitLdarg(2);
      il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.IsNil);
      il.Emit(OpCodes.Brfalse_S, label1);
      il.EmitLdarg(4);
      il.EmitLdc_I4(1);
      il.Emit(OpCodes.Stind_I4);
      il.Emit(OpCodes.Ldnull);
      il.Emit(OpCodes.Ret);
      il.MarkLabel(label1);
      LocalBuilder local1 = il.DeclareLocal(typeof (int));
      il.EmitLdarg(2);
      il.EmitStloc(local1);
      Label label2 = il.DefineLabel();
      il.EmitLdarg(1);
      il.EmitLdarg(2);
      il.EmitLdarg(4);
      il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.ReadArrayHeader);
      il.EmitLdc_I4(2);
      il.Emit(OpCodes.Beq_S, label2);
      il.Emit(OpCodes.Ldstr, "Invalid Union data was detected. Type:" + type.FullName);
      il.Emit(OpCodes.Newobj, DynamicUnionResolver.invalidOperationExceptionConstructor);
      il.Emit(OpCodes.Throw);
      il.MarkLabel(label2);
      DynamicUnionResolver.EmitOffsetPlusReadSize(il);
      LocalBuilder local2 = il.DeclareLocal(typeof (int));
      il.EmitLdarg(1);
      il.EmitLdarg(2);
      il.EmitLdarg(4);
      il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.ReadInt32);
      il.EmitStloc(local2);
      DynamicUnionResolver.EmitOffsetPlusReadSize(il);
      if (!DynamicUnionResolver.IsZeroStartSequential(infos))
      {
        Label label3 = il.DefineLabel();
        il.EmitLdarg(0);
        il.EmitLdfld((FieldInfo) keyToJumpMap);
        il.EmitLdloc(local2);
        il.EmitLdloca(local2);
        il.EmitCall(DynamicUnionResolver.keyMapDictionaryTryGetValue);
        il.Emit(OpCodes.Brtrue_S, label3);
        il.EmitLdc_I4(-1);
        il.EmitStloc(local2);
        il.MarkLabel(label3);
      }
      LocalBuilder local3 = il.DeclareLocal(type);
      Label label4 = il.DefineLabel();
      il.Emit(OpCodes.Ldnull);
      il.EmitStloc(local3);
      il.Emit(OpCodes.Ldloc, local2);
      // ISSUE: object of a compiler-generated type is created
      \u003C\u003E__AnonType0<Label, UnionAttribute>[] array = ((IEnumerable<UnionAttribute>) infos).Select<UnionAttribute, \u003C\u003E__AnonType0<Label, UnionAttribute>>((Func<UnionAttribute, \u003C\u003E__AnonType0<Label, UnionAttribute>>) (x => new \u003C\u003E__AnonType0<Label, UnionAttribute>(il.DefineLabel(), x))).ToArray<\u003C\u003E__AnonType0<Label, UnionAttribute>>();
      il.Emit(OpCodes.Switch, ((IEnumerable<\u003C\u003E__AnonType0<Label, UnionAttribute>>) array).Select<\u003C\u003E__AnonType0<Label, UnionAttribute>, Label>((Func<\u003C\u003E__AnonType0<Label, UnionAttribute>, Label>) (x => x.Label)).ToArray<Label>());
      il.EmitLdarg(2);
      il.EmitLdarg(1);
      il.EmitLdarg(2);
      il.EmitCall(DynamicUnionResolver.MessagePackBinaryTypeInfo.ReadNextBlock);
      il.Emit(OpCodes.Add);
      il.EmitStarg(2);
      il.Emit(OpCodes.Br, label4);
      foreach (\u003C\u003E__AnonType0<Label, UnionAttribute> anonType0 in array)
      {
        il.MarkLabel(anonType0.Label);
        il.EmitLdarg(3);
        il.EmitCall(DynamicUnionResolver.getFormatterWithVerify.MakeGenericMethod(anonType0.Attr.SubType));
        il.EmitLdarg(1);
        il.EmitLdarg(2);
        il.EmitLdarg(3);
        il.EmitLdarg(4);
        il.EmitCall(DynamicUnionResolver.getDeserialize(anonType0.Attr.SubType));
        if (System.Reflection.ReflectionExtensions.GetTypeInfo(anonType0.Attr.SubType).IsValueType)
          il.Emit(OpCodes.Box, anonType0.Attr.SubType);
        il.Emit(OpCodes.Stloc, local3);
        DynamicUnionResolver.EmitOffsetPlusReadSize(il);
        il.Emit(OpCodes.Br, label4);
      }
      il.MarkLabel(label4);
      il.EmitLdarg(4);
      il.EmitLdarg(2);
      il.EmitLdloc(local1);
      il.Emit(OpCodes.Sub);
      il.Emit(OpCodes.Stind_I4);
      il.Emit(OpCodes.Ldloc, local3);
      il.Emit(OpCodes.Ret);
    }

    private static bool IsZeroStartSequential(UnionAttribute[] infos)
    {
      for (int index = 0; index < infos.Length; ++index)
      {
        if (infos[index].Key != index)
          return false;
      }
      return true;
    }

    private static void EmitOffsetPlusReadSize(ILGenerator il)
    {
      il.EmitLdarg(2);
      il.EmitLdarg(4);
      il.Emit(OpCodes.Ldind_I4);
      il.Emit(OpCodes.Add);
      il.EmitStarg(2);
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        TypeInfo typeInfo1 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (T));
        if (typeInfo1.IsNullable())
        {
          TypeInfo typeInfo2 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeInfo1.GenericTypeArguments[0]);
          object formatterDynamic = DynamicUnionResolver.Instance.GetFormatterDynamic(typeInfo2.AsType());
          if (formatterDynamic == null)
            return;
          DynamicUnionResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(typeof (StaticNullableFormatter<>).MakeGenericType(typeInfo2.AsType()), formatterDynamic);
        }
        else
        {
          TypeInfo typeInfo3 = DynamicUnionResolver.BuildType(typeof (T));
          if (typeInfo3 == null)
            return;
          DynamicUnionResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(typeInfo3.AsType());
        }
      }
    }

    private static class MessagePackBinaryTypeInfo
    {
      public static TypeInfo TypeInfo = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (MessagePackBinary));
      public static MethodInfo WriteFixedMapHeaderUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteFixedMapHeaderUnsafe), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteFixedArrayHeaderUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteFixedArrayHeaderUnsafe), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteMapHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteMapHeader), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteArrayHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteArrayHeader), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WritePositiveFixedIntUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WritePositiveFixedIntUnsafe), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteInt32 = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteInt32), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteBytes = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteBytes), new System.Type[3]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (byte[])
      });
      public static MethodInfo WriteNil = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteNil), new System.Type[2]
      {
        DynamicUnionResolver.refByte,
        typeof (int)
      });
      public static MethodInfo ReadBytes = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadBytes), new System.Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicUnionResolver.refInt
      });
      public static MethodInfo ReadInt32 = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadInt32), new System.Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicUnionResolver.refInt
      });
      public static MethodInfo ReadString = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadString), new System.Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicUnionResolver.refInt
      });
      public static MethodInfo IsNil = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (IsNil), new System.Type[2]
      {
        typeof (byte[]),
        typeof (int)
      });
      public static MethodInfo ReadNextBlock = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadNextBlock), new System.Type[2]
      {
        typeof (byte[]),
        typeof (int)
      });
      public static MethodInfo WriteStringUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteStringUnsafe), new System.Type[4]
      {
        DynamicUnionResolver.refByte,
        typeof (int),
        typeof (string),
        typeof (int)
      });
      public static MethodInfo ReadArrayHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadArrayHeader), new System.Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicUnionResolver.refInt
      });
      public static MethodInfo ReadMapHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadMapHeader), new System.Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicUnionResolver.refInt
      });
    }
  }
}
