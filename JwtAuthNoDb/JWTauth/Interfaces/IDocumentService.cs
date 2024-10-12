using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using JWTauth.Models;

namespace JWTauth.Interfaces
{
    public interface IDocumentService
    {
        public Task<List<DocumentTitle>> GetDocumentTitleAsync();
        public Task<DocumentInfo> GetDocumentInformationByIdAsync(GetDocumentInfoByIdRequest request);
    }
}