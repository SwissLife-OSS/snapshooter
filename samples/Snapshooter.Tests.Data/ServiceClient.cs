using System;
using System.Threading.Tasks;

namespace Snapshooter.Tests.Data
{
    public class ServiceClient
    {
        public TestPerson CreatePerson()
        {
            return TestDataBuilder.TestPersonMarkWalton().Build();
        }

		public TestPerson CreatePerson(Guid id, string firstName, string lastName)
        {
            return TestDataBuilder.TestPerson(id, firstName, lastName).Build();
        }

        public async Task<TestPerson> CreatePersonAsync(Guid id, string firstName, string lastName)
        {
            await Task.Delay(10);

            TestPerson person = TestDataBuilder.TestPerson(id, firstName, lastName).Build();

            await Task.Delay(10);

            return person;
        }

        public TestImage CreateImage()
        {
            return TestDataBuilder.TestImageMonaLisa().Build();
        }

        public TestImage CreateImageFaked()
        {
            return TestDataBuilder.TestImageMonaLisaFake().Build();
        }
    }
}
