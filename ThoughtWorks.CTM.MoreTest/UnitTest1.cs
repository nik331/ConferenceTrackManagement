using System;
using System.IO;
using Xunit;

namespace ThoughtWorks.CTM.MoreTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);

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
    }
}
