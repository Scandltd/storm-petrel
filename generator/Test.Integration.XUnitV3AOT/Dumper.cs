using Scand.StormPetrel.Generator.Abstraction;
using System.Diagnostics.CodeAnalysis;
using Test.Integration.XUnitV3AOT.LibraryBeingTested;

namespace Test.Integration.XUnitV3AOT;

internal class Dumper : IGeneratorDumper
{
    public string Dump(GenerationDumpContext generationDumpContext)
    {
        if (generationDumpContext.Value is AddResultNoReflectionDump resultNoReflectionDump)
        {
            // Dump example without Reflection utilization
            return $$$"""
                new()
                {
                    {{{nameof(resultNoReflectionDump.Value)}}} = {{{resultNoReflectionDump.Value}}},
                    {{{nameof(resultNoReflectionDump.ValueAsHexString)}}} = "{{{resultNoReflectionDump.ValueAsHexString}}}",
                }
                """;
        }
        // Reflection-based dump is also doable, but need to reference necessary types
        ReferenceNecessaryTypesInfo();
        var nativeDumper = new Scand.StormPetrel.Generator.TargetProject.GeneratorObjectDumper(new()
        {
            DumpStyle = DumpStyle.CSharp,
            IndentSize = 4,
        });
        return nativeDumper.Dump(generationDumpContext);
    }
    /// <summary>
    /// This method is used to reference the necessary type information for AOT compilation because
    /// libraries methods like <see cref="ObjectDumper.Dump"/> or <see cref="FluentAssertions.Primitives.ObjectAssertions&lt;,&gt;.BeEquivalentTo"/>
    /// utilize Reflection where this necessary type information is required.
    /// The nested type information of <see cref="AddResult"/> is also necessary to be referenced for AOT compilation, because the <see cref="AddResult.NestedInfo"/> property is of type <see cref="AddResultNestedInfo"/>.
    /// See <see cref="https://devblogs.microsoft.com/dotnet/creating-aot-compatible-libraries/?commentid=20262#comment-20262"/> for explanation why walking Type graphs is not supported by <see cref="DynamicallyAccessedMembersAttribute"/>
    /// and thus the need to reference the nested type information of <see cref="AddResult"/> explicitly.
    /// </summary>
    private static void ReferenceNecessaryTypesInfo()
    {
        ReferenceNecessaryTypeInfo(typeof(AddResult));
        ReferenceNecessaryTypeInfo(typeof(AddResultNestedInfo));
    }
    private static void ReferenceNecessaryTypeInfo([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type _)
    {
        // No implementation needed, the method parameter attribute is used to preserve the type information of the specified type for AOT compilation.
    }
}
