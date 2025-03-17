using BookCatalog.DataLayer.Logging;

namespace BookCatalog.Tests.Logging
{
    [TestClass]
    public class LoggingTest
    {
        [TestMethod]
        public void LoggingBasicStringTest()
        {
            // Arrange 
            var input = "Error, there is a problem";
            var stringWriterExpected = new StringWriter();
            Console.SetOut(stringWriterExpected);
            Console.WriteLine(input);

            var expected = stringWriterExpected.ToString();
            ILogger logger = new Logger();


            // Act
            var stringWriterOutput = new StringWriter();
            Console.SetOut(stringWriterOutput);
            input = "Error, there is a problem";
            logger.Log(input);
            var output = stringWriterOutput.ToString();

            // Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void LoggingErrorTest()
        {
            Thread.Sleep(1);
            // Arrange
            var input = "Sequence contains more than one matching element";
            var stringWriterExpected = new StringWriter();
            Console.SetOut(stringWriterExpected);
            Console.WriteLine(input);

            var expected = stringWriterExpected.ToString();
            ILogger loggerError = new Logger();

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
                loggerError.Error(ex);
            }


            var output = stringWriterOutput.ToString();
            // Assert
            Assert.AreEqual(expected, output);
        }
    }
}
