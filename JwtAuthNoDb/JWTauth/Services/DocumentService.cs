using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTauth.Interfaces;
using JWTauth.Models;

namespace JWTauth.Services
{
    public class DocumentService : IDocumentService
    {
        private static readonly List<DocumentInfo> documentInfos = new List<DocumentInfo>
        {
            new DocumentInfo(){DocumentId = 1, Title = "Title 1", Description = "Description 1", OwnerName = "Owner 1"},
            new DocumentInfo(){DocumentId = 2, Title = "Title 2", Description = "Description 2", OwnerName = "Owner 2"},
            new DocumentInfo(){DocumentId = 3, Title = "Title 3", Description = " Description 3", OwnerName = "Owner 3"}
            
        };

        public Task<List<DocumentTitle>> GetDocumentTitleAsync()
        {
            var documentTitleList = documentInfos.Select(document => GenerateDocumentTitleForlist(document)).ToList();
            return Task.FromResult(documentTitleList);
        }

        public Task<DocumentInfo> GetDocumentInformationByIdAsync(GetDocumentInfoByIdRequest request)
        {
            var loadedDocumentInformation = documentInfos.FirstOrDefault(p => p.DocumentId == request.DocumentId);
            return Task.FromResult(loadedDocumentInformation);
        }

        private static DocumentTitle GenerateDocumentTitleForlist(DocumentInfo document)
        {
            return new DocumentTitle{DocumentId = document.DocumentId, Title = document.Title};
        }
    }
}