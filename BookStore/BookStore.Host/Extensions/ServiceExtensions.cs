using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.SQLRepositories;

namespace BookStore.Host.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorRepository, AuthorSqlRepository>();
            services.AddSingleton<IBookRepository, BookSqlRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IAuthorService, AuthorService>();

            return services;
        }
    }
}
