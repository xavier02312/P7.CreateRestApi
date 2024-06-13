using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly LocalDbContext _dbContext;
        public RuleNameRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(RuleName ruleName)
        {
            _dbContext.RuleNames.Add(ruleName);
            _dbContext.SaveChanges();
        }

        public RuleName? Delete(int id)
        {
            var ruleName = _dbContext.RuleNames.FirstOrDefault(r => r.Id == id);
            if (ruleName is not null)
            {
                _dbContext.RuleNames.Remove(ruleName);
                _dbContext.SaveChanges();
            }
            return ruleName;
        }

        public RuleName? Get(int id) => _dbContext.RuleNames.FirstOrDefault(r => r.Id == id);

        public List<RuleName> List() => _dbContext.RuleNames.ToList();

        public RuleName? Update(RuleName ruleName)
        {
            var ruleNameAModifier = _dbContext.RuleNames.FirstOrDefault(r => r.Id == ruleName.Id);
            if (ruleNameAModifier is not null)
            {
                ruleNameAModifier.Name = ruleName.Name;
                ruleNameAModifier.Description = ruleName.Description;
                ruleNameAModifier.Json = ruleName.Json;
                ruleNameAModifier.Template = ruleName.Template;
                ruleNameAModifier.SqlStr = ruleName.SqlStr;
                ruleNameAModifier.SqlPart = ruleName.SqlPart;
                _dbContext.SaveChanges();
            }
            return ruleNameAModifier;
        }
    }
}
