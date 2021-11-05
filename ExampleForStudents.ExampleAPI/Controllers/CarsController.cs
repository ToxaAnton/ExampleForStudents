using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleForStudents.Contracts;
using ExampleForStudents.Core.Abstractions;
using ExampleForStudents.ExampleAPI.ActionResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExampleForStudents.ExampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class CarsController : ControllerBase
    {
        private readonly ILogger _logger; //if you want to log something
        private readonly ICarsService _service;

        public CarsController(ICarsService service, ILogger<CarsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        ///this method goes against REST principe, but it is a common practice to use post method in case if there is a big search criteria
        /// </summary>
        [HttpPost("by-filter")]
        [ProducesResponseType(typeof(IEnumerable<CarDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetBySearchFilterAsync(
            [FromBody] CarsSearchFilterDto filter) =>
            new ResponseWrapperDtoResult<IEnumerable<CarDto>>(await _service.GetAsyncBySearchFilter(filter));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CarDto>> GetAsync(Guid id)
        {
            return new ResponseWrapperDtoResult<CarDto>(await _service.GetAsync(id));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CarDto>> UpdateAsync(Guid id, [FromBody] CarDto car)
        {
            car.Id = id;
            return new ResponseWrapperDtoResult<CarDto>(await _service.UpdateAsync(car));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CarDto>> CreateAsync([FromBody] CarDto car)
        {
            car.Id = new Guid();
            return new ResponseWrapperDtoResult<CarDto>(await _service.CreateAsync(car));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return new ResponseWrapperDtoResult<object>(await _service.DeleteAsync(id));
        }
    }
}