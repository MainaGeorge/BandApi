using BandApi.Entities;
using System;
using System.Collections.Generic;
using BandApi.QueryModifiers;

namespace BandApi.Services.IRepository
{
    public interface IBandAlbumRepository
    {
        IEnumerable<Album> GetAlbumsForABand(Guid bandId);
        Album GetAlbum(Guid bandId, Guid albumId);
        void AddAlbum(Guid bandId, Album album);
        void DeleteAlbum(Album album);
        IEnumerable<Band> GetBands(QueryParameters queryParameters);
        Band GetBand(Guid bandId);
        IEnumerable<Band> GetBands(IEnumerable<Guid> bandIds);
        void AddBand(Band band);
        void DeleteBand(Band band);
        bool BandExists(Guid bandId);
        bool AlbumExists(Guid albumId);
        bool Save();

    }
}
