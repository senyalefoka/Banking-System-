using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Project.Domain.Entities;
using Project.Domain.Interfaces;
using Project.Domain.Validators;

namespace Project.Infrastructure.Repositories
{
    public class PersonalDetailsRepository : IPersonalDetailsRepository
    {
        private readonly employeeDetailsContext _context;
        private readonly IValidator<PersonalDetail> _validator;

        public PersonalDetailsRepository(
            employeeDetailsContext context,
            IValidator<PersonalDetail> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<PersonalDetail> AddAsync(PersonalDetail personalDetail)
        {
            // Validate before adding
            var validationResult = await _validator.ValidateAsync(personalDetail);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Cannot add personal detail: {errors}");
            }

            // Check for duplicates
            var existingByIdNumber = await _context.PersonalDetails
                .AnyAsync(p => p.IDNumber == personalDetail.IDNumber);

            if (existingByIdNumber)
                throw new InvalidOperationException($"ID number {personalDetail.IDNumber} already exists");

            var existingByAccount = await _context.PersonalDetails
                .AnyAsync(p => p.AccountNumber == personalDetail.AccountNumber);

            if (existingByAccount)
                throw new InvalidOperationException($"Account number {personalDetail.AccountNumber} already exists");

            // Always generate new GUID
            personalDetail.Code = Guid.NewGuid();

            _context.PersonalDetails.Add(personalDetail);
            await _context.SaveChangesAsync();
            return personalDetail;
        }

        public async Task UpdateAsync(PersonalDetail personalDetail)
        {
            // Validate before updating
            var validationResult = await _validator.ValidateAsync(personalDetail);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Cannot update personal detail: {errors}");
            }

            // Check if entity exists
            var existing = await GetByIdAsync(personalDetail.Code);
            if (existing == null)
                throw new KeyNotFoundException($"Personal detail with Code {personalDetail.Code} not found");

            // Check for duplicate ID number (excluding current record)
            var duplicateIdNumber = await _context.PersonalDetails
                .AnyAsync(p => p.IDNumber == personalDetail.IDNumber && p.Code != personalDetail.Code);

            if (duplicateIdNumber)
                throw new InvalidOperationException($"ID number {personalDetail.IDNumber} is already assigned to another person");

            // Check for duplicate account number (excluding current record)
            var duplicateAccount = await _context.PersonalDetails
                .AnyAsync(p => p.AccountNumber == personalDetail.AccountNumber && p.Code != personalDetail.Code);

            if (duplicateAccount)
                throw new InvalidOperationException($"Account number {personalDetail.AccountNumber} is already assigned to another person");

            _context.PersonalDetails.Update(personalDetail);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(PersonalDetail personalDetail)
        {
            // Validate first
            var validationResult = await _validator.ValidateAsync(personalDetail);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Cannot save personal detail: {errors}");
            }

            var exists = await _context.PersonalDetails.AnyAsync(p => p.Code == personalDetail.Code);

            if (exists)
            {
                // Check for duplicates when updating
                var duplicateIdNumber = await _context.PersonalDetails
                    .AnyAsync(p => p.IDNumber == personalDetail.IDNumber && p.Code != personalDetail.Code);

                if (duplicateIdNumber)
                    throw new InvalidOperationException($"ID number {personalDetail.IDNumber} is already assigned to another person");

                var duplicateAccount = await _context.PersonalDetails
                    .AnyAsync(p => p.AccountNumber == personalDetail.AccountNumber && p.Code != personalDetail.Code);

                if (duplicateAccount)
                    throw new InvalidOperationException($"Account number {personalDetail.AccountNumber} is already assigned to another person");

                _context.PersonalDetails.Update(personalDetail);
            }
            else
            {
                // Check for duplicates when adding
                var existingByIdNumber = await _context.PersonalDetails
                    .AnyAsync(p => p.IDNumber == personalDetail.IDNumber);

                if (existingByIdNumber)
                    throw new InvalidOperationException($"ID number {personalDetail.IDNumber} already exists");

                var existingByAccount = await _context.PersonalDetails
                    .AnyAsync(p => p.AccountNumber == personalDetail.AccountNumber);

                if (existingByAccount)
                    throw new InvalidOperationException($"Account number {personalDetail.AccountNumber} already exists");

                personalDetail.Code = Guid.NewGuid();
                await _context.PersonalDetails.AddAsync(personalDetail);
            }

            await _context.SaveChangesAsync();
        }

        public Task<PersonalDetail> GetByIdAsync(Guid id)
        {
        
            var personalDetail =  _context.PersonalDetails
                .FirstOrDefaultAsync(p => p.Code == id);
            return personalDetail;
        }

        public Task<IEnumerable<PersonalDetail>> GetAllAsync()
        {
           var personalDetails =  _context.PersonalDetails
                .AsEnumerable();
            return Task.FromResult(personalDetails);
        }

        public Task<IEnumerable<PersonalDetail>> GetByNameAsync(string name)
        {
           var personalDetails =  _context.PersonalDetails
                .Where(p => p.FirstName.Contains(name))
                .AsEnumerable();
            return Task.FromResult(personalDetails);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var personalDetail =  _context.PersonalDetails
                .FirstOrDefaultAsync(p => p.Code == id);
            if (personalDetail == null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}