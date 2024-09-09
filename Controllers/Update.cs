using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using APIConfig.Models;
using APIConfig.Models.dtoModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace APIConfig.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Update : ControllerBase
    {
       private readonly ProjectManager2Context _context;

        public Update(ProjectManager2Context context)
        {
            _context = context;
        }


        [HttpPut("update/job/{id:int}")] 
        public async Task<IActionResult> UpdateRdActiual(int id ,[FromBody] RdActualPlanDto rdActualPlanDto)
        {
            try{
                if(id != rdActualPlanDto.Article){
                    return BadRequest("ID mismacth");
                }

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
                            JobFinish = rdActualPlanDto.JobFinish,
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
                                JobFinish = rdActualPlanDto.JobFinish,
                        };
                        
                         _context.RdHistoryEdits.Add(rdActualHistory);
                        _context.RdActualPlans.Update(rdActualPlan);
                        _context.RdActualPlanOptions.Update(rdActualPlanOtpins);
                       
                       // _context.RdHistoryEdits.Add(rdActualHistory);


                        await _context.SaveChangesAsync();

                        return Ok("Data inserted successfully.");
                    }
                    else
                    {
                        return BadRequest("Value is NULL");
                    }
            }
            catch(Exception){
                return StatusCode(StatusCodes.Status500InternalServerError,"ERROR Update Data");
            }
        }

        [HttpPut("update/project/{id:int}")]
        public async Task<IActionResult> UpdateProject(int id,[FromBody] RdModelHeadDto rdHeadProject){
            try{
                if(id != rdHeadProject.CodeProject){
                    return BadRequest("NULL");
                }

                if(rdHeadProject != null){

                    var result = new RdHeadProject{

                        CodeProject = rdHeadProject.CodeProject,
                        ProjectName = rdHeadProject.ProjectName,
                        ProjectOwner = rdHeadProject.ProjectOwner,
                    };

                    _context.RdHeadProjects.Update(result);
                    await _context.SaveChangesAsync();
                    return Ok("Data Update");
                }
                else{
                    return BadRequest("NULL");
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"ERROR Update Data");
            }
        }

        [HttpPut("update/brand/{id:int}")]
        public async Task<IActionResult> UpdateBrand(int id,[FromBody] RdBrand rdBrand){
            try{
                if(id != rdBrand.BrandNumber){
                    return BadRequest("NULL");
                }

                if(rdBrand != null){
                    var result = new RdBrand{
                        BrandNumber = rdBrand.BrandNumber,
                        NameBrand = rdBrand.NameBrand,
                        InitialsBrand = rdBrand.InitialsBrand,
                    };

                    _context.RdBrands.Update(result);
                    await _context.SaveChangesAsync();
                    return Ok("Data Update");
                }
                else{
                    return BadRequest("NULL");
                }
            }
            catch(Exception){
                 return StatusCode(StatusCodes.Status500InternalServerError,"ERROR Update Data");
            }
        }

        [HttpPut("update/model/{id:int}")]
        public async Task<IActionResult> UpdateModel(int id,[FromBody] RdCapStandard rdModel){
            try{
                if(id != rdModel.CapId)
                {
                    return BadRequest("NULL");
                }

                if(rdModel != null)
                {
                    var model = new RdCapStandard{
                        CapId = rdModel.CapId,
                        CapCode = rdModel.CapCode,
                        CapName = rdModel.CapName,
                        GroupCap = rdModel.GroupCap,
                        CapStandard = rdModel.CapStandard,
                        CapCopy = rdModel.CapCopy,
                    };

                    _context.RdCapStandards.Update(model);
                    await _context.SaveChangesAsync();
                    return Ok("Data Update");
                }
                else{
                    return BadRequest("NULLใน");
                }
            }
            catch(Exception){
                return StatusCode(StatusCodes.Status500InternalServerError,"ERROR Update Data");
            }
        }
        [HttpPut("update/inmoth/{id:int}")]
        public async Task<IActionResult> UpdateCapInMonth(int id,[FromBody] RdMonth rdMonth){
            try{
                if(id != rdMonth.MonthCount){
                    return NotFound("NULL");
                }
                if(rdMonth != null){

                    var model = new RdMonth{
                        MonthCount = rdMonth.MonthCount,
                        CapInMonth = rdMonth.CapInMonth,
                        MonthName = rdMonth.MonthName,
                    };
                    _context.RdMonths.Update(model);
                    await _context.SaveChangesAsync();
                    return Ok("Update Success");
                }
                else{
                    return BadRequest("Null");
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,"ERROR");
            }
        }

        [HttpPut("update/draft/{id:int}")]
        public async Task<IActionResult> UpdateDraft(int id,[FromBody] RdDraftman rdDraft){
            try{
                if(id != rdDraft.DraftmanCode){
                    return BadRequest("NULL");
                }
                

                if(rdDraft != null){

                    var model = new RdDraftman{
                        DraftmanCode = rdDraft.DraftmanCode,
                        DraftmanName = rdDraft.DraftmanName,
                        TeamCode = rdDraft.TeamCode,
                        IdDraftMan = rdDraft.IdDraftMan,
                        DraftmanImage = rdDraft.DraftmanImage,
                    };

                    _context.RdDraftmen.Update(model);
                    await _context.SaveChangesAsync();
                    return Ok(model);

                }
                else{
                    return StatusCode(StatusCodes.Status500InternalServerError,"ERROR Update Data");
                }

            }
            catch(Exception){
                return StatusCode(StatusCodes.Status500InternalServerError,"ERROR Update Data");
            }
        }
     }
}