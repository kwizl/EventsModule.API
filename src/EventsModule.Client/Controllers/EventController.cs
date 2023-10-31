using EventsModule.Client.ApiServices;
using EventsModule.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsModule.Client.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventApiService _service;

        public EventController(IEventApiService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetEvents());
        }

        public async Task<IActionResult> Details(int? ID)
        {
            if (ID == null) return NotFound();

            var singleEvent = await _service.GetEvent(ID);

            if (singleEvent == null) return NotFound();

            return View(singleEvent);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event request)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateEvent(request);

                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(int? ID)
        {
            if (ID == null) return NotFound();

            var findEvent = await _service.FindEvent(ID);

            if (findEvent == null) return NotFound();

            return View(findEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID, Event request)
        {
            if (ID != request.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateEvent(request);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (await EventExists(request.ID) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            return View(request);
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null) return NotFound();

            var firstEvent = await _service.GetEvent(ID);

            if (firstEvent == null) return NotFound();

            return View(firstEvent);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? ID)
        {
            await _service.FindEvent(ID);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EventExists(int ID)
        {
            return await _service.ExistEvents(ID);
        }
    }
}
