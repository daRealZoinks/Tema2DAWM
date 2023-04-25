﻿using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        private readonly ClassService _classService;

        public ClassesController(ClassService classService)
        {
            _classService = classService;
        }

        [HttpPost("add")]
        public IActionResult Add(ClassAddDto payload)
        {
            var result = _classService.Add(payload);

            if (result == null)
            {
                return BadRequest("Class cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public ActionResult<List<ClassViewDto>> GetAll()
        {
            var result = _classService.GetAll();
            return Ok(result);
        }
    }
}
