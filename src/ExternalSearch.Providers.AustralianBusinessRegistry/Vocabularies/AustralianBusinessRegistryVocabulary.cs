using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry.Vocabularies
{
    public static class AustralianBusinessRegistryVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="KnowledgeGraphVocabulary" /> class.
        /// </summary>
        static AustralianBusinessRegistryVocabulary()
        {
            Organization = new AustralianBusinessRegistryDetailsVocabulary();

        }

        public static AustralianBusinessRegistryDetailsVocabulary Organization { get; private set; }

    }
}