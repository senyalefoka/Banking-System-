using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IPersonalDetailsRepository
    {

        Task<PersonalDetail> GetByIdAsync(Guid id);
        Task<IEnumerable<PersonalDetail>> GetAllAsync();
        Task<IEnumerable<PersonalDetail>> GetByNameAsync(string name);
        Task<PersonalDetail> AddAsync(PersonalDetail personalDetail);
        Task UpdateAsync(PersonalDetail personalDetail);
        Task SaveAsync(PersonalDetail personalDetail);
        Task<bool> DeleteAsync(Guid id);
    }

}
