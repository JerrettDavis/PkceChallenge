using FluentAssertions;
using static Pkce.Challenge.Pkce;

namespace Pkce.Challenge.UnitTests;

public class PkceTests
{
    [Fact]
    public void CodeVerifier_Length_DefaultLength_Should_Be_43()
    {
        // Arrange
        var pkce = PkceChallenge();

        // Act
        var codeVerifierLength = pkce.CodeVerifier.Length;

        // Assert
        codeVerifierLength.Should().Be(43);
    }

    [Fact]
    public void CodeChallenge_Pattern_Should_Not_Have_Equal_Plus_Or_Slash()
    {
        // Arrange
        var pkce = PkceChallenge();
        
        // Act
        var codeChallenge = pkce.CodeChallenge;
        
        // Assert
        codeChallenge.Should().NotContain("=");
        codeChallenge.Should().NotContain("+");
        codeChallenge.Should().NotContain("/");
    }
    
    [Fact]
    public void CodeVerifier_Length_Less_Than_43_Should_Throw_ArgumentOutOfRangeException()
    {
        // Act
        Action act = () => _ = PkceChallenge(42);
        
        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>(
                "Because expected a length between 43 and 128. Received 42.");
    }
    
    [Fact]
    public void CodeVerifier_Length_Greater_Than_128_Should_Throw_Exception()
    {
        // Act
        Action act = () => _ = PkceChallenge(129);
        
        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>(
                "Because expected a length between 43 and 128. Received 129.");
    }
    
    [Fact]
    public void VerifyChallenge_Should_Return_True()
    {
        // Arrange
        var challengePair = PkceChallenge();
        
        // Act
        var result = VerifyChallenge(
            challengePair.CodeVerifier, 
            challengePair.CodeChallenge);
        
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyChallenge_Should_Return_False()
    {
        // Arrange
        var challengePair = PkceChallenge();
        
        // Act
        var result = VerifyChallenge(
            challengePair.CodeVerifier, 
            challengePair.CodeChallenge + "a");
        
        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GenerateChallenge_Should_Create_Consistent_Challenge()
    {
        // Arrange & Act
        var challengePair = PkceChallenge();
        var codeChallenge = GenerateChallenge(challengePair.CodeVerifier);
        
        // Assert
        codeChallenge.Should().Be(challengePair.CodeChallenge);
    }
}