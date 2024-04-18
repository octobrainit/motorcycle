using motorcycle.domain.entities;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.UploadImageFile
{
    public interface IUploadImageFileRepository : IRepository<Driver> {
        Task<Driver> FindDriverWithCNH(Guid id, CancellationToken cancellationToken);
        Task<Driver> FindCNHById(Guid id, CancellationToken cancellationToken); 
    }
}
