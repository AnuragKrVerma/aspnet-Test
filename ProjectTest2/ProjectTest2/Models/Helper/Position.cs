﻿namespace ProjectTest2.Models.Helper
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
