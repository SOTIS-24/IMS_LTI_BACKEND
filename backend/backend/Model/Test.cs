﻿namespace backend.Model
{
    public class Test: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }
}
