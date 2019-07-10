using Irony.Parsing;

namespace Alessa.ALex
{
    /// <summary>
    /// ALex grammar class. It is designed to make simple queries withour raw queries or join definitions.
    /// </summary>
    [Language("ALex Languaje Basic", "1.0", "Contains basic features similar to a SQL select syntax.")]
    public class ALexGrammarBasic : ALexGrammarBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ALexGrammarBasic"/> class.
        /// </summary>
        public ALexGrammarBasic()
        {
            base.Root = GrammarBuilder.GetRoot(this, false);
        }
    }
}
