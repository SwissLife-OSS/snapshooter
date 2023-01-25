using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using Xunit;

namespace Snapshooter.Examples.Xunit.MatchOptionsTests.HashTests;

public class HashTests
{
    [Fact]
    public void HashField_HashOneField_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestImage image = serviceClient.CreateImage();

        // assert
        Snapshot.Match(image);
    }

    [Fact]
    public void HashField_HashFieldsByName_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestImage image = serviceClient.CreateImage();
        TestImage imageFaked = serviceClient.CreateImageFaked();

        // assert
        Snapshot.Match(new { image, imageFaked });
    }
}
