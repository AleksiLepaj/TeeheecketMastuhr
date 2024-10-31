using HtmlAgilityPack;
using TeeheecketMastuhr.Models;

namespace TeeheecketMastuhr.Services
{
    public class TicketScraper
    {
        private readonly HttpClient _httpClient;

        public TicketScraper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ticket>> ScrapeTicketsAsync(string htmlContent)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var tickets = new List<Ticket>();

            var nodes = doc.DocumentNode.SelectNodes("//li[@data-bdd='quick-picks-list-item-resale']");
            foreach (var node in nodes)
            {
                var section = node.SelectSingleNode(".//span[@title='Section Description']").InnerText.Split('•')[0].Replace("Sec ", "").Trim();
                var row = node.SelectSingleNode(".//span[@title='Section Description']").InnerText.Split('•')[1].Replace("Row ", "").Trim();
                var priceString = node.SelectSingleNode(".//button[@data-bdd='quick-pick-price-button']").InnerText.Replace("CA $", "").Trim();
                var price = double.Parse(priceString);

                tickets.Add(new Ticket
                {
                    Section = section,
                    Row = row,
                    Price = price
                });
            }

            return tickets;
        }


    }
}
