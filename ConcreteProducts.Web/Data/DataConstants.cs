namespace ConcreteProducts.Web.Data
{
    public class DataConstants
    {
        public const string DefaultNameErrorMessage = "{0} should be between {2} and {1} characters long";

        public const int ProductNameMaxLength = 200;
        public const int ProductNameMinLength = 5;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 200;

        public const int ColorNameMinLength = 3;
        public const int ColorNameMaxLength = 50;

        public const int WeightMinValue = 1;
        public const int WeightMaxValue = 100;

        public const int QuantityInPalletInUnitMeasurementMinValue = 1;
        public const int QuantityInPalletInUnitMeasurementMaxValue = 100;

        public const int QuantityInPalletInPiecesMinValue = 1;
        public const int QuantityInPalletInPiecesMaxValue = 2000;

        public const int CountInUnitMeasurementMinValue = 1;
        public const int CountInUnitMeasurementMaxValue = 200;

        public const string QuantityErrorMessage = "The field must be between {1} and {2}";

        public const string InvalidUnitOfMeasurementValue = "Invalid value";
    }
}
