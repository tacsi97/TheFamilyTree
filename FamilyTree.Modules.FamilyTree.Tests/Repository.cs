using FamilyTree.Business;
using FamilyTree.Modules.FamilyTree.Repository;
using FamilyTree.Services.Repository;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FamilyTree.Modules.FamilyTree.Tests
{
    //TODO: more integration tests when the server side is completed
    //TODO: if the repository fails in saving data remotely, than it must save locally
    public class Repository
    {
        //private HttpRepositoryBase<Business.FamilyTree> _httpRepository;

        //public Repository()
        //{
        //    _httpRepository = new FamilyTreeRepository("", new Token(), new System.Net.Http.HttpClient());
        //}

        //#region Exception throwing tests

        //[Fact]
        //public async void post_request_with_argument_null()
        //{
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.CreateAsync(null, null));
        //}

        //[Fact]
        //public async void post_request_first_argument_null()
        //{
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.CreateAsync(null, "notNull"));
        //}

        //[Fact]
        //public async void post_request_with_argument_empty()
        //{
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.CreateAsync("", ""));
        //}

        //[Fact]
        //public async void put_request_with_argument_null()
        //{
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.ModifyAsync(null, null));
        //}


        //[Fact]
        //public async void delete_request_first_argument_null()
        //{
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.DeleteAsync(null, -1));
        //}

        //[Fact]
        //public async void delete_request_id_lesser_than_zero()
        //{
        //    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _httpRepository.DeleteAsync("uri", -1));
        //}

        //[Fact]
        //public async void get_request_id_lesser_than_zero()
        //{
        //    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _httpRepository.GetAsync("uri", -1));
        //}

        //[Fact]
        //public async void get_request_with_argument_null()
        //{
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _httpRepository.GetAllAsync(null));
        //}

        //#endregion
    }
}
