namespace DSInternals.DataStore;

/// <summary>
/// Distinguished Name Tag.
/// </summary>
/// <remarks>Essentially is a primary key to identify each row within the ntds.dit datatable.</remarks>
public enum DNTag : int
{
    NotAnObject = 1,
    RootObject = 2
}
