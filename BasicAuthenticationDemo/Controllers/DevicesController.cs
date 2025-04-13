using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicAuthenticationDemo.Models;
using BasicAuthenticationDemo.DTOs;
using BasicAuthenticationDemo.Models.Interfaces;

namespace BasicAuthenticationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDTO>>> Get()
        {
            var devices = await _deviceService.GetAllAsync();
            var deviceDtos = devices.Select(u => new DeviceDTO
            {
                Id = u.Id,
                Type = u.Type
            }).ToList();

            return Ok(deviceDtos);
        }

        #region Get
        // GET: api/Device/1
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DeviceDTO>> Get(int id)
        //{
        //    var device = await _deviceService.GetAsync(id);
        //    if (device == null)
        //        return NotFound();

        //    var deviceDto = new DeviceDTO
        //    {
        //        Id = device.Id,
        //        FirstName = device.FirstName,
        //        LastName = device.LastName,
        //        Email = device.Email,
        //        Password = device.Password
        //    };

        //    return Ok(deviceDto);
        //}
        #endregion

        // POST: api/Devices
        [HttpPost]
        public async Task<ActionResult<DeviceDTO>> Create([FromBody] DeviceDTO deviceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO -> Entity
            var device = new Device
            {
                Type = deviceDto.Type
            };

            device = await _deviceService.CreateAsync(device);

            // Map Entity -> DTO
            deviceDto.Id = device.Id;

            return CreatedAtAction(nameof(Get), new { id = device.Id }, deviceDto);
        }

        #region Update
        //// PUT: api/Device/1
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] DeviceDTO deviceDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (id != deviceDto.Id)
        //        return BadRequest("ID in URL doesn't match ID in payload.");

        //    // Map DTO -> Entity
        //    var device = new Device
        //    {
        //        Id = deviceDto.Id,
        //        Type = deviceDto.Type
        //    };

        //    var updated = await _deviceService.UpdateAsync(device);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}
        #endregion
        
        // DELETE: api/Devices/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _deviceService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/devices/type
        [HttpDelete("type/{type}")]
        public async Task<IActionResult> DeleteByType(string type)
        {
            var deleted = await _deviceService.DeleteByTypeAsync(type);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
