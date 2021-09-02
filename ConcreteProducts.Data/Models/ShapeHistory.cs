namespace ConcreteProducts.Data.Models
{
    using System;

    public class ShapeHistory
    {
        public int Id { get; init; }

        public DateTime PutOn { get; set; }

        public DateTime PutOut { get; set; }

        public int DaysUsed { get; init; }

        public int ShapeId { get; set; }

        public Shape Shape { get; init; }
    }
}
