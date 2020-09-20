using FamilyTree.Business;
using FamilyTree.Modules.FamilyTree.Repository;
using FamilyTree.Services.Repository;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FamilyTree.Modules.FamilyTree.Tests
{
    public class Repository
    {
        private HttpRepository<Business.FamilyTree> _httpRepository;

        public Repository()
        {
            _httpRepository = new FamilyTreeRepository(new System.Net.Http.HttpClient());
        }

        [Fact]
        public async void post_request_with_argument_null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.CreateAsync(null));
        }

        
    }
}
