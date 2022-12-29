using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class SkillTypesRepo
    {
        public IObservable<object> DeleteSkillType(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<SkillType> GetSkillType(int id)
        {
            var skillType = new SkillType()
            {
                Id = 1,
                Name = "quan ly",
                Description = "quan li gioi"
            };
            return Observable.FromAsync(() => Task.FromResult(skillType));
        }

        public IObservable<List<SkillType>> GetSkillTypes()
        {
            var skillTypes = new List<SkillType>()
            {
                new SkillType()
                {
                    Id = 1,
                    Name = "quan li",
                    Description = "quan li gioi"
                },
                new SkillType()
                {
                    Id = 2,
                    Name = "code",
                    Description = "code gioi"
                },
            };
            return Observable.FromAsync(() => Task.FromResult(skillTypes));
        }

        public IObservable<SkillType> AddSkillType(SkillType skillType)
        {
            throw new NotImplementedException();
        }

        public IObservable<SkillType> UpdateSkillType(int skillTypeId, SkillType skillType)
        {
            throw new NotImplementedException();
        }
    }
}