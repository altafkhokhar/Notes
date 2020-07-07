﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrozenCode.Note.Contract.DTO;
using FrozenCode.Note.Contract.Services;
using FrozenCode.Note.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Helpers;

namespace FrozenCode.Note.API.Controllers
{
    
    public class NotesController : BaseController
    {
        private INoteService _noteService;
        private readonly AppSettings _appSettings;

        public NotesController( IOptions<AppSettings> appSettings)
        {
            _noteService = new NoteService();
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CreateNote")]//  api/notes/CreateNote

        public IActionResult CreateNote([FromBody]CreateNoteDTO newNote)
        {
            string message = string.Empty;

            var isCreated = _noteService.TryCreateNote(ref newNote, GetUserId());

            if (!isCreated)
                return BadRequest(new { message = "Note is not created!" });

            return Ok(true);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("EditNote")]//  api/notes/EditNote
        public IActionResult EditNote([FromBody]CreateNoteDTO editNote)
        {
            string message = string.Empty;

            var isUpdated = _noteService.Edit(ref editNote, GetUserId());

            if (!isUpdated)
                return BadRequest(new { message = "Cannot update a Note!" });

            return Ok(true);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("DeleteNote")]//  api/notes/DeleteNote
        public IActionResult DeleteNote([FromBody]int noteId)
        {
            string message = string.Empty;

            var isDeleted = _noteService.Delete(noteId, GetUserId());

            if(!isDeleted)
                return BadRequest(new { message = "Cannot delete a Note!" });

            return Ok(true);
        }

        
        [HttpGet]
        [Route("GetNote")]//  api/notes/GetNote
        public IActionResult GetNote([FromQuery]int noteId)
        {
            string message = string.Empty;

            var readNote = _noteService.GetNote(noteId, GetUserId());

            if (readNote == null)
                return BadRequest(new { message = "Invalid note!" });

            return Ok(readNote);
        }
    }
}