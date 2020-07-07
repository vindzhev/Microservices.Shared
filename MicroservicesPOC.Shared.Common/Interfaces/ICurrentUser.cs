namespace MicroservicesPOC.Shared.Common.Interfaces
{
    using System;

    using MicroservicesPOC.Shared.Common.Services;

    public interface ICurrentUser : IScopedService
    {
        Guid UserId { get; }
    }
}
