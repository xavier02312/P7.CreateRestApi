using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _dbContext;

        public CurvePointRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CurvePoint> List() => _dbContext.CurvePoints.ToList();

        public void Create(CurvePoint curvePoint)
        {
            _dbContext.CurvePoints.Add(curvePoint);
            _dbContext.SaveChanges();
        }

        public CurvePoint? Get(int id) => _dbContext.CurvePoints.FirstOrDefault(c => c.Id == id);

        public CurvePoint? Update(CurvePoint curvePoint)
        {
            var curvePointAModifier = _dbContext.CurvePoints.FirstOrDefault(c => c.Id == curvePoint.Id);
            if (curvePointAModifier is not null)
            {
                curvePointAModifier.CurveId = curvePoint.CurveId;
                curvePointAModifier.AsOfDate = curvePoint.AsOfDate;
                curvePointAModifier.CurvePointValue = curvePoint.CurvePointValue;
                _dbContext.SaveChanges();
            }
            return curvePointAModifier;
        }

        public CurvePoint? Delete(int id)
        {
            var curvePoint = _dbContext.CurvePoints.FirstOrDefault(c => c.Id == id);
            if (curvePoint is not null)
            {
                _dbContext.CurvePoints.Remove(curvePoint);
                _dbContext.SaveChanges();
            }
            return curvePoint;
        }
    }
}
