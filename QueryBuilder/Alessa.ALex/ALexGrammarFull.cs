using Irony.Parsing;

namespace Alessa.ALex
{
    /// <summary>
    /// The grammar definition class, it handles special features as RAW queries and join tables.
    /// </summary>
    [Language("ALex Languaje Full", "1.0", "Contains some extra features as ALex Languaje, it includes join tables.")]
    public class ALexGrammarFull : ALexGrammarBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ALexGrammarFull"/> class.
        /// </summary>
        public ALexGrammarFull()
        {
            base.Root = GrammarBuilder.GetRoot(this, true);
        }
    }
}
