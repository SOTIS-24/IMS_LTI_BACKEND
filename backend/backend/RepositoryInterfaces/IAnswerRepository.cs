namespace backend.RepositoryInterfaces
{
    public interface IAnswerRepository
    {
        public void DeleteByQuestionsId(List<long> ids);
    }
}
