using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator.ExtraContextInternal
{
    internal class InvocationExpressionContextInternal : AbstractExtraContextInternal<InvocationSourceContext>
    {
        private readonly IEnumerable<string> _invocationExpressionSplit;
        public InvocationExpressionContextInternal(string invocationExpression)
        {
            _invocationExpressionSplit = invocationExpression.Split('.');
        }
        public string[] GetPath(string[] expressionVarPath)
        {
            var expressionPath = _invocationExpressionSplit.ToArray();
            if (expressionPath.Length == 1)
            {
                expressionPath = expressionVarPath
                                    .Take(expressionVarPath.Length - 2)
                                    .Concat(Enumerable.Repeat(expressionPath[0], 1))
                                    .ToArray();
            }
            var methodNameIndex = expressionPath.Length - 1;
            //Select any method. Appropriate overload should be selected by method arguments
            expressionPath[methodNameIndex] = expressionPath[methodNameIndex]
                + (PartialExtraContext.MethodInfo != null ? "[*]" : "");
            return expressionPath;
        }
        public string InvocationExpressionStormPetrel
        {
            get
            {
                var expressionPathStormPetrel = _invocationExpressionSplit.ToArray();
                for (var i = 0; i < 2; i++)
                {
                    if (expressionPathStormPetrel.Length >= (i + 1))
                    {
                        expressionPathStormPetrel[i] += "StormPetrel";
                    }
                }
                return string.Join(".", expressionPathStormPetrel);
            }
        }

        public ArgumentListSyntax MethodArgs { get; set; }
    }
}
