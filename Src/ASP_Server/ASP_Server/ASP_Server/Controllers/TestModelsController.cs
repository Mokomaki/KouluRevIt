﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_Server.Models;

namespace ASP_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestModelsController : ControllerBase
    {
        private readonly TestContext _context;

        public TestModelsController(TestContext context)
        {
            _context = context;
        }

        // GET: api/TestModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestModel>>> GetTestModels()
        {
            return await _context.TestModels.ToListAsync();
        }

        // GET: api/TestModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestModel>> GetTestModel(long id)
        {
            var testModel = await _context.TestModels.FindAsync(id);

            if (testModel == null)
            {
                return NotFound();
            }

            return testModel;
        }

        // PUT: api/TestModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestModel(long id, TestModel testModel)
        {
            if (id != testModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(testModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TestModels
        [HttpPost]
        public async Task<ActionResult<TestModel>> PostTestModel(TestModel testModel)
        {
            testModel.Id = _context.TestModels.Any() ? _context.TestModels.Last().Id + 1 : 0;

            _context.TestModels.Add(testModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestModel", new { id = testModel.Id }, testModel);
        }

        // DELETE: api/TestModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestModel>> DeleteTestModel(long id)
        {
            var testModel = await _context.TestModels.FindAsync(id);
            if (testModel == null)
            {
                return NotFound();
            }

            _context.TestModels.Remove(testModel);
            await _context.SaveChangesAsync();

            return testModel;
        }

        private bool TestModelExists(long id)
        {
            return _context.TestModels.Any(e => e.Id == id);
        }
    }
}