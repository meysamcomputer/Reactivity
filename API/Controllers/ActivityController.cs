using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivityController : BaseApiController
    {
        private readonly DataBaseContext _context;

        public ActivityController(DataBaseContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAllActivities()
        {
            return await _context.Activity.ToListAsync(); 
        }
          [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetAllActivity(Guid  id)
        {
            return await _context.Activity.FindAsync(id); 
        }

    }
}