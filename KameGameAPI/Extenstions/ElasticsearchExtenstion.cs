using KameGameAPI.Models;
using Nest;

namespace KameGameAPI.Extenstions
{
    public static class ElasticsearchExtenstion
    {
        public static void AddElasticSearch(
            this IServiceCollection services, IConfiguration configuration    
        )
        {
            var url = configuration["Elasticsearch:Uri"];
            var defaultIndex = "/Cards";

            var settings = new ConnectionSettings(new Uri(url))
                .PrettyJson()
                .DefaultIndex(defaultIndex);

            AddDefault(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);

        }
        private static void AddDefault(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<Card>(p =>
            p.Ignore(x => x.price)
                .Ignore(x => x.pictureLink)
                .Ignore(x => x.id)
                .Ignore(x => x.type)
                .Ignore(x => x.attribute)
                .Ignore(x => x.race)
                .Ignore(x => x.stock)
                .Ignore(x => x.set)
                .Ignore(x => x.setId));
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName,i => i.Map<Card>(x=> x.AutoMap()));
        }
    }
}
