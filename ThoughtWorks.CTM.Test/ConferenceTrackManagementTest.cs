using System;
using System.IO;
using Xunit;

namespace ThoughtWorks.CTM.Test
{
    public class ConferenceTrackManagementTest
    {
        [Fact]
        public void Process_With2Tracks_success()
        {
            //Arrange
            var lines = File.ReadAllLines(@".\SampleInput.txt");
            var processor = new Processor();

            //Act
            var conferenceTracks = processor.ProcessFile(lines, 2);
            var output = processor.CreateProgram(conferenceTracks);

            //Assert
            Assert.NotEmpty(output);
            Assert.Equal(2, conferenceTracks.Count);
        }

        [Fact]
        public void Process_With5Tracks_success()
        {
            //Arrange
            var lines = File.ReadAllLines(@".\SampleInput.txt");
            var processor = new Processor();

            //Act
            var conferenceTracks = processor.ProcessFile(lines, 5);
            var output = processor.CreateProgram(conferenceTracks);

            //Assert
            Assert.NotEmpty(output);
            Assert.Equal(5, conferenceTracks.Count);
        }

        [Fact]        
        public void Process_With0Tracks_success()
        {
            //Arrange
            var lines = File.ReadAllLines(@".\SampleInput.txt");
            var processor = new Processor();

            //Act
           
            //Assert
            Assert.Throws<Exception>(() => processor.ProcessFile(lines, 0));
           
        }

        [Fact]
        public void Process_WithNegativeTracks_success()
        {
            //Arrange
            var lines = File.ReadAllLines(@".\SampleInput.txt");
            var processor = new Processor();

            //Act

            //Assert
            Assert.Throws<Exception>(() => processor.ProcessFile(lines, 0));
        }
    }
}
