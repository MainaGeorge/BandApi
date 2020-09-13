using System;
using AutoMapper;
using BandApi.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

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
    }
}
