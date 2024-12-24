﻿namespace backend.Model
{
    public class QuestionResult: Entity
    {
        public long Id { get; set; }
        public float Points { get; set; }
        public bool Passed { get; set; }
        public long QuestionId { get; set; }
        public List<long> AnswersIds { get; set; }
        public long TestResultId { get; set; }

    }
}
