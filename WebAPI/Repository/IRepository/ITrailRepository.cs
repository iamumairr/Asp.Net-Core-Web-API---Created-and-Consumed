using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();

        ICollection<Trail> GetTrailsInNationaPark(int npId);

        Trail GetTrail(int trailId);

        bool TrailExists(string name);

        bool TrailExists(int id);

        bool CreateTrail(Trail trail);

        bool UpdateTrail(Trail trail);

        bool DeleteTrail(Trail trail);

        bool Save();
    }
}