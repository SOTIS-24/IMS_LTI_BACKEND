﻿using backend.Dtos;

namespace backend.IServices
{
    public interface ITestService
    {
        public List<TestDto> GetAll();
    }
}