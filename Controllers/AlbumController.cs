﻿using AutoMapper;
using BandApi.DataTransferObjects;
using BandApi.Entities;
using BandApi.Services.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BandApi.Controllers
{
    [Route("api/bands/{bandId}/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBandAlbumRepository _bandAlbumRepository;

        public AlbumController(IMapper mapper, IBandAlbumRepository bandAlbumRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> GetAlbumsForBand(Guid bandId)
        {

            if (!_bandAlbumRepository.BandExists(bandId))
                return StatusCode(StatusCodes.Status404NotFound);

            var albumsByGivenBand = _bandAlbumRepository.GetAlbumsForABand(bandId);
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<AlbumDto>>(albumsByGivenBand));
        }

        [HttpGet("{albumId}", Name = "GetAlbumForBand")]
        public ActionResult<AlbumDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return StatusCode(StatusCodes.Status404NotFound);

            var albumForBand = _bandAlbumRepository.GetAlbum(bandId, albumId);

            if (albumForBand == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<AlbumDto>(albumForBand));

        }

        [HttpPost]
        public ActionResult<AlbumDto> PostAlbum(Guid bandId, AlbumCreationDto creationDto)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "Band does not exist " });

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var album = _mapper.Map<Album>(creationDto);
            album.BandId = bandId;

            _bandAlbumRepository.AddAlbum(bandId, album);
            _bandAlbumRepository.Save();

            var albumDto = _mapper.Map<AlbumDto>(album);
            return CreatedAtRoute("GetAlbumForBand", new { bandId, albumId = albumDto.BandId }, albumDto);
        }
    }
}
