using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConfig.Models;
using System.Globalization;
using APIConfig.Models.dtoModel;
using Microsoft.AspNetCore.Authorization;



namespace APIConfig.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RdActualController : Controller
    {

        private readonly ProjectManager2Context _context;

        public RdActualController(ProjectManager2Context context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet("articleitem")]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            var result = await (from plan in _context.RdActualPlans
                                join options in _context.RdActualPlanOptions
                                on plan.Article equals options.Article
                                orderby plan.Article descending
                                select new
                                    {
                                        Article = plan.Article,
                                        ProjectName = plan.ProjectName,
                                        TeamName = plan.TeamName,
                                        DraftmanName = plan.DraftmanName,
                                        CapName = plan.CapName,
                                        CapQty = plan.CapQty,
                                        OptionsSelect = options.OptionsSelect,
                                        StartDate = options.StartDate,
                                        FirstDate = options.FirstDate,
                                        FinishDate = options.FinishDate,
                                        ApproveDate = options.ApproveDate
                                    }).ToListAsync();
                                    return Ok(result);
        }

        [HttpGet("articleitem/calendar")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllForCalendar(){
            var result = await _context.RdActualPlans
            .Join(_context.RdActualPlanOptions,
                    rap => rap.Article,
                    rapo => rapo.Article,
                    (rap,rapo) => new {
                        rap.Article,
                        rap.CodeProject,
                        rap.ProjectName,
                        rap.InitialsBrand,
                        rapo.JobNot,
                        rapo.JobFinish,
                        rapo.FirstDate,
                        rapo.FinishDate,
                        rap.CapName,
                        rapo.CapStandard,
                        rapo.CapCopy,
                        rap.CapHour,
                        rap.CapQty,
                        rap.TeamCode,
                        rap.TeamName,
                        rap.DraftmanCode,
                        rap.DraftmanName,
                        rapo.MovementGroup,
                        rapo.StartDate,
                        rapo.ApproveDate,
                        rapo.DateTimeRec,
                        rapo.OptionsSelect,
                        rapo.OverTime
                    }).ToListAsync();
            
            return Ok(result);
        }
        
        [HttpGet("articleitem/history/{articleid}")]
        public async Task<ActionResult<IEnumerable<object>>> GetHistoryOnitem(int articleid){
            var result = await _context.RdHistoryEdits
            .Where(r => r.Article == articleid)
            .Select(a => new RdHistoryEdit{
              Article = a.Article,
              CapQty = a.CapQty,
              CapName = a.CapName,
              CapType = a.CapType,
              CapHour = a.CapHour,
              Remark = a.Remark,
              CodeProject = a.CodeProject,
              ProjectName = a.ProjectName,
              InitialsBrand = a.InitialsBrand,
              TypeNames = a.TypeNames,
              Ot = a.Ot,
              DraftmanCode = a.DraftmanCode,
              DraftmanName = a.DraftmanName,
              OptionsSelect = a.OptionsSelect,
              DtimeSave =  a.DtimeSave,
              DateSave = a.DateSave,
              JobFinish = a.JobFinish,
              JobNot = a.JobNot,
              MovementGroup = a.MovementGroup,
              }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("articleitem/onarticle/{articleid}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllOnArticle(int articleid){
            var result = await _context.RdActualPlans
            .Where(a => a.Article == articleid )
            .Join(_context.RdActualPlanOptions,
                    rap => rap.Article,
                    rapo => rapo.Article,
                    (rap,rapo) => new {
                        rap.Article,
                        rap.CodeProject,
                        rap.ProjectName,
                        rap.InitialsBrand,
                        rapo.JobNot,
                        rapo.JobFinish,
                        rapo.FirstDate,
                        rapo.FinishDate,
                        rap.CapName,
                        rapo.CapStandard,
                        rapo.CapCopy,
                        rap.TeamCode,
                        rap.TeamName,
                        rap.CapType,
                        rap.CapQty,
                        rap.CapHour,
                        rap.TypeNames,
                        rap.DraftmanCode,
                        rap.DraftmanName,
                        rapo.MovementGroup,
                        rapo.StartDate,
                        rapo.ApproveDate,
                        rapo.DateTimeRec,
                        rapo.OptionsSelect,
                        rapo.OverTime
                    }).SingleOrDefaultAsync();
            
            return Ok(result);
        }

        [HttpGet("article/chart/{currentYear:int}")]
        public async Task<ActionResult<IEnumerable<object>>> GetForChart(int currentYear){
            var result = await _context.RdActualPlanOptions
                .Where(a => a.FirstDate.Year == currentYear)
                .GroupBy(a => a.FirstDate.Month)
                .Select(g => new{
                    MonthNum = g.Key,
                    NotFinish = g.Sum(a => a.JobNot),
                    Finish = g.Sum(a => a.JobFinish),
                    MontName = g.First().FirstDate.ToString("MMMM",CultureInfo.InvariantCulture)
                })
                .ToListAsync();
                return Ok(result);
        }
    
        [HttpGet("article/lastArticle")]
        public async Task<ActionResult<IEnumerable<object>>> GetLastArticle(){
            var result = await _context.RdActualPlans
                    .OrderByDescending(a => a.Article)
                    .FirstOrDefaultAsync();
                return Ok(result);
        }

        [HttpGet("article/calendar/{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetOnTeam(int id){
            var result = await _context.RdActualPlans
            .Where(a => a.TeamCode == id) 
            .Join(_context.RdActualPlanOptions,
                    rap => rap.Article,
                    rapo => rapo.Article,
                    (rap,rapo) => new {
                        rap.Article,
                        rap.CodeProject,
                        rap.ProjectName,
                        rap.InitialsBrand,
                        rapo.JobNot,
                        rapo.JobFinish,
                        rapo.FirstDate,
                        rapo.FinishDate,
                        rap.CapName,
                        rapo.CapStandard,
                        rapo.CapCopy,
                        rap.CapHour,
                        rap.CapQty,
                        rap.TeamCode,
                        rap.TeamName,
                        rap.DraftmanCode,
                        rap.DraftmanName,
                        rapo.MovementGroup,
                        rapo.StartDate,
                        rapo.ApproveDate,
                        rapo.DateTimeRec,
                        rapo.OptionsSelect,
                        rapo.OverTime
                    })
                    .ToListAsync();

                    return Ok(result);
        }
    
    }
}

