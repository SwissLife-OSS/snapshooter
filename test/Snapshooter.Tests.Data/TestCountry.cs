namespace Snapshooter.Tests.Data
{
    public class TestCountry
    {
        public string Name { get; set; }

        public CountryCode CountryCode { get; set; }
    }

    public class TestCountryBuilder
    {
        private readonly TestCountry _testCountry;

        private TestCountryBuilder()
        {
            _testCountry = new TestCountry();
        }

        public static TestCountryBuilder Create()
        {
            return new TestCountryBuilder();
        }

        public TestCountryBuilder WithName(string name)
        {
            _testCountry.Name = name;
            return this;
        }

        public TestCountryBuilder WithCountryCode(CountryCode countryCode)
        {
            _testCountry.CountryCode = countryCode;
            return this;
        }

        public TestCountry Build()
        {
            return _testCountry;
        }
    }

    public enum CountryCode
    {
        CH,
        DE,
        FR,
        US,
        EN
    }
}
