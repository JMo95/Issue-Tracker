using AutoMapper;
using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Facades;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.ViewModels.TicketViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.WebApi.Controllers
{

    [Authorize]
    public class AttachmentController : Controller
    {
        private readonly IAttachmentService _attachmentService;
        private readonly ITicketService _ticketService;
        private readonly IProjectFacade _projectFacade;

        public AttachmentController(IAttachmentService attachmentService, ITicketService ticketService, IProjectFacade projectFacade)
        {
            _attachmentService = attachmentService;
            _ticketService = ticketService;
            _projectFacade = projectFacade;
        }

        [HttpPost]
        public async Task<IActionResult> UploadToDatabase(TicketCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var ticket = await _ticketService.FindOne(vm.TicketId);
            TempData["returnurl"] = Request.Headers["Referer"].ToString();
            foreach (var file in vm.files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var user = await _projectFacade.GetUser(User);
                var fileModel = new Attachments
                {
                    CreatedOn = DateTime.UtcNow,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    Description = vm.fileDescription,
                    UploadedBy = user.FirstName + " " + user.LastName
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                await _attachmentService.Create(fileModel);
                ticket.Attachments.Add(fileModel);
                await _ticketService.Update(ticket);
            }

            TempData["Message"] = "File successfully uploaded to Database";
            return Redirect(TempData["returnurl"].ToString());
        }

        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var file = await _attachmentService.FindOne(id);
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }

        public async Task<IActionResult> DeleteFileFromDatabase(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            TempData["returnurl"] = Request.Headers["Referer"].ToString();
            var file = await _attachmentService.FindOne(id);
            await _attachmentService.Delete(id);

            TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from Database.";
            return Redirect(TempData["returnurl"].ToString());
        }

    }
}
 