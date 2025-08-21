using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Scand.StormPetrel.Generator.Common.AssertExpressionDetector
{
    internal abstract class AbstractDetector
    {
        public abstract bool IsExpectedArgument(ArgumentSyntax argument, out ExpressionSyntax actualExpression);
        protected virtual bool IsAppropriateArgumentInTheList(ArgumentSyntax argument, int argumentIndex, string[] supportedArgumentNames, out ArgumentListSyntax argumentList)
        {
            argumentList = null;
            if (!(argument.Parent is ArgumentListSyntax argumentListParent))
            {
                return false;
            }
            argumentList = argumentListParent;
            if (argument.NameColon == null)
            {
                var argByIndex = argumentListParent.Arguments.Count > argumentIndex
                                    ? argumentListParent.Arguments[argumentIndex]
                                    : null;
                return argument == argByIndex;
            }
            return supportedArgumentNames.Contains(argument.NameColon.Name?.Identifier.Text);
        }
    }
}
