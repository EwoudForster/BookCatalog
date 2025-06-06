﻿using BookCatalog.DAL.Logging;

namespace BookCatalog.Tests
{
    
    public class LoggingTest
    {
        [Fact]
        public void LoggingBasicStringTest()
        {
            // Arrange 
            var input = "Error, there is a problem";
            var stringWriterExpected = new StringWriter();
            Console.SetOut(stringWriterExpected);
            Console.WriteLine(input);

            var expected = stringWriterExpected.ToString();
            IGeneralLogger logger = new GeneralLogger();


            // Act
            var stringWriterOutput = new StringWriter();
            Console.SetOut(stringWriterOutput);
            input = "Error, there is a problem";
            logger.Log(input);
            var output = stringWriterOutput.ToString();

            // Assert
            Assert.Equal(expected, output);
        }

        [Fact]
        public void LoggingErrorTest()
        {
            Thread.Sleep(1);
            // Arrange
            var input = "Sequence contains more than one matching element";
            var stringWriterExpected = new StringWriter();
            Console.SetOut(stringWriterExpected);
            Console.WriteLine(input);

            var expected = stringWriterExpected.ToString();
            IGeneralLogger loggerError = new GeneralLogger();

            // Act
            var stringWriterOutput = new StringWriter();
            Console.SetOut(stringWriterOutput);

            var list = new List<string> 
            {
                "test",
                "duplicate",
                "duplicate"
            };

            try
            { 
                Console.Write(list.SingleOrDefault(item => item == "duplicate"));
            }
            catch(Exception ex) 
            {
                loggerError.Error(ex, "");
            }


            var output = stringWriterOutput.ToString();
            // Assert
            Assert.Equal(expected, output);
        }
    }
}
