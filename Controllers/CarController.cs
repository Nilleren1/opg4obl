using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opg1obl;
using opg4obl.Manager;

namespace opg4obl.Controllers
{

    
        [Route("api/[controller]")]
        [ApiController]
        public class CarController : ControllerBase
        {
            private CarManager _carManager = new CarManager();
            // GET: api/<ItemsController>
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [HttpGet]
            public ActionResult<IEnumerable<Car>> Get([FromQuery] string? filterstring,  /*Her laver jeg en header som parameter:  */ [FromHeader] int itemAmount)
            {
                IEnumerable<Car> items = _carManager.GetAll(filterstring);
                //Her laver vi vores egen header.
                //string testHeader = Request.Headers["TestHeader"];

                Response.Headers.Add("Total-Amount", items.Count().ToString());


                if (items == null || items.Count() == 0)
                {
                    return NotFound("Den kunne den altså ikke finde");
                }
                if (itemAmount > 0)
                {
                    return Ok(items.Take(itemAmount));
                }
                else
                {
                    return Ok(items);
                }
            }


            //GET: api/items/Name/<value>
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [Route("Name/{filterstring}")]
            [HttpGet]
            public ActionResult<IEnumerable<Car>> GetFilterstring(string filterstring)
            {
                IEnumerable<Car> items = _carManager.GetAll(filterstring);
                if (items == null || items.Count() == 0)
                {
                    return NotFound("Den kunne den altså ikke finde");
                }

                return Ok(items);
            }

            //api/items/Quality/<value>
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [Route("Price/{Price}")]
            [HttpGet]
            public ActionResult<IEnumerable<Car>> GetByPrice(double Price)
            {
                IEnumerable<Car> items = _carManager.GetByPrice(Price);
                if (items == null)
                {
                    return NotFound("Price could not be found");
                }
                return Ok(items);
            }


            // GET api/<ItemsController>/5

            [HttpGet("{id}")]
            public Car GetId(int id)
            {
                return _carManager.GetById(id);
            }

            // POST api/<ItemsController>[ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [HttpPost]
            public Car Post([FromBody] Car value)
            {
                return _carManager.AddCar(value);
            }

            // PUT api/<ItemsController>/5
            [HttpPut("{id}")]
            public Car Put(int id, [FromBody] Car value)
            {
                return _carManager.UpdateCar(id, value);
            }

        // DELETE api/<ItemsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
            public OkObjectResult Delete(int id)
            {
                return Ok(_carManager.DeleteCar(id));
            }
        }
    }

