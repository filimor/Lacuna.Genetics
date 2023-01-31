using FluentAssertions;
using Lacuna.Genetics.Core;
using Lacuna.Genetics.Core.Models;
using Lacuna.Genetics.Tests.ClassData;
using Xunit;

namespace Lacuna.Genetics.Tests;

public class LabServiceTest
{
    [Theory]
    [ClassData(typeof(StrandsClassData))]
    public void EncodeStrand_OnValidInput_ReturnsValidOutput(string decodedStrand, string expectedStrand)
    {
        // Arrange
        var labService = new LabService();
        var job = new Job { Strand = decodedStrand, Type = Job.EncodeStrand };

        // Act
        var encodedStrand = labService.Analyze(job).StrandEncoded;

        // Assert
        encodedStrand.Should().Be(expectedStrand);
    }

    [Theory]
    [ClassData(typeof(StrandsClassData))]
    public void DecodeStrand_OnValidInput_ReturnsValidOutput(string expectedStrand, string encodedStrand)
    {
        // Arrange
        var labService = new LabService();
        var job = new Job { StrandEncoded = encodedStrand, Type = Job.DecodeStrand };

        // Act
        var decodedStrand = labService.Analyze(job).Strand;

        // Assert
        decodedStrand.Should().Be(expectedStrand);
    }

    [Theory]
    [ClassData(typeof(ActivatedGenesClassData))]
    public void CheckGene_OnActivatedGene_ReturnsTrue(string gene, string strand)
    {
        // Arrange
        var labService = new LabService();
        var job = new Job { StrandEncoded = strand, GeneEncoded = gene, Type = Job.CheckGene };

        // Act
        var result = labService.Analyze(job).IsActivated;

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InactivatedGenesClassData))]
    public void CheckGene_OnInactivatedGene_ReturnsFalse(string gene, string strand)
    {
        // Arrange
        var labService = new LabService();
        var job = new Job { StrandEncoded = strand, GeneEncoded = gene, Type = Job.CheckGene };

        //  Act
        var result = labService.Analyze(job).IsActivated;

        // Assert
        result.Should().BeFalse();
    }
}