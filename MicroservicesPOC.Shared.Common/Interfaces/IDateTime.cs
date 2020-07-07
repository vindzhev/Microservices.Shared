namespace MicroservicesPOC.Shared.Common.Interfaces
{
    using System;

    using MicroservicesPOC.Shared.Common.Services;

    public interface IDateTime : IService
    {
        DateTime Now { get; }
    }
}
