using KodeAlgo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestAlgo
{
    public class TestOppgave3
    {
        [Fact]
        public void QuickSort_IfSizeIs0_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { };
            var sut = new Oppgave3();

            // Assert
            Assert.Throws<ArgumentException>(() => sut.QuickSort(nums, 0, nums.Length - 1));
        }


        [Fact]
        public void QuickSort_IfSizeIs1_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { 1 };
            var sut = new Oppgave3();

            // Assert
            Assert.Throws<ArgumentException>(() => sut.QuickSort(nums, 0, nums.Length - 1));
        }

        [Fact]
        public void QuickSort_ShouldSort_Ascending()
        {
            // Arrange
            var nums = new int[] { 6, 2, 9, 0, 3 };
            var sut = new Oppgave3();

            var expectedOrder = new int[] { 0, 2, 3, 6, 9 }; 

            // Act
            sut.QuickSort(nums, 0, nums.Length - 1);

            // Assert
            Assert.Equal(expectedOrder, nums);
        }
    }
}
