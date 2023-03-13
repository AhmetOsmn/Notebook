namespace Contracts
{
    using System;
    using MassTransit;

    public record InitiateTemplates :
        CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; init; }
        public string Value { get; init; }
    }
}