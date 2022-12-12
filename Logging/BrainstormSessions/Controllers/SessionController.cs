using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Services;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILogger _logger;
        private readonly IEmailLogService _emailLogger;
        

        public SessionController(IBrainstormSessionRepository sessionRepository, ILogger logger , IEmailLogService emailLogger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
            _emailLogger = emailLogger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            {
                _logger.Error("Id has no value");
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            _logger.Debug("Id has value {Id}", id);
            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if (session == null)
            {
                _logger.Error("Session is null");
                return Content("Session not found.");
            }

            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };

            _emailLogger.Log("Ok response", Serilog.Events.LogEventLevel.Error);

            _logger.Debug("Ok response");
            return View(viewModel);
        }
    }
}
