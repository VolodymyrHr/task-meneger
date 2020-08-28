using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using task_maneger.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace task_maneger.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private static List<Task> tasks = new List<Task>();

        // GET tasks
        [HttpGet("")]
        public List<Task> GetTasks()
        {
            return Data.getAllTasks();
        }

        [HttpPost("")]
        public Task PostTask([FromBody] object body)
        {
            var data = (JObject)JsonConvert.DeserializeObject(body.ToString());
            string taskName = data["name"].Value<string>();
            
            return Data.addTask(taskName);
        }
    }
}
