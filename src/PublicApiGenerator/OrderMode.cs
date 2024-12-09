namespace PublicApiGenerator;

/// <summary>
/// Used to instruct the generator how to order types.
/// </summary>
public enum OrderMode
{
    /// <summary>
    /// Order types by <see cref="Type.FullName"/>
    /// </summary>
    FullName = 0,
    /// <summary>
    /// Order types by <see cref="Type.Namespace"/> and then by <see cref="Type.FullName"/>
    /// </summary>
    NamespaceThenFullName = 1,
}
