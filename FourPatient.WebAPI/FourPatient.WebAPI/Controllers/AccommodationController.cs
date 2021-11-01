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
using Accommodation = FourPatient.WebAPI.Models.Accommodation;

// This class holds exposed API endpoints

namespace FourPatient.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodation _accommodationrepo;
        private readonly ILogger<AccommodationController> _logger;

        public AccommodationController(IAccommodation accommodationrepo, ILogger<AccommodationController> logger)
        {
            _logger = logger;
            _accommodationrepo = accommodationrepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Accommodation>> GetAll()
        {
            return Ok(_accommodationrepo.GetAll().Select(p => (Accommodation)Map.Model(p)));
        }

        [HttpGet("{id}")]
        public ActionResult<Accommodation> Get(int id)
        {
            return Ok((Accommodation)Map.Model(_accommodationrepo.Get(id)));
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] Accommodation survey)
        {
            if (ModelState.IsValid)
            {
                _accommodationrepo.Create((Domain.Tables.Accommodation)Map.Table(survey));
            }
            return Ok();
        }

        [HttpPut("Edit")]
        public ActionResult Edit([FromBody] Accommodation survey)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accommodationrepo.Update((Domain.Tables.Accommodation)Map.Table(survey));
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
            _accommodationrepo.Delete(id);
            return Ok();
        }
    }
}
