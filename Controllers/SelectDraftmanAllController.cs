using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConfig.Models;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Authorization;



namespace APIConfig.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize ]
    public class SelectDraftmanAllController : Controller
    {

        private readonly ProjectManager2Context _context;

        public SelectDraftmanAllController(ProjectManager2Context context)
        {
            _context = context;
        }

        /// Draftman
        [HttpGet("/draftmans")]
        public async Task<ActionResult<IEnumerable<object>>> GetRdDraftman()
        {
            var result = await _context.RdDraftmen
                .Join(_context.RdTeams,
                    draftman => draftman.TeamCode,
                    team => team.TeamCode,
                    (draftman, team) => new
                    {
                        TeamCode = team.TeamCode,
                        TeamName = team.TeamName,
                        DraftmanCode = draftman.DraftmanCode,
                        DraftmanName = draftman.DraftmanName
                    })
                .OrderBy(d => d.TeamCode)
                .ThenBy(d => d.DraftmanCode)
                .ToListAsync();

            return Ok(result);
        }
        [HttpGet("draftman/selected/{draftmanCode}")]
        public async Task<ActionResult<object>> GetByDraftmanCode(int draftmanCode)
        {
            var result = await _context.RdDraftmen
                .Where(d => d.DraftmanCode == draftmanCode)
                .Join(_context.RdTeams,
                    draftman => draftman.TeamCode,
                    team => team.TeamCode,
                    (draftman, team) => new
                    {
                        TeamCode = team.TeamCode,
                        TeamName = team.TeamName,
                        DraftmanCode = draftman.DraftmanCode,
                        DraftmanName = draftman.DraftmanName
                    })
                .FirstOrDefaultAsync(); // ใช้ FirstOrDefaultAsync เพื่อคืนค่าหนึ่งรายการหรือ null

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpGet("draftman/selected/lastday/{draftmanCode}")]
        public async Task<ActionResult<object>> GetLastDayOnDraftman(int draftmanCode)
        {
            var result = await _context.RdActualPlanOptions
                .Where(options => options.DraftmanCode == draftmanCode)
                .OrderByDescending(options => options.FinishDate)
                .Select(ops => new
                {
                    LastDay = ops.FinishDate
                }).FirstAsync();

            return Ok(result);
        }

        [HttpGet("draftman/selected/total/{draftmanCode}")]
        public async Task<ActionResult<object>> GetTotalHourOnDraftman(int draftmanCode){
        var result  = await _context.RdActualPlans
                .Join(_context.RdActualPlanOptions,
                      rdap => rdap.Article,
                      rdpo => rdpo.Article,
                      (rdap,rdpo) => new {rdap,rdpo})
                      .Where(rdap => rdap.rdpo.DraftmanCode == draftmanCode)
                      
                .Join(_context.RdMonths,
                    combined => combined.rdap.MonthCount,
                    rdm => rdm.MonthCount,
                    (combined,rdm) => new {combined.rdap,combined.rdpo,rdm})
                       
                .GroupBy(x => x.rdm.CapInMonth)
                .Select(g => new {
                           InMonth = g.Key,
                           RemainingHours = g.Key - g.Sum(x => x.rdap.CapHour),
                           TotalHours = g.Sum(x => x.rdap.CapHour),
                           TotalJobs = g.Sum(x => x.rdap.CapQty),
                           FinishedJobs = g.Sum(x => x.rdpo.JobFinish),
                           PendingJobs = g.Sum(x => x.rdap.CapQty) - g.Sum(x => x.rdpo.JobFinish),
                           ProjectTotal = g.Select(x => x.rdap.CodeProject).Distinct().Count(),
                           ModelNameTotal = g.Select(x => x.rdap.CapName).Distinct().Count(),
                           Brand = g.Select(x => x.rdpo.InitialsBrand).Distinct().Count(),
                           CountDrafr = g.Select(x => x.rdpo.DraftmanName).Distinct().Count(),
                           AllType = g.Select(x => x.rdap.CapType).Distinct().Count()
                }).FirstOrDefaultAsync();
            return Ok(result);
        }

        /// Team
        [HttpGet("team/selected/{teamCode}")]
        public async Task<ActionResult<object>> GetByTeamCode(int teamCode)
        {
            var result = await _context.RdTeams
                .Where(t => t.TeamCode == teamCode)
                .FirstOrDefaultAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("team")]
        public async Task<ActionResult<object>> GetTeam()
        {
            var result = await _context.RdTeams
                        .Select(team => new {
                            TeamCode = team.TeamCode,
                            TeamName = team.TeamName,
                            BossName = team.BossName,
                        }).ToListAsync();
            return Ok(result);
        }
        [HttpGet("ListReports")]
        public async Task<ActionResult<object>> GetReportName()
        {
            var result = await _context.RdListReports
                .Select(listReport => new {
                        NameReports = listReport.NameReports,
                        DateCurrent = listReport.DateCurrent.ToString(),
                        SsrsReports = listReport.SsrsReports
                }).ToListAsync();
            return Ok(result);
        }
        [HttpGet("draftman")]
        public async Task<ActionResult<object>> GetDraft()
        {
            var result = await _context.RdDraftmen
                        
                        .Select(draft => new {
                            TeamCode = draft.TeamCode,
                            DraftmanCode = draft.DraftmanCode,
                            DraftmanName = draft.DraftmanName
                        }).ToListAsync();
            return Ok(result);
        }
        [HttpGet("draftman/calendaronly")]
        public async Task<ActionResult<object>> GetForCalendar(){
            var result = await _context.RdActualPlanOptions
                .Join(_context.RdActualPlans,
                    options => options.Article,
                    plan => plan.Article,
                    (options, plan) => new {
                        Article = plan.Article,
                        StartDate = options.StartDate,
                        FirstDate = options.FirstDate,
                        FinishDate = options.FinishDate,
                        CapName = plan.CapName,
                        DraftmanName = plan.DraftmanName,
                        DraftmanCode = plan.DraftmanCode,
                        CodeProject = plan.CodeProject,
                        ProjectName = plan.ProjectName,
                        OptionsSelect = plan.OptionsSelect
                    })
                .Select(result => new {
                        result.Article,
                        result.StartDate,
                        result.FirstDate,
                        result.FinishDate,
                        result.CapName,
                        result.DraftmanName,
                        result.DraftmanCode,
                        result.CodeProject,
                        result.ProjectName,
                        result.OptionsSelect
                    })
                .ToListAsync();
                return Ok(result); 
        }

        [HttpGet("labelhead/month/{month}")]
        public async Task<ActionResult<object>> GetCapMonth(int month){

            try{
                var result = await _context.RdMonths.Where(a => a.MonthCount == month)
                    .Select(a => new RdMonth{
                        MonthName = a.MonthName,
                        CapInMonth = a.CapInMonth
                    })
                    .FirstOrDefaultAsync();


                if (result == null)
                {
                    return NotFound(new { Message = "NULL" });
                }

                return Ok(result);

            }
            catch(Exception ex)
            { 
                return BadRequest(ex.ToString());
            }           
        }
        [HttpGet("labelhead/{year}")]
        public async Task<ActionResult<object>> GetLabelOnYear(int year){

            try{
                var result = await _context.RdActualPlans
                    .Join(_context.RdActualPlanOptions,
                        rdap => rdap.Article,
                        rdpo => rdpo.Article,
                        (rdap, rdpo) => new { rdap, rdpo })
                    .Where(a => a.rdpo.FirstDate.Year == year)
                    .GroupBy(_ => 0) // Group by an identifier, assuming 'Article' is unique enough for grouping
                    .Select(g => new
                    {
                        ProjectTotal = g.Select(x => x.rdap.ProjectName).Distinct().Count(),
                        CountDrafr = g.Select(x => x.rdap.DraftmanName).Distinct().Count(),
                        Brand = g.Select(x => x.rdap.InitialsBrand).Distinct().Count(),
                        ModelNameTotal = g.Select(x => x.rdap.CapName).Distinct().Count(),
                        AllType = g.Select(x => x.rdap.CapType).Distinct().Count(),
                        TotalJobs = g.Sum(x => x.rdap.CapQty),
                        FinishedJobs = g.Sum(x => x.rdpo.JobFinish),
                        PendingJobs = g.Sum(x => x.rdpo.JobNot)
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return NotFound(new { Message = "NULL" });
                }

                return Ok(result);

            }
            catch(Exception ex)
            { 
                return BadRequest(ex.ToString());
            }           
        }

        [HttpGet("cardlable/calendar/{month}/{draftmanCode}")]
        public async Task<ActionResult<object>> GettoCard(int month ,int draftmanCode){

            try{
                     var result  = await _context.RdActualPlans
                .Join(_context.RdActualPlanOptions,
                      rdap => rdap.Article,
                      rdpo => rdpo.Article,
                      (rdap,rdpo) => new {rdap,rdpo}).Where(x=> x.rdap.DraftmanCode == draftmanCode)
                .Join(_context.RdMonths,
                    combined => combined.rdap.MonthCount,
                    rdm => rdm.MonthCount,
                    (combined,rdm) => new {combined.rdap,combined.rdpo,rdm}
                    ).Where(x => x.rdm.MonthCount == month)
                .GroupBy(x => x.rdm.CapInMonth)
                .Select(g => new {
                           InMonth = g.Key,
                           RemainingHours = g.Key - g.Sum(x => x.rdap.CapHour),
                           TotalHours = g.Sum(x => x.rdap.CapHour),
                           TotalJobs = g.Sum(x => x.rdap.CapQty),
                           FinishedJobs = g.Sum(x => x.rdpo.JobFinish),
                           PendingJobs = g.Sum(x => x.rdap.CapQty) - g.Sum(x => x.rdpo.JobFinish),
                           ProjectTotal = g.Select(x => x.rdap.CodeProject).Distinct().Count(),
                           ModelNameTotal = g.Select(x => x.rdap.CapName).Distinct().Count(),

                }).FirstOrDefaultAsync();

                if(result == null){
                    return NotFound(new { Message = "No data found for the specified month." });
                }
  
                return Ok(result);
            }
            catch(Exception ex)
            { 
                return BadRequest(ex.ToString());
            }           
        }
    }
}

