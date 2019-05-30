using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using JsonNet.ContractResolvers;
using MovieStore.Models;
using MovieStore.Data;
public static class Seeder
{
    public static void Seed(string movieData,
                              IServiceProvider serviceProvider)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };
        List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(movieData, settings);

        using (var serviceScope = serviceProvider.
            GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<MovieStoreDbContext>();
            if (!context.Movie.Any())
            {
                context.AddRange(movies);
                context.SaveChanges();
            }

        }
    }
}