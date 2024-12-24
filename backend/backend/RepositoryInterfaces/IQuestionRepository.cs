using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface IQuestionRepository
    {
        public void DeleteByTestId(long id);
        public Question? GetById(long id);
    }
}
