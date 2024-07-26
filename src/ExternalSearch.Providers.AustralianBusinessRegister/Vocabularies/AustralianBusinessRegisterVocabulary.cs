using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Vocabularies
{
    public static class AustralianBusinessRegisterVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="KnowledgeGraphVocabulary" /> class.
        /// </summary>
        static AustralianBusinessRegisterVocabulary()
        {
            Organization = new AustralianBusinessRegisterDetailsVocabulary();

        }

        public static AustralianBusinessRegisterDetailsVocabulary Organization { get; private set; }

    }
}