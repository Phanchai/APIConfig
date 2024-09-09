using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIConfig.Class;
using APIConfig.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIConfig.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly ProjectManager2Context _context;

        public OptionsController(ProjectManager2Context context)
        {
            _context = context;
        }
        [HttpGet("team/select/{TeamCode}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTeamOnCode(int TeamCode){
            var team = await _context.RdTeams
                            .Where(r => r.TeamCode == TeamCode)
                            .Select(r => new {
                               TeamName =  r.TeamName,
                               TeamCode = r.TeamCode
                            })
                            .FirstOrDefaultAsync();
            return Ok(team);
        }

        [HttpGet("draft/select/{DraftmanCode}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDraftmanOnCode(int DraftmanCode){
            var draft = await _context.RdDraftmen
            .Where(r => r.DraftmanCode == DraftmanCode)
                .Select(r => new{
               DraftmanName = r.DraftmanName,
               DraftmanCode = r.DraftmanCode
            }).FirstOrDefaultAsync();
            return Ok(draft);
        }
        [HttpGet("project/select/{ProjectCode}")]
        public async Task<ActionResult<IEnumerable<object>>> GetPorjectOnCode(int ProjectCode){
            var project = await _context.RdHeadProjects
            .Where(r => r.CodeProject == ProjectCode)
            .Select(r => new{
                CodeProject = r.CodeProject,
                ProjectName = r.ProjectName,
                ProjectOwner = r.ProjectOwner,
                Deskip = r.Deskip,
                DateGet = r.DateGet
            })
            .FirstOrDefaultAsync();
            return Ok(project);
        }

        [HttpGet("model/seleted/{model}")]
        public async Task<ActionResult<IEnumerable<object>>> GetModelOnSelect(string model){
            var result = await _context.RdCapStandards
                        .Where(a => a.CapName == model)
                        .Select(a => new{
                                CapName = a.CapName,
                                CapStandard = a.CapStandard,
                                CapCopy = a.CapCopy
                        }).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<object>>> GetBrand()
        {
            var result = await _context.RdBrands
                        .Select(a => new
                        {
                            BrandNumber = a.BrandNumber,
                            NameBrand = a.NameBrand,
                            InitialsBrand = a.InitialsBrand
                        }).OrderByDescending(a => a.BrandNumber).ToListAsync();
            return Ok(result);
        }
        
         [HttpGet("brand/lastnumber")]
        public async Task<ActionResult<IEnumerable<object>>> GetLastBrand()
        {
            var result = await _context.RdBrands.OrderByDescending(a => a.BrandNumber).FirstAsync();
            return Ok(result);
        }
        
        [HttpGet("project")]
        public async Task<ActionResult<IEnumerable<object>>> GetProject(){
            var result  = await _context.RdHeadProjects
                        .Select(a => new{
                            ProjectName = a.ProjectName,
                            CodeProject = a.CodeProject,
                            ProjectOwner = a.ProjectOwner,
                            Deskip = a.Deskip,
                            DateGet = a.DateGet
                        }).ToListAsync();
            
            return Ok(result);         
            }


        [HttpGet("model")]
        public async Task<ActionResult<IEnumerable<object>>> GetModel(){
            var result  = await _context.RdCapStandards
                        .Select(a => new{
                            CapId = a.CapId,
                            CapCode = a.CapCode,
                            CapName = a.CapName,
                            GroupCap = a.GroupCap,
                            CapStandard =  a.CapStandard,
                            CapCopy = a.CapCopy
                        }).ToListAsync();
            
            return Ok(result);         
            }
        
        [HttpGet("draftman")]
        public async Task<ActionResult<IEnumerable<object>>> GetDraftman(){
            var result  = await _context.RdDraftmen
                        .Select(a => new{
                                DraftmanCode = a.DraftmanCode,
                                DraftmanName = a.DraftmanName,
                                TeamCode = a.TeamCode,
                        }).ToListAsync();
            
            return Ok(result);         
            }

        [HttpGet("month/{monthInt}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTotalHouse(int monthInt){
            var result = await _context.RdMonths
                .Where(rm => rm.MonthCount == monthInt)
                .OrderByDescending(a => a.MonthCount)
                .Select(a => new{
                   MonthName = a.MonthName,
                   MonthCount = a.MonthCount,
                   CapInMonth = a.CapInMonth
                })
                .SingleOrDefaultAsync();

            return Ok(result);
        }

        [HttpGet("month")]
        public async Task<ActionResult<IEnumerable<object>>> GetMonth(){
            var result = await _context.RdMonths
                .Select(a => new{
                   MonthName = a.MonthName,
                   MonthCount = a.MonthCount,
                   CapInMonth = a.CapInMonth
                })
                .ToListAsync();

            return Ok(result);
        }


        [HttpGet("type/selected/{teamCode}/{draftmanCode}/{projectCode}/{brands}/{capName}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTypes(int teamCode,int draftmanCode,int projectCode,string brands,string capName){
           
           try{
                        var result = await _context.RdActualPlans
                .Join(_context.RdActualPlanOptions,
                      rdap => rdap.Article,
                      rdpo => rdpo.Article,
                      (rdap,rdpo) => new {rdap,rdpo})
                .Where(rdap => rdap.rdap.TeamCode == teamCode && 
                       rdap.rdap.DraftmanCode == draftmanCode && 
                       rdap.rdap.CodeProject == projectCode &&
                       rdap.rdap.InitialsBrand == brands && 
                       rdap.rdap.CapName == capName
                       )
                .Select(a => new{
                        Article = a.rdap.Article,
                        TeamName = a.rdap.TeamName,
                        DraftmanCode = a.rdap.DraftmanCode,
                        DraftmanName = a.rdap.DraftmanName,
                        OptionsSelect = a.rdap.OptionsSelect,
                        CapName = a.rdap.CapName

                }).FirstOrDefaultAsync();
                
                if(result == null){
                    return NotFound(new { Message = "No data found for the specified month." });
                }
                else{
                    return Ok(result);
                }
            
           }
           catch(Exception ex)
           {
            return BadRequest(ex.Message);
           }
        }
    }
}

