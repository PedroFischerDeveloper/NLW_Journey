﻿using Journey.Application.UseCases.Activity.Complete;
using Journey.Application.UseCases.Activity.Register;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register(RequestRegisterTripJson request)
        {
            
            var useCase = new RegisterTripUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var useCase = new GetAllUseCase();

            var result = useCase.Execute();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status400BadRequest)]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var useCase = new DeleteByIdUseCase();

            var result = useCase.Execute(id);

            return Ok(result);
            
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {

            var useCase = new DeleteTripByIdUseCase();

            useCase.Execute(id);

            return NoContent();

        }

        [HttpPost]
        [Route("{tripId}/activity")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity([FromRoute]Guid tripId, [FromBody]RequestRegisterActivityJson request)
        {
            var useCase = new RegisterActivityUseCase();

            var response = useCase.Execute(tripId, request);


            return Created(string.Empty, response);

        }

        [HttpPut]
        [Route("{tripId}/activity/{activityId}/complete")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status404NotFound)]
        public IActionResult CompleteActivity(
            [FromRoute] Guid tripId, 
            [FromRoute] Guid activityId)
        {
            var useCase = new CompleteActivityUseCase();

            useCase.Execute(tripId, activityId);


            return NoContent();

        }

        [HttpDelete]
        [Route("{tripId}/activity/{activityId}")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status404NotFound)]
        public IActionResult DeleteActivity(
            [FromRoute] Guid tripId,
            [FromRoute] Guid activityId)
        {
            var useCase = new CompleteActivityUseCase();

            useCase.Execute(tripId, activityId);


            return NoContent();

        }



    }
}
