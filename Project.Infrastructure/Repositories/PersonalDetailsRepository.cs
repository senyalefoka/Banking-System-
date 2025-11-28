using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class PersonalDetailsRepository : IPersonalDetailsRepository
    {
        private readonly employeeDetailsContext _context;

        public PersonalDetailsRepository(employeeDetailsContext context)
        {
            _context = context;
        }

        public async Task<PersonalDetail> GetByIdAsync(Guid id)
        {
            return await _context.PersonalDetails.FindAsync(id);
        }

        public async Task<IEnumerable<PersonalDetail>> GetAllAsync()
        {
            return await _context.PersonalDetails.ToListAsync();
        }

        public async Task<IEnumerable<PersonalDetail>> GetByNameAsync(string name)
        {
            return await _context.PersonalDetails
                .Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name))
                .ToListAsync();
        }

        public async Task<PersonalDetail> AddAsync(PersonalDetail personalDetail)
        {
            // Always generate new GUID to prevent duplicates
            personalDetail.Code = Guid.NewGuid();

            _context.PersonalDetails.Add(personalDetail);
            await _context.SaveChangesAsync();
            return personalDetail;
        }



        public async Task<bool> DeleteAsync(Guid id)
        {
            var personalDetail = await GetByIdAsync(id);
            if (personalDetail != null)
            {
                _context.PersonalDetails.Remove(personalDetail);
                await _context.SaveChangesAsync();
            }
            else
            {
                return false;
            }
            return true;
        }

        public async Task UpdateAsync(PersonalDetail personalDetail)
        {
            _context.PersonalDetails.Update(personalDetail);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(PersonalDetail personalDetail)
        {
            //This method seems redundant with AddAsync/ UpdateAsync
            // You might want to check if it exists and update, otherwise add
            var exists = await _context.PersonalDetails.AnyAsync(p => p.Code == personalDetail.Code);
            if (exists)
            {
                _context.PersonalDetails.Update(personalDetail);
            }
            else
            {
                await _context.PersonalDetails.AddAsync(personalDetail);
            }
            await _context.SaveChangesAsync();
        }

    }
}
