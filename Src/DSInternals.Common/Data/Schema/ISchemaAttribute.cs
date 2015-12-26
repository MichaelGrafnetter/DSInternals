namespace DSInternals.Common.Data
{
    using System;

    public interface ISchemaAttribute
    {
        int? Id { get; }
        string Name { get; }
        AttributeSyntax Syntax { get; }
    }
}
