﻿using FluentAssertions;
using Lacuna.Genetics.Core;
using Lacuna.Genetics.Tests.ClassData;
using Xunit;

namespace Lacuna.Genetics.Tests;

public class LabModuleTest
{
    [Theory]
    [ClassData(typeof(StrandsClassData))]
    public void DecodeStrand_OnValidInput_ReturnValidOutput(string expectedStrand, string encodedStrand)
    {
        // Act
        var decodedStrand = LabModule.DecodeStrand(encodedStrand);

        // Assert
        decodedStrand.Should().Be(expectedStrand);
    }

    [Theory]
    [ClassData(typeof(StrandsClassData))]
    public void EncodeStrand_OnValidInput_ReturnValidOutput(string encodedStrand, string expectedStrand)
    {
        // Act
        var decodedStrand = LabModule.EncodeStrand(encodedStrand);

        // Assert
        decodedStrand.Should().Be(expectedStrand);
    }


    [Theory]
    [ClassData(typeof(ActivatedGenesClassData))]
    public void CheckGene_OnActivatedGene_ReturnTrue(string gene, string strand)
    {
        // Act
        var result = LabModule.CheckGene(strand, gene);

        // Assert
        result.Should().BeTrue();
    }

    //[Theory]
    //[ClassData(typeof(InactivatedGenesClassData))]
    //public void CheckGene_OnInactivatedGene_ReturnFalse(string gene, string strand)
    //{
    //    //  Act
    //    var result = LabModule.CheckGene(strand, gene);

    //    // Assert
    //    result.Should().BeFalse();
    //}
}