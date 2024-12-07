using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TurnTableApplication.Requests;
using TurnTableDomain.Services;

namespace TurnTableAPI.ActionFilters
{
    public class ValidGameCodeFilter : IActionFilter
    {
        private readonly GameManager _gameManager;

        public ValidGameCodeFilter(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string gameCode = GetGameCodeFromRequest(context);

            if (string.IsNullOrEmpty(gameCode) == false)
            {
                bool gameCodeExists = this._gameManager.IsGameActive(gameCode);

                if (gameCodeExists == false)
                {
                    context.Result = ((ControllerBase)context.Controller).NotFound("Game Code not found");
                    return;
                }
            }
            else
            {
                context.Result = ((ControllerBase)context.Controller).BadRequest("Parameter 'GameCode' not provided");
            }
        }

        private string GetGameCodeFromRequest(ActionExecutingContext context)
        {
            if (context.ActionArguments["request"] is BaseGameRequest request)
            {
                return request.GameCode;
            }

            return string.Empty;
        }
    }
}
