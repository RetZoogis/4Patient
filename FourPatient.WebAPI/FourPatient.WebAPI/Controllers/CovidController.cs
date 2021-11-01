using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FourPatient.Domain;
using FourPatient.Domain.Tables;
using FourPatient.WebAPI.Models;
using Covid = FourPatient.WebAPI.Models.Covid;

namespace FourPatient.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CovidController : ControllerBase
    {
        private readonly ICovid _covidrepo;
        private readonly ILogger<CovidController> _logger;

        public CovidController(ICovid covidrepo, ILogger<CovidController> logger)
        {
            _logger = logger;
            _covidrepo = covidrepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Covid>> GetAll()
        {
            return Ok(_covidrepo.GetAll().Select(p => (Covid)Map.Model(p)));
        }

        [HttpGet("{id}")]
        public ActionResult<Covid> Get(int id)
        {
            return Ok((Covid)Map.Model(_covidrepo.Get(id)));
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] Covid survey)
        {
            if (ModelState.IsValid)
            {
                _covidrepo.Create((Domain.Tables.Covid)Map.Table(survey));
            }
            return Ok();
        }

        [HttpPut("Edit")]
        public ActionResult Edit([FromBody] Covid survey)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _covidrepo.Update((Domain.Tables.Covid)Map.Table(survey));
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _covidrepo.Delete(id);
            return Ok();
        }
    }
}
