using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AttendanceApi.Reps;
using AttendanceApi.Domain.Models;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private IPersonnelRepository objRep;
        private IConfiguration configuration { get; set; }

        public PersonnelController(IConfiguration configuration, IPersonnelRepository objRep)
        {
            this.objRep = objRep;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var objs = (await objRep.Get());
                if (objs.ResultObject == null)
                    return NotFound();

                return Ok(objs);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var obj = (await objRep.Get(id.Value));
                    if (obj.ResultObject == null)
                      return NotFound();

                    return Ok(obj);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonnelVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = (await objRep.Add(vm));

                    if(obj.Successed)
                    {
                        if (obj.ResultObject.PersonnelId > 0)
                        {
                            return Ok(obj);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound(obj);
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
         }

        [HttpPut]
        public async Task<IActionResult> Update(PersonnelVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = (await objRep.Update(vm));

                    if (obj.Successed)
                    {
                        if (obj.ResultObject.PersonnelId > 0)
                        {
                            return Ok(obj);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound(obj);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
 
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var obj  = (await objRep.Delete(id.Value));
                    if (obj.Successed)
                    {
                        if (obj.ResultObject == 0)
                            return NotFound();

                        return Ok(obj);
                    }
                    else
                    {
                        return NotFound(obj);
                    }
                  }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
