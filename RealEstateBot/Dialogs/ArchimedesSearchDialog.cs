namespace ArchimedesBot.Dialogs
{
    using System;
    using Search.Dialogs;
    using Search.Services;

    [Serializable]
    public class ArchimedesSearchDialog : SearchDialog
    {
        //private static readonly string[] TopRefiners = { "region", "city", "type" };
        private static readonly string[] TopRefiners = { "Answers" };

        public ArchimedesSearchDialog(ISearchClient searchClient) : base(searchClient, multipleSelection: true)
        {
        }

        protected override string[] GetTopRefiners()
        {
            return TopRefiners;
        }
    }
}
