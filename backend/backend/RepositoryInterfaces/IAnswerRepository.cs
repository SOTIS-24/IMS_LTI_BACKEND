using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface IAnswerRepository
    {
        public void DeleteByQuestionsId(List<long> ids);
        public List<Answer> GetByIds(List<long> ids);
    }
}
