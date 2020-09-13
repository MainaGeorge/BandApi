using System;
using System.Collections.Generic;
using System.Linq;
using BandApi.DataContexts;
using BandApi.Entities;
using BandApi.Services.IRepository;

namespace BandApi.Services.Repository
{
    public class BandAlbumRepository : IBandAlbumRepository
    {
        private readonly AlbumBandDataContext _context;

        public BandAlbumRepository(AlbumBandDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Album> GetAlbumsForABand(Guid bandId)
        {
            if(bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            return _context.Albums.Where(a => a.BandId == bandId).ToList();
        }

        public Album GetAlbum(Guid bandId, Guid albumId)
        {
            if(bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            if(albumId == Guid.Empty)
                throw new ArgumentNullException(nameof(albumId));

            return _context.Albums.FirstOrDefault(a => a.Id == albumId && a.BandId == bandId);
        }

        public void AddAlbum(Guid bandId, Album album)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            if(album == null)
                throw new ArgumentNullException(nameof(album));

            album.BandId = bandId;
            _context.Albums.Add(album);
        }

        public void DeleteAlbum(Album album)
        {
            if(album == null)
                throw new ArgumentNullException(nameof(album));

            _context.Albums.Remove(album);
        }

        public IEnumerable<Band> GetBands()
        {
            return _context.Bands.OrderBy(b => b.Name).ToList();
        }

        public Band GetBand(Guid bandId)
        {
            if(bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            return _context.Bands.FirstOrDefault(b => b.Id == bandId);
        }

        public IEnumerable<Band> GetBands(IEnumerable<Guid> bandIds)
        {
            if(bandIds == null)
                throw new ArgumentNullException(nameof(bandIds));

            return _context.Bands.Where(b => bandIds.Contains(b.Id)).ToList();
        }

        public void AddBand(Band band)
        {
            if(band == null)
                throw new ArgumentNullException(nameof(band));

            _context.Bands.Add(band);
        }

        public void DeleteBand(Band band)
        {
            if(band == null)
                throw new ArgumentNullException(nameof(band));

            _context.Bands.Remove(band);
        }

        public bool BandExists(Guid bandId)
        {
            if(bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            return _context.Bands.Any(b => b.Id == bandId);
        }

        public bool AlbumExists(Guid albumId)
        {
            if(albumId == Guid.Empty)
                throw new ArgumentNullException(nameof(albumId));

            return _context.Albums.Any(a => a.Id == albumId);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
