﻿using backend.Dtos;
using backend.Infrastructure;
using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface ITestRepository
    {
        public List<Test> GetByCourseId(long courseId);

        public Test? GetById(long id);
        public Test Update(Test test);
        public List<Test> GetPublishedByCourseId(long courseId);
        public Test UpdateWithoutQuestions(Test test);
    }
}
