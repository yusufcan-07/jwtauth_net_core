using System.Collections.Generic;
using System.Threading.Tasks;
using JWTauth.Interfaces;
using JWTauth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController: ControllerBase
    {
        readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpPost("GetDocumentTitles")]
        [AllowAnonymous]
        public async Task<ActionResult<List<DocumentTitle>>> GetBookTitles()
        {
            var result = await documentService.GetDocumentTitleAsync();

            return result;
        }

        [HttpPost("GetDocumentInformationById")]
        [Authorize]
        public async Task<ActionResult<DocumentInfo>> GetDocumentInformationById(
            [FromBody] GetDocumentInfoByIdRequest request)
        {
            var result = await documentService.GetDocumentInformationByIdAsync(request);

            return result;

        }

    }
}