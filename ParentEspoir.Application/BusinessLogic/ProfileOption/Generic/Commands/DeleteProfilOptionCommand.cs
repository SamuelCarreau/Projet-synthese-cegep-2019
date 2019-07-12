using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteProfilOptionCommand<TProfilOption> : IRequest where TProfilOption : class, IProfileOption
    {
        public int Id { get; set; }

        public static readonly string IS_LINKED_ERROR_MESSAGE = "Impossible l'option est relier à des entités";

        public async Task Handle(ParentEspoirDbContext context, CancellationToken cancelationToken)
        {
            var option = await context.Set<TProfilOption>().FindAsync(Id);

            if (option != null)
            {
                option.IsDelete = true;

                await context.SaveChangesAsync(cancelationToken);
            }
        }
    }
}
