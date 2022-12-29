using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskordController : ControllerBase
    {
        List<OrdStruct> orders = new List<OrdStruct> { };
        public TaskordController(/*List<OrdStruct> orders*/)
        {
            var order = new OrdStruct { };
            order.Id = 0;
            order.Name = "First";
            order.Tasks = new List<TaskStruct> { new TaskStruct { Id = 0, Complexity = 3, TimeConsumption = 10, PrevTasks = { } },
                                                 new TaskStruct { Id = 1, Complexity = 2, TimeConsumption = 8, PrevTasks = new List<int> { 0 } },
                                                 new TaskStruct { Id = 2, Complexity = 4, TimeConsumption = 7, PrevTasks = new List<int> { 0 } },
                                                 new TaskStruct { Id = 3, Complexity = 5, TimeConsumption = 10, PrevTasks = new List<int> { 1, 2 } } };
            this.orders.Add(order);
            order = new OrdStruct { };
            order.Id = 1;
            order.Name = "Second";
            order.Tasks = new List<TaskStruct> { new TaskStruct { Id = 0, Complexity = 3, TimeConsumption = 10, PrevTasks = { } },
                                                 new TaskStruct { Id = 1, Complexity = 5, TimeConsumption = 10, PrevTasks = new List<int> { 0 } } };
            this.orders.Add(order);

            order = new OrdStruct { };
            order.Id = 2;
            order.Name = "Third";
            order.Tasks = new List<TaskStruct> { };
            this.orders.Add(order);
            //this.orders = orders;
        }

        private readonly ILogger<TaskordController> _logger;

        /*public TaskordController(ILogger<TaskordController> logger)
        {
            _logger = logger;
        }*/

        public string ReturnStringUsingOrdersID(int idParam)
        {
            string retStr = string.Empty;
            retStr += "orderid=" + orders[idParam].Id;
            retStr += " | name=" + orders[idParam].Name + "-> tasks";
            if (orders[idParam].Tasks.Count == 0)
            {
                retStr += " are empty";
            }
            else
            {
                retStr += "= ↓↓↓";
                foreach (var ordtask in orders[idParam].Tasks)
                {
                    retStr += "\ntaskid=" + ordtask.Id;
                    retStr += " | complex=" + ordtask.Complexity;
                    retStr += " | timecon=" + ordtask.TimeConsumption;
                    if (ordtask.PrevTasks != null)
                    {
                        if (ordtask.PrevTasks.Count != 0) retStr += " | prevtasks={" + string.Join(",", ordtask.PrevTasks) + "}";
                        else retStr += " | prevtasks is empty";
                    }
                    else { retStr += " | prevtasks is empty"; }

                }
            }
            retStr += "\n";
            return retStr;
        }


        [HttpGet]
        public string Get()
        {
            string retStr = string.Empty;
            foreach(var order in orders)
            {
                retStr+= ReturnStringUsingOrdersID(order.Id) + "\n";
            }
            return retStr;
        }

        [HttpGet("{id}")]
        public string Get(int id) 
        {
            return ReturnStringUsingOrdersID(id);
        }

        [HttpPost]
        public async Task<ActionResult<OrdStruct>> Post(OrdStruct restoredOrder)
        {
            //OrdStruct restoredOrder = JsonSerializer.Deserialize<OrdStruct>(inputJson);
            if (restoredOrder == null)
            {
                return BadRequest();
            }
            this.orders.Add(restoredOrder);
            return Ok(restoredOrder);
        }

        [HttpPut]
        public async Task<ActionResult<OrdStruct>> Put(OrdStruct ordStruct)
        {
            if (ordStruct == null)
            { return BadRequest(); }
            bool foundFlag = false;
            foreach (var order in orders) { if (ordStruct.Id == order.Id) foundFlag = true; }
            if (!foundFlag) { return NotFound(); }
            orders[ordStruct.Id] = ordStruct;
            return Ok(ordStruct);
        }
    }
}
