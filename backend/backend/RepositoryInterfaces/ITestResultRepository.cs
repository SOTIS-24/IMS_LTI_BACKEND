﻿using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface ITestResultRepository : IRepository<TestResult>
    {
        public TestResult? GetByUserAndTest(string username, long testId);
        public TestResult? GetForStudent(string username, long testId);
        public List<TestResult> GetByTestId(long testId);
        public List<string> GetStudentsByTestId(long testId);
    }
}
