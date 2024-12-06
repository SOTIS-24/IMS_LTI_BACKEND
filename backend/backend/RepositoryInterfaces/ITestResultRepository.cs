﻿using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface ITestResultRepository : IRepository<TestResult>
    {
        public TestResult? GetByUserAndTest(string username, long testId);
    }
}