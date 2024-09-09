using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using APIConfig.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace APIConfig.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Delete : ControllerBase
    {
        private readonly ProjectManager2Context _context;

        public Delete(ProjectManager2Context context)
        {
            _context = context;
        }

        [HttpDelete("delete/article/{id}")]
        public async Task<IActionResult> DeleteRdActual(int id)
        {
            try
            {
                var rdActualPlan = await _context.RdActualPlans.Where(a => a.Article == id).FirstAsync();
                var rdActualPlanOtpins = await _context.RdActualPlanOptions.Where(a => a.Article == id).FirstAsync();
                var RdHistoryEdit = await _context.RdHistoryEdits.Where(a => a.Article == id).ToListAsync();
                _context.RdActualPlans.Remove(rdActualPlan);
                _context.RdActualPlanOptions.Remove(rdActualPlanOtpins);
                _context.RdHistoryEdits.RemoveRange(RdHistoryEdit);

                await _context.SaveChangesAsync();
                return Ok("Record deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete/project/{id}")]
        public async Task<IActionResult> DeleteProject(int id){
            try
            {
                var rdProject = await _context.RdHeadProjects.Where(a => a.CodeProject == id).FirstAsync();
                _context.RdHeadProjects.Remove(rdProject);
                await _context.SaveChangesAsync();
                return Ok("Record deleted successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/brand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id){
            try
            {
                var rdBrand = await _context.RdBrands.Where(a => a.BrandNumber == id).FirstAsync();
                _context.RdBrands.Remove(rdBrand);
                await _context.SaveChangesAsync();
                return Ok("Record deleted successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/model/{id}")]
        public async Task<IActionResult> DeleteModel(int id){
            try
            {
                var rdModel = await _context.RdCapStandards.Where(a => a.CapId == id).FirstAsync();
                _context.RdCapStandards.Remove(rdModel);
                await _context.SaveChangesAsync();
                return Ok("Record deleted successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());               
            }
        }

        [HttpDelete("delete/draft/{id}")]
        public async Task<IActionResult> DeleteDraftman(int id){
            try{
                var rdDraftModel = await _context.RdDraftmen.Where(a => a.DraftmanCode == id).FirstAsync();
                _context.RdDraftmen.Remove(rdDraftModel);
                await _context.SaveChangesAsync();
                return Ok("Record deleted successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}