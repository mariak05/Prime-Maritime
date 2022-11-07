﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ERController : ControllerBase
    {
        private IERService _erService;
        public ERController(IERService erService)
        {
            _erService = erService;
        }

        [HttpGet("GetERList")]
        public ActionResult<Response<List<EMPTY_REPO>>> GetERList(string AGENT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_erService.GetERList(AGENT_CODE)));
        }

        [HttpPost("InsertER")]
        public ActionResult<Response<EMPTY_REPO>> InsertER(EMPTY_REPO request)
        {
            return Ok(_erService.InsertER(request));
        }

        [HttpGet("GetERDetails")]
        public ActionResult<Response<EMPTY_REPO>> GetERDetails(string REPO_NO, string AGENT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_erService.GetERDetails(REPO_NO, AGENT_CODE)));
        }
    }
}
