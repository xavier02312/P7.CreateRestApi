using Dot.Net.WebApi.Controllers;

namespace P7CreateRestApi.Repositories
{
    public interface IRuleNameRepository
    {
        Task<IEnumerable<RuleName>> GetAllRuleName();

        Task Add(RuleName ruleName);

        Task Update(RuleName ruleName);

        Task<RuleName> GetRuleNameId(int id);

        Task Delete(int id);
    }
}
