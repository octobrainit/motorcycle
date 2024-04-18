using Microsoft.EntityFrameworkCore;
using motorcycle.domain.entities;
using motorcycle.shared.CreationalBase;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.UploadImageFile
{
    public class UploadImageFileRepository : BaseRepository<Driver, Database>, IUploadImageFileRepository
    {
        public UploadImageFileRepository(Database database) : base(database) { }

        public async Task<Driver> FindCNHById(Guid id, CancellationToken cancellationToken) =>
            await Context.Drivers.Include(item => item.CNH).Where(item => item.CNH.Id.Equals(id)).SingleOrDefaultAsync() ??
            throw BaseError.Create(MessageType.InfraestructureValidation, "CNH not founded");

        public async Task<Driver> FindDriverWithCNH(Guid id, CancellationToken cancellationToken) =>
            await Context.Drivers.Include(item => item.CNH).SingleOrDefaultAsync(item => item.Id.Equals(id)) ??
                throw BaseError.Create(MessageType.InfraestructureValidation, "Driver not founded");
    }
}
