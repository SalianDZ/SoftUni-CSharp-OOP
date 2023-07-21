using System;
using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> models = new();
        public IReadOnlyCollection<IStudent> Models => models.AsReadOnly();

        public void AddModel(IStudent model)
        {
            models.Add(model);
        }

        public IStudent FindById(int id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] combinedName = name.Split(" ");
            return models.FirstOrDefault(x => x.FirstName == combinedName[0] && x.LastName == combinedName[1]);
        }
    }
}
