using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Models.OutputModel;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        private readonly IRuleNameRepository _ruleNameRepository;
        public RuleNameService(IRuleNameRepository ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }

        public RuleNameOutputModel? Create(RuleNameInputModel inputModel)
        {
            var ruleName = new RuleName
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Json = inputModel.Json,
                Template = inputModel.Template,
                SqlStr = inputModel.SqlStr,
                SqlPart = inputModel.SqlPart,
            };
            _ruleNameRepository.Create(ruleName);
            return ToOutputModel(ruleName);
        }

        public RuleNameOutputModel? Delete(int id)
        {
            var ruleName = _ruleNameRepository.Delete(id);
            if (ruleName is not null)
            {
                return ToOutputModel(ruleName);
            }
            return null;
        }

        public RuleNameOutputModel? Get(int id)
        {
            var ruleName = _ruleNameRepository.Get(id);
            if (ruleName is not null)
            {
                return ToOutputModel(ruleName);
            }
            return null;
        }

        public List<RuleNameOutputModel> List()
        {
            var list = new List<RuleNameOutputModel>();
            var ruleNames = _ruleNameRepository.List();
            foreach (var ruleName in ruleNames)
            {
                list.Add(ToOutputModel(ruleName));
            }
            return list;
        }

        public RuleNameOutputModel? Update(int id, RuleNameInputModel inputModel)
        {
            var ruleName = _ruleNameRepository.Update(new RuleName
            {
                Id = id,
                Name = inputModel.Name,
                Description = inputModel.Description,
                Json = inputModel.Json,
                Template = inputModel.Template,
                SqlStr = inputModel.SqlStr,
                SqlPart = inputModel.SqlPart,
            });
            if (ruleName is not null)
            {
                return ToOutputModel(ruleName);
            }
            return null;
        }

        private RuleNameOutputModel ToOutputModel(RuleName ruleName) =>
            new RuleNameOutputModel
            {
                Id = ruleName.Id,
                Name = ruleName.Name,
                Description = ruleName.Description,
                Json = ruleName.Json,
                Template = ruleName.Template,
                SqlStr = ruleName.SqlStr,
                SqlPart = ruleName.SqlPart
            };
    }
}
