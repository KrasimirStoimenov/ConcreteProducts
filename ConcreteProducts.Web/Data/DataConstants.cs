namespace ConcreteProducts.Web.Data
{
    public class DataConstants
    {
        public const string DefaultNameErrorMessage = "{0} should be between {2} and {1} characters long";

        public const string QuantityErrorMessage = "The field must be between {1} and {2}";

        public const string InvalidUnitOfMeasurementValue = "Invalid value";

        public const int ProductDimensionsMinLength = 3;
        public const int ProductDimensionsMaxLength = 12;
        public const int ProductNameMinLength = 5;
        public const int ProductNameMaxLength = 200;
        public const int ProductWeightMinValue = 1;
        public const int ProductWeightMaxValue = 100;
        public const int ProductQuantityInPalletInUnitMeasurementMinValue = 1;
        public const int ProductQuantityInPalletInUnitMeasurementMaxValue = 100;
        public const int ProductQuantityInPalletInPiecesMinValue = 1;
        public const int ProductQuantityInPalletInPiecesMaxValue = 2000;
        public const int ProductCountInUnitMeasurementMinValue = 1;
        public const int ProductCountInUnitMeasurementMaxValue = 200;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 200;

        public const int ColorNameMinLength = 3;
        public const int ColorNameMaxLength = 50;

        public const int ShapeNameMinLength = 5;
        public const int ShapeNameMaxLength = 200;
        public const int ShapeDimensionsMinLength = 3;
        public const int ShapeDimensionsMaxLength = 12;

        public const int WarehouseNameMinLength = 5;
        public const int WarehouseNameMaxLength = 200;
    }
}
