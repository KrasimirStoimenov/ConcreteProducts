namespace ConcreteProducts.Services.ShapeHistory.Models
{
    using ConcreteProducts.Services.Shapes;

    public class ShapeHistory : IShapeHistory
    {
        private readonly IShapeService shapeService;

        public ShapeHistory(IShapeService shapeService)
        {
            this.shapeService = shapeService;
        }

        public void CurrentlySelectedShape(int shapeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
