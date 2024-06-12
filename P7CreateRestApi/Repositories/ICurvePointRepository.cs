using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePoint>> GetAllPoint();

        Task Add(CurvePoint curvePoint);

        Task Update(CurvePoint curvePoint);

        Task<CurvePoint> GetCurvePointId(int id);

        Task Delete(int id);
    }
}
