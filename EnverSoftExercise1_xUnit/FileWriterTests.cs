using CsvProcessorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnverSoftExercise1_xUnit
{
    public class FileWriterTests
    {
        [Fact]
        public void WriteLines_ShouldWriteAllLinesToFile()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            var writer = new FileWriter();
            var lines = new List<string> { "Line 1", "Line 2", "Line 3" };

            // Act
            writer.WriteLines(tempFile, lines);
            var writtenLines = File.ReadAllLines(tempFile);

            // Assert
            Assert.Equal(lines.Count, writtenLines.Length);
            Assert.Equal("Line 1", writtenLines[0]);
            Assert.Equal("Line 3", writtenLines[2]);

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public void WriteLines_ShouldHandleEmptyList()
        {
            var tempFile = Path.GetTempFileName();
            var writer = new FileWriter();
            var lines = new List<string>();

            // Act
            writer.WriteLines(tempFile, lines);
            var writtenLines = File.ReadAllLines(tempFile);

            // Assert
            Assert.Empty(writtenLines);

            File.Delete(tempFile);
        }

        [Fact]
        public void WriteLines_ShouldOverwriteExistingFile()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "Old Content");

            var writer = new FileWriter();
            var lines = new List<string> { "New Line 1", "New Line 2" };

            writer.WriteLines(tempFile, lines);

            var writtenLines = File.ReadAllLines(tempFile);

            Assert.Equal(2, writtenLines.Length);
            Assert.Equal("New Line 1", writtenLines[0]);

            File.Delete(tempFile);
        }

        [Fact]
        public void WriteLines_ShouldHandleLargeNumberOfLines()
        {
            var tempFile = Path.GetTempFileName();
            var writer = new FileWriter();
            var lines = Enumerable.Range(1, 1000).Select(i => $"Line {i}").ToList();

            writer.WriteLines(tempFile, lines);

            var writtenLines = File.ReadAllLines(tempFile);

            Assert.Equal(1000, writtenLines.Length);
            Assert.Equal("Line 1", writtenLines[0]);
            Assert.Equal("Line 1000", writtenLines[999]);

            File.Delete(tempFile);
        }


        [Fact]
        public void WriteLines_ShouldThrowForInvalidPath()
        {
            var writer = new FileWriter();
            var lines = new List<string> { "Test Line" };

            Assert.Throws<DirectoryNotFoundException>(() => writer.WriteLines(@"Z:\NonExistentFolder\file.txt", lines));
        }


        [Fact]
        public void WriteLines_ShouldThrowIfFileIsLocked()
        {
            var tempFile = Path.GetTempFileName();
            using (var fileStream = File.Open(tempFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                var writer = new FileWriter();
                var lines = new List<string> { "Test Line" };

                Assert.Throws<IOException>(() => writer.WriteLines(tempFile, lines));
            }

            File.Delete(tempFile);
        }


    }
}
