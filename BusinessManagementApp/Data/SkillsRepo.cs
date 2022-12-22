using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class SkillsRepo
    {
        public IObservable<List<SkillOverview>> GetSkillOverviews()
        {
            var overviews = new List<SkillOverview>()
            {
                new SkillOverview()
                {
                    Employee = new Employee() { Id = "1", Name = "Nguyen Van A" },
                    LastUpdatedTime = new DateTime(2022, 11, 12, 13, 45, 55)
                },

                new SkillOverview()
                {
                    Employee = new Employee() { Id = "2", Name = "Mai Thi Xuan" },
                    LastUpdatedTime = new DateTime(2022, 11, 15, 17, 11, 12)
                },
            };

            return Observable.FromAsync(() => Task.FromResult(overviews));
        }

        public IObservable<SkillOverview> GetSkillDetails(string employeeId)
        {
            var overview = new SkillOverview()
            {
                Employee = new Employee() { Id = "1", Name = "Nguyen Van A" },
                LastUpdatedTime = new DateTime(2022, 11, 12, 13, 45, 55),
                Skills = new List<Skill>()
                {
                    new Skill()
                    {
                        EmployeeId = "1",
                        SkillType = new SkillType() { Id = 1, Name = "IT", Description = "Proficiency at using IT equipments and software." },
                        Level = SkillLevel.Excellent,
                        LastUpdatedTime = new DateTime(2022, 11, 12, 13, 45, 55)
                    },

                    new Skill()
                    {
                        EmployeeId = "1",
                        SkillType = new SkillType() { Id = 2, Name = "English", Description = "Proficiency at speaking, reading, listening, writing English." },
                        Level = SkillLevel.Unrated,
                        LastUpdatedTime = new DateTime(2022, 11, 10, 13, 45, 55)
                    }
                }
            };

            return Observable.FromAsync(() => Task.FromResult(overview));
        }

        public IObservable<List<Skill>> UpdateSkills(string employeeId, List<Skill> skills)
        {
            return Observable.FromAsync(() => Task.FromResult(skills));
        }
    }
}