namespace ConcreteProducts.Web.Data
{
    public class DataConstants
    {
        public class ErrorMessages
        {
            public const string DefaultNameErrorMessage = "{0} should be between {2} and {1} characters long";
            public const string QuantityErrorMessage = "The field must be between {1} and {2}";
            public const string InvalidUnitOfMeasurementValue = "Invalid value";
        }

        public class User
        {
            public const int UsernameMinLength = 4;
            public const int UsernameMaxLength = 20;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 20;
        }

        public class Product
        {
            public const int DimensionsMinLength = 3;
            public const int DimensionsMaxLength = 12;
            public const int NameMinLength = 4;
            public const int NameMaxLength = 200;
            public const int WeightMinValue = 1;
            public const int WeightMaxValue = 100;
            public const int QuantityInPalletInUnitMeasurementMinValue = 1;
            public const int QuantityInPalletInUnitMeasurementMaxValue = 100;
            public const int QuantityInPalletInPiecesMinValue = 1;
            public const int QuantityInPalletInPiecesMaxValue = 2000;
            public const int CountInUnitMeasurementMinValue = 1;
            public const int CountInUnitMeasurementMaxValue = 200;
        }

        public class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 200;
        }

        public class Color
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public class Shape
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 200;
            public const int DimensionsMinLength = 3;
            public const int DimensionsMaxLength = 12;
        }

        public class Warehouse
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 200;
        }

        public class WarehouseProducts
        {
            public const int CountMinValue = 1;
            public const int CountMaxValue = int.MaxValue;
        }

    }
}
