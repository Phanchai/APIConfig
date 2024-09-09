using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using APIConfig.Class;
using APIConfig.Models;
using APIConfig.Models.dtoModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace APIConfig.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Insert : Controller
    {
        private readonly ProjectManager2Context _context;

        public Insert(ProjectManager2Context context)
        {
            _context = context;
        }

        [HttpPost("insert/history")]
        public async Task<IActionResult> InsertRdHistory([FromBody] RdActualPlanDto rdActualPlanDto){
            try{

                    DateTime dateConvert = DateTime.Parse(rdActualPlanDto.DateTimeRec);
                        var rdActualHistory = new RdHistoryEdit
                        {
                                Article = rdActualPlanDto.Article,
                                CapQty = rdActualPlanDto.CapQty,
                                CapName = rdActualPlanDto.CapName,
                                CapType = rdActualPlanDto.CapType,
                                CapHour = rdActualPlanDto.CapHour, 
                                InitialsBrand = rdActualPlanDto.InitialsBrand,
                                CodeProject = rdActualPlanDto.CodeProject,
                                ProjectName = rdActualPlanDto.ProjectName,
                                DraftmanCode = rdActualPlanDto.DraftmanCode,
                                DraftmanName = rdActualPlanDto.DraftmanName,
                                TypeNames = rdActualPlanDto.TypeNames,
                                OptionsSelect = rdActualPlanDto.OptionsSelect,
                                DateSave = rdActualPlanDto.DateSave,
                                DtimeSave = rdActualPlanDto.DtimeSave,
                                JobNot = rdActualPlanDto.JobNot,
                                MovementGroup = rdActualPlanDto.MovementGroup,
                                JobFinish = rdActualPlanDto.JobFinish,
                        };
                _context.Add(rdActualHistory);
                await _context.SaveChangesAsync();
                return Ok(rdActualHistory);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insert/job")]
        public async Task<IActionResult> InsertRdArticle([FromBody] RdActualPlanDto rdActualPlanDto)
        {
            try{

                    if(rdActualPlanDto != null)
                    {
                        var rdActualPlan = new RdActualPlan{
                            Article = rdActualPlanDto.Article,
                            CodeProject = rdActualPlanDto.CodeProject,
                            ProjectName = rdActualPlanDto.ProjectName,
                            InitialsBrand = rdActualPlanDto.InitialsBrand,
                            TypeNames = rdActualPlanDto.TypeNames,
                            Remark = rdActualPlanDto.Remark,
                            DraftmanName =rdActualPlanDto.DraftmanName,
                            DraftmanCode = rdActualPlanDto.DraftmanCode,
                            TeamName = rdActualPlanDto.TeamName,
                            TeamCode = rdActualPlanDto.TeamCode,
                            CapName = rdActualPlanDto.CapName,
                            CapType =  rdActualPlanDto.CapType,
                            CapQty = rdActualPlanDto.CapQty,
                            CapHour = rdActualPlanDto.CapHour,
                            MonthCount = rdActualPlanDto.MonthCount,
                            OptionsSelect = rdActualPlanDto.OptionsSelect,
                        };

                        var rdActualPlanOtpins = new RdActualPlanOption{
                            Article = rdActualPlanDto.Article,                
                            CodeProject = rdActualPlanDto.CodeProject,
                            ProjectName = rdActualPlanDto.ProjectName,
                            InitialsBrand = rdActualPlanDto.InitialsBrand,
                            JobNot = rdActualPlanDto.JobNot,
                            JobFinish = 0,
                            CapName = rdActualPlanDto.CapName,
                            DraftmanCode = rdActualPlanDto.DraftmanCode,
                            DraftmanName = rdActualPlanDto.DraftmanName,
                            ApproveDate = rdActualPlanDto.ApproveDate,
                            StartDate = rdActualPlanDto.StartDate,
                            FirstDate = rdActualPlanDto.FirstDate,
                            FinishDate = rdActualPlanDto.FinishDate,
                            CapCopy = rdActualPlanDto.CapCopy,
                            CapStandard = rdActualPlanDto.CapStandard,
                            DateTimeRec = rdActualPlanDto.DateTimeRec,
                            MovementGroup = rdActualPlanDto.MovementGroup,
                            OptionsSelect = rdActualPlanDto.OptionsSelect,
                            Missed = rdActualPlanDto.Missed,
                            OverTime = rdActualPlanDto.OverTime,
                        };

                        var rdActualHistory = new RdHistoryEdit
                        {
                                Article = rdActualPlanDto.Article,
                                CapQty = rdActualPlanDto.CapQty,
                                CapName = rdActualPlanDto.CapName,
                                CapType = rdActualPlanDto.CapType,
                                CapHour = rdActualPlanDto.CapHour,
                                InitialsBrand = rdActualPlanDto.InitialsBrand,
                                CodeProject = rdActualPlanDto.CodeProject,
                                ProjectName = rdActualPlanDto.ProjectName,
                                DraftmanCode = rdActualPlanDto.DraftmanCode,
                                DraftmanName = rdActualPlanDto.DraftmanName,
                                TypeNames = rdActualPlanDto.TypeNames,
                                OptionsSelect = rdActualPlanDto.OptionsSelect,
                                DateSave = rdActualPlanDto.FirstDate.Date,
                                DtimeSave = rdActualPlanDto.StartDate,
                                JobNot = rdActualPlanDto.JobNot,
                                MovementGroup = rdActualPlanDto.MovementGroup,
                                JobFinish = 0
                        };


                        _context.RdActualPlans.Add(rdActualPlan);
                        _context.RdActualPlanOptions.Add(rdActualPlanOtpins);
                        _context.RdHistoryEdits.Add(rdActualHistory);
                        await _context.SaveChangesAsync();

                        return Ok("Data inserted successfully.");
                    }
                    else
                    {
                        return BadRequest("Value is NULL");
                    }

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    
        [HttpPost("insert/project")]
        public async Task<IActionResult> InsertProject([FromBody] RdModelHeadDto rdHeadProject){
            try{
                if(rdHeadProject != null){
                                        var project = new  RdHeadProject{
                        CodeProject = rdHeadProject.CodeProject,
                        ProjectName = rdHeadProject.ProjectName,
                        ProjectOwner = rdHeadProject.ProjectOwner,
                        Deskip = rdHeadProject.Deskip,
                        DateGet = rdHeadProject.DateGet,
                    };

                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    return Ok(rdHeadProject);
                }
                else{
                    return BadRequest("NULL");
                }
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("insert/brand")]
        public async Task<IActionResult> InsertBrand([FromBody] RdModelHeadDto rdBrand)
        {
            try{
                if (rdBrand != null){
                    var result = new RdBrand{
                        BrandNumber = rdBrand.BrandNumber,
                        NameBrand = rdBrand.NameBrand,
                        InitialsBrand = rdBrand.InitialsBrand
                    };

                    _context.Add(result);
                    await _context.SaveChangesAsync();
                    return Ok(rdBrand);
                }
                else{
                    return BadRequest();
                }
            }
            catch(Exception ex){
                return BadRequest(ex.ToString());
            }
        }
    
        [HttpPost("insert/model")]
        public async Task<IActionResult> InsertModel([FromBody] RdModelHeadDto rdModel){
            try
            {
                if(rdModel != null){
                    var result = new RdCapStandard{
                        CapId = rdModel.CapId,
                        CapCode = rdModel.CapCode,
                        CapName = rdModel.CapName,
                        GroupCap = rdModel.GroupCap,
                        CapCopy = rdModel.CapCopy,
                        CapStandard = rdModel.CapStandard,
                    };
                    _context.Add(result);
                    await _context.SaveChangesAsync();
                    return Ok(rdModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("insert/draft")]
        public async Task<IActionResult> InsertDraft([FromBody] RdModelHeadDto rdDraft)
        {
            try
            {
                if(rdDraft != null){
                    var result = new RdDraftman{
                        DraftmanCode = rdDraft.DraftmanCode,
                        DraftmanName = rdDraft.DraftmanName,
                         TeamCode = rdDraft.TeamCode,
                    };

                    _context.Add(result);
                    await _context.SaveChangesAsync();
                    return Ok(rdDraft);
                }
                else{ return BadRequest(); }
                
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }

}

