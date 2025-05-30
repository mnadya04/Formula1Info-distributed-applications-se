using System;

using Formula1Info.Data;
using Formula1Info.DTOs.Helpers;
using Formula1Info.DTOs.TeamDTOs;
using Formula1Info.Models;
using Formula1Info.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Formula1Info.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TeamListDto>> GetAllAsync(string? name, string? country, int page = 1, int pageSize = 10)
        {
            var query = _context.Teams.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(t => t.Name.Contains(name));

            if (!string.IsNullOrEmpty(country))
                query = query.Where(t => t.BaseCountry.Contains(country));

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages == 0) totalPages = 1; 

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TeamListDto
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                    BaseCountry = t.BaseCountry
                })
                .ToListAsync();

            return new PagedResult<TeamListDto>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }


        public async Task<TeamDetailDto?> GetByIdAsync(string id)
        {
            var t = await _context.Teams.FindAsync(id);
            if (t == null) return null;

            return new TeamDetailDto
            {
                TeamId = t.TeamId,
                Name = t.Name,
                BaseCountry = t.BaseCountry,
                FounderYear = t.FounderYear,
                Description = t.Description,
                Budget = t.Budget,
                ImageUrl = t.ImageUrl
            };
        }

        public async Task<bool> CreateAsync(TeamCreateDto dto)
        {
            var team = new Team
            {
                TeamId = Guid.NewGuid().ToString(),
                Name = dto.Name,
                BaseCountry = dto.BaseCountry,
                FounderYear = dto.FounderYear,
                Description = dto.Description,
                Budget = dto.Budget,
                ImageUrl = dto.ImageUrl
            };

            _context.Teams.Add(team);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TeamUpdateDto dto)
        {
            var team = await _context.Teams.FindAsync(dto.TeamId);
            if (team == null) return false;

            team.Name = dto.Name;
            team.BaseCountry = dto.BaseCountry;
            team.FounderYear = dto.FounderYear;
            team.Description = dto.Description;
            team.Budget = dto.Budget;
            team.ImageUrl = dto.ImageUrl;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return false;

            _context.Teams.Remove(team);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}

