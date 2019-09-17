
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;
using Yu.Core.Mvc;
using Yu.Data.Entities;
using Yu.Model.Common.InputModels;
using Yu.Data.Entities.WorkFlow;
using Yu.Service.WorkFlow.WorkFlowDefines;
using Yu.Model.WorkFlow.WorkFlowDefine.InputModels;

namespace Yu.Api.Areas.WorkFlow.Controllers
{
    [Route("api")]
    [Description("工作流定义")]
    public class WorkFlowDefineController : AuthorizeController
    {
        private readonly IWorkFlowDefineService _service;

        public WorkFlowDefineController(IWorkFlowDefineService service)
        {
            _service = service;
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        [HttpGet("workflowDefine")]
        [Description("取得工作流定义数据")]
        public IActionResult GetWorkFlowDefineById([FromQuery] IdQuery query)
        {
            var result = _service.GetWorkFlowById(query.Id);
            return Ok(result);
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        [HttpGet("workflowDefines")]
        [Description("取得工作流定义数据")]
        public IActionResult GetWorkFlowDefines([FromQuery] WorkflowDefineQuery query)
        {
            var result = _service.GetWorkFlowDefines(query.PageIndex, query.PageSize, query.TypeId);
            return Ok(result);
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        [HttpPost("workflowDefine")]
        [Description("添加工作流定义数据")]
        public async Task<IActionResult> AddWorkFlowDefine([FromBody]WorkFlowDefine entity)
        {
            await _service.AddWorkFlowDefineAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        [HttpPut("workflowDefine")]
        [Description("更新工作流定义数据")]
        public async Task<IActionResult> UpdateWorkFlowDefine([FromBody]WorkFlowDefine entity)
        {
            await _service.UpdateWorkFlowDefineAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        [HttpDelete("workflowDefine")]
        [Description("删除工作流定义数据")]
        public async Task<IActionResult> DeleteWorkFlowDefine([FromQuery]IdQuery query)
        {
            await _service.DeleteWorkFlowDefineAsync(query.Id);
            return Ok();
        }
	}
}
