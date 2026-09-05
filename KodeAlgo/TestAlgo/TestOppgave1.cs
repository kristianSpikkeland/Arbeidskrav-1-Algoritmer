using KodeAlgo;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAlgo
{
    public class TestOppgave1
    {
        [Fact]
        public void LinearSearch_Value7Exists_ShouldReturnIndex3()
        {
            // Arrange
            var nums = new int[] { 8, 3, 11, 7, 2, 9, 5 };

            // Act
            var result = Oppgave1.LinearSearch(nums, 7);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void LinearSearch_Value42NotExists_ShouldReturnNegativeIndex()
        {
            // Arrange
            var nums = new int[] { 8, 3, 11, 7, 2, 9, 5 };

            // Act
            var result = Oppgave1.LinearSearch(nums, 42);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void BinarySearch_Value13Exists_ShouldReturnIndex5()
        {
            // Arrange
            var nums = new int[] { 2, 5, 7, 9, 11, 13, 17 };

            // Act
            var result = Oppgave1.BinaryReaderSearch(nums, 13);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void BinarySearch_Value4NotExists_ShouldReturnNegativeIndex()
        {
            // Arrange
            var nums = new int[] { 2, 5, 7, 9, 11, 13, 17 };

            // Act
            var result = Oppgave1.BinaryReaderSearch(nums, 4);

            // Assert
            Assert.Equal(-1, result);
        }
    }
}
