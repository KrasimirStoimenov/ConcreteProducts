namespace ConcreteProducts.Web.Models.ShapeHistories
{
    using System;
    using System.Collections.Generic;

    using ConcreteProducts.Services.ShapeHistory;

    public class SelectShapeFormModel
    {
        //TODO: ValidateAttribute
        public int ShapeId { get; init; }

        public DateTime PutOn { get; init; }

        public IEnumerable<SelectShapeServiceModel> Shapes { get; set; }
    }
}
