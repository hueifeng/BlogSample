using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ClassLibrary1
{
    /// <summary>
    /// 继承ISourceGenerator接口，实现接口用于代码生成策略，它的生命周期由编译器控制，它可以在编译时创建并且添加到编译中的代码，
    /// 它为我们提供了编译时元编程， 从而使我们对C#代码或者非C#源文本进行内部的检查。
    /// </summary>
    [Generator]
    public class CustomGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;
            var simpleCustomInterfaceSymbol = compilation.GetTypeByMetadataName("SourceGeneratorDemo.ICustom");

            const string code = @"
using System;
namespace SourceGeneratorDemo
{   
    public partial class Program{
             public static void Display(){
             Console.WriteLine(""Hello World!"");
            }
    }
}";
            //注释
            {
                //context.AddSource字符串形式的源码添加到编译中
                //SourceText原文本抽象类，SourceText.From静态方法,Code指定要创建的源码内容，
                //Encoding设置保存的编码格式，默认为UTF8.
                //context.SyntaxReceiver可以获取在初始化期间注册的ISyntaxReceiver，获取创建的实例
            }

            if (!(context.SyntaxReceiver is CustomSyntaxReceiver receiver))
                return;
            //语法树可参考Roslyn Docs
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            //context.AddSource("a.class", code);
            context.AddSource("a.class", SourceText.From(text: code, encoding: Encoding.UTF8));

            //https://github.com/gfoidl-Tests/SourceGeneratorTest
            {
                StringBuilder sb = new();
                sb.Append(@"using System;
using System.Runtime.CompilerServices;
#nullable enable
[CompilerGenerated]
public static class ExportDumper
{
    public static void Dump()
    {");
                foreach (BaseTypeDeclarationSyntax tds in receiver.Syntaxes)
                {
                    sb.Append($@"
        Console.WriteLine(""type: {GetType(tds)}\tname: {tds.Identifier}\tfile: {Path.GetFileName(tds.SyntaxTree.FilePath)}"");");
                }
                sb.AppendLine(@"
    }
}");

                SourceText sourceText = SourceText.From(sb.ToString(), Encoding.UTF8);
                context.AddSource("DumpExports.generated", sourceText);

                static string GetType(BaseTypeDeclarationSyntax tds) => tds switch
                {
                    ClassDeclarationSyntax => "class",
                    RecordDeclarationSyntax => "record",
                    StructDeclarationSyntax => "struct",
                    _ => "-"
                };
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //对于Source Generator可以通过添加`Debugger.Launch()`的形式进行对编译时的生成器进行调试，可以通过它很便捷的一步步调试代码.
            //Debugger.Launch();
            //注册一个语法接收器，会在每次生成时被创建
            context.RegisterForSyntaxNotifications(() => new CustomSyntaxReceiver());
        }
    }
}
