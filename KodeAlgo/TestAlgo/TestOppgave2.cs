using KodeAlgo;
using KodeAlgo.Menu;
using KodeAlgo.Oppgave2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TestAlgo
{
    public class TestOppgave2
    {

        [Fact]
        public void Dequeue_RemovingFromEmptyArray_ShouldThrowException()
        {
            // Arrange
            var queue = new CustomQueue<int>();

            // Assert
            Assert.Throws<ArgumentException>(() => queue.Dequeue());
        }


        [Fact]
        public void Enqueue_AddingElementToFullArray_ShouldExpandArray()
        {
            // Arrange
                // Creates a queue with 5 slots in array
                var queue = new CustomQueue<int>(5);

            // Act
                // Adding 5 elements
                queue.Enqueue(3);
                queue.Enqueue(7);
                queue.Enqueue(4);
                queue.Enqueue(6);
                queue.Enqueue(2);

            // Adding element number 6 should double array  
            queue.Enqueue(9);

            // Assert
            Assert.Equal(10, queue.Size);
        }

        [Fact]
        public void Dequeue_RemovingElement_ShouldRemoveEmptySpace()
        {
            // Arrange
                // Creates a queue with 5 slots in array
                var queue = new CustomQueue<int>(5);

            // Act
                // Adding 5 elements
                queue.Enqueue(3);
                queue.Enqueue(7);
                queue.Enqueue(4);
                queue.Enqueue(6);
                queue.Enqueue(2);

            // Adding element number 6 sets queue Size to 10  
            queue.Enqueue(9);

 
            queue.Dequeue();

            // Assert
            Assert.Equal(5, queue.Size);
        }

        [Fact]
        public void Dequeue_RemovingElement_ShouldUseFIFO()
        {
            // Arrange
            var queue = new CustomQueue<int>(3);

            // Act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            var fifo = queue.Dequeue();

            // Assert
            Assert.Equal(1, fifo);
        }

        [Fact]
        public void Peek_ShouldShowFirstInQueue()
        {
            // Arrange
            var queue = new CustomQueue<int>(3);

            // Act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            var fifo = queue.Peek();

            // Assert
            Assert.Equal(1, fifo);
        }

    }
}
