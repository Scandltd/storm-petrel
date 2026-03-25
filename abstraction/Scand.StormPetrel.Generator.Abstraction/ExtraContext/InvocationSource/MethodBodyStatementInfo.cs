namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSource
{
    /// <summary>
    /// Data about the invocation source method statement node to be rewritten.
    /// </summary>
    public sealed class MethodBodyStatementInfo : AbstractMethodInfo
    {
        public int StatementIndex { get; set; }
        public int StatementNodeKind { get; set; }
        public int StatementNodeIndex { get; set; }
    }
}
