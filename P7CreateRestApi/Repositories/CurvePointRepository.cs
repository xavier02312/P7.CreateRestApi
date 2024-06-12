using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context) 
        {
            _context = context; 
        }

        public async Task<IEnumerable<CurvePoint>> GetAllPoint()
        {
            var curvPoint = await _context.CurvePoints.ToListAsync();

            if (curvPoint == null)
            {
                throw new Exception("BidList does not exist.");
            }

            return curvPoint;
        }

        public async Task Add(CurvePoint curvePoint)
        {
            if (CurvePointExists(curvePoint.Id))
            {
                throw new Exception("CurvePoint already exists.");
            }

            _context.CurvePoints.Add(curvePoint);

            await _context.SaveChangesAsync();
        }

        public async Task Update(CurvePoint curvePoint)
        {
            if (!CurvePointExists(curvePoint.Id))
            {
                throw new Exception("CurvePoint does not exist.");
            }

            _context.CurvePoints.Update(curvePoint);

            await _context.SaveChangesAsync();
        }

        public async Task<CurvePoint> GetCurvePointId(int id)
        {

            var curv = await _context.CurvePoints.FindAsync(id);

            if (curv == null)
            {
                throw new Exception("CurvePoint does not exists.");
            }

            return curv;
        }

        public async Task Delete(int id)
        {
            var curveP = await _context.CurvePoints.FindAsync(id);

            if (curveP != null)
            {
                _context.CurvePoints.Remove(curveP);

                await _context.SaveChangesAsync();
            }
        }

        public bool CurvePointExists(int id)
        {
            return _context.CurvePoints.Any(x => x.Id == id);
        }
    }
}
