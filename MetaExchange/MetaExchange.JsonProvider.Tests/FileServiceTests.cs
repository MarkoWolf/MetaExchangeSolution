using FluentAssertions;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Models;
using MetaExchange.JsonProvider.Repositories;
using MetaExchange.JsonProvider.Services;
using MetaExchange.JsonProvider.Tests.TestModels;
using MetaExchange.JsonProvider.Tests.TestUtlilites;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace MetaExchange.JsonProvider.Tests;

public class FileServiceTests
{
    [Fact]
    public void ReadAllFromFolder_ShouldReturnEmptyList_WhenFolderPathIsNotConfigured()
    {
        // Arrange
        var options = Options.Create(new JsonProviderOptions { FilePath = "" });
        var logger = Substitute.For<ILogger<FileService>>();
        var fileService = new FileService(options, logger);

        // Act
        var result = fileService.ReadAllFromFolder<object>();

        // Assert
        result.Should().BeEmpty();
    } 
    
    [Fact]
    public void ReadAllFromFolder_ShouldReturnEmptyList_WhenFolderDoesNotExist()
    {
        // Arrange
        var options = Options.Create(new JsonProviderOptions { FilePath = "NonExistingFolder" });
        var logger = Substitute.For<ILogger<FileService>>();
        var fileService = new FileService(options, logger);

        // Act
        var result = fileService.ReadAllFromFolder<object>();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void ReadAllFromFolder_ShouldReturnEmptyList_WhenNoJsonFilesInFolder()
    {
        // Arrange
        using var tempFolder = TestHelper.CreateTemporaryFolder();
        var options = Options.Create(new JsonProviderOptions { FilePath = tempFolder.FolderPath });
        var logger = Substitute.For<ILogger<FileService>>();
        var fileService = new FileService(options, logger);

        // Act
        var result = fileService.ReadAllFromFolder<object>();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ReadAllFromFolder_ShouldReturnDeserializedObjects_WhenJsonFilesExist()
    {
        // Arrange
        using var tempFolder = TestHelper.CreateTemporaryFolder();
        var options = Options.Create(new JsonProviderOptions { FilePath = tempFolder.FolderPath });
        var logger = Substitute.For<ILogger<FileService>>();
        var fileService = new FileService(options, logger);

        TestHelper.ExtractEmbeddedResource("MetaExchange.JsonProvider.Tests.Resources.exchange-01.json",
            Path.Combine(tempFolder.FolderPath, "exchange-01.json"));
        TestHelper.ExtractEmbeddedResource("MetaExchange.JsonProvider.Tests.Resources.exchange-02.json",
            Path.Combine(tempFolder.FolderPath, "exchange-02.json"));

        // Act
        var result = fileService.ReadAllFromFolder<JsonExchange>();

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainSingle(e => e.Id == "exchange-01");
        result.Should().ContainSingle(e => e.Id == "exchange-02");
    }
    
    
    [Fact]
    public void ReadAllFromFolder_ShouldIgnoreFiles_WhenJsonFormatDoesNotMatchClass()
    {
        // Arrange
        using var tempFolder = TestHelper.CreateTemporaryFolder();
        var options = Options.Create(new JsonProviderOptions { FilePath = tempFolder.FolderPath });
        var logger = Substitute.For<ILogger<FileService>>();
        var fileService = new FileService(options, logger);

        TestHelper.CreateJsonFile(tempFolder.FolderPath, "invalidExchange.json", "{\"Id\": \"exchange-01\"}");

        // Act
        var result = fileService.ReadAllFromFolder<TestExchange>();

        // Assert
        result.Should().BeEmpty();
    }
}