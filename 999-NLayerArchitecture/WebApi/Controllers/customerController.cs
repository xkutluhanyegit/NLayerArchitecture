using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;// Add this line with the correct namespace


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class customerController : ControllerBase
    {
        ICustomerService _customerService;
        public customerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("getall-customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllAsync();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add-customer")]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            var result = await _customerService.AddAsync(customer);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}