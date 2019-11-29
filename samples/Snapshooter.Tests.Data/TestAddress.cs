namespace Snapshooter.Tests.Data
{
    /// <summary>
    /// The address of a person
    /// </summary>
    public class TestAddress
    {
        /// <summary>
        /// The street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The number of the street
        /// </summary>
        public int? StreetNumber { get; set; }

        /// <summary>
        /// The plz
        /// </summary>
        public int? Plz { get; set; }

        /// <summary>
        /// The city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The country living in
        /// </summary>
        public TestCountry Country { get; set; }
    }

    /// <summary>
    /// The address builder is responsible to build an address
    /// for a person.
    /// </summary>
    public class TestAddressBuilder
    {
        private readonly TestAddress _testAddress;

        private TestAddressBuilder()
        {
            _testAddress = new TestAddress();
        }

        public static TestAddressBuilder Create()
        {
            return new TestAddressBuilder();
        }

        public TestAddressBuilder WithStreet(string street)
        {
            _testAddress.Street = street;
            return this;
        }

        public TestAddressBuilder WithStreetNumber(int? streetNumber)
        {
            _testAddress.StreetNumber = streetNumber;
            return this;
        }

        public TestAddressBuilder WithPlz(int? plz)
        {
            _testAddress.Plz = plz;
            return this;
        }

        public TestAddressBuilder WithCity(string city)
        {
            _testAddress.City = city;
            return this;
        }

        public TestAddressBuilder AddCountry(TestCountry testCountry)
        {
            _testAddress.Country = testCountry;
            return this;
        }

        public TestAddress Build()
        {
            return _testAddress;
        }
    }
}