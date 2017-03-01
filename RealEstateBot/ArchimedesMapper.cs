namespace ArchimedesBot
{
    using System.Linq;
    using Microsoft.Azure.Search.Models;
    using Search.Azure.Services;
    using Search.Models;

    //Test
    public class RealEstateMapper : IMapper<DocumentSearchResult, GenericSearchResult>
    {
        public GenericSearchResult Map(DocumentSearchResult documentSearchResult)
        {
            var searchResult = new GenericSearchResult();

            searchResult.Results = documentSearchResult.Results.Select(r => ToSearchHit(r)).ToList();
            searchResult.Facets = documentSearchResult.Facets?.ToDictionary(kv => kv.Key, kv => kv.Value.Select(f => ToFacet(f)));

            return searchResult;
        }

        private static GenericFacet ToFacet(FacetResult facetResult)
        {
            return new GenericFacet
            {
                Value = facetResult.Value,
                Count = facetResult.Count.Value
            };
        }

        private static SearchHit ToSearchHit(SearchResult hit)
        {
            return new SearchHit
            {
                //Key = (string)hit.Document["listingId"],
                //Title = GetTitleForItem(hit),
                //PictureUrl = (string)hit.Document["thumbnail"],
                //Description = (string)hit.Document["description"]

                Key = (string)hit.Document["Id"],
                Output = GetOutputForItem(hit),
                Answer = (string)hit.Document["Answers"]
                //Question = (string)hit.Document["Questions"]
            };
        }

        private static string GetOutputForItem(SearchResult result)
        {
            //var beds = result.Document["beds"];
            //var baths = result.Document["baths"];
            //var city = result.Document["city"];
            //var price = result.Document["price"];

            var answer = result.Document["Answers"];
            var question = result.Document["Questions"];


            //return $"{beds} bedroom, {baths} bath in {city}, ${price:#,0}";
            return $"Your question was \"{question}\" And the answer is \"{answer}\".";
        }
    }
}
