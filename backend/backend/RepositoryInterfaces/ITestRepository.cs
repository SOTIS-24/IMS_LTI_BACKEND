using backend.Dtos;
using backend.Infrastructure;
using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface ITestRepository
    {
        public List<Test> GetAll();

        public Test GetById(long id);
    }
}
