using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace ClassLibrary1
{
    /// <summary>
    /// 语法树定义收集器，可以在这里过滤生成器所需
    /// </summary>
    public class CustomSyntaxReceiver : ISyntaxReceiver
    {
        //BaseTypeDeclarationSyntax 
        public List<BaseTypeDeclarationSyntax> Syntaxes { get; } = new();

        /// <summary>
        ///     访问语法树
        /// </summary>
        /// <param name="syntaxNode"></param>
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            //可以再此处进行过滤，如通过ClassDeclarationSyntax过滤Class类，
            //当然也可以改为BaseTypeDeclarationSyntax,或者也可以使用InterfaceDeclarationSyntax添加接口类等等
            if (syntaxNode is BaseTypeDeclarationSyntax cds)
            {
                Syntaxes.Add(cds);
            }
        }
    }
}
