
namespace Sharpility.Function
{
    /// <summary>
    /// Converts two inputs into output.
    /// </summary>
    /// <typeparam name="TInput1">First input type</typeparam>
    /// <typeparam name="TInput2">Second input type</typeparam>
    /// <typeparam name="TOutput">Output type</typeparam>
    /// <param name="input1">First input</param>
    /// <param name="input2">Second input</param>
    /// <returns>Output</returns>
    public delegate TOutput BiConverter<in TInput1, in TInput2, out TOutput>(TInput1 input1, TInput2 input2);
}
