using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MontyHallSimulation.BusinessLogic;
using MontyHallSimulation.Models;

namespace MontyHallSimulation.Controllers
{
    /// <summary>
    /// MontyHall Game Simulation Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MontyHallSimulationController : ControllerBase
    {
        private readonly MontyHallLogic _MontyHallLogic;       

        public MontyHallSimulationController(MontyHallLogic montyHallLogic)
        {
            _MontyHallLogic = montyHallLogic;
        }

        /// <summary>
        /// Action to give simulation result
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors]
        [Route("Results")]
        public ActionResult<Result> Results(Request request)
        {
            try
            {
                if(request == null || request.inputData <= 0)
                {
                    return BadRequest(request);
                }
                Result result = new Result();
                result = _MontyHallLogic.PlayGames(request.inputData, request.inputChoice);
                if(result == null)
                {
                    return NotFound();
                }
                else
                    return Ok(result);
            }catch (Exception ex)
            {
                return NotFound();
            }
            
        }        
    }
}
