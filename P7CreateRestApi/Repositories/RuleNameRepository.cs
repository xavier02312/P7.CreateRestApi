using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private LocalDbContext _context;

        public RuleNameRepository(LocalDbContext context)
        {  
            _context = context; 
        }

        public async Task<IEnumerable<RuleName>> GetAllRuleName()
        {
            var rulN = await _context.RuleNames.ToListAsync();

            if (rulN == null)
            {
                throw new Exception("RuleName does not exist.");
            }

            return rulN;
        }

        public async Task Add(RuleName ruleName)
        {
            if (RuleNameExists(ruleName.Id))
            {
                throw new Exception("RuleName already exists.");
            }

            _context.RuleNames.Add(ruleName);

            await _context.SaveChangesAsync();
        }

        public async Task Update(RuleName ruleName)
        {
            if (!RuleNameExists(ruleName.Id))
            {
                throw new Exception("RuleName does not exist.");
            }

            _context.RuleNames.Update(ruleName);

            await _context.SaveChangesAsync();
        }

        public async Task<RuleName> GetRuleNameId(int id)
        {

            var ruleNam = await _context.RuleNames.FindAsync(id);

            if (ruleNam == null)
            {
                throw new Exception("RuleName does not exists.");
            }

            return ruleNam;
        }

        public async Task Delete(int id)
        {
            var ruleName = await _context.RuleNames.FindAsync(id);

            if (ruleName != null)
            {
                _context.RuleNames.Remove(ruleName);

                await _context.SaveChangesAsync();
            }
        }

        public bool RuleNameExists(int id)
        {
            return _context.RuleNames.Any(r => r.Id == id);
        }
    }
}
