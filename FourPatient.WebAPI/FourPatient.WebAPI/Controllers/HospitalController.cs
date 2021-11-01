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
using Hospital = FourPatient.WebAPI.Models.Hospital;

namespace FourPatient.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly IHospital _hospitalrepo;
        private readonly ILogger<HospitalController> _logger;

        public HospitalController(IHospital hospitalrepo, ILogger<HospitalController> logger)
        {
            _logger = logger;
            _hospitalrepo = hospitalrepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Hospital>> GetAll()
        {
            return Ok(_hospitalrepo.GetAll().Select(n => (Hospital)Map.Model(n)));
        }

        [HttpGet("search/{str}")]
        public ActionResult<IEnumerable<Hospital>> Search(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                int zip;
                if(int.TryParse(str, out zip))
                {
                    return Ok(_hospitalrepo.SearchZip(zip).Select(n => (Hospital)Map.Model(n)));
                }
                else
                {
                    IEnumerable<Hospital> temp;
                    temp = _hospitalrepo.SearchName(str).Select(n => (Hospital)Map.Model(n));
                    temp = temp.Concat(_hospitalrepo.SearchCity(str).Select(n => (Hospital)Map.Model(n)));
                    temp = temp.Concat(_hospitalrepo.SearchAddress(str).Select(n => (Hospital)Map.Model(n)));
                    temp = temp.Concat(_hospitalrepo.SearchDepartments(str).Select(n => (Hospital)Map.Model(n)));
                    temp = temp.Concat(_hospitalrepo.SearchState(str).Select(n => (Hospital)Map.Model(n)));
                    temp = temp.GroupBy(x => x.Id).Select(x => x.First()).ToList();
                    return Ok(temp);
                }
            }
            return Ok(_hospitalrepo.GetAll().Select(n => (Hospital)Map.Model(n)));
        }

        [HttpGet("{id}")]
        public ActionResult<Hospital> Get(int id)
        {
            return Ok((Hospital)Map.Model(_hospitalrepo.Get(id)));
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] Hospital n)
        {
            if (ModelState.IsValid)
            {
                _hospitalrepo.Create((Domain.Tables.Hospital)Map.Table(n));
            }
            return Ok();
        }

        [HttpPut("Edit")]
        public ActionResult Edit([FromBody] Hospital n)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _hospitalrepo.Update((Domain.Tables.Hospital)Map.Table(n));
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
            _hospitalrepo.Delete(id);
            return Ok();
        }
    }
}
