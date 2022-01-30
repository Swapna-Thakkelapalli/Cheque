using ChequeAPI.Models;
using ChequeAPI.Services;

using Microsoft.AspNetCore.Mvc;

using System;

namespace ChequeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChequeController : ControllerBase
    {
        private readonly IChequeService _chequeService;

        public ChequeController(IChequeService chequeService)
        {
            _chequeService = chequeService;
        }
        [HttpPost]
        [Route("GenerateCheque")]
        public IActionResult GenerateCheque(ChequeDTO chequeDTO)
        {
            try
            {
                var result = _chequeService.GenerateCheque(chequeDTO);
                return Ok(result);
            }
            catch (System.Exception)
            {
                //log
                return NotFound();
            }
            
        }
        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return DateTime.Now.ToString();
        }
    }
}
