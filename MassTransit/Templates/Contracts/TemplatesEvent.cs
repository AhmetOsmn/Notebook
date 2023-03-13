namespace Contracts
{
    using System;

    public record TemplatesEvent
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
}