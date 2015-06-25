
namespace Sharpility.Function
{
    /// <summary>
    /// Consumes two inputs.
    /// </summary>
    /// <typeparam name="TInput1">First input type</typeparam>
    /// <typeparam name="TInput2">Second input type</typeparam>
    /// <param name="input1">First input</param>
    /// <param name="input2">Second input</param>
    public delegate void BiConsumer<in TInput1, in TInput2>(TInput1 input1, TInput2 input2);
}
