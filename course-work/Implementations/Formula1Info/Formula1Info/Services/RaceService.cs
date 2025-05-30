using System;
using Formula1Info.Data;
using Formula1Info.DTOs.Helpers;
using Formula1Info.DTOs.RaceDTOs;
using Formula1Info.Models;
using Formula1Info.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Formula1Info.Services
{
    public class RaceService : IRaceService
    {
        private readonly AppDbContext _context;

        public RaceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<RaceListDto>> GetAllAsync(string? name, bool? isFuture, int page = 1, int pageSize = 5)
        {
            var query = _context.Races.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(r => r.Name.Contains(name));

            if (isFuture.HasValue)
            {
                var now = DateTime.UtcNow;
                query = isFuture.Value
                    ? query.Where(r => r.Date > now)
                    : query.Where(r => r.Date <= now);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RaceListDto
                {
                    RaceId = r.RaceId,
                    Name = r.Name,
                    Location = r.Location,
                    Date = r.Date
                })
                .ToListAsync();

            return new PagedResult<RaceListDto>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        public async Task<RaceDetailDto?> GetByIdAsync(string id)
        {
            var race = await _context.Races.FindAsync(id);
            if (race == null) return null;

            return new RaceDetailDto
            {
                RaceId = race.RaceId,
                Name = race.Name,
                Location = race.Location,
                Date = race.Date,
                Laps = race.Laps,
                CircuitUrl = race.CircuitUrl,
                DriverId = race.DriverId
            };
        }

        public async Task<bool> CreateAsync(RaceCreateDto dto)
        {
            var race = new Race
            {
                RaceId = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Location = dto.Location,
                Date = dto.Date,
                Laps = dto.Laps,
                CircuitUrl = dto.CircuitUrl,
                DriverId = dto.DriverId
            };

            _context.Races.Add(race);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RaceUpdateDto dto)
        {
            var race = await _context.Races.FindAsync(dto.RaceId);
            if (race == null) return false;

            race.Name = dto.Name;
            race.Location = dto.Location;
            race.Date = dto.Date;
            race.Laps = dto.Laps;
            race.CircuitUrl = dto.CircuitUrl;
            race.DriverId = dto.DriverId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var race = await _context.Races.FindAsync(id);
            if (race == null) return false;

            _context.Races.Remove(race);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}

