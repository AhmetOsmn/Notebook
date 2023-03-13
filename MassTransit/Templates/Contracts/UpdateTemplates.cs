namespace Contracts
{
    using System;
    using MassTransit;

    public record UpdateTemplates :
        CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
}