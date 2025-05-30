using System;

using Formula1Info.Data;
using Formula1Info.Models;
using Microsoft.EntityFrameworkCore;
using Formula1Info.DTOs.DriverDTOs;
using Formula1Info.Services.Interfaces;
using Formula1Info.DTOs.Helpers;

namespace Formula1Info.Services
{

    public class DriverService : IDriverService
    {
        private readonly AppDbContext _context;

        public DriverService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<DriverListDto>> GetAllAsync(
    string? nationality,
    string? fullName,
    string? sort,
    int page = 1,
    int pageSize = 5)
        {
            var query = _context.Drivers.AsQueryable();

            if (!string.IsNullOrEmpty(nationality))
                query = query.Where(d => d.Nationality.Contains(nationality));

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(d =>
                    (d.FirstName + " " + d.LastName).ToLower().Contains(fullName.ToLower()));

            if (sort == "championships")
                query = query.OrderByDescending(d => d.NumberOfChampionships);
            else
                query = query.OrderBy(d => d.LastName);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DriverListDto
                {
                    DriverId = d.DriverId,
                    FullName = d.FirstName + " " + d.LastName,
                    Nationality = d.Nationality,
                    NumberOfChampionships = d.NumberOfChampionships
                }).ToListAsync();

            return new PagedResult<DriverListDto>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }



        public async Task<DriverDetailDto?> GetByIdAsync(string id)
        {
            var d = await _context.Drivers.FindAsync(id);
            if (d == null) return null;

            return new DriverDetailDto
            {
                DriverId = d.DriverId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                DateOfBirth = d.DateOfBirth,
                Nationality = d.Nationality,
                DriverImageUrl = d.DriverImageUrl,
                UniqueNumber = d.UniqueNumber,
                NumberOfChampionships = d.NumberOfChampionships,
                TeamId = d.TeamId
            };
        }

        public async Task<bool> CreateAsync(DriverCreateDto dto)
        {
            var exists = await _context.Drivers.AnyAsync(d => d.UniqueNumber == dto.UniqueNumber);
            if (exists)
            {
                return false; 
            }

            var driver = new Driver
            {
                DriverId = Guid.NewGuid().ToString(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                DriverImageUrl = dto.DriverImageUrl,
                UniqueNumber = dto.UniqueNumber,
                NumberOfChampionships = dto.NumberOfChampionships,
                TeamId = dto.TeamId
            };

            _context.Drivers.Add(driver);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DriverUpdateDto dto)
        {
            var driver = await _context.Drivers.FindAsync(dto.DriverId);
            if (driver == null) return false;

            driver.FirstName = dto.FirstName;
            driver.LastName = dto.LastName;
            driver.DateOfBirth = dto.DateOfBirth;
            driver.Nationality = dto.Nationality;
            driver.DriverImageUrl = dto.DriverImageUrl;
            driver.UniqueNumber = dto.UniqueNumber;
            driver.NumberOfChampionships = dto.NumberOfChampionships;
            driver.TeamId = dto.TeamId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return false;

            _context.Drivers.Remove(driver);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}

