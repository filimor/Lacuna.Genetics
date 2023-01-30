using FluentAssertions;
using Lacuna.Genetics.Core;
using Lacuna.Genetics.Tests.ClassData;
using Xunit;

namespace Lacuna.Genetics.Tests;

public class LabServiceTest
{
    [Theory]
    [ClassData(typeof(StrandsClassData))]
    public void DecodeStrand_OnValidInput_ReturnsValidOutput(string expectedStrand, string encodedStrand)
    {
        // Arrange
        var labService = new LabService();

        // Act
        var decodedStrand = labService.DecodeStrand(encodedStrand);

        // Assert
        decodedStrand.Should().Be(expectedStrand);
    }

    [Theory]
    [ClassData(typeof(StrandsClassData))]
    public void EncodeStrand_OnValidInput_ReturnsValidOutput(string encodedStrand, string expectedStrand)
    {
        // Arrange
        var labService = new LabService();

        // Act
        var decodedStrand = labService.EncodeStrand(encodedStrand);

        // Assert
        decodedStrand.Should().Be(expectedStrand);
    }


    [Theory]
    [ClassData(typeof(ActivatedGenesClassData))]
    public void CheckGene_OnActivatedGene_ReturnsTrue(string gene, string strand)
    {
        // Arrange
        var labService = new LabService();

        // Act
        var result = labService.CheckGene(strand, gene);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InactivatedGenesClassData))]
    public void CheckGene_OnInactivatedGene_ReturnsFalse(string gene, string strand)
    {
        // Arrange
        var labService = new LabService();

        //  Act
        var result = labService.CheckGene(strand, gene);

        // Assert
        result.Should().BeFalse();
    }
}