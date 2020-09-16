﻿using AutoMapper;
using BandApi.DataTransferObjects;
using BandApi.Entities;
using BandApi.QueryModifiers;
using BandApi.Services.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BandApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public IActionResult GetBands([FromQuery] QueryParameters queryParameters)
        {
            var bands = _bandAlbumRepository.GetBands(queryParameters);

            var bandsDto = _mapper.Map<IEnumerable<BandDto>>(bands);

            return StatusCode(StatusCodes.Status200OK, bandsDto);
        }

        [HttpGet("{bandId}", Name = "GetBand")]
        public IActionResult GetBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);

            if (band == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var bandDto = _mapper.Map<BandDto>(band);
            return StatusCode(StatusCodes.Status200OK, bandDto);
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand(BandCreationDto creationDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            creationDto.Founded ??= DateTime.Now;

            var band = _mapper.Map<Band>(creationDto);

            _bandAlbumRepository.AddBand(band);
            _bandAlbumRepository.Save();

            var bandDto = _mapper.Map<BandDto>(band);
            return CreatedAtRoute("GetBand", new { bandId = bandDto.Id }, bandDto);

        }

        [HttpDelete("{bandId}")]
        public IActionResult DeleteBand(Guid bandId)
        {
            var bandToDelete = _bandAlbumRepository.GetBand(bandId);

            if (bandToDelete == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            _bandAlbumRepository.DeleteBand(bandToDelete);
            _bandAlbumRepository.Save();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
