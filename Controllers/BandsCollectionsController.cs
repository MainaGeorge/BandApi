using AutoMapper;
using BandApi.DataTransferObjects;
using BandApi.Entities;
using BandApi.Services.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BandApi.Controllers
{
    [Route("api/bandscollections")]
    [ApiController]
    public class BandsCollectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBandAlbumRepository _bandAlbumRepository;

        public BandsCollectionsController(IMapper mapper, IBandAlbumRepository bandAlbumRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
        }

        [HttpPost]
        public ActionResult<IEnumerable<Band>> CreateAGroupOfBandsAtAGo(IEnumerable<BandCreationDto> bandCreationDtos)
        {
            var bandsToCreate = _mapper.Map<IEnumerable<Band>>(bandCreationDtos);

            foreach (var band in bandsToCreate)
            {
                _bandAlbumRepository.AddBand(band);
            }

            _bandAlbumRepository.Save();

            var bandsToReturn = _mapper.Map<IEnumerable<BandDto>>(bandsToCreate);

            return StatusCode(StatusCodes.Status200OK, bandsToReturn);
        }
    }
}
