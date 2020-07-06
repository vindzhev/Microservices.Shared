namespace MicroservicesPOC.Shared.Controllers
{
    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => this._mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
    }
}
